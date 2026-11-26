using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    // Gestión de usuarios (registrar, editar, eliminar y consultar) - solo administrador
    public class UsuarioForm : Form
    {
        readonly UsuarioRepositorio usuarios = new UsuarioRepositorio();
        int? idActual = null;
        TextBox txtCorreo, txtNombre, txtNomina, txtPass, txtConfirmar;
        ComboBox cmbRol;
        DateTimePicker dtpNacimiento;
        Label lblNotaPass;
        DataGridView dgv;

        public UsuarioForm()
        {
            Ui.PrepararForm(this, "Usuarios", "Gestión de usuarios", 1000, 560);

            GroupBox g = Ui.Grupo(this, "Datos del usuario", 12, 70, 360, 470);
            Ui.Etiqueta(g, "Correo electrónico", 16, 28);
            txtCorreo = Ui.Texto(g, 16, 48, 328, 100);
            Ui.Etiqueta(g, "Nombre completo", 16, 80);
            txtNombre = Ui.Texto(g, 16, 100, 328, 100);
            Ui.Etiqueta(g, "Número de nómina", 16, 132);
            txtNomina = Ui.Texto(g, 16, 152, 328, 20);
            Ui.Etiqueta(g, "Rol", 16, 184);
            cmbRol = Ui.Combo(g, 16, 204, 328);
            cmbRol.Items.AddRange(new object[] { "Administrador", "Operativo" });
            Ui.Etiqueta(g, "Fecha de nacimiento", 16, 236);
            dtpNacimiento = Ui.Fecha(g, 16, 256, 328);
            dtpNacimiento.MaxDate = DateTime.Today;
            Ui.Etiqueta(g, "Contraseña", 16, 288);
            txtPass = Ui.Texto(g, 16, 308, 328, 50);
            txtPass.UseSystemPasswordChar = true;
            Ui.Etiqueta(g, "Confirmar contraseña", 16, 340);
            txtConfirmar = Ui.Texto(g, 16, 360, 328, 50);
            txtConfirmar.UseSystemPasswordChar = true;
            lblNotaPass = Ui.Etiqueta(g, "Mínimo 8 caracteres con mayúscula, minúscula y símbolo.", 16, 388);
            lblNotaPass.ForeColor = Color.DimGray;
            lblNotaPass.Font = new Font("Segoe UI", 8F);
            Ui.Boton(g, "Guardar", 16, 420, 104, 34, Ui.Azul, btnGuardar_Click);
            Ui.Boton(g, "Limpiar", 128, 420, 104, 34, Ui.Gris, delegate { Limpiar(); });
            Ui.Boton(g, "Eliminar", 240, 420, 104, 34, Ui.Rojo, btnEliminar_Click);

            dgv = Ui.Grid(this, 384, 78, 604, 462);
            dgv.CellClick += dgv_CellClick;
            Load += delegate { Cargar(); };
        }

        void Cargar()
        {
            if (!Sesion.EsAdmin) { Tema.Aviso("Solo un administrador puede administrar usuarios.", "Usuarios"); Close(); return; }
            try
            {
                dgv.DataSource = usuarios.Listar();
                Tema.FormatoGrid(dgv);
            }
            catch (Exception ex) { Tema.ErrorConexion(ex); }
        }

        void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow f = dgv.Rows[e.RowIndex];
            idActual = Convert.ToInt32(f.Cells["ID"].Value);
            txtCorreo.Text = Convert.ToString(f.Cells["Correo"].Value);
            txtNombre.Text = Convert.ToString(f.Cells["Nombre"].Value);
            txtNomina.Text = Convert.ToString(f.Cells["Nomina"].Value);
            cmbRol.SelectedItem = Convert.ToString(f.Cells["Rol"].Value);
            DateTime nac = Convert.ToDateTime(f.Cells["Fecha de nacimiento"].Value);
            dtpNacimiento.Value = nac < dtpNacimiento.MinDate ? dtpNacimiento.MinDate : nac;
            txtPass.Clear();
            txtConfirmar.Clear();
            lblNotaPass.Text = "Deja la contraseña vacía para no cambiarla.";
        }

        void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario u = new Usuario();
            u.Correo = txtCorreo.Text;
            u.NombreCompleto = txtNombre.Text;
            u.Nomina = txtNomina.Text;
            u.Rol = cmbRol.SelectedItem == null ? "" : cmbRol.SelectedItem.ToString();
            u.FechaNacimiento = dtpNacimiento.Value.Date;
            u.Contrasena = txtPass.Text;

            List<string> errores = u.Validar();
            bool nuevo = idActual == null;
            bool hayPass = txtPass.Text != "" || txtConfirmar.Text != "";
            if (nuevo || hayPass)
            {
                string ep = Validacion.Contrasena(txtPass.Text);
                if (ep != null) errores.Add(ep);
                else if (txtPass.Text != txtConfirmar.Text) errores.Add("La contraseña y su confirmación no coinciden.");
            }
            if (Tema.HayErrores(errores, "Usuarios")) return;

            try
            {
                if (nuevo) usuarios.Insertar(u, Sesion.Correo);
                else
                {
                    u.Id = idActual.Value;
                    usuarios.Actualizar(u);
                    if (hayPass) usuarios.CambiarContrasena(u.Id, txtPass.Text);
                }
                Limpiar();
                Cargar();
                Tema.Info(nuevo ? "Usuario registrado correctamente." : "Usuario actualizado correctamente.", "Usuarios");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Usuarios"); }
        }

        void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idActual == null) { Tema.Info("Primero elige un usuario de la tabla.", "Usuarios"); return; }
            if (!Tema.ConfirmarEliminar("Usuarios")) return;
            try
            {
                usuarios.Eliminar(idActual.Value, Sesion.IdUsuario);
                Limpiar();
                Cargar();
                Tema.Info("Usuario eliminado.", "Usuarios");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Usuarios"); }
        }

        void Limpiar()
        {
            idActual = null;
            txtCorreo.Clear(); txtNombre.Clear(); txtNomina.Clear();
            txtPass.Clear(); txtConfirmar.Clear();
            cmbRol.SelectedIndex = -1;
            dtpNacimiento.Value = DateTime.Today;
            lblNotaPass.Text = "Mínimo 8 caracteres con mayúscula, minúscula y símbolo.";
            dgv.ClearSelection();
        }
    }
}
