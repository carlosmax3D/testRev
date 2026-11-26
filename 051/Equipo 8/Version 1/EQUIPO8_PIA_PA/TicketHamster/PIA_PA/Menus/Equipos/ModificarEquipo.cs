using System;
using System.Linq;
using System.Windows.Forms;
using PIA_PA.Entidades.Equipos;
using PIA_PA.Entidades.Deporte;

namespace PIA_PA
{
    public partial class ModificarEquipo : UserControl
    {
        private readonly IEquiposService _equipos;
        private readonly IDeporteService _deportes;
        private Equipos? _seleccionado;

        public ModificarEquipo(IEquiposService equipos, IDeporteService deportes)
        {
            InitializeComponent();
            _equipos = equipos;
            _deportes = deportes;
            cBDeporte.DataSource = _deportes.ObtenerTodos();
            cBDeporte.DisplayMember = nameof(Deporte.Deporte_Nombre);
            cBDeporte.ValueMember = nameof(Deporte.Deporte_ID);
            btnSeleccionar.Click += Seleccionar_Click;
            btnModificar.Click += Modificar_Click;
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
            txtID1.ReadOnly = true;
        }

        private void Modificar_Click(object? sender, EventArgs e)
        {
            if (_seleccionado == null) { MessageBox.Show("Primero busca un equipo."); return; }
            string nombre = txtNombre.Text.Trim();
            string estatus = txtEstatus.Text.Trim();
            if (nombre.Length < 2 || nombre.Any(char.IsControl)) { MessageBox.Show("Escribe un nombre válido."); return; }
            if (string.IsNullOrWhiteSpace(estatus)) { MessageBox.Show("Especifica el estatus."); return; }
            if (cBDeporte.SelectedValue == null) { MessageBox.Show("Selecciona un deporte."); return; }
            try
            {
                if (_equipos.ObtenerTodos().Any(x => x.Equipo_ID != _seleccionado.Equipo_ID && string.Equals(x.Equipo_Nombre, nombre, StringComparison.OrdinalIgnoreCase)))
                { MessageBox.Show("Ya existe otro equipo con ese nombre."); return; }
                _seleccionado.Equipo_Nombre = nombre;
                _seleccionado.Equipo_Estatus = estatus;
                _seleccionado.Deporte_ID = Convert.ToInt32(cBDeporte.SelectedValue);
                _equipos.Actualizar(_seleccionado);
                MessageBox.Show("Equipo modificado correctamente.");
            }
            catch (Exception ex) { MessageBox.Show("No se pudo modificar el equipo: " + ex.Message); }
        }
    }
}
