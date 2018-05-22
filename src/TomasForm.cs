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
    public partial class TomasForm : Form
    {
        public TomasForm(string strPaciente)
        {
            InitializeComponent();
            txtFiltro.Text = strPaciente;
        }

        private void TomasForm_Load(object sender, EventArgs e)
        {
            LoadTreeView(txtFiltro.Text);
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            source.AddRange(Almacenamiento.GetRecordedPacientes().Select(x => x.Nombre).ToArray());
            txtFiltro.AutoCompleteCustomSource = source;
            txtFiltro.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFiltro.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void LoadTreeView(string strPaciente = "")
        {

            TreeNode nodeRoot = new TreeNode("Pacientes");
            nodeRoot.Tag = "Root";

            foreach (var item in Almacenamiento.GetRecordedPacientes(strPaciente))
            {
                TreeNode nodePaciente = new TreeNode($"{item.Nombre}_{item.Nacimiento.ToString("dd-MM-yyyy")}");
                nodePaciente.Tag = "Paciente";
                TreeNode nodeVideo = new TreeNode("Videos");
                nodeVideo.Tag = "Tipo";
                nodePaciente.Nodes.Add(nodeVideo);

                foreach (var videos in item.SesionesVideo)
                {
                    TreeNode nodeDia = new TreeNode(videos.Date.ToString("dd-MM-yyyy"));
                    nodeDia.Tag = "Dia";
                    nodeVideo.Nodes.Add(nodeDia);
                }

                TreeNode nodeFoto = new TreeNode("Fotos");
                nodeFoto.Tag = "Tipo";
                nodePaciente.Nodes.Add(nodeFoto);

                foreach (var fotos in item.SesionesFoto)
                {
                    TreeNode nodeDia = new TreeNode(fotos.Date.ToString("dd-MM-yyyy"));
                    nodeDia.Tag = "Dia";
                    nodeFoto.Nodes.Add(nodeDia);
                }

                nodeRoot.Nodes.Add(nodePaciente);
            }
            treeTomas.Nodes.Clear();
            treeTomas.Nodes.Add(nodeRoot);
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text.Length < 5)
                return;
            LoadTreeView(txtFiltro.Text);
        }

        private void treeTomas_AfterSelect(object sender, TreeViewEventArgs e)
        {
            abrirToolStripMenuItem.Visible = treeTomas.SelectedNode != null && treeTomas.SelectedNode.Text != "Pacientes"
                && treeTomas.SelectedNode.Text != "Videos" && treeTomas.SelectedNode.Text != "Fotos";
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<TreeNode> lstNodes = new List<TreeNode>();
            TreeNode nodeSelected = treeTomas.SelectedNode;

            while (nodeSelected.Parent != null)
            {
                lstNodes.Add(nodeSelected);
                nodeSelected = nodeSelected.Parent;
            }

            TreeNode nodeNombre = lstNodes.Where(x => x.Tag.ToString() == "Paciente").FirstOrDefault();
            TreeNode nodeTipo = lstNodes.Where(x => x.Tag.ToString() == "Tipo").FirstOrDefault();
            TreeNode nodeDia = lstNodes.Where(x => x.Tag.ToString() == "Dia").FirstOrDefault();

            StringBuilder sbPath = new StringBuilder();
            sbPath.Append(Directorios.ObtenerSubdirectorioDeAplicacion("Datos"));
            if (nodeNombre != null)
                sbPath.Append($@"\{nodeNombre.Text}");          
            if (nodeDia != null)
                sbPath.Append($@"\{nodeDia.Text}");
            if (nodeTipo != null)
                sbPath.Append($@"\{nodeTipo.Text}");
            
            System.Diagnostics.Process.Start("explorer", sbPath.ToString());
        }
    }
}
