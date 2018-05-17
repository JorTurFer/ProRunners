using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRunners
{
  internal static class CameraMgr
  {
    private static List<Camera> m_lstCameras = new List<Camera>();

    public static void AddCamera(Camera camera)
    {
      m_lstCameras.Add(camera);
    }

    public static void StopGrab()
    {
      foreach (Camera cam in m_lstCameras)
      {
        cam.StopGrab();
        cam.AccionTerminada.WaitOne();
      }
    }

    public static Camera GetCamera(CameraIndex eCam)
    {
      return m_lstCameras.Where(x => x.ID == eCam).FirstOrDefault();
    }

    public static void SetVideo(CameraIndex eCam)
    {
      foreach (Camera cam in m_lstCameras.Where(x => eCam == CameraIndex.All ? true : x.ID == eCam))
      {
        cam.SetVideo();
        cam.AccionTerminada.WaitOne();
      }
    }

    public static void StartGrab(CameraIndex eCam, string strPath)
    {
      foreach (Camera cam in m_lstCameras.Where(x => eCam == CameraIndex.All ? true : x.ID == eCam))
      {
        cam.StartGrab(strPath);
        cam.AccionTerminada.WaitOne();
      }
    }

    public static void SetPhoto(CameraIndex eCam)
    {
      foreach (Camera cam in m_lstCameras.Where(x => eCam == CameraIndex.All ? true : x.ID == eCam))
      {
        cam.SetPhoto();
        cam.AccionTerminada.WaitOne();
      }
    }

    public static void TakeSnapshot(CameraIndex eCam)
    {
      foreach (Camera cam in m_lstCameras.Where(x => eCam == CameraIndex.All ? true : x.ID == eCam))
      {
        cam.TakeSnapshot();
        cam.AccionTerminada.WaitOne();
      }
    }

    public static void CloseCameras()
    {
      foreach (Camera cam in m_lstCameras)
      {
        cam.Dispose();
      }
    }

    public static bool IsReady
    {
      get
      {
        bool tmp = false;
        foreach (Camera cam in m_lstCameras)
        {
          if (!tmp)
            tmp = cam.Ready;
        }
        return tmp;
      }
    }
  }
}
