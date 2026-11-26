
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Pantalla_RegistroCatalogos_PIA_PA;

namespace Pantalla_RegistroAsientos_PIA_PA
{
    public partial class pantalla_registroAsientos : Form
    {
        // Zona donde se están registrando los asientos.
        private ZonaTemporal zonaActual;

        // Información general del estadio.
        // Permite consultar la capacidad total y los asientos
        // registrados en todas las zonas.
        private RegistroCatalogoTemporal registroEstadio;

        // Indica si la zona ya fue completada.
        private bool registroCompletado = false;

        // Evita que el botón se ejecute varias veces seguidas.
        private bool procesandoRegistro = false;

        // Indica si el usuario solicitó regresar.
        private bool cancelacionSolicitada = false;

        // Constructor que conserva compatibilidad con el formulario anterior.
        public pantalla_registroAsientos(ZonaTemporal zona)
            : this(zona, null)
        {
        }

        // Constructor que recibe la zona y los datos generales del estadio.
        public pantalla_registroAsientos(
            ZonaTemporal zona,
            RegistroCatalogoTemporal datosEstadio)
        {
            InitializeComponent();

            zonaActual = zona ??
                throw new ArgumentNullException(nameof(zona));

            registroEstadio = datosEstadio;

            // Inicializar la lista de filas si todavía no existe.
            if (zonaActual.Filas == null)
            {
                zonaActual.Filas = new System.Collections.Generic.List<FilaTemporal>();
            }

            // Evitar suscripciones duplicadas.
            txtbox1_registroAsientos.KeyDown -= AvanzarConEnter;
            txtbox2_registroAsientos.KeyDown -= AvanzarConEnter;

            txtbox1_registroAsientos.KeyDown += AvanzarConEnter;
            txtbox2_registroAsientos.KeyDown += AvanzarConEnter;

            // Validar los caracteres permitidos.
            txtbox1_registroAsientos.KeyPress -=
                txtbox1_registroAsientos_KeyPress;

            txtbox2_registroAsientos.KeyPress -=
                txtbox2_registroAsientos_KeyPress;

            txtbox1_registroAsientos.KeyPress +=
                txtbox1_registroAsientos_KeyPress;

            txtbox2_registroAsientos.KeyPress +=
                txtbox2_registroAsientos_KeyPress;

            // Evitar que se cierre accidentalmente una zona incompleta.
            this.FormClosing -= pantalla_registroAsientos_FormClosing;
            this.FormClosing += pantalla_registroAsientos_FormClosing;

            // Configurar la cantidad máxima de caracteres.
            txtbox1_registroAsientos.MaxLength = 3;

            // Mostrar el progreso inicial.
            ActualizarInstrucciones();
        }

        // Controlar el movimiento entre los campos con Enter.
        private void AvanzarConEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;

