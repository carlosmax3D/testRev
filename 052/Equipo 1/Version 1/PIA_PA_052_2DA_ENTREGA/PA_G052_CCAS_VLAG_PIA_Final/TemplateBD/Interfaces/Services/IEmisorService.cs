using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services
{
    public interface IEmisorService
    {
        List<Emisor> ObtenerEmisores();
    }
}
