using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms
{
    public partial class FrmUsuarios : Form
    {
        private readonly IUsuarioService _usuarioService;
        private readonly Usuario _usuarioActual;

        public FrmUsuarios(IUsuarioService usuarioService, Usuario usuarioActual)
        {
            InitializeComponent();

            _usuarioService = usuarioService;
            _usuarioActual = usuarioActual;

        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }
        private void CargarUsuarios()
        {
            try
            {
                dGVUsrioGU.DataSource = _usuarioService.ObtenerUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al cargar los usuarios: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void botonBuscUsrioGU_Click(object sender, EventArgs e)
        {
            string criterio = cBBuscUsrioGU.SelectedItem?.ToString() ?? "";
            string texto = tBBuscUsrioGU.Text.Trim();

            if (string.IsNullOrWhiteSpace(criterio))
            {
                MessageBox.Show(
                    "Selecciona una opción para buscar.",
                    "Búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cBBuscUsrioGU.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show(
                    "Escribe el texto que deseas buscar.",
                    "Búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                tBBuscUsrioGU.Focus();
                return;
            }

            try
            {
                var usuarios = _usuarioService.BuscarUsuarios(criterio, texto);

                dGVUsrioGU.DataSource = null;
                dGVUsrioGU.DataSource = usuarios;

                if (usuarios.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron usuarios con ese criterio de búsqueda.",
                        "Sin resultados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al buscar usuarios: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void botonMostrarTodosGU_Click(object sender, EventArgs e)
        {
            CargarUsuarios();

            tBBuscUsrioGU.Clear();
            cBBuscUsrioGU.SelectedIndex = -1;
        }
        private void botonNuevoUsrioGU_Click(object sender, EventArgs e)
        {
            FrmDetalleUsuario formulario = new FrmDetalleUsuario(_usuarioService, _usuarioActual);

            formulario.ShowDialog();
            formulario.Dispose();

            CargarUsuarios();
        }

        private void botonEditUsrioGU_Click(object sender, EventArgs e)
        {
            if (dGVUsrioGU.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un usuario para editar.");
                return;
            }

            try
            {
                int idUsuario = Convert.ToInt32(
                    dGVUsrioGU.SelectedRows[0].Cells["IdUsuario"].Value);

                Usuario usuario = _usuarioService.ObtenerUsuarioPorId(idUsuario);

                FrmDetalleUsuario formulario = new FrmDetalleUsuario(
                    _usuarioService,
                    _usuarioActual,
                    usuario);

                formulario.ShowDialog();
                formulario.Dispose();

                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al abrir la edición del usuario.\n" + ex.Message);
            }
        }
        private void botonElimUsrioGU_Click(object sender, EventArgs e)
        {
            if (dGVUsrioGU.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecciona un usuario para eliminar.",
                    "Eliminar usuario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idUsuario = Convert.ToInt32(
                dGVUsrioGU.SelectedRows[0].Cells["IdUsuario"].Value);

            string nombreUsuario = Convert.ToString(dGVUsrioGU.SelectedRows[0].Cells["NombreCompleto"].Value);

            DialogResult respuesta = MessageBox.Show(
                $"¿Estás seguro de que deseas eliminar al usuario {nombreUsuario}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _usuarioService.EliminarUsuario(idUsuario);

                MessageBox.Show(
                    "Usuario eliminado correctamente.",
                    "Eliminar usuario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo eliminar el usuario.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void botonRegresarGU_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
