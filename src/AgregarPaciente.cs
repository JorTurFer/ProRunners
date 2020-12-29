using System;
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
    public partial class AgregarPaciente : Form
    {
        Paciente m_paciente = null;
        public AgregarPaciente()
        {
            InitializeComponent();
        }

        private void txt_Name_TextChanged(object sender, EventArgs e)
        {
            if (txt_Name.Text == "")
                lbl_Nombre.ForeColor = Color.Red;
            else
            {
                lbl_Nombre.ForeColor = Color.Black;
                btn_Aceptar.Enabled = lbl_Date.ForeColor == Color.Black;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_paciente = null;
            this.Close();
        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            m_paciente = new Paciente();
            m_paciente.Nacimiento = date.Value;
            m_paciente.Nombre = Auxiliares.RebuildName(txt_Name.Text);
            m_paciente.SesionesVideo = new List<DateTime>();
            m_paciente.SesionesFoto = new List<DateTime>();
            if(!Almacenamiento.AgregarPaciente(m_paciente))
            {
                MessageBox.Show("Ya existe un paciente con ese nombre y fecha de nacimiento", "Aviso");
                return;
            }
            this.Close();
        }

        public Paciente GetPaciente()
        {
            return m_paciente;
        }

        private void date_ValueChanged(object sender, EventArgs e)
        {            
            lbl_Date.ForeColor = Color.Black;
            btn_Aceptar.Enabled = lbl_Nombre.ForeColor == Color.Black;
        }
    }
}
