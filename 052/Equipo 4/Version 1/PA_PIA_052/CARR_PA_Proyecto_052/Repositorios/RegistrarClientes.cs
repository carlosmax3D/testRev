using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CARR_PA_Proyecto_052.Repositorios
{
    public class RegistrarClientes
    {
        public void RegistrarCte(string nomUsuario, string cajatxtNom, string cajatxtDomi, string cajatxtRFC, string cajatxtCorreo, DateTime dtpFechaNac, string cajatxtTelCasa, string cajatxtTelCel) 
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
                var nuevoCliente = new Cliente
                {
                    Nombre = cajatxtNom,
                    Domicilio = cajatxtDomi,
                    RFC = cajatxtRFC,
                    Correo = cajatxtCorreo,
                    FechaNac = dtpFechaNac,
                    IDUsr = idUsuarioActual
                };
                db.Clientes.Add(nuevoCliente);
                db.SaveChanges();

                if (!string.IsNullOrWhiteSpace(cajatxtTelCasa))
                {
                    var telCasa = new TelefonosClientes
                    {
                        IDCts = nuevoCliente.IDCts,
                        Tipo = "Casa",
                        Numero = cajatxtTelCasa
                    };
                    db.TelefonosClientes.Add(telCasa);
                    db.SaveChanges();
                }
                if (!string.IsNullOrWhiteSpace(cajatxtTelCel))
                {
                    var telCelular = new TelefonosClientes
                    {
                        IDCts = nuevoCliente.IDCts,
                        Tipo = "Celular",
                        Numero = cajatxtTelCel
                    };
                    db.TelefonosClientes.Add(telCelular);
                    db.SaveChanges();
                }
            }
        }

        public void ActualizarCte(int idCliente, string nombre, string domicilio, string rfc, string correo, DateTime fechaNac, string telefonoCel, string telefonoCasa, string nombreUsuarioSesion) 
        {
            using (var db = new InventarioContext()) 
            {
                var clienteAEditar = db.Clientes.FirstOrDefault(c => c.IDCts == idCliente);

                if (clienteAEditar != null)
                {
                    int idUsuarioModifico = 0;

                    var usuario = db.Usuarios.FirstOrDefault(u => u.Nombre == nombreUsuarioSesion);
                    if (usuario != null)
                    {
                        idUsuarioModifico = usuario.IDUsr;
                    }

                    clienteAEditar.Nombre = nombre;
                    clienteAEditar.Correo = correo;
                    clienteAEditar.RFC = rfc;
                    clienteAEditar.Domicilio = domicilio;
                    clienteAEditar.FechaNac = fechaNac;

                    var telefonoCelUsuario = db.TelefonosClientes.FirstOrDefault(tc => tc.Tipo == "Celular" && tc.IDCts == idCliente);
                    if (!string.IsNullOrWhiteSpace(telefonoCel))
                    {
                        if (telefonoCelUsuario != null) 
                        { 
                            telefonoCelUsuario.Numero = telefonoCel;
                        }
                        else
                        {
                            db.TelefonosClientes.Add(new TelefonosClientes
                            {
                                IDCts = idCliente,
                                Tipo = "Celular",
                                Numero = telefonoCel
                            });
                        }
                    }
                    else if (telefonoCelUsuario != null)
                    {
                        db.TelefonosClientes.Remove(telefonoCelUsuario);
                    }

                    var telefonoCasaUsuario = db.TelefonosClientes.FirstOrDefault(tc => tc.Tipo == "Casa" && tc.IDCts == idCliente);
                    if (!string.IsNullOrWhiteSpace(telefonoCasa))
                    {
                        if (telefonoCasaUsuario != null)
                        {
                            telefonoCasaUsuario.Numero = telefonoCasa;
                        }
                        else
                        {
                            db.TelefonosClientes.Add(new TelefonosClientes
                            {
                                IDCts = idCliente,
                                Tipo = "Casa",
                                Numero = telefonoCasa
                            });
                        }
                    }
                    else if (telefonoCasaUsuario != null)
                    {
                        db.TelefonosClientes.Remove(telefonoCasaUsuario);
                    }

                    clienteAEditar.IDUsr_ModificadoPor = idUsuarioModifico;
                    clienteAEditar.FechaHora_Modificacion = DateTime.Now;

                    db.SaveChanges();
                }
            }
        }
    }
}
