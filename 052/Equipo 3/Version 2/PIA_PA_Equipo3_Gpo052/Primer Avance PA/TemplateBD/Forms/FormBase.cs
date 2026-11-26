namespace TemplateCSharp.Forms;

/// <summary>
/// Base común para las pantallas. Centraliza el escalado por DPI (evita que la
/// información se vea "pegada" o cortada en pantallas al 125 % / 150 %) y los
/// helpers para crear etiquetas, campos, botones y tablas con espaciado uniforme.
/// </summary>
public class FormBase : Form
{
    protected const int Margen = 24;
    protected const int AnchoEtiqueta = 130;
    protected const int AnchoCampo = 220;
    protected const int AnchoBoton = 110;
    protected const int AltoBoton = 34;
    private const int PasoVertical = 36;

    /// <param name="fijo">true = diálogo de tamaño fijo; false = ventana redimensionable.</param>
    protected FormBase(string titulo, int ancho, int alto, bool fijo = true)
    {
        // El layout queda suspendido hasta que la pantalla llame ResumeLayout(false)
        // al final de su InitializeComponent; ahí se aplica el escalado por DPI.
        SuspendLayout();
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;

        Text = titulo;
        ClientSize = new Size(ancho, alto);
        StartPosition = FormStartPosition.CenterParent;

        if (fijo)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
        }
    }

    /// <summary>Coordenada Y de la fila n de campos (0 = primera).</summary>
    protected static int PosY(int fila) => 20 + fila * PasoVertical;

    // ---------- Controles sueltos ----------

    protected Label Etiqueta(string texto, int x, int y, int ancho)
    {
        Label l = new Label { Text = texto, Location = new Point(x, y + 3), Width = ancho, AutoSize = false };
        Controls.Add(l);
        return l;
    }

    protected TextBox Texto(int x, int y, int ancho)
    {
        TextBox t = new TextBox { Location = new Point(x, y), Width = ancho };
        Controls.Add(t);
        return t;
    }

    protected ComboBox Combo(int x, int y, int ancho)
    {
        ComboBox c = new ComboBox { Location = new Point(x, y), Width = ancho, DropDownStyle = ComboBoxStyle.DropDownList };
        Controls.Add(c);
        return c;
    }

    protected DateTimePicker Fecha(int x, int y, int ancho)
    {
        DateTimePicker d = new DateTimePicker { Location = new Point(x, y), Width = ancho, Format = DateTimePickerFormat.Short };
        Controls.Add(d);
        return d;
    }

    // ---------- Etiqueta + control ----------

    protected TextBox CampoTexto(string etiqueta, int x, int y, int ancho = AnchoCampo)
    {
        Etiqueta(etiqueta, x, y, AnchoEtiqueta);
        return Texto(x + AnchoEtiqueta + 5, y, ancho);
    }

    protected ComboBox CampoCombo(string etiqueta, int x, int y, int ancho = AnchoCampo)
    {
        Etiqueta(etiqueta, x, y, AnchoEtiqueta);
        return Combo(x + AnchoEtiqueta + 5, y, ancho);
    }

    protected DateTimePicker CampoFecha(string etiqueta, int x, int y, int ancho = AnchoCampo)
    {
        Etiqueta(etiqueta, x, y, AnchoEtiqueta);
        return Fecha(x + AnchoEtiqueta + 5, y, ancho);
    }

    protected NumericUpDown CampoNumero(string etiqueta, int x, int y, int decimales = 0, decimal maximo = 100000, int ancho = AnchoCampo)
    {
        Etiqueta(etiqueta, x, y, AnchoEtiqueta);
        NumericUpDown n = new NumericUpDown
        {
            Location = new Point(x + AnchoEtiqueta + 5, y),
            Width = ancho,
            DecimalPlaces = decimales,
            Maximum = maximo
        };
        Controls.Add(n);
        return n;
    }

    // ---------- Botones ----------

    protected Button Boton(string texto, int x, int y, int ancho = AnchoBoton)
    {
        Button b = new Button { Text = texto, Location = new Point(x, y), Size = new Size(ancho, AltoBoton) };
        Controls.Add(b);
        return b;
    }

    /// <summary>Crea una hilera de botones alineados a la izquierda con separación uniforme.</summary>
    protected Button[] Botones(int y, params string[] textos)
    {
        Button[] lista = new Button[textos.Length];
        for (int i = 0; i < textos.Length; i++)
            lista[i] = Boton(textos[i], Margen + i * (AnchoBoton + 12), y);
        return lista;
    }

    // ---------- Recuadro de solo lectura ----------

    protected Label Recuadro(string texto, int x, int y, int ancho, int alto)
    {
        Label l = new Label
        {
            Text = texto,
            Location = new Point(x, y),
            Size = new Size(ancho, alto),
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(8),
            AutoSize = false
        };
        Controls.Add(l);
        return l;
    }

    // ---------- Tablas ----------

    /// <summary>Tabla con columnas manuales (pantallas sin datos todavía).</summary>
    protected DataGridView Tabla(int x, int y, int ancho, int alto, params string[] columnas)
    {
        DataGridView t = CrearTabla(x, y, ancho, alto);
        for (int i = 0; i < columnas.Length; i++)
            t.Columns.Add("col" + i, columnas[i]);
        Controls.Add(t);
        return t;
    }

    /// <summary>Tabla enlazada a una lista: cada columna = (encabezado, propiedad del modelo).</summary>
    protected DataGridView TablaEnlazada(int x, int y, int ancho, int alto, params (string Encabezado, string Propiedad)[] columnas)
    {
        DataGridView t = CrearTabla(x, y, ancho, alto);
        t.AutoGenerateColumns = false;
        foreach (var (encabezado, propiedad) in columnas)
        {
            t.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "col" + propiedad,
                HeaderText = encabezado,
                DataPropertyName = propiedad
            });
        }
        Controls.Add(t);
        return t;
    }

    private static DataGridView CrearTabla(int x, int y, int ancho, int alto)
    {
        return new DataGridView
        {
            Location = new Point(x, y),
            Size = new Size(ancho, alto),
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            ReadOnly = true,
            MultiSelect = false,
            RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = SystemColors.Window
        };
    }
}
