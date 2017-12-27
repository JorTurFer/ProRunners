using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Sistema_Nuria
{
    class CameraManager
    {
        uEye.Camera m_Camera;
        int m_DeviceID;
        int m_FrameCount;
        PictureBox m_Picture;

        public delegate void FrameRecivedEventHandler(FrameRecivedEventArgs e);
        public event FrameRecivedEventHandler FrameReviced;

        protected virtual void OnFrameRecived(FrameRecivedEventArgs e)
        {
            FrameReviced?.Invoke(e);
        }

        public CameraManager(int DevideID, PictureBox Picture)
        {
            uEye.Types.CameraInformation[] cameraList;
            uEye.Info.Camera.GetCameraList(out cameraList);

            m_Camera = new uEye.Camera();
            m_DeviceID = (int)cameraList.Where(x => x.CameraID == 2).FirstOrDefault().DeviceID;
            m_Picture = Picture;
            Init();
        }

        private void Init()
        {
            uEye.Defines.Status statusRet;
            statusRet = initCamera();

            if (statusRet == uEye.Defines.Status.SUCCESS)
            {
                // start capture
                statusRet = m_Camera.Acquisition.Freeze();
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
            uEye.Defines.Status statusRet = uEye.Defines.Status.NO_SUCCESS;

            {
                statusRet = m_Camera.Init(m_DeviceID | (Int32)uEye.Defines.DeviceEnumeration.UseDeviceID, m_Picture.Handle);
                if (statusRet != uEye.Defines.Status.SUCCESS)
                {
                    MessageBox.Show("Initializing the camera failed");
                    return statusRet;
                }

                statusRet = m_Camera.Memory.Allocate();
                if (statusRet != uEye.Defines.Status.SUCCESS)
                {
                    MessageBox.Show("Allocating memory failed");
                    return statusRet;
                }

                // set event
                m_Camera.EventFrame += onFrameEvent;

                // reset framecount
                m_FrameCount = 0;

                // start update timer for our statusbar

                uEye.Types.SensorInfo sensorInfo;
                m_Camera.Information.GetSensorInfo(out sensorInfo);

                m_Picture.SizeMode = PictureBoxSizeMode.Normal;
            }

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
                    FrameRecivedEventArgs fe = new FrameRecivedEventArgs();
                    fe.frame = bitmap;
                    OnFrameRecived(fe);
                    camera.Memory.Unlock(s32MemID);
                    //if (bitmap != null && bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                    //{
                    //    Graphics graphics = Graphics.FromImage(bitmap);

                    //    graphics.Dispose();
                    //    bitmap.Dispose();

                    //    camera.Memory.Unlock(s32MemID);
                    //    camera.Display.Render(s32MemID, uEye.Defines.DisplayRenderMode.FitToWindow);
                    //}

                    ++m_FrameCount;
                }
            }
        }

        public uEye.Defines.Status StartGrab()
        {
            return m_Camera.Acquisition.Capture();
        }
        public uEye.Defines.Status StopGrab()
        {
            return m_Camera.Acquisition.Stop();
        }

    }
}
