using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class Asientos
    {
        [Key] 
        public int IDAsiento { get; set; }
        [Required]
        public int NumAsiento { get; set; }
        [Required]
        public int IDFila { get; set; }
    }
}
