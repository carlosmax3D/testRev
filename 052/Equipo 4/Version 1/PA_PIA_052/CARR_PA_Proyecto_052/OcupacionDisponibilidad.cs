using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CARR_PA_Proyecto_052
{
    [Table("Vista_OcupacionDisponibilidad")]
    public class OcupacionDisponibilidad
    {
        public int ID_Evento { get; set; }
        public string Zona { get; set; } = string.Empty;
        public decimal Precio_Base { get; set; }
        public int Asientos_Total { get; set; }
        public int Asientos_Vendidos { get; set; }
        public int Asientos_Disponibles { get; set; }
        public decimal Porcentaje_Ocupacion { get; set; }
    }
}
