
using System;
using System.Windows.Forms;
using PIA_PA.Comun.Data;

namespace PIA_PA
{
    public partial class Cliente : UserControl
    {
        private readonly DatabaseConnection conexion;

        public Cliente(DatabaseConnection db)
        {
            InitializeComponent();
            conexion = db;

            btnGenerarTXT.Click += (_, _) => Generar();
            btnGuardarTXT.Click += (_, _) => Guardar();

            ConfigurarVistaPrevia();
        }

        private void ConfigurarVistaPrevia()
        {
            txtReporte.Multiline = true;
            txtReporte.ReadOnly = true;
            txtReporte.ScrollBars = ScrollBars.Both;
            txtReporte.WordWrap = false;
        }

        private void Generar()
        {
            try
            {
                GeneradorReportes.MostrarVistaPrevia(txtReporte, CrearTextoReporte());
            }
            catch (Exception ex)
            {
                txtReporte.Clear();
                MessageBox.Show(ex.Message);
            }
        }

        private void Guardar()
        {
            try
            {
                if (!int.TryParse(txtIdCliente.Text.Trim(), out int id) || id <= 0)
                {
                    throw new Exception("Escribe un ID de cliente válido.");
                }

                GeneradorReportes.Exportar(
                    "Historial_Cliente_" + id,
                    CrearTextoReporte()
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string CrearTextoReporte()
        {
            if (!int.TryParse(txtIdCliente.Text.Trim(), out int id) || id <= 0)
                throw new Exception("Escribe un ID de cliente válido.");

            var cliente = GeneradorReportes.Consultar(
                conexion,
                "SELECT * FROM Cliente WHERE Cliente_ID=@id",
                ("@id", id)
            );

            if (cliente.Rows.Count == 0)
                throw new Exception("Cliente no encontrado.");

            var boletos = GeneradorReportes.Consultar(
                conexion,
                @"SELECT
                    t.Ticket_ID,
                    t.Ticket_Estado,
                    t.Partido_ID,
                    t.Asiento_ID,
                    v.Venta_Info_ID,
                    v.Venta_Info_Fecha,
                    v.Venta_Info_Total,
                    v.Pagador_RFC,
                    c.Cancelacion_Fecha,
                    c.Cancelacion_Motivo
                FROM Ticket t
                JOIN Venta_Info v
                    ON t.Venta_Info_ID = v.Venta_Info_ID
                LEFT JOIN Cancelacion c
                    ON c.Ticket_ID = t.Ticket_ID
                WHERE t.Cliente_ID = @id
                ORDER BY v.Venta_Info_Fecha",
                ("@id", id)
            );

            var facturas = GeneradorReportes.Consultar(
                conexion,
                @"SELECT DISTINCT
                    v.Venta_Info_ID,
                    v.Venta_Info_Fecha,
                    v.Venta_Info_Subtotal,
                    v.Venta_Info_IVA,
                    v.Venta_Info_Total,
                    v.Pagador_RFC,
                    p.Pagador_Nombre_Legal,
                    e.Emisor_Nombre_Legal
                FROM Venta_Info v
                JOIN Ticket t
                    ON t.Venta_Info_ID = v.Venta_Info_ID
                JOIN Pagador p
                    ON p.Pagador_RFC = v.Pagador_RFC
                JOIN Emisor e
                    ON e.Emisor_ID = v.Emisor_ID
                WHERE t.Cliente_ID = @id
                ORDER BY v.Venta_Info_ID",
                ("@id", id)
            );

            int cancelados = 0;

            foreach (System.Data.DataRow fila in boletos.Rows)
            {
                if (fila["Cancelacion_Fecha"] != DBNull.Value)
                    cancelados++;
            }

            string texto =
                $"HISTORIAL CLIENTE {id}\n" +
                $"Total boletos: {boletos.Rows.Count}\n" +
                $"Cancelados: {cancelados}\n\n" +
                "DATOS CLIENTE\n" +
                GeneradorReportes.Formatear(cliente) +
                "\nBOLETOS Y CANCELACIONES\n" +
                GeneradorReportes.Formatear(boletos) +
                "\nDATOS DE FACTURACION / VENTAS\n" +
                GeneradorReportes.Formatear(facturas);

            return texto;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
