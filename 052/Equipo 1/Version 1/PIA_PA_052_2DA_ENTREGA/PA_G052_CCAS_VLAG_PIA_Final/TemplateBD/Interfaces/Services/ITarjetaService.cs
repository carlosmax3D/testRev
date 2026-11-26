using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Interfaces.Services
{
    public interface ITarjetaService
    {
        bool ValidarNumeroTarjeta(string numero);
        string DetectarMarca(string numero);
    }
}
