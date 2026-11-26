using System.Data;

namespace TemplateCSharp.Forms
{
    /// <summary>
    /// Lista de clientes que coinciden con una búsqueda, para elegir el correcto
    /// (por ejemplo, cuando hay varios clientes con los mismos apellidos).
    /// </summary>
    public partial class FrmSeleccionarCliente : Form
    {
        public DataRow? ClienteElegido { get; private set; }

        public FrmSeleccionarCliente(DataTable clientes)
        {
            InitializeComponent();

            UiHelper.ConfigurarGrid(
                dGVClientesSC,
                true,
                ("Nombre", "Nombre", TipoColumna.Texto),
                ("Apellido", "Apellidos", TipoColumna.Texto),
                ("RFC", "RFC", TipoColumna.Texto),
                ("Correo", "Correo electrónico", TipoColumna.Texto));

            dGVClientesSC.DataSource = clientes;
            AcceptButton = botonSeleccionarSC;
            CancelButton = botonCancelarSC;
        }

        private void botonSeleccionarSC_Click(object sender, EventArgs e)
        {
            Elegir();
        }

        private void dGVClientesSC_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Elegir();
            }
        }

        private void Elegir()
        {
            DataRow? fila = UiHelper.FilaSeleccionada(dGVClientesSC);

            if (fila == null)
            {
                UiHelper.Advertir("Selecciona un cliente de la lista.", "Seleccionar cliente");
                return;
            }

            ClienteElegido = fila;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void botonCancelarSC_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
