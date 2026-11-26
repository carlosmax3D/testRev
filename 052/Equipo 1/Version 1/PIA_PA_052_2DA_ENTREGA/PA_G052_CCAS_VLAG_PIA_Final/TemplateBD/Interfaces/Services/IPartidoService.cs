using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services
{
    public interface IPartidoService
    {
        List<Partido> ObtenerPartidos();
        Partido? ObtenerPartido(int id);
        List<Partido> BuscarPartidos(DateTime? fecha, int? idEstadio, string? nombreEquipo);
        void CrearPartido(Partido partido);
        void ActualizarPartido(Partido partido);
        void ActualizarPartidosTerminados();

    }
}
