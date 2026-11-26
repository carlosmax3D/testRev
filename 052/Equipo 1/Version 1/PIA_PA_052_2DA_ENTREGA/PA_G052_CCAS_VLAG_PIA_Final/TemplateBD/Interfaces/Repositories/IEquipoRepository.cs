using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories
{
    public interface IEquipoRepository
    {
        List<Equipo> GetAll();
        Equipo? GetById(int id);
        void Add(Equipo equipo);
        void Update(Equipo equipo);
        int ExisteEquipo(string nombre, int idExcluir = 0);

    }
}
