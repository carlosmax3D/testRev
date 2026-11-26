using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories
{
    public interface IZonaRepository
    {
        List<Zona> GetAll();
        Zona? GetById(int id);
        List<Zona> GetByEstadio(int idEstadio); //ComboBox en cascada
        void Add(Zona zona);
        void Update(Zona zona);
        int ExisteZona(int idEstadio, string nombreZona, int idExcluir = 0);

    }
}
