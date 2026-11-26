using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CARR_PA_Proyecto_052.Repositorios
{
    public class RegistrarPartido
    {
        public int RegistrarP(DateTime fechaPartido, DateTime horaPartido, int idEstadio) 
        {
            DateTime fechaHoraPartido = fechaPartido.Date + horaPartido.TimeOfDay;
            using (var db = new InventarioContext()) 
            {
                var nuevoPartido = new Partidos
                {
                    Estado = "Programado",
                    FechaHora = fechaHoraPartido,
                    IDEst = idEstadio
                };
                db.Partidos.Add(nuevoPartido);
                db.SaveChanges();

                return nuevoPartido.IDEvnt;
            }
        }
    }
}
