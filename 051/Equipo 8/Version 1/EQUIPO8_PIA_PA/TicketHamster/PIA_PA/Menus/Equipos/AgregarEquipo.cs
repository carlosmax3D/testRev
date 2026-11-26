using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PIA_PA.Entidades.Equipos;
using PIA_PA.Entidades.Deporte;

namespace PIA_PA
{
    public partial class AgregarEquipo : UserControl
    {
        private readonly IEquiposService _equipos;
        private readonly IDeporteService _deportes;

        public AgregarEquipo(IEquiposService equipos, IDeporteService deportes)
        {
            InitializeComponent();
            _equipos = equipos; _deportes = deportes;
            cBDeporte.DataSource = _deportes.ObtenerTodos();
            cBDeporte.DisplayMember = nameof(Deporte.Deporte_Nombre);
            cBDeporte.ValueMember = nameof(Deporte.Deporte_ID);
            btnGuardar.Click += Guardar_Click;
            label5.Text = "Estatus:";
            txtEstatus.Text = "Activo";
        }

        private void Guardar_Click(object? sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string estatus = txtEstatus.Text.Trim();
            if (nombre.Length < 2 || !Regex.IsMatch(nombre, @"^[\p{L}\p{M}0-9 .'-]+$"))
            { MessageBox.Show("Escribe un nombre de equipo válido."); txtNombre.Focus(); return; }
            if (string.IsNullOrWhiteSpace(estatus)) { MessageBox.Show("Especifica el estatus del equipo."); return; }
            if (cBDeporte.SelectedValue == null) { MessageBox.Show("Selecciona un deporte."); return; }
            try
            {
                var lista = _equipos.ObtenerTodos();
                if (lista.Any(x => string.Equals(x.Equipo_Nombre, nombre, StringComparison.OrdinalIgnoreCase)))
                { MessageBox.Show("Ya existe un equipo con ese nombre."); return; }
                int id = lista.Count == 0 ? 1 : lista.Max(x => x.Equipo_ID) + 1;
                _equipos.Insertar(new Equipos { Equipo_ID = id, Equipo_Nombre = nombre, Equipo_Estatus = estatus, Deporte_ID = Convert.ToInt32(cBDeporte.SelectedValue) });
                MessageBox.Show("Equipo agregado correctamente.");
                txtNombre.Clear(); txtEstatus.Text = "Activo";
            }
            catch (Exception ex) { MessageBox.Show("No se pudo agregar el equipo: " + ex.Message); }
        }
    }
}
