using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PIA_PA.Comun.Data;

namespace PIA_PA
{
    public partial class Ocupacion : UserControl
    {
        private readonly DatabaseConnection conexion;

        public Ocupacion(DatabaseConnection db)
        {
            InitializeComponent();
            conexion = db;
            label2.Text = "Reporte de ocupación";
            btnGenerarTXT.Click += (_, _) => Generar();
            btnGuardarTXT.Click += (_, _) => Guardar();
            txtReporte.Multiline = true;
            txtReporte.ScrollBars = ScrollBars.Both;
            txtReporte.WordWrap = false;
            txtReporte.ReadOnly = true;
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
                MessageBox.Show("No se pudo generar el reporte: " + ex.Message);
            }
        }

        private void Guardar()
        {
            try
            {
                string contenido = CrearTextoReporte();
                int id = int.Parse(txtIdPartido.Text.Trim());
                GeneradorReportes.Exportar("Ocupacion_Partido_" + id, contenido);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar el reporte: " + ex.Message);
            }
        }

        private string CrearTextoReporte()
        {
                if (!int.TryParse(txtIdPartido.Text.Trim(), out int id) || id <= 0)
                    throw new Exception("Ingresa un ID de partido válido.");

                var partido = GeneradorReportes.Consultar(conexion, @"
                    SELECT p.Partido_ID AS ID, el.Equipo_Nombre AS Local,
                           ev.Equipo_Nombre AS Visitante, p.Partido_Fecha AS Fecha,
                           p.Partido_Hora AS Hora, e.Estadio_Nombre AS Estadio,
                           e.Estadio_Capacidad AS Total
                    FROM Partido p
                    JOIN Equipos el ON el.Equipo_ID = p.Equipo_ID_Local
                    JOIN Equipos ev ON ev.Equipo_ID = p.Equipo_ID_Visitante
                    JOIN Estadio e ON e.Estadio_ID = p.Estadio_ID
                    WHERE p.Partido_ID = @id", ("@id", id));

                if (partido.Rows.Count == 0)
                    throw new Exception("No existe un partido con ese ID.");

                var areas = GeneradorReportes.Consultar(conexion, @"
                    SELECT a.Area_ID AS ID, a.Area_Nombre AS Area,
                           a.Area_Capacidad AS Total,
                           (SELECT COUNT(*) FROM Ticket t
                            JOIN Asiento s ON s.Asiento_ID = t.Asiento_ID
                            WHERE t.Partido_ID = @id AND s.Area_ID = a.Area_ID
                              AND t.Ticket_Estado NOT IN ('Cancelado', 'Cancelada')
                              AND NOT EXISTS (SELECT 1 FROM Cancelacion c WHERE c.Ticket_ID = t.Ticket_ID)) AS Ocupados
                    FROM Area a
                    JOIN Partido p ON p.Estadio_ID = a.Estadio_ID
                    WHERE p.Partido_ID = @id
                    ORDER BY a.Area_ID", ("@id", id));

                // Contar también boletos sin asiento asignado para mantener el total del partido.
                var conteo = GeneradorReportes.Consultar(conexion, @"
                    SELECT COUNT(*) AS Ocupados FROM Ticket t
                    WHERE t.Partido_ID = @id
                      AND t.Ticket_Estado NOT IN ('Cancelado', 'Cancelada')
                      AND NOT EXISTS (SELECT 1 FROM Cancelacion c WHERE c.Ticket_ID = t.Ticket_ID)", ("@id", id));

                var info = partido.Rows[0];
                int total = Convert.ToInt32(info["Total"]);
                int ocupados = Convert.ToInt32(conteo.Rows[0]["Ocupados"]);
                int disponibles = total - ocupados;
                var sb = new StringBuilder();
                sb.AppendLine("REPORTE DE OCUPACIÓN");
                sb.AppendLine("====================");
                sb.AppendLine($"Total de asientos: {total}");
                sb.AppendLine($"Asientos ocupados: {ocupados}");
                sb.AppendLine($"Asientos disponibles: {disponibles}");
                sb.AppendLine();
                sb.AppendLine("INFORMACIÓN DEL PARTIDO");
                sb.AppendLine($"ID: {id}");
                sb.AppendLine($"Local: {info["Local"]}");
                sb.AppendLine($"Visitante: {info["Visitante"]}");
                sb.AppendLine($"Fecha: {Convert.ToDateTime(info["Fecha"]):dd/MM/yyyy}");
                sb.AppendLine($"Hora: {info["Hora"]}");
                sb.AppendLine($"Estadio: {info["Estadio"]}");
                sb.AppendLine();
                sb.AppendLine("OCUPACIÓN POR ÁREA");
                sb.AppendLine("==================");
                foreach (DataRow area in areas.Rows)
                {
                    int capacidad = Convert.ToInt32(area["Total"]);
                    int vendidos = Convert.ToInt32(area["Ocupados"]);
                    sb.AppendLine($"Área {area["ID"]}: {area["Area"]}");
                    sb.AppendLine($"  Asientos totales: {capacidad}");
                    sb.AppendLine($"  Asientos ocupados: {vendidos}");
                    sb.AppendLine($"  Asientos libres: {capacidad - vendidos}");
                    sb.AppendLine();
                }
                if (areas.Rows.Count == 0) sb.AppendLine("No hay áreas registradas para este estadio.");
                return sb.ToString();
        }
        private void label1_Click(object sender, EventArgs e) { }
    }
}
