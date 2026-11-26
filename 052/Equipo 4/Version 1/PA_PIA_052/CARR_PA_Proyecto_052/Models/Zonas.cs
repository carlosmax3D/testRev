using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class Zonas
    {
        [Key] 
        public int IDZona { get; set; }
        [Required]
        [MaxLength(100)]
        public string NomZona { get; set; } = string.Empty;
        [Required]
        public Decimal PrecioBase { get; set; }
        [Required]
        public int Cupo { get; set; }
        [Required]
        public int IDEst { get; set; }
    }
}
