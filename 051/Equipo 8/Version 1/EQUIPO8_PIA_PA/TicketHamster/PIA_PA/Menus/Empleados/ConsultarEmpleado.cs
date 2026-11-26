using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PIA_PA.Entidades.Usuario;
using PIA_PA.Menus;

namespace PIA_PA
{
    public partial class ConsultarEmpleado : UserControl
    {
        private readonly IUsuarioService _service;
        private static readonly Dictionary<string, string> Encabezados = new()
        {
            [nameof(Usuario.Usuario_ID)] = "ID",
            [nameof(Usuario.Usuario_Nombre)] = "Nombre",
            [nameof(Usuario.Usuario_Nombre_2)] = "Segundo nombre",
            [nameof(Usuario.Usuario_Apellido)] = "Apellido",
            [nameof(Usuario.Usuario_Apellido_2)] = "Segundo apellido",
            [nameof(Usuario.Usuario_Tipo)] = "Tipo",
            [nameof(Usuario.Usuario_Email)] = "Email"
        };

        public ConsultarEmpleado(IUsuarioService service)
        {
            InitializeComponent();
            _service = service ?? throw new ArgumentNullException(nameof(service));
            CargarDatos();
            btnActualizar.Click += (_, _) => CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                var empleados = _service.ObtenerTodos()
                    .Where(u => u.Usuario_Tipo.Contains("operativo", StringComparison.OrdinalIgnoreCase))
                    .ToList();
                ConsultaGridHelper.Cargar(gridCatalogo, cBoxConsultarPartidos, empleados, Encabezados);
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar empleados: " + ex.Message); }
        }

        private void btnConsultarPartidos_Click(object sender, EventArgs e) =>
            ConsultaGridHelper.Buscar(gridCatalogo, cBoxConsultarPartidos, txtBoxConsultarPartidos.Text);
    }
}
