using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using System.Threading;
// Contains common types for AVI format like FourCC
using SharpAvi;
// Contains types used for writing like AviWriter
using SharpAvi.Output;
// Contains types related to encoding like Mpeg4VideoEncoderVcm
using SharpAvi.Codecs;

namespace ProRunners
{
    public partial class GrabarForm : Form
    {
        const int N_CAMERAS = 2;

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public void SetProgressState(ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }

        private PerformanceCounter ramCounter;

        DriveInfo m_currentDrive = null;

        Paciente m_Paciente;

        //Datos imagenes
        bool m_bGrabing = false;
        bool[] m_bSaveImage = new bool[2];
        Queue<byte[]>[] m_lstImages = new Queue<byte[]>[N_CAMERAS];
        int m_nWidht;
        int m_nHeight;
        
        //Generacion de video
        AutoResetEvent[] m_eventStartCompress = new AutoResetEvent[N_CAMERAS];
        bool[] m_bCompressing = new bool[N_CAMERAS];
        Thread[] m_threadAvi = new Thread[N_CAMERAS];
        long[] m_lTotalFrames = new long[N_CAMERAS];
        static int m_nThread;

        DateTime m_DateStartGrab = DateTime.Now;
        DateTime m_DateEndGrab;

        public GrabarForm(Paciente pac)
        {
            InitializeComponent();

            m_nThread = 0;
            
            int nIndexPuntos = Application.StartupPath.IndexOf(':');
            string strDriveNanme = Application.StartupPath.Substring(0, nIndexPuntos);

            m_currentDrive = DriveInfo.GetDrives().Where(x => x.Name.Contains(strDriveNanme)).First();

            ramCounter = new PerformanceCounter("Memory", "Available MBytes", true);

            long memKb;
            GetPhysicallyInstalledSystemMemory(out memKb);
            pb_Memory.Maximum = Convert.ToInt32(memKb / 1024);
            pb_Memory.Value = Convert.ToInt32(ramCounter.NextValue());

            pb_HDD.Maximum = Convert.ToInt32(m_currentDrive.TotalSize / 1024);
            pb_HDD.Value = Convert.ToInt32(m_currentDrive.TotalFreeSpace / 1024);
      
            m_Paciente = pac;
            for (int i = 0; i < m_lstImages.Length; i++)
                m_lstImages[i] = new Queue<byte[]>();
            for (int i = 0; i < m_eventStartCompress.Length; i++)
                m_eventStartCompress[i] = new AutoResetEvent(false);
        }

        private void GrabarForm_Load(object sender, EventArgs e)
        {
            CameraMgr.GetCamera(CameraIndex.Cam1).FrameReviced += Cam0_FrameReviced;
            CameraMgr.GetCamera(CameraIndex.Cam2).FrameReviced += Cam1_FrameReviced;
        }

        private void Cam1_FrameReviced(FrameRecivedEventArgs e)
        {
            try
            {
                SaveImage(e.frame, 1);
                ImageToAvi(e.frame, 1);
                m_lTotalFrames[1]++;
                pict_Cam1.BeginInvoke(new MethodInvoker(() => { pict_Cam1.Image = e.frame; }));
            }
            catch 
            {
            }
        }

        private void Cam0_FrameReviced(FrameRecivedEventArgs e)
        {
            try
            {
                SaveImage(e.frame, 0);
                ImageToAvi(e.frame, 0);
                m_lTotalFrames[0]++;
                pict_Cam0.BeginInvoke(new MethodInvoker(() => { pict_Cam0.Image = e.frame; }));
            }
            catch 
            {
            }
        }

        private void GrabarForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CameraMgr.StopGrab();
            CameraMgr.GetCamera(CameraIndex.Cam1).FrameReviced -= Cam0_FrameReviced;
            CameraMgr.GetCamera(CameraIndex.Cam2).FrameReviced -= Cam1_FrameReviced;
        }

