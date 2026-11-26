using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PIA_PA.Entidades.Cliente;
using PIA_PA.Menus;

namespace PIA_PA
{
    public partial class ConsultarCliente : UserControl
    {
        private readonly IClienteService _service;
        private static readonly Dictionary<string, string> Encabezados = new()
        {
            [nameof(Entidades.Cliente.Cliente.Cliente_ID)] = "ID",
            [nameof(Entidades.Cliente.Cliente.Cliente_RFC)] = "RFC",
            [nameof(Entidades.Cliente.Cliente.Cliente_Email)] = "Email",
            [nameof(Entidades.Cliente.Cliente.Cliente_Fecha_Nacimiento)] = "Fecha nacimiento",
            [nameof(Entidades.Cliente.Cliente.Cliente_Nombre)] = "Nombre",
            [nameof(Entidades.Cliente.Cliente.Cliente_Segundo_Nombre)] = "Segundo nombre",
            [nameof(Entidades.Cliente.Cliente.Cliente_Apellido_Paterno)] = "Apellido paterno",
            [nameof(Entidades.Cliente.Cliente.Cliente_Apellido_Materno)] = "Apellido materno",
            [nameof(Entidades.Cliente.Cliente.Direccion_ID)] = "Dirección ID",
            [nameof(Entidades.Cliente.Cliente.Telefono_ID)] = "Teléfono ID"
        };

        public ConsultarCliente(IClienteService service)
        {
            InitializeComponent();
            _service = service ?? throw new ArgumentNullException(nameof(service));
            CargarDatos();
            btnActualizar.Click += (_, _) => CargarDatos();
        }

        private void CargarDatos()
        {
            try { ConsultaGridHelper.Cargar(gridCatalogo, cBoxConsultarPartidos, _service.ObtenerTodos(), Encabezados); }
            catch (Exception ex) { MessageBox.Show("Error al cargar clientes: " + ex.Message); }
        }

        private void btnConsultarPartidos_Click(object sender, EventArgs e) =>
            ConsultaGridHelper.Buscar(gridCatalogo, cBoxConsultarPartidos, txtBoxConsultarPartidos.Text);
    }
}
