using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Forms;
using CARR_PA_Proyecto_052.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CARR_PA_Proyecto_052.Repositorios
{
    public class RegistrarVenta
    {
        public Guid RegistrarVentCompleta(string nomUsuario,int idCliente, int idEvnt, int idZona, int idFila, List<int> idsAsientos) 
        {
            int idUsuarioActual = 0;

            using (var db = new InventarioContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    decimal precioBase = db.Zonas
                        .Where(Z => Z.IDZona == idZona)
                        .Select(Z => Z.PrecioBase)
                        .FirstOrDefault();

                    decimal subtotal = precioBase * idsAsientos.Count;

                    var checarUsuario = db.Usuarios
                        .Where(p => p.Nombre == nomUsuario)
                        .FirstOrDefault();
                    if (checarUsuario != null)
                    {
                        idUsuarioActual = checarUsuario.IDUsr;
                    }

                    var nuevaVenta = new Ventas
                    {
                        Subtotal = subtotal,
                        IDUsr = idUsuarioActual,
                        FechaHoraVent = DateTime.Now
                    };

                    db.Ventas.Add(nuevaVenta);
                    db.SaveChanges();

                    var ventaRealizada = new VentasRealizadas
                    {
                        GUID = nuevaVenta.GUID,
                        IDCts = idCliente
                    };

                    db.VentasRealizadas.Add(ventaRealizada);

                    foreach (int idAsiento in idsAsientos)
                    {
                        var detalle = new DetalleVentas
                        {
                            GUID = nuevaVenta.GUID,
                            IDEvnt = idEvnt,
                            IDZona = idZona,
                            IDFila = idFila,
                            IDAsiento = idAsiento
                        };
                        db.DetalleVentas.Add(detalle);
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    return nuevaVenta.GUID;
                }
                catch (Exception ex) 
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
