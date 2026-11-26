
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Pantalla_RegistroCatalogos_PIA_PA;

namespace Pantalla_RegistroEstadio_PIA_PA
{
    public partial class pantalla_registroestadio : Form
    {
        // Datos compartidos temporalmente con el catálogo.
        // Permite guardar la información del estadio para utilizarla
        // después en la pantalla de registro del catálogo.
        private RegistroCatalogoTemporal registro;

        // Ruta de la imagen seleccionada.
        // Guarda la ubicación de la copia del mapa en la computadora.
        private string rutaMapa = "";

        // Constructor que recibe el objeto donde se almacenarán
        // temporalmente los datos del estadio.
        public pantalla_registroestadio(RegistroCatalogoTemporal datos)
        {
            InitializeComponent();

            // Guarda la referencia al objeto recibido.
            registro = datos ?? throw new ArgumentNullException(nameof(datos));

            // Avanzar con Enter entre los TextBox.
            txtbox1_nombreEstadio.KeyDown += AvanzarConEnter;
            txtbox2_ciudadEstadio.KeyDown += AvanzarConEnter;
            txtbox3_capacidadEstadio.KeyDown += AvanzarConEnter;

            // Validar los caracteres permitidos en cada campo.
            txtbox1_nombreEstadio.KeyPress +=
                txtbox1_nombreEstadio_KeyPress;

            txtbox2_ciudadEstadio.KeyPress +=
                txtbox2_ciudadEstadio_KeyPress;

            // Convertir automáticamente a mayúsculas.
            txtbox1_nombreEstadio.TextChanged +=
                txtbox1_nombreEstadio_TextChanged;

            txtbox2_ciudadEstadio.TextChanged +=
                txtbox2_ciudadEstadio_TextChanged;

            // Permitir únicamente números en la capacidad.
            txtbox3_capacidadEstadio.KeyPress +=
                txtbox3_capacidadEstadio_KeyPress;

            // Actualizar la capacidad cuando cambie el TextBox.
            txtbox3_capacidadEstadio.TextChanged +=
                txtxbox3_capacidadEstadio_TextChanged;

            // Si ya existen datos guardados, mostrarlos nuevamente.
            CargarDatosAnteriores();
        }

        // Cargar los datos que ya se encuentran en el objeto compartido.
        private void CargarDatosAnteriores()
        {
            txtbox1_nombreEstadio.Text = registro.NombreEstadio ?? "";
            txtbox2_ciudadEstadio.Text = registro.Ciudad ?? "";

            if (registro.Capacidad > 0)
            {
                txtbox3_capacidadEstadio.Text =
                    registro.Capacidad.ToString();
            }

            if (!string.IsNullOrWhiteSpace(registro.RutaMapa) &&
                File.Exists(registro.RutaMapa))
            {
                rutaMapa = registro.RutaMapa;

                try
                {
                    imagen_mapaEstadio.SizeMode =
                        PictureBoxSizeMode.Zoom;

                    imagen_mapaEstadio.Image =
                        CargarImagenSinBloquear(rutaMapa);
                }
                catch
                {
                    rutaMapa = "";
                }
            }
        }

