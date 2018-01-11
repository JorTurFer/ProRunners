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

        public GrabarForm(Paciente pac)
        {
            InitializeComponent();
            m_Paciente = pac;
        }

        private void GrabarForm_Load(object sender, EventArgs e)
        {

            Program.CameraMgr2.FrameReviced += Cam2_FrameReviced;

        }

        private void Cam2_FrameReviced(FrameRecivedEventArgs e)
        {
            try
            {
                pictureBox1.Invoke(new MethodInvoker(() => { pictureBox1.Image = e.frame; }));
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            Program.CameraMgr2.StartGrab(Almacenamiento.GetDayFolder(m_Paciente));
            Program.CameraMgr2.AccionTerminada.WaitOne();
            btnStop.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            Program.CameraMgr2.StopGrab();
            Program.CameraMgr2.AccionTerminada.WaitOne();
            btnStart.Enabled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            btnSnap.Enabled = false;

            Program.CameraMgr2.TakeSnapshot(Almacenamiento.GetDayFolder(m_Paciente));
            Program.CameraMgr2.AccionTerminada.WaitOne();
            btnSnap.Enabled = true;
        }

        private void GrabarForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.CameraMgr2.StopGrab();
            Program.CameraMgr2.FrameReviced -= Cam2_FrameReviced;

        }
    }
}
