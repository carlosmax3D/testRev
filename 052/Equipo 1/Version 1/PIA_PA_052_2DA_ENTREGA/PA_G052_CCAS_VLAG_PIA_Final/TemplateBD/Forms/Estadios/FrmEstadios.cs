using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;
using System.IO;
using System.Drawing;


namespace TemplateCSharp.Forms.Estadios
{
    public partial class FrmEstadios : Form
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly IEstadioService _estadioService;

        private int? idEstadioEdicion = null; //null = es nuevo, con valor = editando
        private string _nombreArchivoMapa = "sin_mapa.png";
        private int? idEstadioSeleccionado = null;

        public FrmEstadios(IServiceProvider serviceProvider, IEstadioService estadioService)

        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _estadioService = estadioService;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCiudad_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtCapacidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // validacioness
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCiudad.Text) ||
                string.IsNullOrWhiteSpace(TxtCapacidad.Text))
            {
                MessageBox.Show("Complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(TxtCapacidad.Text, out int capacidad) || capacidad <= 0)
            {
                MessageBox.Show("La capacidad debe ser un número mayor a cero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_nombreArchivoMapa == "sin_mapa.png" || picMapa.Image == null)
            {
                MessageBox.Show("Debe seleccionar una imagen del mapa del estadio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // 2 se creea  el objeto Estadio
            var estadio = new Estadio
            {
                IdEstadio = idEstadioEdicion ?? 0,
                Nombre = txtNombre.Text.Trim(),
                Ciudad = txtCiudad.Text.Trim(),
                CapacidadTotal = capacidad,
                MapaEstadio = _nombreArchivoMapa,
                UsuarioRegistro = Sesion.IdUsuario
            };

            try
            {
                //servicio popping up
                if (idEstadioEdicion == null)
                {
                    _estadioService.CrearEstadio(estadio);
                    MessageBox.Show("Estadio registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _estadioService.ActualizarEstadio(estadio);
                    MessageBox.Show("Estadio actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Limpiamos y recargamos
                btnLimpiar_Click(null, null);
                CargarEstadios();
            }
            catch (UnauthorizedAccessException ex)
            {
                //Atrapamos el error si un Usuario Operativo intenta registrar, igual no deberia pero pss no esta de mas
                MessageBox.Show(ex.Message, "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtCiudad.Clear();
            TxtCapacidad.Clear();

            // Reiniciamos el modo edición
            idEstadioEdicion = null;
            btnRegistrar.Text = "Registrar";
            txtNombre.Focus();
            idEstadioSeleccionado = null;

            picMapa.Image = null!;
            label6.Visible = true;
            _nombreArchivoMapa = "sin_mapa.png";

        }

        private void FrmEstadios_Load(object sender, EventArgs e)
        {
            CargarEstadios();
        }
        private void CargarEstadios()
        {
            try
            {
                var lista = _estadioService.ObtenerEstadios();

                dgvEstadios.AutoGenerateColumns = false; // Usamos las columnas de tu diseño
                dgvEstadios.DataSource = lista;

                // Mapeamos las columnas del diseñador a las propiedades del Modelo
                dgvEstadios.Columns["Column1"].DataPropertyName = "Nombre";
                dgvEstadios.Columns["Column2"].DataPropertyName = "Ciudad";
                dgvEstadios.Columns["Column3"].DataPropertyName = "CapacidadTotal";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estadios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvEstadios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void dgvEstadios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var estadioSeleccionado = (Estadio)dgvEstadios.Rows[e.RowIndex].DataBoundItem;
                if (estadioSeleccionado != null)
                {
                    idEstadioSeleccionado = estadioSeleccionado.IdEstadio;
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idEstadioSeleccionado == null || dgvEstadios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un estadio de la tabla para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var estadioSeleccionado = (Estadio)dgvEstadios.CurrentRow.DataBoundItem;
            if (estadioSeleccionado == null) return;

            idEstadioEdicion = estadioSeleccionado.IdEstadio;
            txtNombre.Text = estadioSeleccionado.Nombre;
            txtCiudad.Text = estadioSeleccionado.Ciudad;
            TxtCapacidad.Text = estadioSeleccionado.CapacidadTotal.ToString();
            btnRegistrar.Text = "Actualizar";

            _nombreArchivoMapa = estadioSeleccionado.MapaEstadio;
            string rutaImagen = Path.Combine(Application.StartupPath, "Mapas", _nombreArchivoMapa);

            if (File.Exists(rutaImagen))
            {
                picMapa.Image = Image.FromFile(rutaImagen);
                picMapa.SizeMode = PictureBoxSizeMode.Zoom;
                label6.Visible = false;
            }
            else
            {
                picMapa.Image = null!;
                label6.Visible = true;
            }

        }

        private void btnSeleccionarMapa_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";
            dialogo.Title = "Seleccionar mapa del estadio";

            if (dialogo.ShowDialog() != DialogResult.OK) return;

            try
            {
                //Crear carpeta "Mapas" junto al ejecutable si no existe
                string carpetaMapas = Path.Combine(Application.StartupPath, "Mapas");
                if (!Directory.Exists(carpetaMapas))
                    Directory.CreateDirectory(carpetaMapas);

                //Copiar la imagen a la carpeta con un nombre único
                string nombreOriginal = Path.GetFileName(dialogo.FileName);
                string nombreDestino = $"{DateTime.Now:yyyyMMddHHmmss}_{nombreOriginal}";
                string rutaDestino = Path.Combine(carpetaMapas, nombreDestino);
                File.Copy(dialogo.FileName, rutaDestino, true);

                //Cargar la imagen al PictureBox
                picMapa.Image = Image.FromFile(rutaDestino);
                picMapa.SizeMode = PictureBoxSizeMode.Zoom;

                //Guardar el nombre del archivo (solo el nombre)
                _nombreArchivoMapa = nombreDestino;

                //Ocultar el label
                label6.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
