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
        Paciente m_paciente = null;
        public MainForm()
        {
            InitializeComponent();
            label1.Text = "";
        }

        private void seleccionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeleccionarPaciente selForm = new SeleccionarPaciente();
            selForm.ShowDialog();
            m_paciente = selForm.GetPaciente();
        }

        private void timerLabel_Tick(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (m_paciente != null)
                sb.AppendLine($"Paciente: {m_paciente?.Nombre}");
            sb.AppendLine(DateTime.Now.ToString("HH:mm:ss"));
            label1.Text = sb.ToString();
        }


        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AgregarPaciente agForm = new AgregarPaciente();
            agForm.ShowDialog();
            m_paciente = agForm.GetPaciente();
        }

        private void grabarVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrabarForm grab = new GrabarForm();
            grab.ShowDialog();
        }      

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
