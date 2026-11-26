
using System;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Pantalla_HistorialUsuarios_PIA_PA
{
    public class pantalla_editarUsuario : Form
    {
        private readonly int nomina;

        private TextBox txtPrimerNombre;
        private TextBox txtSegundoNombre;
        private TextBox txtPrimerApellido;
        private TextBox txtSegundoApellido;
        private TextBox txtCorreo;
        private DateTimePicker dtpFechaNacimiento;

        private Button btnGuardar;
        private Button btnCancelar;

        public pantalla_editarUsuario(
            int nomina,
            string primerNombre,
            string segundoNombre,
            string primerApellido,
            string segundoApellido,
            string correo,
            DateTime fechaNacimiento)
        {
            this.nomina = nomina;

            // Configuración general del formulario.
            this.Text = "Editar usuario";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(480, 460);
            this.Font = new Font("Segoe UI", 10);
            this.BackColor = Color.White;
            this.AutoScaleMode = AutoScaleMode.Font;

            CrearControles();

            // Cargar los datos actuales del usuario.
            txtPrimerNombre.Text = primerNombre;
            txtSegundoNombre.Text = segundoNombre;
            txtPrimerApellido.Text = primerApellido;
            txtSegundoApellido.Text = segundoApellido;
            txtCorreo.Text = correo;

            dtpFechaNacimiento.Value =
                fechaNacimiento > DateTime.Today
                    ? DateTime.Today
                    : fechaNacimiento;

            // La nómina no se puede modificar.
            this.AcceptButton = btnGuardar;
            this.CancelButton = btnCancelar;
        }

        // Crear todos los controles del formulario.
        private void CrearControles()
        {
            TableLayoutPanel tabla = new TableLayoutPanel();

            tabla.Dock = DockStyle.Fill;
            tabla.ColumnCount = 2;
            tabla.RowCount = 8;
            tabla.Padding = new Padding(15);
            tabla.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 38));
            tabla.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 62));

            for (int i = 0; i < 8; i++)
            {
                tabla.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 12.5f));
            }

            // Nómina.
            Label lblNomina = CrearEtiqueta("Nómina:");
            TextBox txtNomina = new TextBox();

            txtNomina.Text = nomina.ToString();
            txtNomina.ReadOnly = true;
            txtNomina.Dock = DockStyle.Fill;
            txtNomina.BackColor = Color.LightGray;

            tabla.Controls.Add(lblNomina, 0, 0);
            tabla.Controls.Add(txtNomina, 1, 0);

            // Primer nombre.
            txtPrimerNombre = CrearTextBox(50, true);

            tabla.Controls.Add(
                CrearEtiqueta("Primer nombre *"), 0, 1);

            tabla.Controls.Add(txtPrimerNombre, 1, 1);

            // Segundo nombre (opcional).
            txtSegundoNombre = CrearTextBox(50, true);

            tabla.Controls.Add(
                CrearEtiqueta("Segundo nombre"), 0, 2);

            tabla.Controls.Add(txtSegundoNombre, 1, 2);

            // Primer apellido.
            txtPrimerApellido = CrearTextBox(50, true);

            tabla.Controls.Add(
                CrearEtiqueta("Primer apellido *"), 0, 3);

            tabla.Controls.Add(txtPrimerApellido, 1, 3);

            // Segundo apellido.
            txtSegundoApellido = CrearTextBox(50, true);

            tabla.Controls.Add(
                CrearEtiqueta("Segundo apellido *"), 0, 4);

            tabla.Controls.Add(txtSegundoApellido, 1, 4);

            // Correo.
            txtCorreo = CrearTextBox(100, false);

            tabla.Controls.Add(
                CrearEtiqueta("Correo electrónico *"), 0, 5);

            tabla.Controls.Add(txtCorreo, 1, 5);

            // Fecha de nacimiento.
            dtpFechaNacimiento = new DateTimePicker();

            dtpFechaNacimiento.Format = DateTimePickerFormat.Custom;
            dtpFechaNacimiento.CustomFormat = "dd/MM/yyyy";
            dtpFechaNacimiento.Dock = DockStyle.Fill;
            dtpFechaNacimiento.MaxDate = DateTime.Today;
            dtpFechaNacimiento.Value = new DateTime(2000, 1, 1);

            tabla.Controls.Add(
                CrearEtiqueta("Fecha de nacimiento *"), 0, 6);

            tabla.Controls.Add(dtpFechaNacimiento, 1, 6);

            // Botones.
            FlowLayoutPanel panelBotones = new FlowLayoutPanel();

            panelBotones.Dock = DockStyle.Fill;
            panelBotones.FlowDirection = FlowDirection.RightToLeft;
            panelBotones.WrapContents = false;

            btnGuardar = new Button();
            btnGuardar.Text = "Guardar";
            btnGuardar.Width = 110;
            btnGuardar.Height = 35;
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Width = 110;
            btnCancelar.Height = 35;
            btnCancelar.DialogResult = DialogResult.Cancel;

            panelBotones.Controls.Add(btnCancelar);
            panelBotones.Controls.Add(btnGuardar);

            tabla.Controls.Add(panelBotones, 1, 7);

            this.Controls.Add(tabla);
        }

        // Crear etiquetas.
        private Label CrearEtiqueta(string texto)
        {
            Label etiqueta = new Label();

            etiqueta.Text = texto;
            etiqueta.Dock = DockStyle.Fill;
            etiqueta.TextAlign = ContentAlignment.MiddleLeft;
            etiqueta.AutoSize = false;

            return etiqueta;
        }

        // Crear cajas de texto.
        private TextBox CrearTextBox(int longitudMaxima, bool esNombre)
        {
            TextBox caja = new TextBox();

            caja.MaxLength = longitudMaxima;
            caja.Dock = DockStyle.Fill;

            if (esNombre)
            {
                // Convertir automáticamente las letras a mayúsculas.
                caja.CharacterCasing = CharacterCasing.Upper;

                // Evitar números y símbolos al escribir.
                caja.KeyPress += Nombre_KeyPress;
            }

            return caja;
        }

        // Validar los caracteres permitidos en los nombres.
        private void Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir teclas de control, como borrar y retroceso.
            if (char.IsControl(e.KeyChar))
                return;

            // Permitir únicamente letras y espacios.
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Comprobar también los caracteres pegados en el campo.
        private bool EsNombreValido(string nombre)
        {
            return nombre.All(c =>
                char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        // Validar la información antes de guardarla.
        private bool ValidarDatos()
        {
            string primerNombre = txtPrimerNombre.Text.Trim();
            string segundoNombre = txtSegundoNombre.Text.Trim();
            string primerApellido = txtPrimerApellido.Text.Trim();
            string segundoApellido = txtSegundoApellido.Text.Trim();
            string correoTexto = txtCorreo.Text.Trim();

            if (string.IsNullOrWhiteSpace(primerNombre))
            {
                MessageBox.Show(
                    "El primer nombre es obligatorio.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPrimerNombre.Focus();
                return false;
            }

            if (!EsNombreValido(primerNombre))
            {
                MessageBox.Show(
                    "El primer nombre solamente puede contener letras y espacios.",
                    "Nombre inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPrimerNombre.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(segundoNombre) &&
                !EsNombreValido(segundoNombre))
            {
                MessageBox.Show(
                    "El segundo nombre solamente puede contener letras y espacios.",
                    "Nombre inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSegundoNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(primerApellido))
            {
                MessageBox.Show(
                    "El primer apellido es obligatorio.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPrimerApellido.Focus();
                return false;
            }

            if (!EsNombreValido(primerApellido))
            {
                MessageBox.Show(
                    "El primer apellido solamente puede contener letras y espacios.",
                    "Apellido inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPrimerApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(segundoApellido))
            {
                MessageBox.Show(
                    "El segundo apellido es obligatorio.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSegundoApellido.Focus();
                return false;
            }

            if (!EsNombreValido(segundoApellido))
            {
                MessageBox.Show(
                    "El segundo apellido solamente puede contener letras y espacios.",
                    "Apellido inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSegundoApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(correoTexto))
            {
                MessageBox.Show(
                    "El correo electrónico es obligatorio.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCorreo.Focus();
                return false;
            }

            // Validar el formato del correo.
            try
            {
                MailAddress correo = new MailAddress(correoTexto);

                if (correo.Address != correoTexto)
                {
                    throw new FormatException();
                }
            }
            catch
            {
                MessageBox.Show(
                    "Ingresa un correo electrónico válido.",
                    "Correo inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCorreo.Focus();
                return false;
            }

            if (dtpFechaNacimiento.Value.Date > DateTime.Today)
            {
                MessageBox.Show(
                    "La fecha de nacimiento no puede ser futura.",
                    "Fecha inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        // Guardar los cambios del usuario.
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            DialogResult confirmacion = MessageBox.Show(
                "¿Deseas guardar los cambios realizados al usuario?",
                "Confirmar actualización",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                int resultado = ConexionBD.ActualizarUsuarioHistorial(
                    nomina,
                    txtPrimerNombre.Text.Trim().ToUpperInvariant(),
                    txtSegundoNombre.Text.Trim().ToUpperInvariant(),
                    txtPrimerApellido.Text.Trim().ToUpperInvariant(),
                    txtSegundoApellido.Text.Trim().ToUpperInvariant(),
                    txtCorreo.Text.Trim(),
                    dtpFechaNacimiento.Value.Date);

                if (resultado > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "No se encontró el usuario activo o no se realizaron cambios.",
                        "Actualización no realizada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron guardar los cambios.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