        // Controla el movimiento entre los campos cuando se presiona Enter.
        private void AvanzarConEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (sender == txtbox1_nombreEstadio)
                {
                    txtbox2_ciudadEstadio.Focus();
                }
                else if (sender == txtbox2_ciudadEstadio)
                {
                    txtbox3_capacidadEstadio.Focus();
                }
                else if (sender == txtbox3_capacidadEstadio)
                {
                    boton1_mapaEstadio.Focus();
                }
            }
        }

        // Comprobar si un carácter es válido para un nombre de estadio.
        // Se permiten letras, números, espacios, guiones, puntos,
        // apóstrofes y el símbolo &.
        private bool CaracterNombreEstadioValido(char caracter)
        {
            return char.IsLetter(caracter) ||
                   char.IsDigit(caracter) ||
                   char.IsWhiteSpace(caracter) ||
                   caracter == '-' ||
                   caracter == '\'' ||
                   caracter == '.' ||
                   caracter == '&';
        }

        // Comprobar si un carácter es válido para una ciudad.
        // No se permiten números.
        private bool CaracterCiudadValido(char caracter)
        {
            return char.IsLetter(caracter) ||
                   char.IsWhiteSpace(caracter) ||
                   caracter == '-' ||
                   caracter == '\'';
        }

        // Validar todos los caracteres del nombre del estadio.
        private bool NombreEstadioValido(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return false;

            foreach (char caracter in nombre)
            {
                if (!CaracterNombreEstadioValido(caracter))
                    return false;
            }

            return true;
        }

        // Validar todos los caracteres de la ciudad.
        private bool CiudadValida(string ciudad)
        {
            if (string.IsNullOrWhiteSpace(ciudad))
                return false;

            foreach (char caracter in ciudad)
            {
                if (!CaracterCiudadValido(caracter))
                    return false;
            }

            return true;
        }

        // Permitir letras, números y signos válidos en el nombre.
        private void txtbox1_nombreEstadio_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !CaracterNombreEstadioValido(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Permitir solamente letras y signos válidos en la ciudad.
        private void txtbox2_ciudadEstadio_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !CaracterCiudadValido(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Permitir solamente números en el campo de capacidad.
        private void txtbox3_capacidadEstadio_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Convertir el nombre del estadio automáticamente a mayúsculas.
        private void txtbox1_nombreEstadio_TextChanged(
            object sender, EventArgs e)
        {
            ConvertirMayusculas(txtbox1_nombreEstadio);
        }

        // Convertir la ciudad automáticamente a mayúsculas.
        private void txtbox2_ciudadEstadio_TextChanged(
            object sender, EventArgs e)
        {
            ConvertirMayusculas(txtbox2_ciudadEstadio);
        }

        // Método general para convertir el contenido de un TextBox
        // a mayúsculas sin perder la posición del cursor.
        private void ConvertirMayusculas(TextBox cuadro)
        {
            int posicionCursor = cuadro.SelectionStart;

            string textoMayusculas =
                cuadro.Text.ToUpperInvariant();

            if (cuadro.Text != textoMayusculas)
            {
                cuadro.Text = textoMayusculas;

                cuadro.SelectionStart = Math.Min(
                    posicionCursor,
                    cuadro.Text.Length);
            }
        }

        // Cargar una imagen sin mantener bloqueado el archivo original.
        private Image CargarImagenSinBloquear(string ruta)
        {
            if (!File.Exists(ruta))
            {
                throw new FileNotFoundException(
                    "No se encontró el archivo de imagen.",
                    ruta);
            }

            byte[] bytesImagen = File.ReadAllBytes(ruta);

            if (bytesImagen.Length == 0)
            {
                throw new InvalidDataException(
                    "El archivo de imagen está vacío.");
            }

            using (MemoryStream memoria = new MemoryStream(bytesImagen))
            {
                using (Image imagenTemporal = Image.FromStream(memoria))
                {
                    return new Bitmap(imagenTemporal);
                }
            }
        }

        // Seleccionar el mapa del estadio y copiarlo
        // a una carpeta de la computadora.
        private void boton1_mapaEstadio_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog abrir = new OpenFileDialog())
            {
                abrir.Title = "Seleccionar mapa del estadio";
                abrir.Filter =
                    "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";
                abrir.CheckFileExists = true;
                abrir.Multiselect = false;

                if (abrir.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string documentos = Environment.GetFolderPath(
                            Environment.SpecialFolder.MyDocuments);

                        string carpetaMapas = Path.Combine(
                            documentos,
                            "MapasEstadios");

                        Directory.CreateDirectory(carpetaMapas);

                        string nombreArchivo =
                            Path.GetFileName(abrir.FileName);

                        string destino = Path.Combine(
                            carpetaMapas,
                            nombreArchivo);

                        if (File.Exists(destino))
                        {
                            string nombre =
                                Path.GetFileNameWithoutExtension(nombreArchivo);

                            string extension =
                                Path.GetExtension(nombreArchivo);

                            destino = Path.Combine(
                                carpetaMapas,
                                nombre + "_" +
                                DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") +
                                extension);
                        }

                        using (Image imagenValidacion =
                            CargarImagenSinBloquear(abrir.FileName))
                        {
                        }

                        File.Copy(abrir.FileName, destino, false);

                        rutaMapa = destino;

                        Image nuevaImagen =
                            CargarImagenSinBloquear(rutaMapa);

                        Image imagenAnterior = imagen_mapaEstadio.Image;

                        imagen_mapaEstadio.Image = nuevaImagen;

                        imagenAnterior?.Dispose();

                        imagen_mapaEstadio.SizeMode =
                            PictureBoxSizeMode.Zoom;

                        // Guardar también la ruta en el objeto compartido.
                        registro.RutaMapa = rutaMapa;

                        MessageBox.Show(
                            "El mapa se guardó correctamente en tu computadora.\n\n" +
                            "Ubicación:\n" + rutaMapa,
                            "Mapa guardado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (OutOfMemoryException)
                    {
                        rutaMapa = "";
                        registro.RutaMapa = "";

                        MessageBox.Show(
                            "No se pudo interpretar la imagen seleccionada.\n\n" +
                            "Comprueba que sea un archivo PNG, JPG o BMP válido " +
                            "y que no esté dañado.",
                            "Imagen no válida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        rutaMapa = "";
                        registro.RutaMapa = "";

                        MessageBox.Show(
                            "No se pudo guardar o mostrar el mapa.\n\n" +
                            "Detalle: " + ex.Message,
                            "Error al seleccionar el mapa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Guardar temporalmente los datos del estadio.
        // Todavía NO se insertan en SQL Server.
        private void boton2_guardarEstadio_Click(object sender, EventArgs e)
        {
            string nombre = txtbox1_nombreEstadio.Text.Trim();
            string ciudad = txtbox2_ciudadEstadio.Text.Trim();
            string capacidadTexto =
                txtbox3_capacidadEstadio.Text.Trim();

            // Validar que el nombre no esté vacío.
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show(
                    "Ingresa el nombre del estadio.",
                    "Nombre incompleto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtbox1_nombreEstadio.Focus();
                return;
            }

            // Validar los caracteres del nombre.
            if (!NombreEstadioValido(nombre))
            {
                MessageBox.Show(
                    "El nombre del estadio solamente puede contener letras, " +
                    "números, espacios, guiones, puntos, apóstrofes y el símbolo &.",
                    "Nombre incorrecto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtbox1_nombreEstadio.Focus();
                return;
            }

            // Validar que la ciudad no esté vacía.
            if (string.IsNullOrWhiteSpace(ciudad))
            {
                MessageBox.Show(
                    "Ingresa la ciudad donde se encuentra el estadio.",
                    "Ciudad incompleta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtbox2_ciudadEstadio.Focus();
                return;
            }

            // Validar que la ciudad no contenga números u otros caracteres.
            if (!CiudadValida(ciudad))
            {
                MessageBox.Show(
                    "La ciudad solamente puede contener letras, espacios, " +
                    "guiones y apóstrofes. No se permiten números.",
                    "Ciudad incorrecta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtbox2_ciudadEstadio.Focus();
                return;
            }

            // Validar que la capacidad no esté vacía.
            if (string.IsNullOrWhiteSpace(capacidadTexto))
            {
                MessageBox.Show(
                    "Ingresa la capacidad total del estadio.",
                    "Capacidad incompleta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtbox3_capacidadEstadio.Focus();
                return;
            }

            // Validar que la capacidad sea un entero positivo.
            if (!int.TryParse(capacidadTexto, out int capacidad) ||
                capacidad <= 0)
            {
                MessageBox.Show(
                    "La capacidad debe ser un número entero mayor que cero.",
                    "Capacidad incorrecta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtbox3_capacidadEstadio.Focus();
                return;
            }

            // Validar que se haya seleccionado un mapa.
            if (string.IsNullOrWhiteSpace(rutaMapa))
            {
                MessageBox.Show(
                    "Selecciona una imagen del mapa del estadio.",
                    "Mapa no seleccionado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                boton1_mapaEstadio.Focus();
                return;
            }

            // Validar que el archivo del mapa todavía exista.
            if (!File.Exists(rutaMapa))
            {
                MessageBox.Show(
                    "No se encontró la imagen del mapa en tu computadora. " +
                    "Selecciona nuevamente la imagen.",
                    "Mapa no encontrado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                rutaMapa = "";
                registro.RutaMapa = "";
                boton1_mapaEstadio.Focus();
                return;
            }

            // Comprobar nuevamente que el archivo sea una imagen válida.
            try
            {
                using (Image imagenValidacion =
                    CargarImagenSinBloquear(rutaMapa))
                {
                }
            }
            catch
            {
                MessageBox.Show(
                    "El mapa seleccionado ya no es una imagen válida. " +
                    "Selecciona nuevamente un archivo JPG, JPEG, PNG o BMP.",
                    "Mapa incorrecto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                rutaMapa = "";
                registro.RutaMapa = "";
                boton1_mapaEstadio.Focus();
                return;
            }

            // Guardar todos los datos en el objeto compartido.
            registro.NombreEstadio = nombre;
            registro.Ciudad = ciudad;
            registro.Capacidad = capacidad;
            registro.RutaMapa = rutaMapa;

            MessageBox.Show(
                "Datos del estadio guardados correctamente.\n\n" +
                "Nombre: " + registro.NombreEstadio + "\n" +
                "Ciudad: " + registro.Ciudad + "\n" +
                "Capacidad total: " + registro.Capacidad + " asientos\n\n" +
                "El mapa quedó guardado en tu computadora.\n\n" +
                "Ahora registra al menos una zona.",
                "Continuar con zonas",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Regresar al catálogo.
        private void boton3_atrasEstadio_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Evento que se ejecuta al hacer clic en la imagen del mapa.
        private void imagen_mapaEstadio_Click(object sender, EventArgs e)
        {
        }

        // Evento que se ejecuta cuando se carga el formulario.
        private void pantalla_registroestadio_Load(object sender, EventArgs e)
        {
        }

        // Evento que se ejecuta cuando cambia el texto de capacidad.
        private void txtxbox3_capacidadEstadio_TextChanged(
            object sender, EventArgs e)
        {
            // Actualizar la capacidad temporal mientras se escribe.
            if (int.TryParse(
                txtbox3_capacidadEstadio.Text.Trim(),
                out int capacidad) &&
                capacidad > 0)
            {
                registro.Capacidad = capacidad;
            }
            else
            {
                // No conservar una capacidad anterior si el campo
                // está vacío o contiene un valor inválido.
                registro.Capacidad = 0;
            }
        }

        private void label1_ingresar_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label2_nombreE_Click(object sender, EventArgs e)
        {
        }

        private void label3_ciudadE_Click(object sender, EventArgs e)
        {
        }

        // Liberar la imagen del PictureBox cuando se cierra el formulario.
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (imagen_mapaEstadio.Image != null)
            {
                imagen_mapaEstadio.Image.Dispose();
                imagen_mapaEstadio.Image = null;
            }

            base.OnFormClosed(e);
        }
    }
}
