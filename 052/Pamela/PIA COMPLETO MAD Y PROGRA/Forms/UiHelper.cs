using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

/// <summary>Tipo de dato de una columna de un grid (define cómo se muestra).</summary>
public enum TipoColumna
{
    Texto,
    Moneda,
    Fecha,
    FechaHora,
    Entero,
    Porcentaje
}

/// <summary>Opción de un ComboBox llenado desde la base de datos.</summary>
public class OpcionCombo
{
    public int Id { get; set; }

    public string Texto { get; set; } = string.Empty;

    public DataRow? Fila { get; set; }

    public override string ToString()
    {
        return Texto;
    }
}

/// <summary>Funciones comunes de las pantallas: grids, combos y mensajes.</summary>
public static class UiHelper
{
    // ---------------------------------------------------------------
    // GRIDS: columnas con letreros propios (no los nombres de los campos)
    // y sin mostrar los identificadores internos.
    // ---------------------------------------------------------------
    public static void ConfigurarGrid(
        DataGridView grid,
        bool ajustarAlAncho,
        params (string Campo, string Titulo, TipoColumna Tipo)[] columnas)
    {
        grid.AutoGenerateColumns = false;
        grid.Columns.Clear();
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.BackgroundColor = SystemColors.Window;
        grid.AutoSizeColumnsMode = ajustarAlAncho
            ? DataGridViewAutoSizeColumnsMode.Fill
            : DataGridViewAutoSizeColumnsMode.AllCells;

        foreach ((string Campo, string Titulo, TipoColumna Tipo) definicion in columnas)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.Name = definicion.Campo;
            columna.DataPropertyName = definicion.Campo;
            columna.HeaderText = definicion.Titulo;
            columna.ReadOnly = true;
            columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            columna.Tag = definicion.Tipo;

            if (definicion.Tipo == TipoColumna.Moneda
                || definicion.Tipo == TipoColumna.Entero
                || definicion.Tipo == TipoColumna.Porcentaje)
            {
                columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            grid.Columns.Add(columna);
        }

        grid.CellFormatting -= Grid_CellFormatting;
        grid.CellFormatting += Grid_CellFormatting;
    }

    private static void Grid_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        DataGridView? grid = sender as DataGridView;

        if (grid == null || e.ColumnIndex < 0 || e.Value == null || e.Value == DBNull.Value)
        {
            return;
        }

        object? etiqueta = grid.Columns[e.ColumnIndex].Tag;

        if (etiqueta == null || !(etiqueta is TipoColumna))
        {
            return;
        }

        TipoColumna tipo = (TipoColumna)etiqueta;

