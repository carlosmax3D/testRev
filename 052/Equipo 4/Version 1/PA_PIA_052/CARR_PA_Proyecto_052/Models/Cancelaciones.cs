using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class Cancelaciones
    {
        [Key]
        public int IDCancel { get; set; }

        [Required]
        [MaxLength(255)]
        public string Motivo { get; set; } = string.Empty;

        [Required]
        public DateTime FechaHora { get; set; }

        [Required]
        public int IDUsr { get; set; }

        [Required]
        public Guid GUID { get; set; }
    }
}
