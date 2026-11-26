using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;


namespace TemplateCSharp.Forms.Usuarios
{
    public partial class FrmUsuarios : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUsuarioService _usuarioService;
        private int? idEdicion = null; //null= Nuevo, valor= Editando

        public FrmUsuarios(IServiceProvider serviceProvider, IUsuarioService usuarioService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _usuarioService = usuarioService;

            //Conectar el Load que no está en el diseñador
            this.Load += FrmUsuarios_Load;
        }

        private void FrmUsuarios_Load(object? sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            try
            {
                var lista = _usuarioService.Consultar();

                dgvUsuarios.AutoGenerateColumns = false; //Usar nuestras columnas del diseñador
                dgvUsuarios.DataSource = lista;

                //Mapear las columnas del diseñador a las propiedades del modelo
                dgvUsuarios.Columns["Column1"].DataPropertyName = "NombreCompleto";
                dgvUsuarios.Columns["Column2"].DataPropertyName = "NumeroNomina";
                dgvUsuarios.Columns["Column3"].DataPropertyName = "Correo";
                dgvUsuarios.Columns["Column4"].DataPropertyName = "TipoUsuario";
                dgvUsuarios.Columns["Column5"].DataPropertyName = "FechaNacimiento";

                //Formato a la fecha
                dgvUsuarios.Columns["Column5"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
            }
        }

        private void Limpiar()
        {
            idEdicion = null;
            txtNombre.Clear();
            txtApellidoP.Clear();
            txtApellidoM.Clear();
            txtCorreo.Clear();
            txtNomina.Clear();
            txtContrasena.Clear();
            cmbTipoUsuario.SelectedIndex = -1;
            dtpFechaNacimiento.Value = DateTime.Today;

            txtContrasena.Enabled = true; //Asegurar que se pueda escribir
            btnGuardar.Text = "Guardar";
            txtNombre.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoP.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoM.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtNomina.Text) ||
                (idEdicion == null && string.IsNullOrWhiteSpace(txtContrasena.Text)))
            {
                MessageBox.Show("Complete todos los campos obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbTipoUsuario.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione el tipo de usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var usuario = new Usuario
            {
                IdUsuario = idEdicion ?? 0,
                Nombre = txtNombre.Text.Trim(),
                ApellidoP = txtApellidoP.Text.Trim(),
                ApellidoM = txtApellidoM.Text.Trim(),
                Correo = txtCorreo.Text.Trim().ToLower(),
                NumeroNomina = txtNomina.Text.Trim(),
                FechaNacimiento = dtpFechaNacimiento.Value.Date,
                TipoUsuario = cmbTipoUsuario.SelectedItem?.ToString() ?? "",
                Contrasena = txtContrasena.Text,
                UsuarioRegistro = Sesion.IdUsuario
            };

            var resultado = idEdicion == null
                ? _usuarioService.Insertar(usuario)
                : _usuarioService.Actualizar(usuario);

            if (resultado.ok)
            {
                if (idEdicion == null)
                {
                    using var form = _serviceProvider.GetRequiredService<FrmUsuarioCreado>();
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Usuario actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Limpiar();
                CargarUsuarios(); //Refrescar tabla
            }
            else
            {
                MessageBox.Show(resultado.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null || dgvUsuarios.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Seleccione un usuario de la tabla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var u = (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;
            idEdicion = u.IdUsuario;

            txtNombre.Text = u.Nombre;
            txtApellidoP.Text = u.ApellidoP;
            txtApellidoM.Text = u.ApellidoM;
            txtCorreo.Text = u.Correo;
            txtNomina.Text = u.NumeroNomina;
            dtpFechaNacimiento.Value = u.FechaNacimiento;
            cmbTipoUsuario.SelectedItem = u.TipoUsuario;

            // Por seguridad, dejamos la contraseña vacía.
            // El usuario puede escribir una nueva o dejarla en blanco para conservar la anterior.
            txtContrasena.Clear();
            btnGuardar.Text = "Actualizar";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null || dgvUsuarios.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Seleccione un usuario de la tabla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var u = (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;

            using var form = _serviceProvider.GetRequiredService<FrmEliminarUsuario>();
            form.
                IdUsuarioAEliminar = u.IdUsuario; //Pasamos el ID

            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarUsuarios(); //Si se eliminó, refrescamos
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(filtro))
            {
                CargarUsuarios();
            }
            else
            {
                var lista = _usuarioService.BuscarPorApellidos(filtro);
                dgvUsuarios.DataSource = lista;
            }
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Este evento puede quedarse vacío, ya usamos btnEditar_Click
        }
    }
}

