using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CARR_PA_Proyecto_052.Repositorios
{
    public class RegistrarZona
    {
        public void RegistrarZn(string nombreZn, decimal precioBase, int cupo, int idEst)
        {
            using (var db = new InventarioContext())
            {
                var nuevaZona = new Zonas
                {
                    NomZona = nombreZn,
                    PrecioBase = precioBase,
                    Cupo = cupo,
                    IDEst = idEst

                };
                db.Zonas.Add(nuevaZona);
                db.SaveChanges();
            }
        }
    }
}
