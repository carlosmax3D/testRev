using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;

namespace TemplateCSharp.Forms.Usuarios
{
    public partial class FrmEliminarUsuario : Form
    {
        private readonly IUsuarioService _usuarioService;

        // Constructor para el diseñador de WinForms (NO usar en runtime)
        public FrmEliminarUsuario()
        {
            InitializeComponent();
            _usuarioService = null!;
        }

        // Constructor para inyección de dependencias (runtime)
        public FrmEliminarUsuario(IUsuarioService usuarioService)
        {
            InitializeComponent();
            _usuarioService = usuarioService;
        }

        // Propiedad para recibir el ID desde FrmUsuarios
        public int IdUsuarioAEliminar { get; set; }

        // Evento que el diseñador espera encontrar
        private void FrmEliminarUsuario_Load(object sender, EventArgs e)
        {
            // No se necesita lógica adicional aquí
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (IdUsuarioAEliminar <= 0)
            {
                MessageBox.Show("No se seleccionó un usuario válido.", "Error");
                this.Close();
                return;
            }

            var resultado = _usuarioService.Eliminar(IdUsuarioAEliminar);

            MessageBox.Show(resultado.mensaje,
                resultado.ok ? "Información" : "Error",
                MessageBoxButtons.OK,
                resultado.ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (resultado.ok)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}