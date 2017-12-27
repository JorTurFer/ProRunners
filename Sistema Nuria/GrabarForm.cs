using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Nuria
{
    public partial class GrabarForm : Form
    {
        CameraManager cam2;
        public GrabarForm()
        {
            InitializeComponent();
        }

        private void GrabarForm_Load(object sender, EventArgs e)
        {
           cam2 = new CameraManager(2, pictureBox1 );
            cam2.FrameReviced += Cam2_FrameReviced;
           
        }

        private void Cam2_FrameReviced(FrameRecivedEventArgs e)
        {
            pictureBox1.Image = e.frame;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cam2.StartGrab();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cam2.StopGrab();
        }
       
    }
}
