using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using CARR_PA_Proyecto_052.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace CARR_PA_Proyecto_052.Repositorios
{
    public class RegistrarAdmin
    {
        public void RegistrarAn(string nombreUsuario, string cajatxtUsuNombre, string cajatxtEmail, string cajatxtCont, string cajatxtNumNomina, DateTime dtpFechaNac) 
        {
            int idUsuarioActual = 0;

            using (var db = new InventarioContext())
            {
                var checarUsuario = db.Usuarios
                    .Where(p => p.Nombre == nombreUsuario)
                    .FirstOrDefault();
                if (checarUsuario != null)
                {
                    idUsuarioActual = checarUsuario.IDUsr;
                }
                var nuevoUsuario = new Usuario
                {
                    Rol = "Administrativo",
                    Nombre = cajatxtUsuNombre,
                    Correo = cajatxtEmail,
                    Contrasena = LogicaUsuario.PasswordHelper.generarHash(cajatxtCont),
                    NumNomina = cajatxtNumNomina,
                    FechaNac = dtpFechaNac,
                    FechaHoraReg = DateTime.Now,
                    UsrReg = idUsuarioActual
                };
                db.Usuarios.Add(nuevoUsuario);
                db.SaveChanges();
            }
        }

        public void ActualizarAn(int IDCliente, string nombre, string correo, string contraseñaNueva, string numNomina, DateTime dtpFechaNac, string nombreAdminSesion)
        {
            using (var db = new InventarioContext())
            {
                var usuarioAEditar = db.Usuarios.FirstOrDefault(u => u.IDUsr == IDCliente);

                if (usuarioAEditar != null)
                {
                    int idAdminModifico = 0;
                    var adminActual = db.Usuarios.FirstOrDefault(p => p.Nombre == nombreAdminSesion);
                    if (adminActual != null)
                    {
                        idAdminModifico = adminActual.IDUsr;
                    }
                    usuarioAEditar.Nombre = nombre;
                    usuarioAEditar.Correo = correo;
                    if (!string.IsNullOrEmpty(contraseñaNueva))
                    {
                        usuarioAEditar.Contrasena = LogicaUsuario.PasswordHelper.generarHash(contraseñaNueva);
                    }
                    usuarioAEditar.NumNomina = numNomina;
                    usuarioAEditar.FechaNac = dtpFechaNac;
                    usuarioAEditar.IDUsr_ModificadoPor = idAdminModifico;
                    usuarioAEditar.FechaHora_Modificacion = DateTime.Now;

                    db.SaveChanges();
                }
            }
        }

    }
}
