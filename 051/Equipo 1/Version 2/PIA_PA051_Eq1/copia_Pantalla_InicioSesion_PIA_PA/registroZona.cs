
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Pantalla_RegistroCatalogos_PIA_PA;
using Pantalla_RegistroAsientos_PIA_PA;

namespace Pantalla_RegistroZona_PIA_PA
{
    public partial class pantalla_registroZona : Form
    {
        // Guarda los datos temporales del estadio y sus zonas.
        private RegistroCatalogoTemporal registro;

        // Evita que el evento de registro se ejecute varias veces simultáneamente.
        private bool procesandoRegistro = false;

        // Evita que el TextChanged vuelva a ejecutarse al convertir a mayúsculas.
        private bool convirtiendoMayusculas = false;

        // Constructor del formulario.
        public pantalla_registroZona(RegistroCatalogoTemporal datos)
        {
            InitializeComponent();

            // Guarda la referencia al registro compartido.
            registro = datos ?? throw new ArgumentNullException(nameof(datos));

            // Evita suscripciones duplicadas al evento KeyDown.
            txtbox1_registroZona.KeyDown -= AvanzarConEnter;
            txtbox2_registroZona.KeyDown -= AvanzarConEnter;
            txtbox3_registroZona.KeyDown -= AvanzarConEnter;

            // Asigna el evento KeyDown a los campos de texto.
            txtbox1_registroZona.KeyDown += AvanzarConEnter;
            txtbox2_registroZona.KeyDown += AvanzarConEnter;
            txtbox3_registroZona.KeyDown += AvanzarConEnter;

            // Validar los caracteres permitidos en cada campo.
            txtbox1_registroZona.KeyPress +=
                txtbox1_registroZona_KeyPress;

            txtbox2_registroZona.KeyPress +=
                txtbox2_registroZona_KeyPress;

            txtbox3_registroZona.KeyPress +=
                txtbox3_registroZona_KeyPress;

            // Evita que Enter active automáticamente un botón predeterminado.
            this.AcceptButton = null;
        }

        // Calcula la suma de los cupos de todas las zonas.
        private int TotalCuposZonas()
        {
            if (registro == null || registro.Zonas == null)
                return 0;

            return registro.Zonas.Sum(z => z.Cupo);
        }

        // Calcula todos los asientos registrados en el estadio.
        private int TotalAsientosEstadio()
        {
            if (registro == null || registro.Zonas == null)
                return 0;

            return registro.Zonas
                .Where(z => z.Filas != null)
                .SelectMany(z => z.Filas)
                .Sum(f => f.CantidadAsientos);
        }

        // Controla el movimiento entre campos al presionar Enter.
        private void AvanzarConEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            e.Handled = true;

            if (procesandoRegistro)
                return;

            if (sender == txtbox1_registroZona)
            {
                txtbox2_registroZona.Focus();
            }
            else if (sender == txtbox2_registroZona)
            {
                txtbox3_registroZona.Focus();
            }
            else if (sender == txtbox3_registroZona)
            {
                boton1_registroZona.PerformClick();
            }
        }

        // Validar los caracteres permitidos para el nombre de la zona.
        // Se permiten letras, números, espacios y guiones.
        private bool CaracterNombreZonaValido(char caracter)
        {
            return char.IsLetter(caracter) ||
                   char.IsDigit(caracter) ||
                   char.IsWhiteSpace(caracter) ||
                   caracter == '-';
        }

        // Comprobar que el nombre de la zona solamente tenga
        // caracteres permitidos.
        private bool NombreZonaValido(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return false;

            foreach (char caracter in nombre)
            {
                if (!CaracterNombreZonaValido(caracter))
                    return false;
            }

            return true;
        }

        // Permitir solamente letras, números, espacios y guiones
        // en el nombre de la zona.
        private void txtbox1_registroZona_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !CaracterNombreZonaValido(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Permitir números y un separador decimal en el precio.
        private void txtbox2_registroZona_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            string separadorDecimal =
                CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            if (e.KeyChar.ToString() == separadorDecimal &&
                !txtbox2_registroZona.Text.Contains(separadorDecimal))
            {
                return;
            }

            e.Handled = true;
        }

