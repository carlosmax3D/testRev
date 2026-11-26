using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class DetalleVentas
    {
        [Key]
        public int IDDetVent { get; set; }

        [Required]
        public Guid GUID { get; set; }

        [Required]
        public int IDEvnt { get; set; }

        [Required]
        public int IDZona { get; set; }

        [Required]
        public int IDFila { get; set; }

        [Required]
        public int IDAsiento { get; set; }
    }
}