        private void SaveImage(Bitmap bmp, int camIndex)
        {
            if (m_bSaveImage[camIndex])
            {
                m_bSaveImage[camIndex] = false;
                bmp.Save($"{Almacenamiento.GetPictureFolder(m_Paciente)}\\{DateTime.Now.ToString("HH.mm.ss dd_MM_yyyy")}_{camIndex}.Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void ImageToAvi(Bitmap bmp, int camIndex)
        {
            if (m_bGrabing)
            {
                m_lstImages[camIndex].Enqueue(bmp.BitmapToByteArray(out m_nWidht, out m_nHeight));
            }
        }

        void SetRadioButtonsState(bool bEnabled)
        {
            rB_2.Enabled = rb_A.Enabled = rB_B.Enabled = bEnabled;
        }

        private void pict_Grab_Click(object sender, EventArgs e)
        {
            if (!m_bGrabing)
            {
                pict_Cam0.Image = new Bitmap(2000, 2500);
                pict_Cam1.Image = new Bitmap(2000, 2500);
                pict_Cam0.Visible = true;
                pict_Cam1.Visible = true;

                m_nThread = 0;
                lbl_Time.Text = "00:00";
                lbl_FPS_Display.Text = "0";
                lbl_Duracion.Visible = lbl_Time.Visible = true;
                lbl_Counters.Visible = lbl_Images.Visible = lbl_FPS.Visible = lbl_FPS_Display.Visible = Properties.Settings.Default.bDebug;
                pict_Grab.Image = Properties.Resources.stop;
                SetRadioButtonsState(false);
                //Coloco en modo video
                if (rB_2.Checked)
                {
                    CameraMgr.SetVideo(CameraIndex.All);
                }
                else if (rb_A.Checked)
                {
                    CameraMgr.SetVideo(CameraIndex.Cam1);
                }
                else if (rB_B.Checked)
                {
                    CameraMgr.SetVideo(CameraIndex.Cam2);
                }

                if (rB_2.Checked)
                {
                    for (int i = 0; i < m_threadAvi.Length; i++)
                    {
                        m_lTotalFrames[i] = 0;
                        m_threadAvi[i] = new Thread(threadImageToAvi);
                        m_threadAvi[i].Name = $"threadImageToAvi {i}";
                        m_threadAvi[i].Start();
                    }
                    CameraMgr.StartGrab(CameraIndex.All,Almacenamiento.GetDayFolder(m_Paciente));                    
                }
                else if (rb_A.Checked)
                {
                    m_lTotalFrames[0] = 0;
                    m_lTotalFrames[1] = 0;
                    m_threadAvi[0] = new Thread(threadImageToAvi);
                    m_threadAvi[0].Name = $"threadImageToAvi {0}";
                    m_threadAvi[0].Start();

                    CameraMgr.StartGrab(CameraIndex.Cam1, Almacenamiento.GetDayFolder(m_Paciente));
                }
                else if (rB_B.Checked)
                {
                    m_nThread = 1; //Cincelo esto para que coja que es el 1 si solo esta la camara 1
                    m_lTotalFrames[0] = 0;
                    m_lTotalFrames[1] = 0;
                    m_threadAvi[1] = new Thread(threadImageToAvi);
                    m_threadAvi[1].Name = $"threadImageToAvi {1}";
                    m_threadAvi[1].Start();

                  CameraMgr.StartGrab(CameraIndex.Cam2, Almacenamiento.GetDayFolder(m_Paciente));
                }
                m_DateStartGrab = DateTime.Now;
                m_bGrabing = true;
            }
            else
            {
                pict_Cam0.Visible = false;
                pict_Cam1.Visible = false;
                m_bGrabing = false;
                m_DateEndGrab = DateTime.Now;
                lbl_Counters.Visible = lbl_Images.Visible = lbl_FPS.Visible = lbl_FPS_Display.Visible = lbl_Duracion.Visible = lbl_Time.Visible = false;
                pict_Grab.Image = Properties.Resources.rec;
                SetRadioButtonsState(true);
                CameraMgr.StopGrab();

                //new Thread(BitmapToAvi).Start();
                try
                {
                    pict_Cam0.Image = new Bitmap(2000, 2000);
                    pict_Cam1.Image = new Bitmap(2000, 2500);
                }
                catch
                {
                }
                for (int i = 0; i < N_CAMERAS; i++)
                {
                    if (!m_bCompressing[i] && m_threadAvi[i] != null)
                        m_eventStartCompress[i].Set();
                }
                for (int i = 0; i < N_CAMERAS; i++)
                {
                    if (m_threadAvi[i] != null)
                        m_threadAvi[i].Join();
                    m_threadAvi[i] = null;
                }
                //BitmapToAvi();
            }
        }

        private void pict_Photo_Click(object sender, EventArgs e)
        {
            if (m_bGrabing)
                return;

            pict_Cam0.SizeMode = PictureBoxSizeMode.StretchImage;
            pict_Cam0.Image = new Bitmap(2000, 2500);
            pict_Cam1.Image = new Bitmap(2000, 2500);
            pict_Cam0.Visible = true;
            pict_Cam1.Visible = true;
            pict_Photo.Enabled = false;
            m_bSaveImage[0] = true;
            m_bSaveImage[1] = true;

            if (rB_2.Checked)
            {
                CameraMgr.SetPhoto(CameraIndex.All);
                CameraMgr.TakeSnapshot(CameraIndex.All);
            }
            else if (rb_A.Checked)
            {
                CameraMgr.SetPhoto(CameraIndex.Cam1);
                CameraMgr.TakeSnapshot(CameraIndex.Cam1);
            }
            else if (rB_B.Checked)
            {
                CameraMgr.SetPhoto(CameraIndex.Cam2);
                CameraMgr.TakeSnapshot(CameraIndex.Cam2);
            }

            pict_Photo.Enabled = true;
        }

        private void threadImageToAvi()
        {
            int nCamIndex = m_nThread++;
            m_bCompressing[nCamIndex] = false;
            m_eventStartCompress[nCamIndex].WaitOne();
            m_bCompressing[nCamIndex] = true;
            var frameRate = Convert.ToDecimal(m_lstImages[nCamIndex].Count() / (m_DateEndGrab - m_DateStartGrab).TotalSeconds);
            var writer = new AviWriter($"{Almacenamiento.GetRecordFolder(m_Paciente)}\\{DateTime.Now.ToString("HH.mm.ss dd_MM_yyyy")}_{nCamIndex}.avi")
            {
                FramesPerSecond = frameRate,
                // Emitting AVI v1 index in addition to OpenDML index (AVI v2)
                // improves compatibility with some software, including 
                // standard Windows programs like Media Player and File Explorer
                EmitIndex1 = true
            };

            var encoder = new MotionJpegVideoEncoderWpf(m_nWidht, m_nHeight, Properties.Settings.Default.VideoQuality);
            var stream = writer.AddEncodingVideoStream(encoder, width: m_nWidht, height: m_nHeight);
            stream.Width = m_nWidht;
            stream.Height = m_nHeight;
      
            while (m_lstImages[nCamIndex].Count > 0 || m_bGrabing)
            {
                byte[] frameData = new byte[0];
                try
                {
                    frameData = m_lstImages[nCamIndex].Dequeue();
                }
                catch (InvalidOperationException) //Salta si cola vacia
                {
                    Thread.Sleep(1000);
                    continue;
                }
                stream.WriteFrame(true, // is key frame? (many codecs use concept of key frames, for others - all frames are keys)
                              frameData, // array with frame data
                              0, // starting index in the array
                              frameData.Length); // length of the data
            }
            writer.Close();
        }

        double CustomiceProgressBar(ProgressBar currentProgress)
        {
            double lfPercentage = Math.Round((((double)(currentProgress.Value - currentProgress.Minimum) / (double)(currentProgress.Maximum - currentProgress.Minimum)) * 100), 2);
      
            if (lfPercentage < 10 && Convert.ToInt32(currentProgress.Tag) != 2)
            {
                SetProgressState(currentProgress, 2);
                currentProgress.Tag = 2;
            }
            else if (lfPercentage < 25 && Convert.ToInt32(currentProgress.Tag) != 3)
            {
                SetProgressState(currentProgress, 3);
                currentProgress.Tag = 3;
            }
            else if (Convert.ToInt32(currentProgress.Tag) != 1)
            {
                SetProgressState(currentProgress, 1);
                currentProgress.Tag = 1;
            }
            return lfPercentage;
        }

        private void MemoryTimer_Tick(object sender, EventArgs e)
        {
            lbl_Counters.Text = $"{m_lstImages[0].Count}({m_lTotalFrames[0]})--{m_lstImages[1].Count}({m_lTotalFrames[1]})";

            pb_Memory.Value = Convert.ToInt32(ramCounter.NextValue());
            pb_HDD.Value = Convert.ToInt32(m_currentDrive.TotalFreeSpace / 1024);
            TimeSpan timeDiff = DateTime.Now - m_DateStartGrab;
            lbl_Time.Text = timeDiff.ToString(@"mm\:ss");
            lbl_FPS_Display.Text = $"{Math.Round(m_lTotalFrames[0] / timeDiff.TotalSeconds, 1)}--{Math.Round(m_lTotalFrames[1] / timeDiff.TotalSeconds, 1)}";

            CustomiceProgressBar(pb_HDD);
            double lfRamPercentage = CustomiceProgressBar(pb_Memory);

            if (m_bGrabing && (timeDiff.TotalSeconds > Properties.Settings.Default.nSecondsToStart || lfRamPercentage < Properties.Settings.Default.nMinimunRamToStart))
            {
                for (int i = 0; i < m_eventStartCompress.Length; i++)
                    if (!m_bCompressing[i] && m_threadAvi[i] != null)
                    {
                        m_DateEndGrab = DateTime.Now;
                        m_eventStartCompress[i].Set();
                    }
            }
        }
    }
}
