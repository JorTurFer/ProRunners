using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Sistema_Nuria
{
    public partial class GrabarForm : Form
    {
        Paciente m_Paciente;
        bool m_bGrabing = false;
        bool[] m_bSaveImage = new bool[2];


        public GrabarForm(Paciente pac)
        {
            InitializeComponent();
            m_Paciente = pac;
        }

        private void GrabarForm_Load(object sender, EventArgs e)
        {

            Program.lstCameras[0].FrameReviced += Cam0_FrameReviced;
            Program.lstCameras[1].FrameReviced += Cam1_FrameReviced;

        }

        private void Cam1_FrameReviced(FrameRecivedEventArgs e)
        {
            try
            {
                SaveImage(e.frame, 1);
                pict_Cam1.Invoke(new MethodInvoker(() => { pict_Cam1.Image = e.frame; }));
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
                pict_Cam0.Invoke(new MethodInvoker(() => { pict_Cam0.Image = e.frame; }));
            }
            catch
            {

            }
        }

        private void GrabarForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.lstCameras[0].StopGrab();
            Program.lstCameras[1].StopGrab();
            Program.lstCameras[0].FrameReviced -= Cam0_FrameReviced;
            Program.lstCameras[1].FrameReviced -= Cam1_FrameReviced;


        }

       private void SaveImage(Bitmap bmp, int camIndex)
        {
            if (m_bSaveImage[camIndex])
            {
                m_bSaveImage[camIndex] = false;
                bmp.Save($"{Almacenamiento.GetDayFolder(m_Paciente)}\\Fotos\\{DateTime.Now.ToString("HH.mm.ss dd_MM_yyyy")}_{camIndex}.Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        void SetRadioButtonsState(bool bEnabled)
        {
            rB_2.Enabled = rb_A.Enabled = rB_B.Enabled = bEnabled;
        }

        private void pict_Grab_Click(object sender, EventArgs e)
        {
            pict_Cam0.Image = new Bitmap(2000, 2500);
            pict_Cam1.Image = new Bitmap(2000, 2500);
            if (!m_bGrabing)
            {
                pict_Grab.Image = Properties.Resources.stop;
                SetRadioButtonsState(false);
                if (rB_2.Checked || rb_A.Checked)
                {
                    Program.lstCameras[0].StartGrab(Almacenamiento.GetDayFolder(m_Paciente));
                    Program.lstCameras[0].AccionTerminada.WaitOne();
                }
                if (rB_2.Checked || rB_B.Checked)
                {
                    Program.lstCameras[1].StartGrab(Almacenamiento.GetDayFolder(m_Paciente));
                    Program.lstCameras[1].AccionTerminada.WaitOne();
                }
            }
            else
            {
                pict_Grab.Image = Properties.Resources.rec;
                SetRadioButtonsState(true);
                Program.lstCameras[0].StopGrab();
                Program.lstCameras[0].AccionTerminada.WaitOne();
                Program.lstCameras[1].StopGrab();
                Program.lstCameras[1].AccionTerminada.WaitOne();
            }
            m_bGrabing = !m_bGrabing;
        }

        private void pict_Photo_Click(object sender, EventArgs e)
        {
            pict_Cam0.SizeMode = PictureBoxSizeMode.StretchImage;
            pict_Cam0.Image = new Bitmap(2000, 2500);
            pict_Cam1.Image = new Bitmap(2000, 2500);
            pict_Photo.Enabled = false;
            m_bSaveImage[0] = true;
            m_bSaveImage[1] = true;

            if (rB_2.Checked || rb_A.Checked)
            {
                Program.lstCameras[0].TakeSnapshot();
                Program.lstCameras[0].AccionTerminada.WaitOne();
            }
            if (rB_2.Checked || rB_B.Checked)
            {
                Program.lstCameras[1].TakeSnapshot();
                Program.lstCameras[1].AccionTerminada.WaitOne();
            }          
           
            pict_Photo.Enabled = true;
        }
    }
}
