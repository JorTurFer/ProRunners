using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVT.VmbAPINET;

namespace ProRunners.AlliedVision
{
    public class AlliedVisionCamera : Camera
    {
        private VimbaHelper m_vimbaHelper = null;
        private CameraInfo m_cameraInfo;
        public AlliedVisionCamera(CameraIndex DeviceID) : base(DeviceID)
        {
            VimbaHelper vimbaHelper = new VimbaHelper();
            vimbaHelper.Startup(null);
            m_vimbaHelper = vimbaHelper;
            var id = DeviceID == CameraIndex.Cam1 ? Properties.Settings.Default.AlliedID1 : Properties.Settings.Default.AlliedID2;
            m_cameraInfo = m_vimbaHelper.CameraList.First(x => x.ID == id);
            m_vimbaHelper.OpenCamera(m_cameraInfo.ID);
            Ready = true;
        }


        public override void SetPhoto()
        {
            var color = ID == CameraIndex.Cam1 ? Properties.Settings.Default.Camera1Color : Properties.Settings.Default.Camera2Color;
            m_vimbaHelper.SetPhotoValues(color ? AlliedVisionPixelFormat.BGR8 : AlliedVisionPixelFormat.Mono8);
            AccionTerminada.Set();
        }

        public override void SetVideo()
        {
            var color = ID == CameraIndex.Cam1 ? Properties.Settings.Default.Camera1Color : Properties.Settings.Default.Camera2Color;
            m_vimbaHelper.SetVideoValues(color ? AlliedVisionPixelFormat.BGR8 : AlliedVisionPixelFormat.Mono8);
            AccionTerminada.Set();
        }

        public override void StartGrab(string strPath)
        {
            m_vimbaHelper.StartContinuousImageAcquisition(this.OnNewFrame);
            AccionTerminada.Set();
        }

        public override void StopGrab()
        {
            m_vimbaHelper.StopContinuousImageAcquisition();
            AccionTerminada.Set();
        }

        public override void TakeSnapshot()
        {
            var image = m_vimbaHelper.AcquireSingleImage();
            OnNewFrame(this,new FrameEventArgs(image));
            AccionTerminada.Set();
        }

        //public override void ImageReleased() => m_vimbaHelper.ImageInUse = true;

        protected override void DisposeResources()
        {
            m_vimbaHelper?.CloseCamera();
            m_vimbaHelper?.Shutdown();
        }
    }
}
