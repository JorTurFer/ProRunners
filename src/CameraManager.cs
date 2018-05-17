using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading;

namespace ProRunners
{
  class Camera : IDisposable
  {
    #region IDisposable Support
    private bool disposedValue = false; // Para detectar llamadas redundantes

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          m_eventoSalir.Set();
        }

        disposedValue = true;
      }
    }



    // Este código se agrega para implementar correctamente el patrón descartable.
    public void Dispose()
    {
      Dispose(true);
    }
    #endregion

    enum Acciones
    {
      Init,
      Foto,
      StartVideo,
      StopVideo,
      SetFormatVideo,
      SetFormatPhoto
    }

    Thread m_threadCiclo;
    ManualResetEvent m_eventoSalir = new ManualResetEvent(false);
    AutoResetEvent m_eventoAccion = new AutoResetEvent(false);
    public AutoResetEvent AccionTerminada { get; set; }
    uEye.Camera m_Camera;

    int m_DeviceID;
    int m_FrameCount;
    int m_nMemoryID = -1;
    ImageFormat m_lastFormat;

    Queue<Acciones> m_queueAcciones = new Queue<Acciones>();

    string m_strPath = "";
    bool m_bSnapShot = false;

    public delegate void FrameRecivedEventHandler(FrameRecivedEventArgs e);
    public event FrameRecivedEventHandler FrameReviced;
    protected virtual void OnFrameRecived(FrameRecivedEventArgs e)
    {
      if (m_bSnapShot)
      {
        m_bSnapShot = false;
        m_Camera.Acquisition.Stop();
      }

      FrameReviced?.Invoke(e);
    }

    public Camera(int DevideID)
    {
      Ready = false;
      AccionTerminada = new AutoResetEvent(false);
      uEye.Types.CameraInformation[] cameraList;
      uEye.Info.Camera.GetCameraList(out cameraList);

      m_Camera = new uEye.Camera();
      m_DeviceID = (int)cameraList.Where(x => x.CameraID == DevideID).FirstOrDefault().DeviceID;
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
              var res = m_Camera.Acquisition.Capture();
              //res = m_Camera.Image.Save($"{m_strPath}\\Fotos\\{DateTime.Now.ToString("HH.mm.ss dd_MM_yyyy")}_{m_DeviceID}.Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
              break;
            case Acciones.StartVideo:
              var res2 = m_Camera.Acquisition.Capture();
              //res2 = m_Camera.Video.Start($"{m_strPath}\\Videos\\{DateTime.Now.ToString("HH.mm.ss dd_MM_yyyy")}_{m_DeviceID}.avi");
              break;
            case Acciones.StopVideo:
              //var res3 = m_Camera.Video.Stop();
              var res3 = m_Camera.Acquisition.Stop();
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
        //statusRet = m_Camera.Acquisition.Freeze();
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

      statusRet = m_Camera.Init(m_DeviceID | (Int32)uEye.Defines.DeviceEnumeration.UseDeviceID, IntPtr.Zero);
      if (statusRet != uEye.Defines.Status.SUCCESS)
      {
        MessageBox.Show("Initializing the camera failed");
        return statusRet;
      }
      //m_Camera.Parameter.Load(@"C:\Users\jorge\Desktop\IDS\cam0.ini");
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
          FrameRecivedEventArgs fe = new FrameRecivedEventArgs();
          fe.frame = bitmap;
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
          var resu = m_Camera.Memory.Free(m_nMemoryID);
        }

        var res = m_Camera.Size.ImageFormat.Set((uint)formato);
        res = m_Camera.Memory.Allocate(out m_nMemoryID, true);
        //m_Camera.Memory.Allocate();
        m_lastFormat = formato;
      }
    }

    public void StartGrab(string strPath)
    {
      m_strPath = strPath;
      EnqueueAction(Acciones.StartVideo);
    }

    public void StopGrab()
    {
      EnqueueAction(Acciones.StopVideo);
    }

    public void SetVideo()
    {
      EnqueueAction(Acciones.SetFormatVideo);
    }

    public void SetPhoto()
    {
      EnqueueAction(Acciones.SetFormatPhoto);
    }

    public void TakeSnapshot()
    {
      EnqueueAction(Acciones.Foto);
    }

    public bool Ready { get; set; }

  }
}
