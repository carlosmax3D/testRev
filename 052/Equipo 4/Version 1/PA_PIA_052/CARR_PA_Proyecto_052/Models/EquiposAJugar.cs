using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class EquiposAJugar
    {
        [Key]
        public int IDEqJug { get; set; }

        [Required]
        public int IDEvnt { get; set; }

        [Required]
        [MaxLength(100)]
        public string Equipo { get; set; } = string.Empty;
    }
}
