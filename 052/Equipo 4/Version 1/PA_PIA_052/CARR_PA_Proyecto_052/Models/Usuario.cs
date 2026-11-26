using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class Usuario
    {
        [Key] 
        public int IDUsr { get; set; }
        [Required]
        [MaxLength(50)]
        public string Rol { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Correo { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string Contrasena { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string NumNomina { get; set; } = string.Empty;
        public int? UsrReg { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public DateTime? FechaNac { get; set; }
        public int? IDUsr_ModificadoPor { get; set; }
        public DateTime? FechaHora_Modificacion { get; set; }
    }
}
