using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PIA_PA.Entidades.Estadio;

namespace PIA_PA
{
    public partial class AgregarEstadio : UserControl
    {
        private readonly IEstadioService _service;
        private readonly Dictionary<int, AreaRegistro> _areas = new();
        private int _cantidadAreas;
        private bool _actualizando;

        public AgregarEstadio(IEstadioService service)
        {
            InitializeComponent();
            _service = service ?? throw new ArgumentNullException(nameof(service));
            txtCapacidad.ReadOnly = true;
            cBArea.DropDownStyle = ComboBoxStyle.DropDownList;
            txtArea.TextChanged += (_, _) => CambiarCantidadAreas();
            cBArea.SelectedIndexChanged += (_, _) => MostrarArea();
            btnGuardarArea.Click += (_, _) => GuardarArea();
            btnGuardarEstadio.Click += (_, _) => GuardarEstadio();
            CambiarCantidadAreas();
        }

        private void CambiarCantidadAreas()
        {
            if (_actualizando) return;
            _actualizando = true;
            try
            {
                int anterior = cBArea.SelectedItem is int n ? n : 1;
                _cantidadAreas = int.TryParse(txtArea.Text, out int cantidad) && cantidad > 0 && cantidad <= 100 ? cantidad : 0;
                foreach (int indice in _areas.Keys.Where(i => i > _cantidadAreas).ToList()) _areas.Remove(indice);
                cBArea.Items.Clear();
                for (int i = 1; i <= _cantidadAreas; i++) cBArea.Items.Add(i);
                if (_cantidadAreas > 0) cBArea.SelectedItem = Math.Min(anterior, _cantidadAreas);
                ActualizarCapacidad();
            }
            finally { _actualizando = false; }
            MostrarArea();
        }

        private void MostrarArea()
        {
            if (_actualizando) return;
            if (cBArea.SelectedItem is int indice && _areas.TryGetValue(indice, out AreaRegistro? area))
            {
                txtNombreArea.Text = area.Nombre;
                txtFila.Text = area.Filas.ToString();
                txtColumna.Text = area.Columnas.ToString();
            }
            else
            {
                txtNombreArea.Clear();
                txtFila.Clear();
                txtColumna.Clear();
            }
        }

        private void ActualizarCapacidad()
        {
            try
            {
                int total = 0;
                foreach (var area in _areas.Values) total = checked(total + checked(area.Filas * area.Columnas));
                txtCapacidad.Text = total.ToString();
            }
            catch (OverflowException) { txtCapacidad.Text = "Excede el límite"; }
        }

        private void GuardarArea()
        {
            if (cBArea.SelectedItem is not int indice)
            {
                MessageBox.Show("Indica primero una cantidad de áreas válida (1 a 100)."); return;
            }
            if (string.IsNullOrWhiteSpace(txtNombreArea.Text) || txtNombreArea.Text.Trim().Length > 50 ||
                !int.TryParse(txtFila.Text, out int filas) || filas <= 0 ||
                !int.TryParse(txtColumna.Text, out int columnas) || columnas <= 0)
            {
                MessageBox.Show("Introduce un nombre de área (máximo 50 caracteres) y filas/columnas positivas."); return;
            }
            try { _ = checked(filas * columnas); }
            catch (OverflowException) { MessageBox.Show("El área tiene demasiados asientos."); return; }
            _areas[indice] = new AreaRegistro { Nombre = txtNombreArea.Text.Trim(), Filas = filas, Columnas = columnas };
            ActualizarCapacidad();
            MessageBox.Show($"Área {indice} guardada temporalmente.");
        }

        private void GuardarEstadio()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || txtNombre.Text.Trim().Length > 50 ||
                string.IsNullOrWhiteSpace(txtCiudad.Text) || txtCiudad.Text.Trim().Length > 50)
            {
                MessageBox.Show("Indica el nombre y la ciudad (máximo 50 caracteres cada uno)."); return;
            }
            if (_cantidadAreas == 0 || _areas.Count != _cantidadAreas ||
                Enumerable.Range(1, _cantidadAreas).Any(i => !_areas.ContainsKey(i)))
            {
                MessageBox.Show("Debes guardar temporalmente todas las áreas antes de registrar el estadio."); return;
            }
            try
            {
                var estadio = new Estadio { Estadio_Nombre = txtNombre.Text.Trim(), Estadio_Ciudad = txtCiudad.Text.Trim() };
                _service.InsertarCompleto(estadio, Enumerable.Range(1, _cantidadAreas).Select(i => _areas[i]).ToList());
                MessageBox.Show($"Estadio registrado correctamente. ID: {estadio.Estadio_ID}.");
                _areas.Clear();
                txtNombre.Clear(); txtCiudad.Clear(); txtArea.Clear();
                txtNombreArea.Clear(); txtFila.Clear(); txtColumna.Clear();
                ActualizarCapacidad();
            }
            catch (Exception ex) { MessageBox.Show("No se pudo guardar el estadio: " + ex.Message); }
        }
    }
}
