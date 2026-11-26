using CARR_PA_Proyecto_052.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CARR_PA_Proyecto_052.Repositorios
{
    public class EstadoPartidos
    {
        public void ActualizarEstadosPartidos()
        {
            using (var db = new InventarioContext())
            {
                var ahora = DateTime.Now;

                var partidosVencidos = db.Partidos
                    .Where(p => p.Estado == "Programado" && p.FechaHora < ahora)
                    .ToList();

                if (partidosVencidos.Any())
                {
                    foreach (var partido in partidosVencidos)
                    {
                        partido.Estado = "Terminado";
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}
