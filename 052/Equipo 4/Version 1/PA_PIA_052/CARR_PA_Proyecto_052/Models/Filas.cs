using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class Filas
    {
        [Key] 
        public int IDFila { get; set; }
        [Required]
        public int NumFila { get; set; }
        [Required]
        public int IDZona { get; set; }
    }
}
