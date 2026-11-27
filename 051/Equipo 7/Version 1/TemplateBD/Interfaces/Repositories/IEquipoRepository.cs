using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;


namespace TemplateCSharp.Interfaces.Repositories;

public interface IEquipoRepository
{
    List<Equipo> GetAll();
    void Add(Equipo equipo);
}