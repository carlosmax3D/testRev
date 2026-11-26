using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class Ventas
    {
        [Key] 
        public Guid GUID { get; set; }
        [Required]
        public Decimal Subtotal { get; set; }
        [Required]
        public Decimal IVA { get; set; }
        [Required]
        public Decimal Total { get; set; }
        [Required]
        public int IDUsr { get; set; }
        [Required]
        public DateTime? FechaHoraVent { get; set; }
    }
}
