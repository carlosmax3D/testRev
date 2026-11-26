
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PIA_PA.Entidades.Venta_Info;
using PIA_PA.Menus;

namespace PIA_PA
{
    public partial class ConsultarVenta : UserControl
    {
        private readonly IVenta_InfoService _service;
        private List<Venta_Info> _ventas = new();

        public ConsultarVenta(IVenta_InfoService service)
        {
            InitializeComponent();

            _service = service;

            btnActualizar.Click += (_, _) => CargarDatos();
            btnBuscar.Click += (_, _) => Buscar();

            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                _ventas = _service.ObtenerTodos();

                MostrarVentas(_ventas);

                cBoxConsultarVenta.Items.Clear();
                cBoxConsultarVenta.Items.AddRange(new object[]
                {
                    "ID", "Fecha", "Total", "Usuario"
                });

                cBoxConsultarVenta.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al consultar ventas: " + ex.Message);
            }
        }

        private void MostrarVentas(List<Venta_Info> ventas)
        {
            gridCatalogo.DataSource = null;
            gridCatalogo.Columns.Clear();
            gridCatalogo.AutoGenerateColumns = true;
            gridCatalogo.ReadOnly = true;

            gridCatalogo.DataSource = ventas;
        }

        private void Buscar()
        {
            string texto = txtBoxConsultarVenta.Text.Trim();

            if (texto.Length == 0)
            {
                MostrarVentas(_ventas);
                return;
            }

            string campo =
                cBoxConsultarVenta.SelectedItem?.ToString() ?? "";

            var resultado = _ventas.Where(v =>
                campo switch
                {
                    "ID" => v.Venta_Info_ID.ToString().Contains(texto),
                    "Fecha" => v.Venta_Info_Fecha.ToString().Contains(texto),
                    "Total" => v.Venta_Info_Total.ToString().Contains(texto),
                    "Usuario" => v.Usuario_ID.ToString().Contains(texto),
                    _ => false
                }).ToList();

            MostrarVentas(resultado);
        }

        private void btnConsultarPartidos_Click(
            object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