                if (sender == txtbox1_registroAsientos)
                {
                    txtbox2_registroAsientos.Focus();
                }
                else if (sender == txtbox2_registroAsientos)
                {
                    boton1_registroAsientos.Focus();
                }
            }
        }

        // Permitir únicamente letras de la A a la Z en el identificador.
        private void txtbox1_registroAsientos_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsLetter(e.KeyChar) ||
                e.KeyChar > 127)
            {
                e.Handled = true;
                return;
            }

            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        // Permitir únicamente números en la cantidad de asientos.
        private void txtbox2_registroAsientos_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Obtener el total de asientos registrados en la zona actual.
        private int TotalAsientosRegistradosZona()
        {
            if (zonaActual.Filas == null)
                return 0;

            return zonaActual.Filas.Sum(
                fila => fila.CantidadAsientos);
        }

        // Obtener el total de asientos registrados en todas las zonas.
        private int TotalAsientosRegistradosEstadio()
        {
            if (registroEstadio == null ||
                registroEstadio.Zonas == null)
            {
                return TotalAsientosRegistradosZona();
            }

            return registroEstadio.Zonas
                .Where(zona => zona.Filas != null)
                .SelectMany(zona => zona.Filas)
                .Sum(fila => fila.CantidadAsientos);
        }

        // Calcular cuántos asientos faltan para completar la zona.
        private int AsientosFaltantesZona()
        {
            return zonaActual.Cupo -
                   TotalAsientosRegistradosZona();
        }

        // Calcular cuántos asientos faltan para alcanzar
        // la capacidad total del estadio.
        private int AsientosFaltantesEstadio()
        {
            if (registroEstadio == null)
                return 0;

            return registroEstadio.Capacidad -
                   TotalAsientosRegistradosEstadio();
        }

        // Actualizar las instrucciones y el progreso mostrado.
        private void ActualizarInstrucciones()
        {
            int registradosZona =
                TotalAsientosRegistradosZona();

            int faltantesZona =
                AsientosFaltantesZona();

            string instrucciones =
                "REGISTRO DE ASIENTOS\n\n" +
                "Cupo de la zona: " + zonaActual.Cupo + "\n" +
                "Asientos registrados en esta zona: " +
                registradosZona + "\n" +
                "Asientos faltantes en esta zona: " +
                faltantesZona;

            if (registroEstadio != null)
            {
                int registradosEstadio =
                    TotalAsientosRegistradosEstadio();

                int faltantesEstadio =
                    AsientosFaltantesEstadio();

                instrucciones +=
                    "\n\nCAPACIDAD TOTAL DEL ESTADIO\n\n" +
                    "Capacidad total: " +
                    registroEstadio.Capacidad + "\n" +
                    "Asientos registrados en el estadio: " +
                    registradosEstadio + "\n" +
                    "Capacidad disponible: " +
                    faltantesEstadio;
            }

            label1_instruccionesAsientos.Text = instrucciones;
        }

        // Registrar una fila y su cantidad de asientos.
        private void boton1_registroAsientos_Click(
            object sender, EventArgs e)
        {
            // Evitar ejecuciones repetidas.
            if (procesandoRegistro)
                return;

            procesandoRegistro = true;

            try
            {
                // Obtener los datos ingresados.
                string nombreFila =
                    txtbox1_registroAsientos.Text.Trim().ToUpper();

                string cantidadTexto =
                    txtbox2_registroAsientos.Text.Trim();

                // Validar que se haya ingresado el identificador.
                if (string.IsNullOrWhiteSpace(nombreFila))
                {
                    MessageBox.Show(
                        "Ingresa el identificador de la fila.",
                        "Campo obligatorio",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox1_registroAsientos.Focus();
                    return;
                }

                // Validar el formato del identificador.
                if (!Regex.IsMatch(nombreFila, @"^[A-Z]{1,3}$"))
                {
                    MessageBox.Show(
                        "El identificador de la fila debe contener " +
                        "solamente letras de la A a la Z.",
                        "Identificador incorrecto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox1_registroAsientos.Focus();
                    return;
                }

                // Validar que la fila no esté repetida.
                bool filaExiste = zonaActual.Filas.Any(
                    fila => fila.Nombre.Equals(
                        nombreFila,
                        StringComparison.OrdinalIgnoreCase));

                if (filaExiste)
                {
                    MessageBox.Show(
                        "Esta fila ya fue registrada en la zona.",
                        "Fila duplicada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox1_registroAsientos.Focus();
                    txtbox1_registroAsientos.SelectAll();
                    return;
                }

                // Validar que se haya ingresado la cantidad.
                if (string.IsNullOrWhiteSpace(cantidadTexto))
                {
                    MessageBox.Show(
                        "Ingresa la cantidad de asientos de la fila.",
                        "Campo obligatorio",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox2_registroAsientos.Focus();
                    return;
                }

                // Convertir la cantidad a entero.
                if (!int.TryParse(cantidadTexto, out int cantidadAsientos) ||
                    cantidadAsientos <= 0)
                {
                    MessageBox.Show(
                        "La cantidad de asientos debe ser un número entero " +
                        "mayor que cero.",
                        "Cantidad incorrecta",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox2_registroAsientos.Focus();
                    return;
                }

                // Calcular el espacio disponible en la zona.
                int registradosZona =
                    TotalAsientosRegistradosZona();

                int faltantesZona =
                    zonaActual.Cupo - registradosZona;

                // Comprobar que no se supere el cupo de la zona.
                if (cantidadAsientos > faltantesZona)
                {
                    MessageBox.Show(
                        "La cantidad de asientos supera el cupo disponible " +
                        "de esta zona.\n\n" +
                        "Cupo de la zona: " + zonaActual.Cupo + "\n" +
                        "Asientos registrados: " + registradosZona + "\n" +
                        "Asientos disponibles: " + faltantesZona,
                        "Se excede el cupo de la zona",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtbox2_registroAsientos.Focus();
                    txtbox2_registroAsientos.SelectAll();
                    return;
                }

                // Comprobar la capacidad global del estadio.
                if (registroEstadio != null)
                {
                    int faltantesEstadio =
                        AsientosFaltantesEstadio();

                    if (faltantesEstadio <= 0)
                    {
                        MessageBox.Show(
                            "Ya se alcanzó la capacidad total del estadio.\n\n" +
                            "Capacidad total: " +
                            registroEstadio.Capacidad + "\n" +
                            "Asientos registrados: " +
                            TotalAsientosRegistradosEstadio(),
                            "Estadio completado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    if (cantidadAsientos > faltantesEstadio)
                    {
                        MessageBox.Show(
                            "La cantidad de asientos supera la capacidad " +
                            "disponible del estadio.\n\n" +
                            "Capacidad total: " +
                            registroEstadio.Capacidad + "\n" +
                            "Asientos registrados: " +
                            TotalAsientosRegistradosEstadio() + "\n" +
                            "Asientos que todavía puedes registrar: " +
                            faltantesEstadio,
                            "Se excede la capacidad del estadio",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        txtbox2_registroAsientos.Focus();
                        txtbox2_registroAsientos.SelectAll();
                        return;
                    }
                }

                // Crear el registro de la fila.
                FilaTemporal nuevaFila = new FilaTemporal
                {
                    Nombre = nombreFila,
                    CantidadAsientos = cantidadAsientos
                };

                // Guardar la fila en la zona actual.
                zonaActual.Filas.Add(nuevaFila);

                // Actualizar las cantidades.
                registradosZona =
                    TotalAsientosRegistradosZona();

                faltantesZona =
                    AsientosFaltantesZona();

                int registradosEstadio =
                    TotalAsientosRegistradosEstadio();

                // Comprobar si la zona ya está completa.
                if (faltantesZona == 0)
                {
                    registroCompletado = true;

                    ActualizarInstrucciones();

                    string mensaje =
                        "¡La zona se completó correctamente!\n\n" +
                        "Zona: " + zonaActual.Nombre + "\n" +
                        "Cupo de la zona: " + zonaActual.Cupo + "\n" +
                        "Asientos registrados: " + registradosZona;

                    // Comprobar también el progreso global del estadio.
                    if (registroEstadio != null)
                    {
                        int faltantesEstadio =
                            AsientosFaltantesEstadio();

                        mensaje +=
                            "\n\nCAPACIDAD DEL ESTADIO\n" +
                            "Capacidad total: " +
                            registroEstadio.Capacidad + "\n" +
                            "Asientos registrados: " +
                            registradosEstadio + "\n" +
                            "Asientos pendientes: " +
                            faltantesEstadio;

                        if (faltantesEstadio == 0)
                        {
                            mensaje +=
                                "\n\n¡Se completó la capacidad total del estadio!";
                        }
                    }

                    MessageBox.Show(
                        mensaje,
                        "Zona completada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }

                // Si la zona todavía no está completa,
                // informar que la fila se registró correctamente.
                ActualizarInstrucciones();

                string mensajeFila =
                    "La fila se registró correctamente.\n\n" +
                    "Fila: " + nombreFila + "\n" +
                    "Asientos registrados en esta fila: " +
                    cantidadAsientos + "\n\n" +
                    "Asientos faltantes en la zona: " +
                    faltantesZona;

                if (registroEstadio != null)
                {
                    mensajeFila +=
                        "\n\nAsientos registrados en el estadio: " +
                        registradosEstadio +
                        "\nCapacidad disponible del estadio: " +
                        AsientosFaltantesEstadio();
                }

                MessageBox.Show(
                    mensajeFila,
                    "Fila registrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Limpiar los campos para registrar otra fila.
                txtbox1_registroAsientos.Clear();
                txtbox2_registroAsientos.Clear();

                txtbox1_registroAsientos.Focus();
            }
            finally
            {
                procesandoRegistro = false;
            }
        }

        // Regresar al formulario anterior sin eliminar
        // las filas que ya fueron registradas.
        private void boton2_regresarAsientos_Click(
            object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas regresar al registro de zonas?\n\n" +
                "Las filas que ya registraste se conservarán.",
                "Confirmar regreso",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                cancelacionSolicitada = true;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // Evitar que se cierre el formulario con la X
        // cuando todavía faltan asientos de la zona.
        private void pantalla_registroAsientos_FormClosing(
            object sender, FormClosingEventArgs e)
        {
            if (!registroCompletado && !cancelacionSolicitada)
            {
                e.Cancel = true;

                MessageBox.Show(
                    "La zona todavía no está completa.\n\n" +
                    "Registra los asientos faltantes o utiliza el botón " +
                    "REGRESAR para volver al formulario anterior.",
                    "Registro incompleto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // Evento que se ejecuta cuando se carga el formulario.
        private void pantalla_registroAsientos_Load(
            object sender, EventArgs e)
        {
            ActualizarInstrucciones();
        }

        // Evento del identificador de fila.
        private void txtbox1_registroAsientos_TextChanged(
            object sender, EventArgs e)
        {
        }

        // Evento de la cantidad de asientos.
        private void txtbox2_registroAsientos_TextChanged(
            object sender, EventArgs e)
        {
        }

        // Evento del texto de instrucciones.
        private void label1_instruccionesAsientos_Click(
            object sender, EventArgs e)
        {
        }
    }
}
