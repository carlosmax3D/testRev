using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    // Mensajes y utilidades que comparten todas las pantallas
    static class Tema
    {
        public static void Info(string texto, string titulo)
        {
            MessageBox.Show(texto, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Aviso(string texto, string titulo)
        {
            MessageBox.Show(texto, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string texto, string titulo = "Error")
        {
            MessageBox.Show(texto, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Pregunta(string texto, string titulo)
        {
            return MessageBox.Show(texto, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static void ErrorConexion(Exception ex)
        {
            Error("No se pudo conectar a la base de datos:\n" + ex.Message);
        }

        // Muestra una lista de errores de validación (si hay) y regresa true si hubo
        public static bool HayErrores(List<string> errores, string titulo)
        {
            if (errores == null || errores.Count == 0) return false;
            Aviso(string.Join("\n", errores), titulo);
            return true;
        }

        // Muestra un mensaje entendible según el error (de negocio, de SQL Server o de conexión)
        public static void Manejar(Exception ex, string titulo)
        {
            SqlException sql = ex as SqlException;
            if (sql != null)
            {
                if (sql.Number >= 50000) Aviso(sql.Message, titulo);                       // regla de negocio (THROW)
                else if (sql.Number == 2627 || sql.Number == 2601) Aviso("Ya existe un registro con ese dato (RFC, correo, nombre, asiento, etc. repetido).", titulo);
                else if (sql.Number == 547) Aviso("El dato no cumple las reglas de la base de datos o tiene registros relacionados.", titulo);
                else Error("Error de base de datos: " + sql.Message, titulo);
            }
            else if (ex is InvalidOperationException || ex is System.IO.IOException)
                Error("No se pudo completar la operación:\n" + ex.Message, titulo);
            else
                Error(ex.Message, titulo);
        }

        // Llena un ComboBox con una tabla de dos columnas: (valor, texto)
        public static void LlenarCombo(ComboBox cb, DataTable dt)
        {
            cb.DataSource = null;
            cb.DisplayMember = dt.Columns[1].ColumnName;
            cb.ValueMember = dt.Columns[0].ColumnName;
            cb.DataSource = dt;
            cb.SelectedIndex = -1;
        }

        // Pregunta de confirmación para eliminar
        public static bool ConfirmarEliminar(string titulo)
        {
            return Pregunta("¿Seguro que quieres eliminar este registro?", titulo);
        }

        // Encabezados que llevan acento (en la base los alias van sin acento)
        static readonly Dictionary<string, string> Acentos = new Dictionary<string, string>
        {
            { "Nomina", "Nómina" },
            { "Tel. casa", "Teléfono de casa" },
            { "Tel. celular", "Teléfono celular" },
            { "Metodo de pago", "Método de pago" },
            { "Porcentaje de ocupacion", "Porcentaje de ocupación" },
            { "Asientos disponibles", "Asientos disponibles" }
        };

        // Da formato a un DataGridView que se llenó con un DataTable:
        // oculta los ID, pone letreros apropiados y formato de fechas y dinero
        public static void FormatoGrid(DataGridView g)
        {
            foreach (DataGridViewColumn col in g.Columns)
            {
                if (col.Name == "ID" || col.Name.StartsWith("id_"))
                {
                    col.Visible = false;
                    continue;
                }
                string nuevo;
                if (Acentos.TryGetValue(col.HeaderText, out nuevo)) col.HeaderText = nuevo;
            }
            if (!(g.Tag is string) || (string)g.Tag != "formato")
            {
                g.CellFormatting += FormatoCelda;
                g.Tag = "formato";
            }
            g.ClearSelection();
        }

        static void FormatoCelda(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView g = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Value == null || e.Value == DBNull.Value) return;
            string encabezado = g.Columns[e.ColumnIndex].HeaderText ?? "";
            if (e.Value is DateTime)
            {
                DateTime d = (DateTime)e.Value;
                e.Value = encabezado.ToLower().Contains("hora") ? Formato.FechaHora(d) : Formato.Fecha(d);
                e.FormattingApplied = true;
            }
            else if (e.Value is decimal)
            {
                decimal m = (decimal)e.Value;
                e.Value = encabezado.Contains("Porcentaje") ? Formato.Porcentaje(m) : Formato.Dinero(m);
                e.FormattingApplied = true;
            }
        }
    }
}
