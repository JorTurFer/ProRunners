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
    public partial class SeleccionarPaciente : Form
    {
        List<Paciente> m_lstPacientes;
        Paciente m_paciente;


        public SeleccionarPaciente()
        {
            InitializeComponent();
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            source.AddRange(Almacenamiento.GetRecordedPacientes().Select(x => x.Nombre).ToArray());
            txtFiltro.AutoCompleteCustomSource = source;
            txtFiltro.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFiltro.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void SeleccionarPaciente_Load(object sender, EventArgs e)
        {
            LoadDatagrid();
        }

        void LoadDatagrid(string strPaciente = "")
        {
            m_lstPacientes = Almacenamiento.GetRecordedPacientes(strPaciente).OrderBy(x => x.Nombre).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Edad", typeof(int));
            dt.Columns.Add("Sesiones Video", typeof(int));
            dt.Columns.Add("Sesiones Foto", typeof(int));

            DataRow dr;

            foreach (var pac in m_lstPacientes)
            {
                dr = dt.NewRow();
                dr[0] = pac.Nombre;
                dr[1] = pac.Edad;
                dr[2] = pac.SesionesVideo.Count;
                dr[3] = pac.SesionesFoto.Count;
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }


        public Paciente GetPaciente()
        {
            return m_paciente;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                m_paciente = m_lstPacientes[e.RowIndex];
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Close();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            LoadDatagrid(txtFiltro.Text);
        }
    }
}