        try
        {
            switch (tipo)
            {
                case TipoColumna.Moneda:
                    e.Value = Formato.Moneda(Convert.ToDecimal(e.Value));
                    e.FormattingApplied = true;
                    break;

                case TipoColumna.Fecha:
                    e.Value = Formato.Fecha(Convert.ToDateTime(e.Value));
                    e.FormattingApplied = true;
                    break;

                case TipoColumna.FechaHora:
                    e.Value = Formato.FechaHora(Convert.ToDateTime(e.Value));
                    e.FormattingApplied = true;
                    break;

                case TipoColumna.Entero:
                    e.Value = Formato.Entero(Convert.ToInt32(e.Value));
                    e.FormattingApplied = true;
                    break;

                case TipoColumna.Porcentaje:
                    e.Value = Convert.ToDecimal(e.Value).ToString("0.00", CultureInfo.InvariantCulture) + " %";
                    e.FormattingApplied = true;
                    break;

                default:
                    // Los folios (GUID) se muestran en mayúsculas
                    if (e.Value is Guid folio)
                    {
                        e.Value = folio.ToString().ToUpperInvariant();
                        e.FormattingApplied = true;
                    }

                    break;
            }
        }
        catch (Exception)
        {
            // Si el valor no se puede convertir se muestra tal como viene
        }
    }

    /// <summary>Devuelve el renglón de datos seleccionado en un grid enlazado a un DataTable.</summary>
    public static DataRow? FilaSeleccionada(DataGridView grid)
    {
        DataGridViewRow? renglon = grid.SelectedRows.Count > 0 ? grid.SelectedRows[0] : grid.CurrentRow;

        if (renglon == null)
        {
            return null;
        }

        DataRowView? vista = renglon.DataBoundItem as DataRowView;

        return vista?.Row;
    }

    // ---------------------------------------------------------------
    // COMBOS
    // ---------------------------------------------------------------
    public static void LlenarCombo(ComboBox combo, List<OpcionCombo> opciones)
    {
        combo.DropDownStyle = ComboBoxStyle.DropDownList;
        combo.BeginUpdate();
        combo.Items.Clear();

        foreach (OpcionCombo opcion in opciones)
        {
            combo.Items.Add(opcion);
        }

        combo.EndUpdate();
        combo.SelectedIndex = opciones.Count > 0 ? 0 : -1;
    }

    /// <summary>Id de la opción elegida; null si es la opción "(Todos)" o no hay selección.</summary>
    public static int? IdSeleccionado(ComboBox combo)
    {
        OpcionCombo? opcion = combo.SelectedItem as OpcionCombo;

        if (opcion == null || opcion.Id <= 0)
        {
            return null;
        }

        return opcion.Id;
    }

    public static void ConfigurarFecha(DateTimePicker selector)
    {
        selector.Format = DateTimePickerFormat.Custom;
        selector.CustomFormat = "dd/MMM/yyyy";
    }

    // ---------------------------------------------------------------
    // CAPTURA: solo dígitos / números decimales
    // ---------------------------------------------------------------
    public static void SoloDigitos(TextBox caja)
    {
        caja.KeyPress += (sender, e) =>
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        };
    }

    public static void SoloDecimales(TextBox caja)
    {
        caja.KeyPress += (sender, e) =>
        {
            bool esPunto = e.KeyChar == '.' && !caja.Text.Contains('.');

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !esPunto)
            {
                e.Handled = true;
            }
        };
    }

    // ---------------------------------------------------------------
    // MENSAJES
    // ---------------------------------------------------------------
    public static void Informar(string mensaje, string titulo = "Operación exitosa")
    {
        MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static void Advertir(string mensaje, string titulo = "Revisa los datos")
    {
        MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static bool Confirmar(string mensaje, string titulo = "Confirmar")
    {
        return MessageBox.Show(
            mensaje,
            titulo,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2) == DialogResult.Yes;
    }

    /// <summary>
    /// Muestra el error de forma entendible para el usuario. Los mensajes de las
    /// reglas de negocio (RAISERROR en los procedimientos almacenados) llegan con
    /// el número 50000 y se muestran tal cual.
    /// </summary>
    public static void MostrarError(Exception ex, string titulo = "No se pudo completar la operación")
    {
        if (ex is ArgumentException || ex is InvalidOperationException)
        {
            // Validaciones de los servicios
            string texto = Regex.Replace(ex.Message, @"\s*\(Parameter '[^']*'\)", string.Empty);
            MessageBox.Show(texto, "Revisa los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (ex is SqlException sql)
        {
            if (sql.Number == 50000)
            {
                string texto = sql.Errors.Count > 0 ? sql.Errors[0].Message : sql.Message;
                MessageBox.Show(texto, "Revisa los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (sql.Number == 2627 || sql.Number == 2601)
            {
                MessageBox.Show(
                    "Ya existe un registro con esos datos (valor duplicado).",
                    titulo,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (sql.Number == 547)
            {
                MessageBox.Show(
                    "La operación no cumple una restricción de la base de datos " +
                    "(dato fuera del dominio permitido o registro relacionado con otros).",
                    titulo,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                "No fue posible completar la operación en la base de datos. " +
                "Verifica la conexión con SQL Server.\n\nDetalle: " + sql.Message,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        MessageBox.Show(
            "Ocurrió un error inesperado.\n\nDetalle: " + ex.Message,
            titulo,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    // ---------------------------------------------------------------
    // MAPAS DE ESTADIOS (la imagen se guarda en la base de datos)
    // ---------------------------------------------------------------

    /// <summary>Convierte los bytes guardados en una imagen; null si no es una imagen válida.</summary>
    public static Image? CrearImagen(byte[]? datos)
    {
        if (datos == null || datos.Length == 0)
        {
            return null;
        }

        try
        {
            // Se copia a un Bitmap para no depender del flujo de memoria
            using MemoryStream memoria = new MemoryStream(datos);
            using Image original = Image.FromStream(memoria);
            return new Bitmap(original);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>Muestra una imagen en un PictureBox liberando la anterior.</summary>
    public static void MostrarEnCuadro(PictureBox cuadro, byte[]? datos)
    {
        Image? anterior = cuadro.Image;
        cuadro.Image = CrearImagen(datos);
        anterior?.Dispose();
    }

    /// <summary>Abre el mapa en una ventana grande.</summary>
    public static void MostrarMapa(byte[]? datos, string nombreEstadio)
    {
        using Image? imagen = CrearImagen(datos);

        if (imagen == null)
        {
            Advertir("El estadio no tiene una imagen de mapa válida.", "Mapa del estadio");
            return;
        }

        using Form ventana = new Form();
        using PictureBox cuadro = new PictureBox();

        ventana.Text = "Mapa del estadio - " + nombreEstadio;
        ventana.StartPosition = FormStartPosition.CenterParent;
        ventana.ClientSize = new Size(820, 600);
        ventana.MinimizeBox = false;
        ventana.ShowInTaskbar = false;

        cuadro.Dock = DockStyle.Fill;
        cuadro.SizeMode = PictureBoxSizeMode.Zoom;
        cuadro.BackColor = Color.White;
        cuadro.Image = imagen;

        ventana.Controls.Add(cuadro);
        ventana.ShowDialog();
    }
}
