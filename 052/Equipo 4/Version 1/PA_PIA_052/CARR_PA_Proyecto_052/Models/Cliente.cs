using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class Cliente
    {
        [Key] 
        public int IDCts { get; set; }
        [Required]
        [MaxLength(100)]

        public string Nombre { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        public string Domicilio { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string RFC { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]    
        public string Correo { get; set; } = string.Empty;
        public DateTime? FechaNac { get; set; }
        public int? IDUsr { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? IDUsr_ModificadoPor { get; set; }
        public DateTime? FechaHora_Modificacion { get; set; }
    }
}
