using CARR_PA_Proyecto_052.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052.Forms
{
    public partial class RegEstadio : Form
    {
        private string archivo = "";
        private void ValidarCamposRegistro()
        {
            bool camposCompletos = !string.IsNullOrWhiteSpace(cajatxtNom.Text) &&
                    !string.IsNullOrWhiteSpace(cajatxtCd.Text) &&
                    !string.IsNullOrWhiteSpace(cajatxtCapTtl.Text);

            bool esNumeroCapacidad = int.TryParse(cajatxtCapTtl.Text, out int capacidad) && capacidad > 0;

            bool tieneMapa = !string.IsNullOrWhiteSpace(archivo);

            btnConfirmar.Enabled = camposCompletos && esNumeroCapacidad && tieneMapa;
        }

        private string GuardarCopiaLocal(string rutaOriginal)
        {
            string carpetaDestino = Path.Combine(Application.StartupPath, "Recursos");

            if (!Directory.Exists(carpetaDestino))
            {
                Directory.CreateDirectory(carpetaDestino);
            }

            string extension = Path.GetExtension(rutaOriginal);
            string nombreUnico = Guid.NewGuid().ToString() + extension;

            string rutaCompletaDestino = Path.Combine(carpetaDestino, nombreUnico);

            File.Copy(rutaOriginal, rutaCompletaDestino, true);

            return Path.Combine("Recursos", nombreUnico);
        }

        private void MostrarImagenSinBloquear(string ruta)
        {
            string rutaFinal = Path.IsPathRooted(ruta) ? ruta : Path.Combine(Application.StartupPath, ruta);

            if (File.Exists(rutaFinal))
            {
                using (FileStream stream = new FileStream(rutaFinal, FileMode.Open, FileAccess.Read))
                {
                    pBMapa.Image = new Bitmap(stream);
                }
            }
        }
        private string nombreUsuarioInSesion;
        public RegEstadio(string nombre)
        {
            InitializeComponent();
            nombreUsuarioInSesion = nombre;

        }

        private void btnSelectMap_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofdMapa = new OpenFileDialog())
            {
                ofdMapa.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofdMapa.ShowDialog() == DialogResult.OK)
                {
                    archivo = ofdMapa.FileName;
                    MostrarImagenSinBloquear(archivo);
                    ValidarCamposRegistro();
                }
            }
        }

        private void RegEstadio_Load(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = false;

            cajatxtNom.TextChanged += (s, ev) => ValidarCamposRegistro();
            cajatxtCd.TextChanged += (s, ev) => ValidarCamposRegistro();
            cajatxtCapTtl.TextChanged += (s, ev) => ValidarCamposRegistro();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(cajatxtCapTtl.Text, out int numeroCapTtl))
            {
                MessageBox.Show("Solo puede ingresar numeros enteros", "Numero de Capacidad Total Invalido");
                return;
            }

            if (numeroCapTtl <= 0)
            {
                MessageBox.Show("Solo puede ingresar valores mayores a 0", "Numero de Capacidad Total Invalido");
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Están los datos del estadio correctos?", "Confirmación", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    int capacidad = int.Parse(cajatxtCapTtl.Text);

                    string rutaRelativaParaBD = GuardarCopiaLocal(archivo);

                    RegistrarEstadio regEst = new RegistrarEstadio();
                    regEst.RegistrarEst(nombreUsuarioInSesion, cajatxtNom.Text, cajatxtCd.Text, capacidad, rutaRelativaParaBD);

                    MessageBox.Show("Estadio registrado exitosamente.", "Éxito", MessageBoxButtons.OK);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al registrar el estadio:\n" + ex.Message, "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
