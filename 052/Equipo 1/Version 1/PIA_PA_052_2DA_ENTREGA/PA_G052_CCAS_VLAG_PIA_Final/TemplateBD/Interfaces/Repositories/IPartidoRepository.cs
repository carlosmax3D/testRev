using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories
{
    public interface IPartidoRepository
    {
        List<Partido> GetAll();
        Partido? GetById(int id);
        List<Partido> GetByFiltros(DateTime? fecha, int? idEstadio, string? nombreEquipo); //Para la búsqueda por filtros
        void Add(Partido partido);
        void Update(Partido partido);
        void ActualizarPartidosTerminados();
    }
}
