using System.Data;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms
{
    /// <summary>Captura de un cliente nuevo o edición de uno existente.</summary>
    public partial class FrmDetalleCliente : Form
    {
        private readonly IClienteService _clienteService;
        private readonly ISesionUsuario _sesion;
        private readonly Cliente? _clienteAEditar;
        private readonly DataTable _telefonos = new DataTable();

        /// <summary>Id del cliente guardado (lo usa la pantalla de venta al registrar un cliente nuevo).</summary>
        public int IdClienteGuardado { get; private set; }

        public FrmDetalleCliente(IClienteService clienteService, ISesionUsuario sesion, Cliente? clienteAEditar)
        {
            InitializeComponent();

            _clienteService = clienteService;
            _sesion = sesion;
            _clienteAEditar = clienteAEditar;

            Text = _clienteAEditar == null ? "Nuevo cliente" : "Editar cliente";
            txtDirDC.Text = "Domicilio:";

            // Longitudes máximas iguales a las de la base de datos
            textBox1.MaxLength = 80;     // Nombre
            textBox2.MaxLength = 100;    // Apellidos
            textBox3.MaxLength = 250;    // Domicilio completo
            textBox4.MaxLength = 13;     // RFC
            textBox4.CharacterCasing = CharacterCasing.Upper;
            textBox5.MaxLength = 100;    // Correo
            tBTelDC.MaxLength = 10;
            UiHelper.SoloDigitos(tBTelDC);
            UiHelper.ConfigurarFecha(dateTimePicker1);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;

            _telefonos.Columns.Add("Tipo", typeof(string));
            _telefonos.Columns.Add("Telefono", typeof(string));

            UiHelper.ConfigurarGrid(
                dataGridView1,
                true,
                ("Tipo", "Tipo de teléfono", TipoColumna.Texto),
                ("Telefono", "Número (10 dígitos)", TipoColumna.Texto));

            dataGridView1.DataSource = _telefonos;

            botonAgregTelDC.Click += botonAgregTelDC_Click;
            botonGuardarDC.Click += botonGuardarDC_Click;
            botonCancelDC.Click += botonCancelDC_Click;

            if (_clienteAEditar != null)
            {
                CargarDatos(_clienteAEditar);
            }
            else
            {
                dateTimePicker1.Value = DateTime.Today.AddYears(-18);
            }
        }

        private void CargarDatos(Cliente cliente)
        {
            textBox1.Text = cliente.Nombre;
            textBox2.Text = cliente.Apellido;
            textBox3.Text = cliente.DomicilioCompleto;
            textBox4.Text = cliente.RFC;
            textBox5.Text = cliente.Correo;
            dateTimePicker1.Value = cliente.FechaNacimiento;

            if (cliente.TelefonoCasa.Length > 0)
            {
                _telefonos.Rows.Add("Casa", cliente.TelefonoCasa);
            }

            if (cliente.TelefonoCelular.Length > 0)
            {
                _telefonos.Rows.Add("Celular", cliente.TelefonoCelular);
            }
        }

        private string TelefonoDeTipo(string tipo)
        {
            foreach (DataRow fila in _telefonos.Rows)
            {
                if (Convert.ToString(fila["Tipo"]) == tipo)
                {
                    return Convert.ToString(fila["Telefono"]) ?? string.Empty;
                }
            }

            return string.Empty;
        }

        private void botonAgregTelDC_Click(object? sender, EventArgs e)
        {
            string tipo = comboBox1.SelectedItem?.ToString() ?? string.Empty;
            string telefono = tBTelDC.Text.Trim();

            if (tipo.Length == 0)
            {
                UiHelper.Advertir("Selecciona el tipo de teléfono (Casa o Celular).");
                comboBox1.Focus();
                return;
            }

            if (!Validaciones.EsTelefonoValido(telefono))
            {
                UiHelper.Advertir("El teléfono debe tener exactamente 10 dígitos.");
                tBTelDC.Focus();
                return;
            }

            if (TelefonoDeTipo(tipo).Length > 0)
            {
                UiHelper.Advertir(
                    "Ya hay un teléfono de tipo " + tipo + ". Si quieres cambiarlo, selecciónalo en la lista y elimínalo primero.");
                return;
            }

            foreach (DataRow fila in _telefonos.Rows)
            {
                if (Convert.ToString(fila["Telefono"]) == telefono)
                {
                    UiHelper.Advertir("Ese número ya está en la lista. El teléfono de casa y el celular deben ser diferentes.");
                    tBTelDC.Focus();
                    return;
                }
            }

            _telefonos.Rows.Add(tipo, telefono);

            tBTelDC.Clear();
            comboBox1.SelectedIndex = tipo == "Casa" ? 1 : 0;
            tBTelDC.Focus();
        }

        // Botón "Eliminar" teléfono (enlazado por el diseñador)
        private void botonElimTelDC_Click(object sender, EventArgs e)
        {
            DataRow? fila = UiHelper.FilaSeleccionada(dataGridView1);

            if (fila == null)
            {
                UiHelper.Advertir("Selecciona el teléfono que quieres quitar de la lista.");
                return;
            }

            _telefonos.Rows.Remove(fila);
        }

        private void botonGuardarDC_Click(object? sender, EventArgs e)
        {
            // Ningún campo vacío
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                UiHelper.Advertir("Escribe el nombre del cliente.");
                textBox1.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                UiHelper.Advertir("Escribe los apellidos del cliente.");
                textBox2.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                UiHelper.Advertir("Escribe el domicilio completo del cliente.");
                textBox3.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                UiHelper.Advertir("Escribe el RFC del cliente.");
                textBox4.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                UiHelper.Advertir("Escribe el correo electrónico del cliente.");
                textBox5.Focus();
                return;
            }

            if (TelefonoDeTipo("Casa").Length == 0 || TelefonoDeTipo("Celular").Length == 0)
            {
                UiHelper.Advertir("Agrega el teléfono de casa y el teléfono celular del cliente.");
                tBTelDC.Focus();
                return;
            }

            bool esEdicion = _clienteAEditar != null;

            Cliente cliente = new Cliente
            {
                IdCliente = _clienteAEditar?.IdCliente ?? 0,
                Nombre = textBox1.Text,
                Apellido = textBox2.Text,
                DomicilioCompleto = textBox3.Text,
                RFC = textBox4.Text,
                Correo = textBox5.Text,
                FechaNacimiento = dateTimePicker1.Value.Date,
                TelefonoCasa = TelefonoDeTipo("Casa"),
                TelefonoCelular = TelefonoDeTipo("Celular")
            };

            try
            {
                if (esEdicion)
                {
                    if (!UiHelper.Confirmar("¿Deseas guardar los cambios del cliente?", "Confirmar cambios"))
                    {
                        return;
                    }

                    _clienteService.ActualizarCliente(cliente, _sesion.IdUsuario);
                    IdClienteGuardado = cliente.IdCliente;

                    UiHelper.Informar("Los cambios del cliente se guardaron correctamente.", "Clientes");
                }
                else
                {
                    IdClienteGuardado = _clienteService.RegistrarCliente(cliente, _sesion.IdUsuario);

                    UiHelper.Informar("Cliente registrado correctamente.", "Clientes");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, esEdicion ? "No se pudo actualizar el cliente" : "No se pudo registrar el cliente");
            }
        }

        private void botonCancelDC_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
    }
}
