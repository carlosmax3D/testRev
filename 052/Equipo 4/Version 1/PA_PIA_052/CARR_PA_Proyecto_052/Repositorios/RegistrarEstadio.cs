using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CARR_PA_Proyecto_052.Repositorios
{
    public class RegistrarEstadio
    {
        public void RegistrarEst(string nomUsuario, string nombre, string ciudad, int capTotal, string mapa) 
        {
            int idUsuarioActual = 0;

            using (var db = new InventarioContext())
            {
                var checarUsuario = db.Usuarios
                    .Where(p => p.Nombre == nomUsuario)
                    .FirstOrDefault();
                if (checarUsuario != null)
                {
                    idUsuarioActual = checarUsuario.IDUsr;
                }

                var nuevoEstadio = new Estadios
                {
                    Nombre = nombre,
                    Ciudad = ciudad,
                    CapacidadTotal = capTotal,
                    Mapa = mapa,
                    IDUsr = idUsuarioActual
                };

                db.Estadios.Add(nuevoEstadio);
                db.SaveChanges();
            }
        }
    }
}
