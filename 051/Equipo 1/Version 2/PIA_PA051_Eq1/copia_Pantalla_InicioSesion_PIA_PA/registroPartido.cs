
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Pantalla_RegistroCatalogos_PIA_PA;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Pantalla_RegistroPartido_PIA_PA
{
    public partial class pantalla_registroPartido : Form
    {
        // Identificador temporal para representar el estadio nuevo.
        // Este valor nunca se insertará en SQL Server.
        private const int ID_ESTADIO_TEMPORAL = -1;

        // Guarda la referencia al registro temporal del catálogo.
        private RegistroCatalogoTemporal registro;

        // Evitan que los eventos TextChanged se ejecuten repetidamente.
        private bool actualizandoFecha = false;
        private bool actualizandoHora = false;
        private bool convirtiendoMayusculas = false;

        // Constructor que recibe los datos temporales del catálogo.
        public pantalla_registroPartido(RegistroCatalogoTemporal datos)
        {
            InitializeComponent();

            if (datos == null)
            {
                throw new ArgumentNullException(
                    nameof(datos),
                    "No se recibió el registro temporal del catálogo.");
            }

            registro = datos;

            // Configurar límites de caracteres de acuerdo con SQL Server.
            txtbox1_registroPartido.MaxLength = 50;
            txtbox2_registroPartido.MaxLength = 10;
            txtbox3_registroPartido.MaxLength = 5;
            txtbox4_registroPartido.MaxLength = 50;
            txtbox5_registroPartido.MaxLength = 50;

            // Configurar avance con Enter.
            txtbox1_registroPartido.KeyDown -= AvanzarConEnter;
            txtbox1_registroPartido.KeyDown += AvanzarConEnter;

            txtbox2_registroPartido.KeyDown -= AvanzarConEnter;
            txtbox2_registroPartido.KeyDown += AvanzarConEnter;

            txtbox3_registroPartido.KeyDown -= AvanzarConEnter;
            txtbox3_registroPartido.KeyDown += AvanzarConEnter;

            txtbox4_registroPartido.KeyDown -= AvanzarConEnter;
            txtbox4_registroPartido.KeyDown += AvanzarConEnter;

            txtbox5_registroPartido.KeyDown -= AvanzarConEnter;
            txtbox5_registroPartido.KeyDown += AvanzarConEnter;

            cmbobox1_registroPartido.KeyDown -= AvanzarConEnter;
            cmbobox1_registroPartido.KeyDown += AvanzarConEnter;

            // Configurar validación de letras en los equipos y el estado.
            txtbox1_registroPartido.KeyPress -= TextoSoloLetras_KeyPress;
            txtbox1_registroPartido.KeyPress += TextoSoloLetras_KeyPress;

            txtbox4_registroPartido.KeyPress -= TextoSoloLetras_KeyPress;
            txtbox4_registroPartido.KeyPress += TextoSoloLetras_KeyPress;

            txtbox5_registroPartido.KeyPress -= TextoSoloLetras_KeyPress;
            txtbox5_registroPartido.KeyPress += TextoSoloLetras_KeyPress;

            // Configurar conversión a mayúsculas y filtro de texto.
            txtbox1_registroPartido.TextChanged -=
                txtbox1_registroPartido_TextChanged;
            txtbox1_registroPartido.TextChanged +=
                txtbox1_registroPartido_TextChanged;

            txtbox4_registroPartido.TextChanged -=
                txtbox4_registroPartido_TextChanged;
            txtbox4_registroPartido.TextChanged +=
                txtbox4_registroPartido_TextChanged;

            txtbox5_registroPartido.TextChanged -=
                txtbox5_registroPartido_TextChanged;
            txtbox5_registroPartido.TextChanged +=
                txtbox5_registroPartido_TextChanged;

            // Configurar formato automático de fecha.
            txtbox2_registroPartido.KeyPress -=
                txtbox2_registroPartido_KeyPress;
            txtbox2_registroPartido.KeyPress +=
                txtbox2_registroPartido_KeyPress;

            txtbox2_registroPartido.TextChanged -=
                txtbox2_registroPartido_TextChanged;
            txtbox2_registroPartido.TextChanged +=
                txtbox2_registroPartido_TextChanged;

            // Configurar formato automático de hora.
            txtbox3_registroPartido.KeyPress -=
                txtbox3_registroPartido_KeyPress;
            txtbox3_registroPartido.KeyPress +=
                txtbox3_registroPartido_KeyPress;

            txtbox3_registroPartido.TextChanged -=
                txtbox3_registroPartido_TextChanged;
            txtbox3_registroPartido.TextChanged +=
                txtbox3_registroPartido_TextChanged;
        }

        // Avanzar al siguiente control al presionar Enter.
        private void AvanzarConEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                SelectNextControl(
                    (Control)sender,
                    true,
                    true,
                    true,
                    true
                );
            }
        }

        // Permitir únicamente letras y espacios.
        // Se aceptan letras acentuadas y la Ñ.
        private void TextoSoloLetras_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        // Filtrar caracteres no permitidos, incluso al pegar texto.
        private void FiltrarTextoSoloLetras(TextBox textBox)
        {
            if (convirtiendoMayusculas)
            {
                return;
            }

            convirtiendoMayusculas = true;

            try
            {
                int posicionOriginal = textBox.SelectionStart;
                string textoOriginal = textBox.Text;

                // Conservar únicamente letras y espacios.
                string textoFiltrado = new string(
                    textoOriginal
                        .Where(c => char.IsLetter(c) || c == ' ')
                        .ToArray()
                );

                // Convertir el texto a mayúsculas.
                textoFiltrado = textoFiltrado.ToUpperInvariant();

                if (textoOriginal != textoFiltrado)
                {
                    int caracteresValidosAntes = textoOriginal
                        .Take(posicionOriginal)
                        .Count(c => char.IsLetter(c) || c == ' ');

                    textBox.Text = textoFiltrado;

                    textBox.SelectionStart = Math.Min(
                        caracteresValidosAntes,
                        textBox.Text.Length
                    );
                }
            }
            finally
            {
                convirtiendoMayusculas = false;
            }
        }

        // Equipo visitante.
        private void txtbox1_registroPartido_TextChanged(
            object sender, EventArgs e)
        {
            FiltrarTextoSoloLetras(txtbox1_registroPartido);
        }

        // Equipo local.
        private void txtbox4_registroPartido_TextChanged(
            object sender, EventArgs e)
        {
            FiltrarTextoSoloLetras(txtbox4_registroPartido);
        }

        // Estado del partido.
        private void txtbox5_registroPartido_TextChanged(
            object sender, EventArgs e)
        {
            FiltrarTextoSoloLetras(txtbox5_registroPartido);
        }

        // Permitir únicamente números al escribir la fecha.
        private void txtbox2_registroPartido_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Permitir únicamente números al escribir la hora.
        private void txtbox3_registroPartido_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Formatear automáticamente la fecha como dd/mm/aaaa.
        private void txtbox2_registroPartido_TextChanged(
            object sender, EventArgs e)
        {
            if (actualizandoFecha)
            {
                return;
            }

            actualizandoFecha = true;

            try
            {
                int posicionOriginal =
                    txtbox2_registroPartido.SelectionStart;

                int digitosAntes = txtbox2_registroPartido.Text
                    .Take(posicionOriginal)
                    .Count(char.IsDigit);

                string numeros = new string(
                    txtbox2_registroPartido.Text
                        .Where(char.IsDigit)
                        .Take(8)
                        .ToArray()
                );

                string fechaFormateada = numeros;

                if (numeros.Length > 2)
                {
                    fechaFormateada =
                        numeros.Substring(0, 2) + "/" +
                        numeros.Substring(2);
                }

                if (numeros.Length > 4)
                {
                    fechaFormateada =
                        numeros.Substring(0, 2) + "/" +
                        numeros.Substring(2, 2) + "/" +
                        numeros.Substring(4);
                }

                txtbox2_registroPartido.Text = fechaFormateada;

                txtbox2_registroPartido.SelectionStart =
                    Math.Min(
                        ObtenerPosicionFormateada(
                            fechaFormateada,
                            digitosAntes
                        ),
                        fechaFormateada.Length
                    );
            }
            finally
            {
                actualizandoFecha = false;
            }
        }

        // Formatear automáticamente la hora como HH:mm.
        private void txtbox3_registroPartido_TextChanged(
            object sender, EventArgs e)
        {
            if (actualizandoHora)
            {
                return;
            }

            actualizandoHora = true;

            try
            {
                int posicionOriginal =
                    txtbox3_registroPartido.SelectionStart;

                int digitosAntes = txtbox3_registroPartido.Text
                    .Take(posicionOriginal)
                    .Count(char.IsDigit);

                string numeros = new string(
                    txtbox3_registroPartido.Text
                        .Where(char.IsDigit)
                        .Take(4)
                        .ToArray()
                );

                string horaFormateada = numeros;

                if (numeros.Length > 2)
                {
                    horaFormateada =
                        numeros.Substring(0, 2) + ":" +
                        numeros.Substring(2);
                }

                txtbox3_registroPartido.Text = horaFormateada;

                txtbox3_registroPartido.SelectionStart =
                    Math.Min(
                        ObtenerPosicionFormateada(
                            horaFormateada,
                            digitosAntes
                        ),
                        horaFormateada.Length
                    );
            }
            finally
            {
                actualizandoHora = false;
            }
        }

        // Calcular la posición del cursor después de agregar
        // automáticamente los separadores.
        private int ObtenerPosicionFormateada(
            string texto,
            int cantidadDigitos)
        {
            if (cantidadDigitos <= 0)
            {
                return 0;
            }

            int encontrados = 0;

            for (int i = 0; i < texto.Length; i++)
            {
                if (char.IsDigit(texto[i]))
                {
                    encontrados++;

                    if (encontrados == cantidadDigitos)
                    {
                        int posicion = i + 1;

                        if (posicion < texto.Length &&
                            !char.IsDigit(texto[posicion]))
                        {
                            posicion++;
                        }

                        return posicion;
                    }
                }
            }

            return texto.Length;
        }

        // Consultar los estadios existentes mediante un procedimiento almacenado.
        private void CargarEstadios()
        {
            DataTable tabla = new DataTable();

            tabla.Columns.Add("ID_Estadio", typeof(int));
            tabla.Columns.Add("NombreMostrar", typeof(string));
            tabla.Columns.Add("EsTemporal", typeof(bool));

            try
            {
                // La conexión se realiza desde C#.
                using (SqlConnection conexion =
                       ConexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    // Llamar al procedimiento almacenado.
                    using (SqlCommand comando = new SqlCommand(
                        "dbo.sp_ConsultarEstadios",
                        conexion))
                    {
                        comando.CommandType =
                            CommandType.StoredProcedure;

                        using (SqlDataReader lector =
                               comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                tabla.Rows.Add(
                                    Convert.ToInt32(
                                        lector["ID_Estadio"]
                                    ),
                                    lector["Nombre_Estadio"].ToString(),
                                    false
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron consultar los estadios registrados.\n\n" +
                    ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            // Agregar el estadio que se está capturando actualmente.
            if (registro.TieneEstadio)
            {
                tabla.Rows.Add(
                    ID_ESTADIO_TEMPORAL,
                    registro.NombreEstadio.Trim().ToUpperInvariant() +
                    " (NUEVO - CATÁLOGO ACTUAL)",
                    true
                );
            }

            // Asignar los datos al ComboBox.
            cmbobox1_registroPartido.DataSource = null;

            cmbobox1_registroPartido.DisplayMember =
                "NombreMostrar";

            cmbobox1_registroPartido.ValueMember =
                "ID_Estadio";

            cmbobox1_registroPartido.DataSource = tabla;

            cmbobox1_registroPartido.DropDownStyle =
                ComboBoxStyle.DropDownList;

            // Seleccionar inicialmente el estadio nuevo.
            if (registro.TieneEstadio)
            {
                cmbobox1_registroPartido.SelectedValue =
                    ID_ESTADIO_TEMPORAL;
            }
            else
            {
                cmbobox1_registroPartido.SelectedIndex = -1;
            }
        }

        // Evento de carga del formulario.
        private void pantalla_registroPartido_Load(
            object sender, EventArgs e)
        {
            CargarEstadios();
        }

        // Validar que el texto contenga exclusivamente letras y espacios.
        private bool EsTextoSoloLetras(string texto)
        {
            return !string.IsNullOrWhiteSpace(texto) &&
                   texto.All(c => char.IsLetter(c) || c == ' ') &&
                   texto.Any(char.IsLetter);
        }

        // Validar los datos antes de almacenarlos temporalmente.
        private bool ValidarPartido(
            out PartidoTemporal partido)
        {
            partido = null;

            string equipoVisitante =
                txtbox1_registroPartido.Text.Trim().ToUpperInvariant();

            string fechaTexto =
                txtbox2_registroPartido.Text.Trim();

            string horaTexto =
                txtbox3_registroPartido.Text.Trim();

            string equipoLocal =
                txtbox4_registroPartido.Text.Trim().ToUpperInvariant();

            string estado =
                txtbox5_registroPartido.Text.Trim().ToUpperInvariant();

            // Validar equipo visitante.
            if (!EsTextoSoloLetras(equipoVisitante))
            {
                MessageBox.Show(
                    "El equipo visitante es obligatorio y solamente " +
                    "puede contener letras y espacios.",
                    "Equipo visitante incorrecto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtbox1_registroPartido.Focus();
                return false;
            }

            // Validar equipo local.
            if (!EsTextoSoloLetras(equipoLocal))
            {
                MessageBox.Show(
                    "El equipo local es obligatorio y solamente " +
                    "puede contener letras y espacios.",
                    "Equipo local incorrecto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtbox4_registroPartido.Focus();
                return false;
            }

            // Validar estado.
            if (!EsTextoSoloLetras(estado))
            {
                MessageBox.Show(
                    "El estado es obligatorio y solamente " +
                    "puede contener letras y espacios.",
                    "Estado incorrecto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtbox5_registroPartido.Focus();
                return false;
            }

            // Evitar que el equipo local y visitante sean iguales.
            if (string.Equals(
                equipoLocal,
                equipoVisitante,
                StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "El equipo local y el visitante no pueden ser iguales.",
                    "Equipos incorrectos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtbox4_registroPartido.Focus();
                return false;
            }

            // Validar fecha.
            if (!DateTime.TryParseExact(
                fechaTexto,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime fecha))
            {
                MessageBox.Show(
                    "La fecha no es válida.\n\n" +
                    "Utiliza el formato dd/mm/aaaa.\n" +
                    "Ejemplo: 15/12/2026.",
                    "Fecha incorrecta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtbox2_registroPartido.Focus();
                return false;
            }

            // Validar hora en formato de 24 horas.
            if (!TimeSpan.TryParseExact(
                horaTexto,
                @"hh\:mm",
                CultureInfo.InvariantCulture,
                out TimeSpan hora) ||
                hora < TimeSpan.Zero ||
                hora >= TimeSpan.FromDays(1))
            {
                MessageBox.Show(
                    "La hora no es válida.\n\n" +
                    "Utiliza el formato de 24 horas HH:mm.\n" +
                    "Ejemplo: 20:30.",
                    "Hora incorrecta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtbox3_registroPartido.Focus();
                return false;
            }

            // Validar que la fecha y hora no sean anteriores al momento actual.
            DateTime fechaHoraPartido = fecha.Date.Add(hora);
            DateTime fechaHoraActual = DateTime.Now;

            if (fechaHoraPartido <= fechaHoraActual)
            {
                MessageBox.Show(
                    "No se pueden registrar partidos en fechas u horas pasadas.\n\n" +
                    "El partido debe programarse para un momento posterior al actual.\n\n" +
                    "Fecha y hora actual: " +
                    fechaHoraActual.ToString("dd/MM/yyyy HH:mm") +
                    "\nFecha y hora ingresada: " +
                    fechaHoraPartido.ToString("dd/MM/yyyy HH:mm"),
                    "Fecha y hora incorrectas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                if (fecha.Date < fechaHoraActual.Date)
                {
                    txtbox2_registroPartido.Focus();
                }
                else
                {
                    txtbox3_registroPartido.Focus();
                }

                return false;
            }

            // Validar que exista una selección en el ComboBox.
            if (cmbobox1_registroPartido.SelectedIndex < 0 ||
                cmbobox1_registroPartido.SelectedValue == null)
            {
                MessageBox.Show(
                    "Debes seleccionar un estadio.",
                    "Falta el estadio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cmbobox1_registroPartido.Focus();
                return false;
            }

            int idSeleccionado = Convert.ToInt32(
                cmbobox1_registroPartido.SelectedValue
            );

            // Crear el partido temporal.
            partido = new PartidoTemporal
            {
                EquipoVisitante = equipoVisitante,
                Fecha = fecha,
                Hora = hora,
                EquipoLocal = equipoLocal,
                Estado = estado,

                // null representa el estadio nuevo.
                // Un ID positivo representa un estadio existente.
                IDEstadioSeleccionado =
                    idSeleccionado == ID_ESTADIO_TEMPORAL
                        ? (int?)null
                        : idSeleccionado
            };

            return true;
        }

        // Botón para agregar el partido al registro temporal.
        private void boton1_registroPartido_Click(
            object sender, EventArgs e)
        {
            try
            {
                if (registro.Partidos == null)
                {
                    registro.Partidos =
                        new System.Collections.Generic.List<PartidoTemporal>();
                }

                if (!ValidarPartido(out PartidoTemporal nuevoPartido))
                {
                    return;
                }

                // Evitar registrar exactamente el mismo partido
                // con los mismos equipos, fecha, hora y estadio.
                bool partidoDuplicado = registro.Partidos.Any(p =>
                    string.Equals(
                        p.EquipoLocal,
                        nuevoPartido.EquipoLocal,
                        StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(
                        p.EquipoVisitante,
                        nuevoPartido.EquipoVisitante,
                        StringComparison.OrdinalIgnoreCase) &&
                    p.Fecha.Date == nuevoPartido.Fecha.Date &&
                    p.Hora == nuevoPartido.Hora &&
                    p.IDEstadioSeleccionado ==
                        nuevoPartido.IDEstadioSeleccionado
                );

                if (partidoDuplicado)
                {
                    MessageBox.Show(
                        "Este partido ya fue agregado al catálogo.",
                        "Partido duplicado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                // Agregar el partido al objeto temporal.
                registro.Partidos.Add(nuevoPartido);

                MessageBox.Show(
                    "El partido se agregó correctamente al registro temporal.\n\n" +
                    "Todavía no se ha guardado en SQL Server.\n\n" +
                    "Partidos capturados: " + registro.Partidos.Count,
                    "Partido agregado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Limpiar los campos para registrar otro partido.
                txtbox1_registroPartido.Clear();
                txtbox2_registroPartido.Clear();
                txtbox3_registroPartido.Clear();
                txtbox4_registroPartido.Clear();
                txtbox5_registroPartido.Clear();

                // Mantener la selección del estadio.
                txtbox1_registroPartido.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al agregar el partido.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Botón para regresar al menú de catálogos.
        private void boton2_registroPartido_Click(
            object sender, EventArgs e)
        {
            this.Close();
        }

        // Eventos conservados para las conexiones del diseñador.
        private void label1_registroPartido_Click(
            object sender, EventArgs e)
        {
        }

        private void label2_registroPartido_Click(
            object sender, EventArgs e)
        {
        }

        private void cmbobox1_registroPartido_SelectedIndexChanged(
            object sender, EventArgs e)
        {
        }
    }
}
