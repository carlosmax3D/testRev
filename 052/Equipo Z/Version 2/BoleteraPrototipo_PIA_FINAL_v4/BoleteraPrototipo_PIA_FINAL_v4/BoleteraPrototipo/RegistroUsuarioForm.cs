using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    // Registro de usuario desde la pantalla de Login.
    // Sin sesión iniciada solo se pueden crear usuarios Operativos
    // (los administradores se crean desde la pantalla Usuarios).
    public class RegistroUsuarioForm : Form
    {
        readonly UsuarioRepositorio usuarios = new UsuarioRepositorio();
        TextBox txtCorreo, txtNombre, txtNomina, txtPass, txtConfirmar;
        ComboBox cmbRol;
        DateTimePicker dtpNacimiento;
        CheckBox chkMostrar;

        public RegistroUsuarioForm()
        {
            Ui.PrepararForm(this, "Registro de usuario", "Registro de usuario", 420, 590);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int y = 76;
            Ui.Etiqueta(this, "Correo electrónico", 24, y);
            txtCorreo = Ui.Texto(this, 24, y + 20, 370, 100);
            y += 54;
            Ui.Etiqueta(this, "Nombre completo", 24, y);
            txtNombre = Ui.Texto(this, 24, y + 20, 370, 100);
            y += 54;
            Ui.Etiqueta(this, "Número de nómina", 24, y);
            txtNomina = Ui.Texto(this, 24, y + 20, 370, 20);
            y += 54;
            Ui.Etiqueta(this, "Rol", 24, y);
            cmbRol = Ui.Combo(this, 24, y + 20, 370);
            cmbRol.Items.Add("Operativo");
            if (Sesion.EsAdmin) cmbRol.Items.Add("Administrador");
            cmbRol.SelectedIndex = 0;
            cmbRol.Enabled = Sesion.EsAdmin;
            y += 54;
            Ui.Etiqueta(this, "Fecha de nacimiento", 24, y);
            dtpNacimiento = Ui.Fecha(this, 24, y + 20, 370);
            dtpNacimiento.MaxDate = DateTime.Today;
            y += 54;
            Ui.Etiqueta(this, "Contraseña", 24, y);
            txtPass = Ui.Texto(this, 24, y + 20, 370, 50);
            txtPass.UseSystemPasswordChar = true;
            y += 54;
            Ui.Etiqueta(this, "Confirmar contraseña", 24, y);
            txtConfirmar = Ui.Texto(this, 24, y + 20, 370, 50);
            txtConfirmar.UseSystemPasswordChar = true;
            y += 50;
            chkMostrar = new CheckBox();
            chkMostrar.Text = "Mostrar contraseñas";
            chkMostrar.AutoSize = true;
            chkMostrar.Location = new Point(24, y);
            chkMostrar.CheckedChanged += delegate
            {
                txtPass.UseSystemPasswordChar = !chkMostrar.Checked;
                txtConfirmar.UseSystemPasswordChar = !chkMostrar.Checked;
            };
            Controls.Add(chkMostrar);
            Label nota = Ui.Etiqueta(this, "Mínimo 8 caracteres con mayúscula, minúscula y un símbolo.", 24, y + 24);
            nota.ForeColor = Color.DimGray;
            nota.Font = new Font("Segoe UI", 8F);

            Ui.Boton(this, "Registrar", 24, y + 54, 180, 36, Ui.Azul, btnRegistrar_Click);
            Ui.Boton(this, "Cancelar", 214, y + 54, 180, 36, Ui.Gris, delegate { Close(); });
        }

        void btnRegistrar_Click(object sender, EventArgs e)
        {
            Usuario u = new Usuario();
            u.Correo = txtCorreo.Text;
            u.NombreCompleto = txtNombre.Text;
            u.Nomina = txtNomina.Text;
            u.Rol = cmbRol.SelectedItem == null ? "" : cmbRol.SelectedItem.ToString();
            u.FechaNacimiento = dtpNacimiento.Value.Date;
            u.Contrasena = txtPass.Text;

            List<string> errores = u.Validar();
            string ep = Validacion.Contrasena(txtPass.Text);
            if (ep != null) errores.Add(ep);
            else if (txtPass.Text != txtConfirmar.Text) errores.Add("La contraseña y su confirmación no coinciden.");
            if (Tema.HayErrores(errores, "Registro de usuario")) return;

            try
            {
                usuarios.Insertar(u, Sesion.IdUsuario > 0 ? Sesion.Correo : u.Correo.Trim());
                Tema.Info("Usuario registrado correctamente. Ya puedes iniciar sesión.", "Registro de usuario");
                Close();
            }
            catch (Exception ex) { Tema.Manejar(ex, "Registro de usuario"); }
        }
    }
}
