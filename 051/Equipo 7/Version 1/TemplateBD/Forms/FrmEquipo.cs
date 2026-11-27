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


namespace TemplateCSharp.Forms;

public partial class FrmEquipo : Form
{
    private readonly IEquipoService _equipoService;

    public FrmEquipo(IEquipoService equipoService)
    {
        InitializeComponent();
        _equipoService = equipoService;
    }

    private void FrmEquipo_Load(object sender, EventArgs e)
    {
        CargarEquipos();
    }

    private void CargarEquipos()
    {
        try
        {
            dgvEquipos.DataSource = null;
            dgvEquipos.DataSource = _equipoService.ObtenerEquipos();

            if (dgvEquipos.Columns["UsuarioAuditoria"] != null)
                dgvEquipos.Columns["UsuarioAuditoria"].Visible = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar equipos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCrear_Click(object sender, EventArgs e)
    {
        try
        {
            Equipo nuevoEquipo = new()
            {
                Nombre = txt_Nombre.Text.Trim(),
                Ciudad = txt_Ciudad.Text.Trim()
            };

            _equipoService.RegistrarEquipo(nuevoEquipo);

            MessageBox.Show("Equipo registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txt_Nombre.Clear();
            txt_Ciudad.Clear();
            txt_Nombre.Focus();
            CargarEquipos();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void label1_Click(object sender, EventArgs e) { }
}
