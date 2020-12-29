using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ProRunners.IDS
{
    class IDSCamera : Camera
    {
        #region IDisposable Support

        protected override void DisposeResources()
        {
            m_eventoSalir.Set();
        }
        #endregion

        Thread m_threadCiclo;
        ManualResetEvent m_eventoSalir = new ManualResetEvent(false);
        AutoResetEvent m_eventoAccion = new AutoResetEvent(false);
        uEye.Camera m_Camera;

        static uEye.Types.CameraInformation[] m_cameraList;

        int m_DeviceID;
        int m_FrameCount;
        int m_nMemoryID = -1;
        ImageFormat m_lastFormat;

        Queue<Acciones> m_queueAcciones = new Queue<Acciones>();

        string m_strPath = "";
        bool m_bSnapShot = false;

        protected virtual void OnFrameRecived(FrameEventArgs e)
        {
            if (m_bSnapShot)
            {
                m_bSnapShot = false;
                m_Camera.Acquisition.Stop();
            }

            base.OnNewFrame(this,e);
        }

        public IDSCamera(CameraIndex DevideID) : base(DevideID)
        {
            Ready = false;            

            //Si no he obtenido la lista de camaras, la obtengo
            if (m_cameraList == null || m_cameraList.Length == 0)
                uEye.Info.Camera.GetCameraList(out m_cameraList);

            if (m_cameraList.Length == 0)
            {
                MessageBox.Show("No se han encontrado camaras...");
            }
            m_Camera = new uEye.Camera();
            m_DeviceID = (int)m_cameraList.Where(x => x.CameraID == (int)DevideID).FirstOrDefault().DeviceID;
            EnqueueAction(Acciones.Init);
            m_threadCiclo = new Thread(Cycle);
            m_threadCiclo.IsBackground = true;
            m_threadCiclo.Name = $"Camera {DevideID} thread";
            m_threadCiclo.Start();
        }

        private void Cycle()
        {
            WaitHandle[] waitevents = new WaitHandle[2];
            waitevents[0] = m_eventoAccion;
            waitevents[1] = m_eventoSalir;

            bool bSeguir = true;

            while (bSeguir)
            {
                int nEvent = WaitHandle.WaitAny(waitevents, 500);

                if (nEvent == 0)
                {
                    if (m_queueAcciones.Count == 0)
                        continue;
                    Acciones acc = m_queueAcciones.Dequeue();
                    switch (acc)
                    {
                        case Acciones.Init:
                            Init();

                            break;
                        case Acciones.Foto:
                            m_bSnapShot = true;
                            m_Camera.Acquisition.Capture();
                            break;
                        case Acciones.StartVideo:
                            m_Camera.Acquisition.Capture();
                            break;
                        case Acciones.StopVideo:
                            m_Camera.Acquisition.Stop();
                            break;
                        case Acciones.SetFormatPhoto:
                            SetImageFormat(ImageFormat.Foto);
                            break;
                        case Acciones.SetFormatVideo:
                            SetImageFormat(Properties.Settings.Default.VideoFormat.ToImageFormat());
                            break;
                    }
                    AccionTerminada.Set();
                }
                else if (nEvent == 1)
                {
                    bSeguir = false;
                }
            }
            m_Camera.Exit();
        }

        private void EnqueueAction(Acciones acc)
        {
            AccionTerminada.Reset();
            m_queueAcciones.Enqueue(acc);
            m_eventoAccion.Set();
        }

        private void Init()
        {
            uEye.Defines.Status statusRet;
            statusRet = initCamera();

            if (statusRet == uEye.Defines.Status.SUCCESS)
            {
                // start capture
                if (statusRet != uEye.Defines.Status.SUCCESS)
                {
                    MessageBox.Show("Starting live video failed");
                }
            }

            // cleanup on any camera error
            if (statusRet != uEye.Defines.Status.SUCCESS && m_Camera.IsOpened)
            {
                m_Camera.Exit();
            }
        }

        private uEye.Defines.Status initCamera()
        {
            uEye.Defines.Status statusRet = m_Camera.Init(m_DeviceID | (Int32)uEye.Defines.DeviceEnumeration.UseDeviceID, IntPtr.Zero);
            if (statusRet != uEye.Defines.Status.SUCCESS)
            {
                MessageBox.Show("Initializing the camera failed");
                return statusRet;
            }
            uEye.DeviceFeatureJpegCompression compressor = new uEye.DeviceFeatureJpegCompression(m_Camera);
            compressor.Set(100);
            uEye.ColorConverter conv = new uEye.ColorConverter(m_Camera);
            conv.Set(uEye.Defines.ColorMode.BGR8Packed, uEye.Defines.ColorConvertMode.Jpeg);

            SetImageFormat(ImageFormat.Foto);
            // set event
            m_Camera.EventFrame += onFrameEvent;

            Ready = true;

            return statusRet;
        }

        private void onFrameEvent(object sender, EventArgs e)
        {
            // convert sender object to our camera object
            uEye.Camera camera = sender as uEye.Camera;

            if (camera.IsOpened)
            {
                uEye.Defines.DisplayMode mode;
                camera.Display.Mode.Get(out mode);

                // only display in dib mode
                if (mode == uEye.Defines.DisplayMode.DiB)
                {
                    Int32 s32MemID;
                    camera.Memory.GetActive(out s32MemID);
                    camera.Memory.Lock(s32MemID);

                    // do any drawings?

                    Bitmap bitmap;
                    m_Camera.Memory.ToBitmap(s32MemID, out bitmap);
                    FrameEventArgs fe = new FrameEventArgs(bitmap);
                    OnFrameRecived(fe);
                    camera.Memory.Unlock(s32MemID);

                    ++m_FrameCount;
                }
            }
        }

        private void SetImageFormat(ImageFormat formato)
        {
            if (m_lastFormat != formato)
            {
                if (m_nMemoryID != -1)
                {
                    m_Camera.Memory.Free(m_nMemoryID);
                }

                m_Camera.Size.ImageFormat.Set((uint)formato);
                m_Camera.Memory.Allocate(out m_nMemoryID, true);
                m_lastFormat = formato;
            }
        }

        public override void StartGrab(string strPath)
        {
            m_strPath = strPath;
            EnqueueAction(Acciones.StartVideo);
        }

        public override void StopGrab()
        {
            EnqueueAction(Acciones.StopVideo);
        }

        public override void SetVideo()
        {
            EnqueueAction(Acciones.SetFormatVideo);
        }

        public override void SetPhoto()
        {
            EnqueueAction(Acciones.SetFormatPhoto);
        }

        public override void TakeSnapshot()
        {
            EnqueueAction(Acciones.Foto);
        }

        //public override void ImageReleased() { }
    }
}
