using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IEstadioRepository
{
    List<Estadio> GetAll();
    void Add(Estadio estadio);
}