        // Permitir únicamente números en el cupo.
        private void txtbox3_registroZona_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Evento del botón que registra una zona.
        private void boton1_registroZona_Click(object sender, EventArgs e)
        {
            if (procesandoRegistro)
                return;

            procesandoRegistro = true;

            try
            {
                // Comprueba que exista un registro y que el estadio esté capturado.
                if (registro == null || !registro.TieneEstadio)
                {
                    MessageBox.Show(
                        "Primero registra los datos del estadio.",
                        "Falta el estadio",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Comprueba que la capacidad del estadio sea válida.
                if (registro.Capacidad <= 0)
                {
                    MessageBox.Show(
                        "La capacidad del estadio no es válida. " +
                        "Regresa al registro del estadio y verifica su capacidad.",
                        "Capacidad incorrecta",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Asegura que la colección de zonas esté inicializada.
                if (registro.Zonas == null)
                {
                    registro.Zonas =
                        new System.Collections.Generic.List<ZonaTemporal>();
                }

                // Obtiene los datos escritos en los campos.
                string nombre = txtbox1_registroZona.Text.Trim();
                string precioTexto = txtbox2_registroZona.Text.Trim();
                string cupoTexto = txtbox3_registroZona.Text.Trim();

                // Verifica que el nombre no esté vacío.
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show(
                        "Ingresa el nombre de la zona.",
                        "Nombre incompleto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox1_registroZona.Focus();
                    return;
                }

                // Verifica que el precio no esté vacío.
                if (string.IsNullOrWhiteSpace(precioTexto))
                {
                    MessageBox.Show(
                        "Ingresa el precio de la zona.",
                        "Precio incompleto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox2_registroZona.Focus();
                    return;
                }

                // Verifica que el cupo no esté vacío.
                if (string.IsNullOrWhiteSpace(cupoTexto))
                {
                    MessageBox.Show(
                        "Ingresa el cupo de la zona.",
                        "Cupo incompleto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox3_registroZona.Focus();
                    return;
                }

                // Validar los caracteres del nombre.
                if (!NombreZonaValido(nombre))
                {
                    MessageBox.Show(
                        "El nombre de la zona solamente puede contener letras, " +
                        "números, espacios y guiones.",
                        "Nombre incorrecto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox1_registroZona.Focus();
                    return;
                }

                // Asegurar que el nombre se almacene en mayúsculas.
                nombre = nombre.ToUpperInvariant();

                // Validar que el precio sea un número decimal mayor que cero.
                if (!decimal.TryParse(
                        precioTexto,
                        NumberStyles.AllowDecimalPoint,
                        CultureInfo.CurrentCulture,
                        out decimal precio) ||
                    precio <= 0)
                {
                    MessageBox.Show(
                        "El precio debe ser un número mayor que cero.\n\n" +
                        "Utiliza el separador decimal de tu configuración regional.",
                        "Precio incorrecto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox2_registroZona.Focus();
                    return;
                }

                // Validar que el cupo sea un entero positivo.
                if (!int.TryParse(cupoTexto, out int cupo) || cupo <= 0)
                {
                    MessageBox.Show(
                        "El cupo debe ser un número entero mayor que cero.",
                        "Cupo incorrecto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox3_registroZona.Focus();
                    return;
                }

                // Comprueba que no exista otra zona con el mismo nombre.
                bool existe = registro.Zonas.Any(z =>
                    string.Equals(
                        z.Nombre,
                        nombre,
                        StringComparison.OrdinalIgnoreCase));

                if (existe)
                {
                    MessageBox.Show(
                        "Ya registraste una zona con ese nombre. Escribe otro.",
                        "Zona duplicada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox1_registroZona.Focus();
                    return;
                }

                // Obtener la suma de los cupos de las zonas existentes.
                int cuposRegistrados = TotalCuposZonas();

                // Calcular la capacidad que todavía puede asignarse a zonas.
                int capacidadDisponible =
                    registro.Capacidad - cuposRegistrados;

                // Comprobar si el estadio ya tiene toda su capacidad distribuida.
                if (capacidadDisponible <= 0)
                {
                    MessageBox.Show(
                        "Ya se distribuyó toda la capacidad del estadio.\n\n" +
                        $"Capacidad del estadio: {registro.Capacidad}\n" +
                        $"Cupos asignados a zonas: {cuposRegistrados}\n\n" +
                        "No puedes registrar otra zona.",
                        "Capacidad del estadio completada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Evitar que la nueva zona supere la capacidad disponible.
                if (cupo > capacidadDisponible)
                {
                    MessageBox.Show(
                        "El cupo de esta zona supera la capacidad disponible " +
                        "del estadio.\n\n" +
                        $"Capacidad total del estadio: {registro.Capacidad}\n" +
                        $"Cupos asignados: {cuposRegistrados}\n" +
                        $"Capacidad disponible: {capacidadDisponible}",
                        "Se excede la capacidad del estadio",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox3_registroZona.Focus();
                    return;
                }

                // Crea el objeto temporal de la nueva zona.
                ZonaTemporal nuevaZona = new ZonaTemporal
                {
                    Nombre = nombre,
                    Precio = (double)precio,
                    Cupo = cupo,
                    Filas = new System.Collections.Generic.List<FilaTemporal>()
                };

                // Agrega la zona a la colección temporal.
                registro.Zonas.Add(nuevaZona);

                // Confirma que la zona fue agregada.
                MessageBox.Show(
                    "Zona agregada correctamente.\n\n" +
                    "Nombre: " + nombre + "\n" +
                    "Precio base: " + precio.ToString("N2") + "\n" +
                    "Cupo: " + cupo + "\n" +
                    "Total de zonas registradas: " + registro.Zonas.Count,
                    "Zona agregada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Abrir el formulario de asientos como ventana modal.
                DialogResult resultadoAsientos;

                using (pantalla_registroAsientos asientos =
                       new pantalla_registroAsientos(nuevaZona, registro))
                {
                    resultadoAsientos = asientos.ShowDialog(this);
                }

                // Calcular el avance de todo el estadio.
                int totalAsientos = TotalAsientosEstadio();

                int asientosFaltantes =
                    registro.Capacidad - totalAsientos;

                // Si el usuario regresó sin completar la zona,
                // se conservan la zona y las filas capturadas.
                if (resultadoAsientos == DialogResult.Cancel)
                {
                    MessageBox.Show(
                        "Regresaste al registro de zonas.\n\n" +
                        "La información de esta zona y las filas capturadas " +
                        "se conservaron temporalmente.\n\n" +
                        $"Asientos registrados en el estadio: {totalAsientos}\n" +
                        $"Asientos faltantes para completar el estadio: " +
                        $"{asientosFaltantes}",
                        "Registro pendiente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else if (resultadoAsientos == DialogResult.OK)
                {
                    // La zona se completó correctamente.
                    // Se conserva el mensaje de confirmación de zona.
                    MessageBox.Show(
                        "La zona y sus asientos se registraron correctamente.",
                        "Registro completado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Comprobar si ya se registraron todos los asientos del estadio.
                    if (totalAsientos == registro.Capacidad)
                    {
                        MessageBox.Show(
                            "¡Se registraron todos los asientos del estadio!\n\n" +
                            $"Capacidad del estadio: {registro.Capacidad}\n" +
                            $"Asientos registrados: {totalAsientos}\n\n" +
                            "La capacidad total del estadio quedó cubierta.\n\n" +
                            "Regresarás al menú de registro de catálogos.",
                            "Estadio completado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Cerrar el formulario de zonas.
                        // Al cerrarse, se regresará al menú de registro de catálogos.
                        this.Close();

                        return;
                    }
                    else
                    {
                        MessageBox.Show(
                            "El estadio todavía tiene asientos pendientes de registrar.\n\n" +
                            $"Capacidad del estadio: {registro.Capacidad}\n" +
                            $"Asientos registrados: {totalAsientos}\n" +
                            $"Asientos faltantes: {asientosFaltantes}",
                            "Estadio pendiente",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }

                // Limpiar los campos para permitir registrar otra zona.
                txtbox1_registroZona.Clear();
                txtbox2_registroZona.Clear();
                txtbox3_registroZona.Clear();

                // Coloca el cursor nuevamente en el campo del nombre.
                txtbox1_registroZona.Focus();

                // Recupera el foco del formulario de zonas.
                this.Activate();
            }
            finally
            {
                // Permite volver a ejecutar el evento después de terminar.
                procesandoRegistro = false;
            }
        }

        // Evento del botón para cerrar el formulario de zonas.
        private void boton2_registroZona_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Evento Load del formulario.
        private void pantalla_registroZona_Load(object sender, EventArgs e)
        {
        }

        // Evento Click de la etiqueta.
        private void label1_registroZona_Click(object sender, EventArgs e)
        {
        }

        // Evento TextChanged del campo del nombre.
        // IMPORTANTE: no se vuelve a suscribir en el constructor.
        // Si el diseñador ya lo tiene conectado, este método se ejecutará.
        private void txtbox1_registroZona_TextChanged(object sender, EventArgs e)
        {
            if (convirtiendoMayusculas)
                return;

            convirtiendoMayusculas = true;

            try
            {
                int posicionCursor =
                    txtbox1_registroZona.SelectionStart;

                string textoMayusculas =
                    txtbox1_registroZona.Text.ToUpperInvariant();

                if (txtbox1_registroZona.Text != textoMayusculas)
                {
                    txtbox1_registroZona.Text = textoMayusculas;

                    txtbox1_registroZona.SelectionStart = Math.Min(
                        posicionCursor,
                        txtbox1_registroZona.Text.Length);
                }
            }
            finally
            {
                convirtiendoMayusculas = false;
            }
        }

        // Evento TextChanged del campo del precio.
        private void txtbox2_registroZona_TextChanged(object sender, EventArgs e)
        {
        }

        // Evento TextChanged del campo del cupo.
        private void txtbox3_registroZona_TextChanged(object sender, EventArgs e)
        {
        }

        // Evento Click de la segunda etiqueta.
        private void label2_Click(object sender, EventArgs e)
        {
        }
    }
}
