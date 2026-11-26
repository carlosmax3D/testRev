using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class Facturas
    {
        [Key]
        public int FolioFac { get; set; }

        [Required]
        [MaxLength(150)]
        public string EmRazon { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string EmRFC { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string EmDomicilio { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string RecNombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string RecRFC { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string RecDomicilio { get; set; } = string.Empty;

        [Required]
        public TimeSpan HoraEmision { get; set; }

        [Required]
        public DateTime FechaEmision { get; set; }

        [Required]
        [MaxLength(50)]
        public string Metodo_de_Pago { get; set; } = string.Empty;

        [Required]
        public Guid GUID { get; set; }

        [Required]
        public int IDCts { get; set; }
    }
}
