using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CARR_PA_Proyecto_052.Repositorios
{
    public class RegistrarEquiposAJugar
    {
        public void RegistrarEqAJug(string equipoLocal, string equipoVisitante, int idPartido) 
        {
            using (var db = new InventarioContext()) 
            {
                var nuevoEquipoLocal = new EquiposAJugar
                {
                    Equipo = equipoLocal,
                    IDEvnt = idPartido
                };
                var nuevoEquipoVis = new EquiposAJugar
                {
                    Equipo = equipoVisitante,
                    IDEvnt = idPartido
                };
                db.EquiposAJugar.Add(nuevoEquipoLocal);
                db.EquiposAJugar.Add(nuevoEquipoVis);
                db.SaveChanges();
            }
        }
    }
}
