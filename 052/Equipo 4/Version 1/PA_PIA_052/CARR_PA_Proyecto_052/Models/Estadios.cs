using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class Estadios
    {
        [Key] 
        public int IDEst { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Ciudad { get; set; } = string.Empty;
        [Required]
        public int CapacidadTotal { get; set; }
        [Required]
        [MaxLength(255)]
        public string Mapa { get; set; } = string.Empty;
        [Required]
        public int IDUsr { get; set; }
        public DateTime? FechaHoraReg { get; set; }
    }
}
