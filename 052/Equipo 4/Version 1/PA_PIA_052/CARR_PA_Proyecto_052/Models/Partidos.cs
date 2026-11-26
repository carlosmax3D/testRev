using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class Partidos
    {
        [Key] 
        public int IDEvnt { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public DateTime FechaHora { get; set; }
        [Required]
        public int IDEst { get; set; }
    }
}
