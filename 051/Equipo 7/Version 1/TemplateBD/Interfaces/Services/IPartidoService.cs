using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IPartidoService
{
    List<Partido> ObtenerPartidos();
    void RegistrarPartido(Partido partido);
}