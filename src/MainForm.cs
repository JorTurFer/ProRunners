﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProRunners
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
            if (!CameraMgr.IsReady)
            {
                MessageBox.Show("Las camaras se estan conectando, espera un momento");
                return;
            }
            else if(m_Paciente == null)
            {
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CameraMgr.CloseCameras();            
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
