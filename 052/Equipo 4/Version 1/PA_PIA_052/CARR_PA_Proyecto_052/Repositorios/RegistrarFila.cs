using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CARR_PA_Proyecto_052.Repositorios
{
    
    public class RegistrarFila
    {
        public void RegistrarFa(int totalFilas, int idZona, int asientosPorFila)
        {
            using (var db = new InventarioContext())
            {
                for (int f = 1; f <= totalFilas; f++)
                {
                    var nuevaFila = new Filas
                    {
                        NumFila = f,
                        IDZona = idZona
                    };
                    db.Filas.Add(nuevaFila);
                    db.SaveChanges();

                    for (int a = 1; a <= asientosPorFila; a++)
                    {
                        var nuevoAsiento = new Asientos 
                        {
                            NumAsiento = a,
                            IDFila = nuevaFila.IDFila
                        };
                        db.Asientos.Add(nuevoAsiento);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
