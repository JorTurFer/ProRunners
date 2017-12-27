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
    public partial class AgregarPaciente : Form
    {
        DateTime m_Nacimiento = new DateTime();
        Paciente m_paciente = null;
        public AgregarPaciente()
        {
            InitializeComponent();
        }

        private void txt_Date_TextChanged(object sender, EventArgs e)
        {
            if(DateTime.TryParse(txt_Date.Text, out m_Nacimiento))
            {
                lbl_Date.ForeColor = Color.Black;
            }
            else
            {
                lbl_Date.ForeColor = Color.Red;
                btn_Aceptar.Enabled = lbl_Nombre.ForeColor == Color.Black;
            }
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
            m_paciente.Nacimiento = m_Nacimiento;
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
    }
}
