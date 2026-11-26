
using System;
using System.Data;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Pantalla_HistorialCliente_PIA_PA;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Pantalla_EdicionCliente_PIA_PA
{
    // Formulario que permite consultar, editar y eliminar clientes.
    public partial class pantalla_edicionCliente : Form
    {
        // Guarda la pantalla del historial desde donde se abrió la edición.
        private pantalla_historialCliente historialAnterior;

        // Indica si se está realizando una eliminación.
        private bool eliminarPresionado = false;

        // Indica si se está realizando una edición.
        private bool editarPresionado = false;

        // Guarda los clientes consultados desde la base de datos.
        private DataTable tablaClientes;

        // Guarda el RFC original del cliente seleccionado.
        private string rfcOriginal = string.Empty;

        private int nominaUsuario;

        // Guarda el control de edición actual.
        private TextBox editorActual;

        // Guarda el nombre de la columna que se está editando.
        private string columnaEditorActual = string.Empty;

        // =========================================================
        // CONSTRUCTORES
        // =========================================================

        // Constructor utilizado cuando se abre desde el historial.
        public pantalla_edicionCliente(
            pantalla_historialCliente historial,
            int nomina)
        {
            InitializeComponent();

            historialAnterior = historial;
            nominaUsuario = nomina;
        }

        // Constructor sin parámetros para el diseñador de Windows Forms.
        public pantalla_edicionCliente()
        {
            InitializeComponent();

            historialAnterior = null;
            nominaUsuario = 0;
        }

        // =========================================================
        // EVENTO LOAD
        // =========================================================

        private void pantalla_edicionCliente_Load(
            object sender, EventArgs e)
        {
            // Conectar los eventos de validación.
            datagridview1_edicionCliente.CellValidating -=
                datagridview1_edicionCliente_CellValidating;

            datagridview1_edicionCliente.CellValidating +=
                datagridview1_edicionCliente_CellValidating;

            datagridview1_edicionCliente.EditingControlShowing -=
                datagridview1_edicionCliente_EditingControlShowing;

            datagridview1_edicionCliente.EditingControlShowing +=
                datagridview1_edicionCliente_EditingControlShowing;

            // Cargar los clientes desde la base de datos.
            CargarClientes();

            // Al iniciar, el DataGridView solamente permite consultar.
            ConfigurarSoloLectura();
        }

        // =========================================================
        // CONFIGURAR DATAGRIDVIEW
        // =========================================================

        private void ConfigurarSoloLectura()
        {
            datagridview1_edicionCliente.ReadOnly = true;

            datagridview1_edicionCliente.AllowUserToAddRows = false;

            datagridview1_edicionCliente.AllowUserToDeleteRows = false;

            datagridview1_edicionCliente.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            datagridview1_edicionCliente.MultiSelect = false;

            datagridview1_edicionCliente.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.DisplayedCells;

            datagridview1_edicionCliente.EditMode =
                DataGridViewEditMode.EditProgrammatically;
        }

        // =========================================================
        // CARGAR CLIENTES
        // =========================================================

        private void CargarClientes()
        {
            try
            {
                // Consultar los clientes mediante la clase central.
                tablaClientes = ConexionBD.ObtenerClientesEdicion();

                // Mostrar los datos en el DataGridView.
                datagridview1_edicionCliente.DataSource = tablaClientes;

                // Configurar la tabla.
                ConfigurarSoloLectura();

                // Proteger las columnas que no deben modificarse.
                ProtegerColumnas();

                // Limpiar el RFC seleccionado.
                rfcOriginal = string.Empty;

                editarPresionado = false;
                eliminarPresionado = false;

                if (tablaClientes.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No hay clientes registrados.",
                        "Clientes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los clientes:\n\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================================================
        // PROTEGER RFC Y NÓMINAS
        // =========================================================

        private void ProtegerColumnas()
        {
            string[] columnasProtegidas =
            {
                "RFC_Cliente",
                "Nomina_Ope",
                "Nomina_Admin"
            };

            foreach (DataGridViewColumn columna
                     in datagridview1_edicionCliente.Columns)
            {
                columna.ReadOnly =
                    Array.Exists(
                        columnasProtegidas,
                        nombre => nombre == columna.Name
                    );
            }
        }

        // =========================================================
        // OBTENER VALOR DE UNA CELDA
        // =========================================================

        private string ObtenerTexto(string nombreColumna)
        {
            if (datagridview1_edicionCliente.CurrentRow == null)
            {
                return string.Empty;
            }

            if (!datagridview1_edicionCliente.Columns.Contains(
                nombreColumna))
            {
                return string.Empty;
            }

            object valor = datagridview1_edicionCliente.CurrentRow
                .Cells[nombreColumna].Value;

            return valor == null || valor == DBNull.Value
                ? string.Empty
                : Convert.ToString(valor).Trim();
        }

        // =========================================================
        // EVENTO AL SELECCIONAR UNA CELDA
        // =========================================================

        private void datagridview1_edicionCliente_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow fila =
                datagridview1_edicionCliente.Rows[e.RowIndex];

            if (fila.IsNewRow ||
                !datagridview1_edicionCliente.Columns.Contains(
                    "RFC_Cliente"))
            {
                return;
            }

            object valorRFC = fila.Cells["RFC_Cliente"].Value;

            rfcOriginal = valorRFC == null ||
                          valorRFC == DBNull.Value
                ? string.Empty
                : Convert.ToString(valorRFC).Trim();
        }

        // =========================================================
        // IDENTIFICAR LA COLUMNA QUE SE ESTÁ EDITANDO
        // =========================================================

        private void datagridview1_edicionCliente_EditingControlShowing(
            object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Desconectar el evento anterior para evitar duplicados.
            if (editorActual != null)
            {
                editorActual.KeyPress -= EditorActual_KeyPress;
                editorActual.TextChanged -= EditorActual_TextChanged;
            }

            editorActual = e.Control as TextBox;

            if (editorActual == null)
            {
                return;
            }

            columnaEditorActual =
                datagridview1_edicionCliente.CurrentCell
                .OwningColumn.Name;

            editorActual.KeyPress += EditorActual_KeyPress;
            editorActual.TextChanged += EditorActual_TextChanged;
        }

        // =========================================================
        // VALIDACIÓN DE CARACTERES MIENTRAS SE ESCRIBE
        // =========================================================

        private void EditorActual_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir teclas de control, como Retroceso.
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // NOMBRES Y APELLIDOS: solamente letras y separadores.
            if (EsColumnaNombre(columnaEditorActual))
            {
                if (!char.IsLetter(e.KeyChar) &&
                    e.KeyChar != ' ' &&
                    e.KeyChar != '-' &&
                    e.KeyChar != '\'')
                {
                    e.Handled = true;
                }

                return;
            }

            // TELÉFONOS Y NÚMERO DE VIVIENDA: solamente números.
            if (columnaEditorActual == "Tel_casa" ||
                columnaEditorActual == "Celular" ||
                columnaEditorActual == "Numero_Vivienda")
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }

                int maximo =
                    columnaEditorActual == "Numero_Vivienda" ? 10 : 10;

                if (editorActual.Text.Length >= maximo &&
                    editorActual.SelectionLength == 0)
                {
                    e.Handled = true;
                }

                return;
            }

            // CORREO: no permitir espacios.
            if (columnaEditorActual == "Correoelectronico")
            {
                if (char.IsWhiteSpace(e.KeyChar))
                {
                    e.Handled = true;
                }

                return;
            }

            // DIRECCIONES: letras, números y caracteres habituales.
            if (EsColumnaDireccion(columnaEditorActual))
            {
                string permitidos = " #.,/'-";

                if (!char.IsLetterOrDigit(e.KeyChar) &&
                    !permitidos.Contains(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }
        }

        // =========================================================
        // CONVERTIR NOMBRES A MAYÚSCULAS MIENTRAS SE ESCRIBE
        // =========================================================

        private void EditorActual_TextChanged(object sender, EventArgs e)
        {
            if (!EsColumnaNombre(columnaEditorActual) ||
                editorActual == null)
            {
                return;
            }

            int posicion = editorActual.SelectionStart;

            string textoMayusculas =
                editorActual.Text.ToUpperInvariant();

            if (editorActual.Text != textoMayusculas)
            {
                editorActual.Text = textoMayusculas;

                editorActual.SelectionStart =
                    Math.Min(posicion, editorActual.Text.Length);
            }
        }

        // =========================================================
        // IDENTIFICAR COLUMNAS DE NOMBRES
        // =========================================================

        private bool EsColumnaNombre(string nombre)
        {
            return nombre == "Primer_Nombre" ||
                   nombre == "Segundo_Nombre" ||
                   nombre == "Primer_Apellido" ||
                   nombre == "Segundo_Apellido";
        }

        // =========================================================
        // IDENTIFICAR COLUMNAS DE DIRECCIÓN
        // =========================================================

        private bool EsColumnaDireccion(string nombre)
        {
            return nombre == "Calle" ||
                   nombre == "Colonia" ||
                   nombre == "Ciudad" ||
                   nombre == "Municipio";
        }

        // =========================================================
        // VALIDAR CADA CELDA AL TERMINAR DE EDITARLA
        // =========================================================

        private void datagridview1_edicionCliente_CellValidating(
            object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!editarPresionado || e.RowIndex < 0)
            {
                return;
            }

            string columna =
                datagridview1_edicionCliente.Columns[e.ColumnIndex].Name;

            string valor =
                Convert.ToString(e.FormattedValue)?.Trim() ?? string.Empty;

            // No validar columnas protegidas.
            if (columna == "RFC_Cliente" ||
                columna == "Nomina_Ope" ||
                columna == "Nomina_Admin")
            {
                return;
            }

            string mensaje;

            if (!ValidarCampo(columna, valor, out mensaje))
            {
                e.Cancel = true;

                MessageBox.Show(
                    mensaje,
                    "Dato no válido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        // =========================================================
        // VALIDAR SEGÚN EL CAMPO
        // =========================================================

        private bool ValidarCampo(string columna, string valor, out string mensaje)
        {
            mensaje = string.Empty;

            // NOMBRES Y APELLIDOS.
            if (EsColumnaNombre(columna))
            {
                bool opcional = columna == "Segundo_Nombre";

                if (string.IsNullOrWhiteSpace(valor))
                {
                    if (opcional)
                        return true;

                    mensaje = "Este campo es obligatorio.";
                    return false;
                }

                if (!Regex.IsMatch(
                    valor,
                    @"^[\p{L}]+(?:[ '-][\p{L}]+)*$"))
                {
                    mensaje =
                        "Este campo solamente permite letras, espacios, guiones y apóstrofes.";

                    return false;
                }

                return true;
            }

            // CORREO ELECTRÓNICO.
            if (columna == "Correoelectronico")
            {
                if (string.IsNullOrWhiteSpace(valor))
                {
                    mensaje = "El correo electrónico es obligatorio.";
                    return false;
                }

                if (valor.Contains(" "))
                {
                    mensaje = "El correo electrónico no puede contener espacios.";
                    return false;
                }

                string patronCorreo =
                    @"^[A-Za-z0-9.!#$%&'*+/=?^_`{|}~-]+@" +
                    @"[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?" +
                    @"(?:\.[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?)+$";

                if (!Regex.IsMatch(valor, patronCorreo))
                {
                    mensaje =
                        "Ingresa un correo válido, por ejemplo: usuario@dominio.com.";

                    return false;
                }

                try
                {
                    MailAddress correo = new MailAddress(valor);

                    if (correo.Address != valor)
                    {
                        mensaje = "El correo electrónico no tiene un formato válido.";
                        return false;
                    }
                }
                catch
                {
                    mensaje = "El correo electrónico no tiene un formato válido.";
                    return false;
                }

                return true;
            }

            // CELULAR.
            if (columna == "Celular")
            {
                if (!Regex.IsMatch(valor, @"^[0-9]{10}$"))
                {
                    mensaje =
                        "El celular es obligatorio y debe contener exactamente 10 dígitos.";

                    return false;
                }

                return true;
            }

            // TELÉFONO DE CASA.
            if (columna == "Tel_casa")
            {
                if (string.IsNullOrWhiteSpace(valor))
                    return true;

                if (!Regex.IsMatch(valor, @"^[0-9]{10}$"))
                {
                    mensaje =
                        "El teléfono de casa debe contener exactamente 10 dígitos.";

                    return false;
                }

                return true;
            }

            // NÚMERO DE VIVIENDA.
            if (columna == "Numero_Vivienda")
            {
                if (!Regex.IsMatch(valor, @"^[0-9]{1,10}$"))
                {
                    mensaje =
                        "El número de vivienda debe contener entre 1 y 10 dígitos.";

                    return false;
                }

                return true;
            }

            // DIRECCIONES.
            if (EsColumnaDireccion(columna))
            {
                if (string.IsNullOrWhiteSpace(valor))
                {
                    mensaje = "Este campo es obligatorio.";
                    return false;
                }

                if (!Regex.IsMatch(
                    valor,
                    @"^[\p{L}\p{N} #.,/'-]+$"))
                {
                    mensaje =
                        "Este campo contiene caracteres no permitidos.";

                    return false;
                }

                return true;
            }

            // FECHA DE NACIMIENTO.
            if (columna == "Fecha_nacimiento")
            {
                DateTime fecha;

                if (!DateTime.TryParse(
                    valor,
                    CultureInfo.CurrentCulture,
                    DateTimeStyles.None,
                    out fecha))
                {
                    mensaje = "Ingresa una fecha de nacimiento válida.";
                    return false;
                }

                if (fecha.Date > DateTime.Today || fecha.Year < 1900)
                {
                    mensaje =
                        "La fecha no puede ser futura y debe ser del año 1900 en adelante.";

                    return false;
                }

                return true;
            }

            return true;
        }

        // =========================================================
        // BOTÓN 1: GUARDAR CAMBIOS O CONFIRMAR ELIMINACIÓN
        // =========================================================

        private void boton1_edicionCliente_Click(
            object sender, EventArgs e)
        {
            if (eliminarPresionado)
            {
                EliminarCliente();
                return;
            }

            if (editarPresionado)
            {
                GuardarCambiosCliente();
                return;
            }

            MessageBox.Show(
                "Primero selecciona un cliente y presiona el botón Editar o Eliminar.",
                "Acción requerida",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // =========================================================
        // BOTÓN 2: EDITAR CLIENTE
        // =========================================================

        private void boton2_edicionCliente_Click(
            object sender, EventArgs e)
        {
            if (datagridview1_edicionCliente.CurrentRow == null ||
                datagridview1_edicionCliente.CurrentRow.IsNewRow)
            {
                MessageBox.Show(
                    "Selecciona un cliente antes de editar.",
                    "Seleccionar cliente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            rfcOriginal = ObtenerTexto("RFC_Cliente");

            if (string.IsNullOrWhiteSpace(rfcOriginal))
            {
                MessageBox.Show(
                    "No se pudo obtener el RFC del cliente seleccionado.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            // Activar el modo edición.
            editarPresionado = true;
            eliminarPresionado = false;

            // Permitir editar los datos del cliente.
            datagridview1_edicionCliente.ReadOnly = false;

            datagridview1_edicionCliente.EditMode =
                DataGridViewEditMode.EditOnKeystrokeOrF2;

            // Mantener protegidas las columnas RFC y nóminas.
            ProtegerColumnas();

            MessageBox.Show(
                "Ahora puedes modificar los datos del cliente.\n\n" +
                "Los nombres se convertirán automáticamente a mayúsculas.\n" +
                "Los teléfonos y el número de vivienda solamente aceptan números.\n" +
                "El RFC y las nóminas no pueden modificarse.\n\n" +
                "Cuando termines, presiona Guardar.",
                "Modo de edición",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // =========================================================
        // GUARDAR CAMBIOS DEL CLIENTE
        // =========================================================

        private void GuardarCambiosCliente()
        {
            try
            {
                // Finalizar la edición de la celda actual.
                if (!datagridview1_edicionCliente.EndEdit())
                {
                    return;
                }

                // Actualizar los cambios pendientes en el origen de datos.
                if (datagridview1_edicionCliente.DataSource != null)
                {
                    BindingContext[
                        datagridview1_edicionCliente.DataSource
                    ].EndCurrentEdit();
                }

                // Obtener los datos de la fila seleccionada.
                string primerNombre = ObtenerTexto("Primer_Nombre");
                string segundoNombre = ObtenerTexto("Segundo_Nombre");
                string primerApellido = ObtenerTexto("Primer_Apellido");
                string segundoApellido = ObtenerTexto("Segundo_Apellido");
                string correo = ObtenerTexto("Correoelectronico");
                string telefonoCasa = ObtenerTexto("Tel_casa");
                string celular = ObtenerTexto("Celular");
                string calle = ObtenerTexto("Calle");
                string colonia = ObtenerTexto("Colonia");
                string ciudad = ObtenerTexto("Ciudad");
                string numeroVivienda = ObtenerTexto("Numero_Vivienda");
                string municipio = ObtenerTexto("Municipio");
                string fechaTexto = ObtenerTexto("Fecha_nacimiento");

                // Validar todos los campos antes de guardar.
                string[] columnas =
                {
                    "Primer_Nombre",
                    "Segundo_Nombre",
                    "Primer_Apellido",
                    "Segundo_Apellido",
                    "Correoelectronico",
                    "Tel_casa",
                    "Celular",
                    "Calle",
                    "Colonia",
                    "Ciudad",
                    "Numero_Vivienda",
                    "Municipio",
                    "Fecha_nacimiento"
                };

                foreach (string columna in columnas)
                {
                    string valor = ObtenerTexto(columna);
                    string mensaje;

                    if (!ValidarCampo(columna, valor, out mensaje))
                    {
                        MessageBox.Show(
                            mensaje,
                            "Dato no válido: " + columna,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );

                        if (datagridview1_edicionCliente.Columns.Contains(columna))
                        {
                            datagridview1_edicionCliente.CurrentCell =
                                datagridview1_edicionCliente.CurrentRow
                                .Cells[columna];
                        }

                        return;
                    }
                }

                // Convertir los nombres a mayúsculas.
                primerNombre = primerNombre.ToUpperInvariant();
                segundoNombre = segundoNombre.ToUpperInvariant();
                primerApellido = primerApellido.ToUpperInvariant();
                segundoApellido = segundoApellido.ToUpperInvariant();

                // Convertir y validar la fecha de nacimiento.
                DateTime fechaNacimiento;

                if (!DateTime.TryParse(
                    fechaTexto,
                    CultureInfo.CurrentCulture,
                    DateTimeStyles.None,
                    out fechaNacimiento))
                {
                    MessageBox.Show(
                        "Ingresa una fecha de nacimiento válida.",
                        "Fecha no válida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                // Confirmar la actualización.
                DialogResult confirmacion = MessageBox.Show(
                    "¿Estás seguro de que deseas guardar los cambios del cliente?",
                    "Confirmar actualización",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion != DialogResult.Yes)
                {
                    return;
                }

                // Actualizar mediante la clase central de conexión.
                int filasAfectadas = ConexionBD.ActualizarCliente(
                    rfcOriginal,
                    primerNombre,
                    segundoNombre,
                    primerApellido,
                    segundoApellido,
                    correo.Trim(),
                    telefonoCasa,
                    celular,
                    fechaNacimiento,
                    calle,
                    colonia,
                    ciudad,
                    numeroVivienda,
                    municipio,
                    nominaUsuario
                );

                if (filasAfectadas > 0)
                {
                    MessageBox.Show(
                        "Los datos del cliente se actualizaron correctamente.",
                        "Actualización exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Volver a cargar los datos.
                    CargarClientes();
                }
                else
                {
                    MessageBox.Show(
                        "No se realizaron cambios en el cliente.",
                        "Sin cambios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar los cambios:\n\n" + ex.Message,
                    "Error de actualización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================================================
        // BOTÓN 3: PREPARAR ELIMINACIÓN
        // =========================================================

        private void boton3_edicionCliente_Click(
            object sender, EventArgs e)
        {
            if (datagridview1_edicionCliente.CurrentRow == null ||
                datagridview1_edicionCliente.CurrentRow.IsNewRow)
            {
                MessageBox.Show(
                    "Selecciona un cliente antes de eliminar.",
                    "Seleccionar cliente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            rfcOriginal = ObtenerTexto("RFC_Cliente");

            if (string.IsNullOrWhiteSpace(rfcOriginal))
            {
                MessageBox.Show(
                    "No se pudo obtener el RFC del cliente.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            editarPresionado = false;
            eliminarPresionado = true;

            ConfigurarSoloLectura();

            MessageBox.Show(
                "Has seleccionado eliminar al cliente con RFC: " +
                rfcOriginal +
                "\n\nPresiona el botón Guardar para revisar los registros relacionados y confirmar la eliminación.",
                "Eliminar cliente",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        // =========================================================
        // ELIMINAR CLIENTE
        // =========================================================

        private void EliminarCliente()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(rfcOriginal))
                {
                    MessageBox.Show(
                        "No se encontró el RFC del cliente seleccionado.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }

                // Consultar las cantidades de registros relacionados.
                DataTable relacionados =
                    ConexionBD.ObtenerRelacionadosCliente(rfcOriginal);

                if (relacionados.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron registros relacionados con el cliente.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    return;
                }

                DataRow datos = relacionados.Rows[0];

                // Obtener las cantidades de registros.
                string detalle =
                    "Cliente: " + rfcOriginal + "\n\n" +
                    "Registros que se eliminarán:\n" +
                    "Clientes: " + datos["Clientes"] + "\n" +
                    "Ventas: " + datos["Ventas"] + "\n" +
                    "Facturas: " + datos["Facturas"] + "\n" +
                    "Cancelaciones: " + datos["Cancelaciones"] + "\n" +
                    "Registros relacionados: " + datos["RegistrosTiene"] + "\n" +
                    "Pagos: " + datos["Pagos"] + "\n" +
                    "Tarjetas: " + datos["Tarjetas"] + "\n" +
                    "Créditos: " + datos["Creditos"] + "\n" +
                    "Débitos: " + datos["Debitos"] + "\n" +
                    "Efectivos: " + datos["Efectivos"] + "\n\n" +
                    "Esta acción eliminará permanentemente los registros indicados.\n" +
                    "Los registros compartidos de administradores, operativos, emisores y comprobantes no se eliminarán.\n\n" +
                    "¿Deseas continuar?";

                DialogResult confirmacion = MessageBox.Show(
                    detalle,
                    "Advertencia: eliminación de cliente",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmacion != DialogResult.Yes)
                {
                    eliminarPresionado = false;
                    return;
                }

                // Eliminar mediante el procedimiento almacenado llamado desde ConexionBD.
                ConexionBD.EliminarClienteCompleto(rfcOriginal, nominaUsuario);

                MessageBox.Show(
                    "El cliente y sus registros relacionados se eliminaron correctamente.",
                    "Eliminación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Recargar los clientes.
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible eliminar el cliente:\n\n" + ex.Message,
                    "Error de eliminación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================================================
        // BOTÓN 4: REGRESAR AL HISTORIAL
        // =========================================================

        private void boton4_edicionCliente_Click(
            object sender, EventArgs e)
        {
            // Si existe el historial anterior, volver a mostrarlo.
            if (historialAnterior != null)
            {
                historialAnterior.Show();
                historialAnterior.Activate();
            }

            // Cerrar la pantalla de edición.
            this.Close();
        }
    }
}
