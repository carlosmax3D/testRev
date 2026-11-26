using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    // Ayudas para armar pantallas con el mismo estilo (azul marino, botones planos)
    static class Ui
    {
        public static readonly Color Navy = Color.FromArgb(24, 54, 98);
        public static readonly Color Azul = Color.FromArgb(0, 120, 212);
        public static readonly Color Gris = Color.FromArgb(108, 117, 125);
        public static readonly Color Rojo = Color.FromArgb(192, 57, 43);
        public static readonly Color Verde = Color.FromArgb(39, 150, 90);
        public static readonly Color Naranja = Color.FromArgb(214, 120, 30);

        public static void PrepararForm(Form f, string titulo, string encabezado, int ancho, int alto)
        {
            f.Text = titulo;
            f.Font = new Font("Segoe UI", 9F);
            f.ClientSize = new Size(ancho, alto);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.BackColor = Color.White;
            Panel p = new Panel();
            p.BackColor = Navy;
            p.Dock = DockStyle.Top;
            p.Height = 60;
            Label l = new Label();
            l.AutoSize = true;
            l.Text = encabezado;
            l.ForeColor = Color.White;
            l.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            l.Location = new Point(16, 12);
            p.Controls.Add(l);
            f.Controls.Add(p);
        }

        public static GroupBox Grupo(Control padre, string texto, int x, int y, int w, int h)
        {
            GroupBox g = new GroupBox();
            g.Text = texto;
            g.SetBounds(x, y, w, h);
            padre.Controls.Add(g);
            return g;
        }

        public static Label Etiqueta(Control padre, string texto, int x, int y)
        {
            Label l = new Label();
            l.AutoSize = true;
            l.Text = texto;
            l.Location = new Point(x, y);
            padre.Controls.Add(l);
            return l;
        }

        public static TextBox Texto(Control padre, int x, int y, int w, int max)
        {
            TextBox t = new TextBox();
            t.SetBounds(x, y, w, 23);
            t.MaxLength = max;
            padre.Controls.Add(t);
            return t;
        }

        public static ComboBox Combo(Control padre, int x, int y, int w)
        {
            ComboBox c = new ComboBox();
            c.SetBounds(x, y, w, 23);
            c.DropDownStyle = ComboBoxStyle.DropDownList;
            padre.Controls.Add(c);
            return c;
        }

        public static DateTimePicker Fecha(Control padre, int x, int y, int w)
        {
            DateTimePicker d = new DateTimePicker();
            d.SetBounds(x, y, w, 23);
            d.Format = DateTimePickerFormat.Short;
            padre.Controls.Add(d);
            return d;
        }

        public static Button Boton(Control padre, string texto, int x, int y, int w, int h, Color color, EventHandler accion)
        {
            Button b = new Button();
            b.Text = texto;
            b.SetBounds(x, y, w, h);
            b.BackColor = color;
            b.ForeColor = Color.White;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.Cursor = Cursors.Hand;
            b.UseVisualStyleBackColor = false;
            if (accion != null) b.Click += accion;
            padre.Controls.Add(b);
            return b;
        }

        public static DataGridView Grid(Control padre, int x, int y, int w, int h)
        {
            DataGridView g = new DataGridView();
            g.SetBounds(x, y, w, h);
            g.ReadOnly = true;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.AllowUserToResizeRows = false;
            g.MultiSelect = false;
            g.RowHeadersVisible = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.BackgroundColor = Color.White;
            g.BorderStyle = BorderStyle.FixedSingle;
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersDefaultCellStyle.BackColor = Navy;
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = Navy;
            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(244, 247, 251);
            padre.Controls.Add(g);
            return g;
        }

        public static int Id(DataGridView g, int fila)
        {
            return Convert.ToInt32(g.Rows[fila].Cells["ID"].Value);
        }
    }
}
