using System;
using System.Threading;

namespace ProRunners
{
    public abstract class Camera : IDisposable
    {
        private bool disposedValue;

        public CameraIndex ID { get; protected set; }
        public bool Ready { get; set; }
        public AutoResetEvent AccionTerminada { get; set; }
        protected enum Acciones
        {
            Init,
            Foto,
            StartVideo,
            StopVideo,
            SetFormatVideo,
            SetFormatPhoto
        }

        public delegate void FrameRecivedEventHandler(object sender,FrameEventArgs e);
        public event FrameRecivedEventHandler FrameReviced;

        public Camera(CameraIndex DevideID)
        {
            ID = DevideID;
            AccionTerminada = new AutoResetEvent(false);
        }

        protected virtual void OnNewFrame(object sender, FrameEventArgs e)
        {
            FrameReviced?.Invoke(sender,e);
            //ImageReleased();
        }

        public abstract void StartGrab(string strPath);

        public abstract void StopGrab();

        public abstract void SetVideo();

        public abstract void SetPhoto();

        public abstract void TakeSnapshot();

        //public abstract void ImageReleased();

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeResources();
                }

                disposedValue = true;
            }
        }

        protected abstract void DisposeResources();
    }
}
