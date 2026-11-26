using System;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;

namespace TemplateCSharp.Forms.Estadios
{
    public partial class FrmAsientosRegistrados : Form
    {
        private readonly IAsientoService _asientoService;

        public FrmAsientosRegistrados(IAsientoService asientoService)
        {
            InitializeComponent();
            _asientoService = asientoService;
        }

        private void FrmAsientosRegistrados_Load(object sender, EventArgs e)
        {
            try
            {
                var lista = _asientoService.ObtenerAsientos();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = lista;

                
                dataGridView1.Columns["Column1"].DataPropertyName = "NombreZona";
                dataGridView1.Columns["Column2"].DataPropertyName = "Fila";
                dataGridView1.Columns["Column3"].DataPropertyName = "NumeroAsiento";
                dataGridView1.Columns["Column4"].DataPropertyName = "NombreEstadio";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
