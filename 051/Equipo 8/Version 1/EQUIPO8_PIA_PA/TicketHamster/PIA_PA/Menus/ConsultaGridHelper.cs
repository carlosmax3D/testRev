using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PIA_PA.Menus
{
    internal static class ConsultaGridHelper
    {
        // Rellena el ancho disponible solo cuando todas las columnas caben
        // sin reducir sus anchos minimos. Se actualiza al redimensionar el grid.
        public static void AjustarColumnas(DataGridView grid)
        {
            grid.Resize -= AlRedimensionarGrid;
            grid.Resize += AlRedimensionarGrid;
            AplicarAnchoAdaptable(grid);
        }

        private static void AlRedimensionarGrid(object? sender, EventArgs e)
        {
            if (sender is DataGridView grid)
                AplicarAnchoAdaptable(grid);
        }

        private static void AplicarAnchoAdaptable(DataGridView grid)
        {
            if (grid.IsDisposed || grid.Columns.Count == 0) return;

            int disponible = grid.ClientSize.Width
                - (grid.RowHeadersVisible ? grid.RowHeadersWidth : 0)
                - SystemInformation.VerticalScrollBarWidth - 2;

            int necesario = 0;
            foreach (DataGridViewColumn columna in grid.Columns)
                if (columna.Visible) necesario += columna.MinimumWidth;

            if (necesario <= disponible)
            {
                foreach (DataGridViewColumn columna in grid.Columns)
                    columna.FillWeight = columna.MinimumWidth;
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                foreach (DataGridViewColumn columna in grid.Columns)
                    if (columna.Visible) columna.Width = columna.MinimumWidth;
            }

            grid.ScrollBars = ScrollBars.Both;
        }

        public static void Cargar<T>(
            DataGridView grid,
            ComboBox combo,
            IEnumerable<T> datos,
            IReadOnlyDictionary<string, string> encabezados)
        {
            grid.DataSource = null;
            grid.Columns.Clear();
            grid.AutoGenerateColumns = true;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grid.ScrollBars = ScrollBars.Both;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.DataSource = datos.ToList();

            foreach (DataGridViewColumn columna in grid.Columns)
            {
                columna.Width = 150;
                columna.MinimumWidth = 150;
                if (encabezados.TryGetValue(columna.DataPropertyName, out string? texto))
                    columna.HeaderText = texto;
            }

            AjustarColumnas(grid);

            combo.Items.Clear();
            combo.Items.AddRange(encabezados.Values.Cast<object>().ToArray());
            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }

        public static void Buscar(DataGridView grid, ComboBox combo, string texto)
        {
            if (combo.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una columna para buscar.");
                return;
            }

            string encabezado = combo.SelectedItem.ToString() ?? string.Empty;
            DataGridViewColumn? columna = grid.Columns
                .Cast<DataGridViewColumn>()
                .FirstOrDefault(c => string.Equals(c.HeaderText, encabezado, StringComparison.OrdinalIgnoreCase));

            if (columna == null)
            {
                MessageBox.Show("No se encontró la columna seleccionada.");
                return;
            }

            texto = texto.Trim();
            foreach (DataGridViewRow fila in grid.Rows)
            {
                if (fila.IsNewRow) continue;

                string valor = Convert.ToString(fila.Cells[columna.Index].Value) ?? string.Empty;
                fila.Visible = texto.Length == 0 ||
                    valor.Contains(texto, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
