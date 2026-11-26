using Microsoft.Data.SqlClient;

namespace VentaBoletosApp.UI;

/// <summary>Ayudantes de interfaz: estilo comun, controles y manejo de errores.</summary>
public static class Ui
{
    public static readonly Color Primario = Color.FromArgb(31, 58, 95);
    public static readonly Color Fondo = Color.FromArgb(244, 246, 250);
    public static readonly Color Acento = Color.FromArgb(46, 125, 50);
    public static readonly Color Peligro = Color.FromArgb(192, 57, 43);
    public static readonly Color Neutro = Color.FromArgb(96, 110, 128);

    public static readonly Font Fuente = new("Segoe UI", 9f);
    public static readonly Font FuenteTitulo = new("Segoe UI", 15f, FontStyle.Bold);

    public static void Preparar(Form f, string titulo, int ancho, int alto)
    {
        f.Text = titulo;
        f.ClientSize = new Size(ancho, alto);
        f.StartPosition = FormStartPosition.CenterParent;
        f.Font = Fuente;
        f.BackColor = Fondo;
    }

    public static Label Titulo(string texto) => new()
    {
        Text = texto,
        Font = FuenteTitulo,
        ForeColor = Primario,
        AutoSize = true,
        Margin = new Padding(0, 0, 0, 6)
    };

    public static Label Etiqueta(string texto, bool negritas = false) => new()
    {
        Text = texto,
        AutoSize = true,
        Anchor = AnchorStyles.Left,
        Font = negritas ? new Font(Fuente, FontStyle.Bold) : Fuente,
        Margin = new Padding(3, 8, 8, 3)
    };

    public static TextBox Texto(int ancho = 240, int maximo = 0, bool contrasena = false)
    {
        TextBox t = new() { Width = ancho, UseSystemPasswordChar = contrasena };

        if (maximo > 0)
        {
            t.MaxLength = maximo;
        }

        return t;
    }

    public static ComboBox Combo(int ancho = 240) => new()
    {
        Width = ancho,
        DropDownStyle = ComboBoxStyle.DropDownList
    };

    public static DateTimePicker Fecha(bool conHora = false, int ancho = 170) => new()
    {
        Width = ancho,
        Format = DateTimePickerFormat.Custom,
        CustomFormat = conHora ? "dd-MMM-yyyy HH:mm" : "dd-MMM-yyyy"
    };

    public static Button Boton(string texto, EventHandler? click, Color? color = null, int ancho = 120)
    {
        Button b = new()
        {
            Text = texto,
            Width = ancho,
            Height = 32,
            FlatStyle = FlatStyle.Flat,
            BackColor = color ?? Primario,
            ForeColor = Color.White,
            Cursor = Cursors.Hand,
            Margin = new Padding(3, 6, 6, 3)
        };

        b.FlatAppearance.BorderSize = 0;

        if (click is not null)
        {
            b.Click += click;
        }

        return b;
    }

    public static DataGridView Grid()
    {
        DataGridView g = new()
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            AutoGenerateColumns = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            RowHeadersVisible = false,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            EnableHeadersVisualStyles = false,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
            ColumnHeadersHeight = 32
        };

