using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories
{
    public interface IAsientoRepository
    {
        List<Asiento> GetAll();
        Asiento? GetById(int id);
        List<Asiento> GetByZona(int idZona); //Para el comboBox en cascada
        void Add(Asiento asiento);
        void Update(Asiento asiento);
        int ExisteAsiento(int idZona, string fila, int numeroAsiento, int idExcluir = 0);

    }
}
