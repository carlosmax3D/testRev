using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CARR_PA_Proyecto_052.Repositorios
{
    public class CargarPartidosDisponibles
    {
        public void Ejecutar(DataGridView dgvDestino)
        {
            try
            {
                using (var db = new InventarioContext())
                {
                    var partidosDisponibles = (from p in db.Partidos
                                               join e in db.Estadios on p.IDEst equals e.IDEst
                                               where p.FechaHora >= DateTime.Now
                                               select new
                                               {
                                                   p.IDEvnt,
                                                   Estadio = e.Nombre,
                                                   p.FechaHora,
                                                   Equipos = db.EquiposAJugar
                                                               .Where(eq => eq.IDEvnt == p.IDEvnt)
                                                               .Select(eq => eq.Equipo)
                                                               .ToList(),
                                                   AsientosTotales = db.Zonas
                                                                       .Where(z => z.IDEst == e.IDEst)
                                                                       .Sum(z => (int?)z.Cupo) ?? 0,
                                                   AsientosVendidos = db.DetalleVentas
                                                                        .Count(dv => dv.IDEvnt == p.IDEvnt)
                                               }).AsEnumerable().Select(x => new PartidosDisponibles
                                               {
                                                   ID_Partido = x.IDEvnt,
                                                   Estadio = x.Estadio,
                                                   Enfrentamiento = string.Join(" vs ", x.Equipos),
                                                   Fecha_Hora = x.FechaHora,
                                                   Asientos_Totales = x.AsientosTotales,
                                                   Asientos_Vendidos = x.AsientosVendidos,
                                                   Asientos_Disponibles = x.AsientosTotales - x.AsientosVendidos
                                               }).ToList();

                    dgvDestino.DataSource = partidosDisponibles;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los partidos: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}
