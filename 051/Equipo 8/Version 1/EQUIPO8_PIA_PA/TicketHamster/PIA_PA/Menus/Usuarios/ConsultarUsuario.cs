using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PIA_PA.Entidades.Usuario;
using PIA_PA.Menus;

namespace PIA_PA
{
    public partial class ConsultarUsuario : UserControl
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
            [nameof(Usuario.Usuario_Email)] = "Email",
            [nameof(Usuario.Contrasena_ID)] = "Contraseña ID"
        };

        public ConsultarUsuario(IUsuarioService service)
        {
            InitializeComponent();
            _service = service ?? throw new ArgumentNullException(nameof(service));
            CargarDatos();
            btnActualizar.Click += (_, _) => CargarDatos();
        }

        private void CargarDatos()
        {
            try { ConsultaGridHelper.Cargar(gridCatalogo, cBoxConsultarPartidos, _service.ObtenerTodos(), Encabezados); }
            catch (Exception ex) { MessageBox.Show("Error al cargar usuarios: " + ex.Message); }
        }

        private void btnConsultarPartidos_Click(object sender, EventArgs e) =>
            ConsultaGridHelper.Buscar(gridCatalogo, cBoxConsultarPartidos, txtBoxConsultarPartidos.Text);
    }
}
