using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Utilities;

namespace TemplateCSharp.Forms
{
    public partial class FrmUsuarios : Form
    {
        private readonly ITipoUsuarioService _tipoUsuarioService;
        private readonly Usuario _usuarioActual;

        public FrmUsuarios(ITipoUsuarioService tipoUsuarioService, Usuario usuarioActual)
        {
            InitializeComponent();
            _tipoUsuarioService = tipoUsuarioService;
            _usuarioActual = usuarioActual;
        }

        private async void FrmUsuarios_Load(object sender, EventArgs e)
        {
            cbEstatusUsuario.Items.Add(new OpcionCombo() { Valor = 1, Texto = "ACTIVO" });
            cbEstatusUsuario.Items.Add(new OpcionCombo() { Valor = 0, Texto = "NO ACTIVO" });

            cbEstatusUsuario.DisplayMember = "Texto";
            cbEstatusUsuario.ValueMember = "Valor";
            cbEstatusUsuario.SelectedIndex = 0;

            await CargarTiposUsuarioAsync();
        }

        private async Task CargarTiposUsuarioAsync()
        {
            List<TipoUsuario> listaTipoUsuario =
                await _tipoUsuarioService.ObtenerTodosAsync();

            cbTipoUsuario.Items.Clear();

            foreach (TipoUsuario item in listaTipoUsuario)
            {
                cbTipoUsuario.Items.Add(new OpcionCombo()
                {
                    Valor = item.Id_tipo,
                    Texto = item.Descripcion
                });
            }

            cbTipoUsuario.DisplayMember = "Texto";
            cbTipoUsuario.ValueMember = "Valor";

            if (cbTipoUsuario.Items.Count > 0)
                cbTipoUsuario.SelectedIndex = 0;
        }

        private void btnGuardarUsuario_Click(object sender, EventArgs e)
        {

            int fila = dgvDataUsuario.Rows.Add();

            dgvDataUsuario.Rows[fila].Cells["IdUsuario"].Value = txtId.Text;
            dgvDataUsuario.Rows[fila].Cells["Email"].Value = txtEmailUsuario.Text;
            dgvDataUsuario.Rows[fila].Cells["PW"].Value = txtPW.Text;
            dgvDataUsuario.Rows[fila].Cells["Nombre"].Value = txtNombreUsuario.Text;
            dgvDataUsuario.Rows[fila].Cells["Apellidos"].Value = txtApellidosUsuario.Text;
            dgvDataUsuario.Rows[fila].Cells["NumNomina"].Value = txtNumNominaUsuario.Text;
            dgvDataUsuario.Rows[fila].Cells["FechaNac"].Value = dtpFechaNac.Value;

            dgvDataUsuario.Rows[fila].Cells["IdTipo"].Value =
                ((OpcionCombo)cbTipoUsuario.SelectedItem).Valor;

            dgvDataUsuario.Rows[fila].Cells["TipoUsuario"].Value =
                ((OpcionCombo)cbTipoUsuario.SelectedItem).Texto;

            dgvDataUsuario.Rows[fila].Cells["Estatus"].Value =
                ((OpcionCombo)cbEstatusUsuario.SelectedItem).Texto;

            dgvDataUsuario.Rows[fila].Cells["EstatusValor"].Value =
                ((OpcionCombo)cbEstatusUsuario.SelectedItem).Valor;

            dgvDataUsuario.Rows[fila].Cells["IdUsuarioReg"].Value =
                _usuarioActual.Id_usuario;

            dgvDataUsuario.Rows[fila].Cells["UsuarioRegistrador"].Value =
                $"{_usuarioActual.Nombre} {_usuarioActual.Apellidos}";

            DateTime fechaHora = DateTime.Now;

            dgvDataUsuario.Rows[fila].Cells["FechaRegistro"].Value = fechaHora.Date;
            dgvDataUsuario.Rows[fila].Cells["HoraRegistro"].Value = fechaHora.ToString("HH:mm:ss");
        }
    }
}
