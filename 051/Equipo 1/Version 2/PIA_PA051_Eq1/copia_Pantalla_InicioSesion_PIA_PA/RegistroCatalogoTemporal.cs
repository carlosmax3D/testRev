using System;
using System.Collections.Generic;

namespace Pantalla_RegistroCatalogos_PIA_PA
{
    public class RegistroCatalogoTemporal
    {
        public string NombreEstadio { get; set; }

        public string Ciudad { get; set; }

        public int Capacidad { get; set; }

        public string RutaMapa { get; set; }

        public bool TieneEstadio
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NombreEstadio);
            }
        }

        public List<ZonaTemporal> Zonas { get; set; } =
            new List<ZonaTemporal>();

        public List<PartidoTemporal> Partidos { get; set; } =
            new List<PartidoTemporal>();
    }

    public class ZonaTemporal
    {
        public string Nombre { get; set; }

        public double Precio { get; set; }

        public int Cupo { get; set; }

        public List<FilaTemporal> Filas { get; set; } =
            new List<FilaTemporal>();
    }

    public class FilaTemporal
    {
        public string Nombre { get; set; }

        public int CantidadAsientos { get; set; }
    }

    // Representa un partido que todavía no se ha guardado en SQL Server.
    public class PartidoTemporal
    {
        public string EquipoVisitante { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public string EquipoLocal { get; set; }

        public string Estado { get; set; }

        // Si es null, se utilizará el ID del estadio nuevo.
        // Si tiene un valor, corresponde a un estadio ya registrado.
        public int? IDEstadioSeleccionado { get; set; }
    }
}