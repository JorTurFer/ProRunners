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
    public partial class MainForm : Form
    {
        Paciente m_Paciente = null;
        public MainForm()
        {
            InitializeComponent();
            label1.Text = "";
        }

        private void seleccionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SeleccionarPaciente selForm = new SeleccionarPaciente())
            {
                selForm.ShowDialog();
                m_Paciente = selForm.GetPaciente();
            }
        }

        private void timerLabel_Tick(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (m_Paciente != null)
                sb.AppendLine($"Paciente: {m_Paciente?.Nombre}");
            sb.AppendLine(DateTime.Now.ToString("HH:mm:ss"));
            label1.Text = sb.ToString();
        }


        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AgregarPaciente agForm = new AgregarPaciente())
            {
                agForm.ShowDialog();
                m_Paciente = agForm.GetPaciente();
            }
        }

        private void grabarVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Program.lstCameras[0].Ready || !Program.lstCameras[1].Ready)
            {
                Sintetizador.DecirAsync("Las cámaras se estan conectando, espera un momento por favor");
                MessageBox.Show("Las camaras se estan conectando, espera un momento");
                return;
            }
            else if(m_Paciente == null)
            {
                Sintetizador.DecirAsync("Falta seleccionar paciente");
                MessageBox.Show("Falta seleccionar paciente");
                return;
            }
            using (GrabarForm grab = new GrabarForm(m_Paciente))
            {
                grab.ShowDialog();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hacerFotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(var camera in Program.lstCameras)
            {
                camera.Dispose();
            }
            
        }

        private void visualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TomasForm tomas = new TomasForm(m_Paciente?.Nombre))
            {
                tomas.ShowDialog();
            }
        }
    }
}
