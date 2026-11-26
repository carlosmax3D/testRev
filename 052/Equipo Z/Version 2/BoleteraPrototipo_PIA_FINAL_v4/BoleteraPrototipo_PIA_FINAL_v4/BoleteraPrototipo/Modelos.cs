using System;
using System.Collections.Generic;

namespace BoleteraPrototipo
{
    // Clase base: los datos que comparten usuarios y clientes
    public abstract class Persona
    {
        public string NombreCompleto;
        public string Correo;
        public DateTime FechaNacimiento = DateTime.Today;

        // Regresa la lista de errores (vacía si todo está bien)
        public virtual List<string> Validar()
        {
            List<string> errores = new List<string>();
            string e = Validacion.Requerido(NombreCompleto, "Nombre completo");
            if (e != null) errores.Add(e);
            e = Validacion.Correo(Correo);
            if (e != null) errores.Add(e);
            if (FechaNacimiento.Date > DateTime.Today) errores.Add("La fecha de nacimiento no puede ser futura.");
            return errores;
        }
    }

    public class Usuario : Persona
    {
        public int Id;
        public string Nomina;
        public string Rol;
        public string Contrasena;   // texto (solo se usa al registrar o cambiar contraseña)

        public override List<string> Validar()
        {
            List<string> errores = base.Validar();
            string e = Validacion.Requerido(Nomina, "Número de nómina");
            if (e != null) errores.Add(e);
            if (Rol != "Administrador" && Rol != "Operativo") errores.Add("Elige el rol del usuario.");
            return errores;
        }
    }

    public class Cliente : Persona
    {
        public int Id;
        public string Domicilio;
        public string Rfc;
        public string TelCasa;
        public string TelCelular;

        public override List<string> Validar()
        {
            List<string> errores = base.Validar();
            string e = Validacion.Requerido(Domicilio, "Domicilio completo");
            if (e != null) errores.Add(e);
            e = Validacion.Rfc(Rfc);
            if (e != null) errores.Add(e);
            e = Validacion.Telefono(TelCasa, false, "El teléfono de casa");
            if (e != null) errores.Add(e);
            e = Validacion.Telefono(TelCelular, true, "El teléfono celular");
            if (e != null) errores.Add(e);
            return errores;
        }
    }
}
