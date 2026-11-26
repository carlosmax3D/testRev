using System;
using System.Windows.Forms;
using PIA_PA.Entidades.Equipos;
using PIA_PA.Entidades.Deporte;

namespace PIA_PA.Menus
{
    public partial class EliminarEquipo : UserControl
    {
        private readonly IEquiposService _equipos;
        private Equipos? _seleccionado;

        public EliminarEquipo(IEquiposService equipos, IDeporteService deportes)
        {
            InitializeComponent();
            _equipos = equipos;
            cBDeporte.DataSource = deportes.ObtenerTodos();
            cBDeporte.DisplayMember = nameof(Deporte.Deporte_Nombre);
            cBDeporte.ValueMember = nameof(Deporte.Deporte_ID);
            foreach (Control c in new Control[] { txtID2, txtNombre, txtEstatus })
                if (c is TextBox tb) tb.ReadOnly = true;
            btnSeleccionar.Click += Seleccionar_Click;
            btnEliminar.Click += Eliminar_Click;
        }

        private void Seleccionar_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtID1.Text.Trim(), out int id) || id <= 0)
            { MessageBox.Show("Escribe un ID de equipo válido."); return; }
            _seleccionado = _equipos.ObtenerPorId(id);
            if (_seleccionado == null) { MessageBox.Show("No se encontró el equipo."); return; }
            txtID2.Text = _seleccionado.Equipo_ID.ToString();
            txtNombre.Text = _seleccionado.Equipo_Nombre;
            txtEstatus.Text = _seleccionado.Equipo_Estatus;
            cBDeporte.SelectedValue = _seleccionado.Deporte_ID;
        }

        private void Eliminar_Click(object? sender, EventArgs e)
        {
            if (_seleccionado == null) { MessageBox.Show("Primero busca un equipo."); return; }
            if (MessageBox.Show("¿Eliminar el equipo seleccionado? Si tiene partidos asociados, la base de datos puede impedirlo.", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                _equipos.Eliminar(_seleccionado.Equipo_ID);
                MessageBox.Show("Equipo eliminado correctamente.");
                _seleccionado = null;
                txtID1.Clear(); txtID2.Clear(); txtNombre.Clear(); txtEstatus.Clear();
            }
            catch (Exception ex) { MessageBox.Show("No se pudo eliminar el equipo: " + ex.Message); }
        }
    }
}
