using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class VentasRealizadas
    {
        [Key]
        public int IDVentRea { get; set; }

        [Required]
        public int IDCts { get; set; }

        [Required]
        public Guid GUID { get; set; }
    }
}