        g.ColumnHeadersDefaultCellStyle.BackColor = Primario;
        g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        g.ColumnHeadersDefaultCellStyle.Font = new Font(Fuente, FontStyle.Bold);
        g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 242, 247);
        g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 160, 210);
        g.DefaultCellStyle.SelectionForeColor = Color.Black;

        g.CellFormatting += FormatearCelda;

        return g;
    }

    /// <summary>Aplica el estilo comun y los formatos de fecha/dinero a una cuadricula creada en el diseñador.</summary>
    public static void Estilizar(DataGridView g)
    {
        g.EnableHeadersVisualStyles = false;
        g.ColumnHeadersHeight = 32;
        g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        g.ColumnHeadersDefaultCellStyle.BackColor = Primario;
        g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        g.ColumnHeadersDefaultCellStyle.Font = new Font(Fuente, FontStyle.Bold);
        g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 242, 247);
        g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 160, 210);
        g.DefaultCellStyle.SelectionForeColor = Color.Black;
        g.CellFormatting -= FormatearCelda;
        g.CellFormatting += FormatearCelda;
    }

    /// <summary>Agrega una columna ligada a una propiedad. tag: "hora" (fecha con hora) o "pct" (porcentaje).</summary>
    public static void Col(DataGridView g, string propiedad, string titulo, float peso = 100, string? tag = null)
    {
        g.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = propiedad,
            Name = "col" + propiedad + g.Columns.Count,
            HeaderText = titulo,
            FillWeight = peso,
            Tag = tag,
            SortMode = DataGridViewColumnSortMode.NotSortable
        });
    }

    private static void FormatearCelda(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (sender is not DataGridView g || e.Value is null || e.ColumnIndex < 0)
        {
            return;
        }

        string? tag = g.Columns[e.ColumnIndex].Tag as string;

        switch (e.Value)
        {
            case DateTime fecha:
                e.Value = tag == "hora" ? Services.Formato.FechaHora(fecha) : Services.Formato.Fecha(fecha);
                e.FormattingApplied = true;
                break;

            case decimal valor:
                e.Value = tag == "pct" ? Services.Formato.Porcentaje(valor) : Services.Formato.Moneda(valor);
                e.FormattingApplied = true;
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                break;

            case bool activo:
                e.Value = activo ? "Sí" : "No";
                e.FormattingApplied = true;
                break;
        }
    }

    // ---------- Mensajes ----------

    public static string MensajeError(Exception ex)
    {
        if (ex is SqlException sql)
        {
            SqlError? error = sql.Errors.Cast<SqlError>()
                .FirstOrDefault(x => x.Class >= 11 && x.Number != 3609);

            return sql.Number switch
            {
                2627 or 2601 => "Ya existe un registro con esos datos.",
                547 => "La operación no es válida porque viola una regla de integridad de los datos.",
                -1 or 2 or 53 or 4060 or 18456 =>
                    "No se pudo conectar con la base de datos. Verifique que SQL Server esté en ejecución " +
                    "y que la cadena de conexión de appsettings.json sea correcta.",
                _ => error?.Message ?? sql.Message
            };
        }

        return ex.Message;
    }

    public static void Error(IWin32Window? dueno, Exception ex) =>
        MessageBox.Show(dueno, MensajeError(ex), "No se pudo completar la operación",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);

    public static void Aviso(IWin32Window? dueno, string mensaje, string titulo = "Información") =>
        MessageBox.Show(dueno, mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public static bool Confirmar(IWin32Window? dueno, string mensaje, string titulo = "Confirmar") =>
        MessageBox.Show(dueno, mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

    /// <summary>Ejecuta una accion mostrando cualquier error al usuario. Regresa true si no hubo error.</summary>
    public static bool Ejecutar(IWin32Window? dueno, Action accion)
    {
        try
        {
            accion();
            return true;
        }
        catch (Exception ex)
        {
            Error(dueno, ex);
            return false;
        }
    }

    public static string? PedirTexto(IWin32Window? dueno, string titulo, string pregunta, bool contrasena = false)
    {
        using Form f = new()
        {
            Text = titulo,
            ClientSize = new Size(360, 130),
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            Font = Fuente
        };

        Label l = new() { Text = pregunta, Left = 15, Top = 15, Width = 330 };
        TextBox t = new() { Left = 15, Top = 42, Width = 330, UseSystemPasswordChar = contrasena };
        Button ok = Boton("Aceptar", null, Primario, 100);
        ok.Left = 140;
        ok.Top = 82;
        ok.DialogResult = DialogResult.OK;
        Button cancelar = Boton("Cancelar", null, Neutro, 100);
        cancelar.Left = 245;
        cancelar.Top = 82;
        cancelar.DialogResult = DialogResult.Cancel;

        f.Controls.AddRange([l, t, ok, cancelar]);
        f.AcceptButton = ok;
        f.CancelButton = cancelar;

        return f.ShowDialog(dueno) == DialogResult.OK ? t.Text : null;
    }

    public static void LlenarCombo<T>(ComboBox combo, IEnumerable<T> datos, string mostrar, string valor, string? placeholder = null)
    {
        List<T> lista = datos.ToList();

        combo.DataSource = null;
        combo.DisplayMember = mostrar;
        combo.ValueMember = valor;
        combo.DataSource = lista;

        if (lista.Count > 0 || placeholder is null)
        {
            combo.SelectedIndex = lista.Count > 0 ? 0 : -1;
        }
    }
}

/// <summary>Panel de dos columnas (etiqueta / control) para armar formularios.</summary>
public class PanelCampos : TableLayoutPanel
{
    private int _fila;

    public PanelCampos()
    {
        ColumnCount = 2;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Padding = new Padding(4);
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        BackColor = Color.Transparent;
    }

    public T Agregar<T>(string etiqueta, T control) where T : Control
    {
        RowStyles.Add(new RowStyle(SizeType.AutoSize));
        Controls.Add(Ui.Etiqueta(etiqueta), 0, _fila);
        control.Margin = new Padding(3, 4, 3, 4);
        Controls.Add(control, 1, _fila);
        _fila++;
        RowCount = _fila;

        return control;
    }
}
