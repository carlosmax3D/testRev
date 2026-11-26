using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    public partial class EstadiosForm : Form
    {
        readonly EstadioRepositorio estadios = new EstadioRepositorio();
        int? idActual = null; // null = registro nuevo

        // mapa del estadio (se agrega por código debajo de los campos del diseño)
        PictureBox picMapa;
        Button btnMapa, btnQuitarMapa;
        byte[] mapaNuevo = null;       // imagen elegida por el usuario
        bool mapaCambio = false;       // true si eligió o quitó el mapa al editar

        public EstadiosForm()
        {
            InitializeComponent();
            AgregarControlesMapa();
        }

        void AgregarControlesMapa()
        {
            const int extra = 170;
            grpDatos.Height += extra;
            btnGuardar.Top += extra;
            btnLimpiar.Top += extra;
            btnEliminar.Top += extra;

            Label lbl = new Label();
            lbl.AutoSize = true;
            lbl.Text = "Mapa del estadio";
            lbl.Location = new Point(16, 155);
            grpDatos.Controls.Add(lbl);

            btnMapa = new Button();
            btnMapa.Text = "Elegir imagen...";
            btnMapa.SetBounds(150, 150, 110, 28);
            btnMapa.Click += btnMapa_Click;
            grpDatos.Controls.Add(btnMapa);

            btnQuitarMapa = new Button();
            btnQuitarMapa.Text = "Quitar";
            btnQuitarMapa.SetBounds(266, 150, 74, 28);
            btnQuitarMapa.Click += btnQuitarMapa_Click;
            grpDatos.Controls.Add(btnQuitarMapa);

            picMapa = new PictureBox();
            picMapa.SetBounds(16, 186, 324, 130);
            picMapa.SizeMode = PictureBoxSizeMode.Zoom;
            picMapa.BorderStyle = BorderStyle.FixedSingle;
            picMapa.BackColor = Color.WhiteSmoke;
            grpDatos.Controls.Add(picMapa);
        }

        private void EstadiosForm_Load(object sender, EventArgs e)
        {
            try { CargarGrid(); }
            catch (Exception ex) { Tema.ErrorConexion(ex); }
        }

        void CargarGrid()
        {
            dgvDatos.DataSource = estadios.Listar();
            Tema.FormatoGrid(dgvDatos);
        }

        void MostrarMapa(byte[] bytes)
        {
            if (picMapa.Image != null) { picMapa.Image.Dispose(); picMapa.Image = null; }
            if (bytes == null) return;
            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                using (Image img = Image.FromStream(ms))
                {
                    picMapa.Image = new Bitmap(img);
                }
            }
            catch { picMapa.Image = null; }
        }

        private void btnMapa_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Mapa del estadio";
                dlg.Filter = "Imágenes (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    FileInfo fi = new FileInfo(dlg.FileName);
                    if (fi.Length > 3 * 1024 * 1024) { Tema.Aviso("La imagen pesa más de 3 MB. Elige una más pequeña.", "Estadios"); return; }
                    byte[] bytes = File.ReadAllBytes(dlg.FileName);
                    MostrarMapa(bytes);
                    if (picMapa.Image == null) { Tema.Aviso("El archivo no es una imagen válida.", "Estadios"); return; }
                    mapaNuevo = bytes;
                    mapaCambio = true;
                }
                catch (Exception ex) { Tema.Manejar(ex, "Estadios"); }
            }
        }

        private void btnQuitarMapa_Click(object sender, EventArgs e)
        {
            mapaNuevo = null;
            mapaCambio = true;
            MostrarMapa(null);
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow fila = dgvDatos.Rows[e.RowIndex];
            idActual = Convert.ToInt32(fila.Cells["ID"].Value);
            txtNombre.Text = Convert.ToString(fila.Cells["Nombre"].Value);
            txtCiudad.Text = Convert.ToString(fila.Cells["Ciudad"].Value);
            nudCapacidad.Value = Convert.ToDecimal(fila.Cells["Capacidad"].Value);
            mapaNuevo = null;
            mapaCambio = false;
            try { MostrarMapa(estadios.ObtenerMapa(idActual.Value)); }
            catch (Exception ex) { Tema.Manejar(ex, "Estadios"); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string ciudad = txtCiudad.Text.Trim();
            if (nombre == "") { Tema.Info("Escribe: Nombre", "Falta un dato"); txtNombre.Focus(); return; }
            if (ciudad == "") { Tema.Info("Escribe: Ciudad", "Falta un dato"); txtCiudad.Focus(); return; }
            if (nudCapacidad.Value <= 0) { Tema.Info("La capacidad total debe ser mayor a 0.", "Falta un dato"); nudCapacidad.Focus(); return; }
            try
            {
                bool nuevo = idActual == null;
                if (nuevo) estadios.Insertar(nombre, ciudad, (int)nudCapacidad.Value, mapaNuevo);
                else estadios.Actualizar(idActual.Value, nombre, ciudad, (int)nudCapacidad.Value, mapaCambio, mapaNuevo);
                Limpiar();
                CargarGrid();
                Tema.Info(nuevo ? "Estadio registrado correctamente." : "Estadio actualizado correctamente.", "Estadios");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Estadios"); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idActual == null) { Tema.Info("Primero elige un estadio de la tabla.", "Estadios"); return; }
            if (!Tema.ConfirmarEliminar("Estadios")) return;
            try
            {
                estadios.Eliminar(idActual.Value);
                Limpiar();
                CargarGrid();
                Tema.Info("Estadio eliminado.", "Estadios");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Estadios"); }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        void Limpiar()
        {
            idActual = null;
            txtNombre.Clear();
            txtCiudad.Clear();
            nudCapacidad.Value = 0;
            mapaNuevo = null;
            mapaCambio = false;
            MostrarMapa(null);
            dgvDatos.ClearSelection();
        }
    }
}
