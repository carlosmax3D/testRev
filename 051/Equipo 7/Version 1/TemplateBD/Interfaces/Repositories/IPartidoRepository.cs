using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;


namespace TemplateCSharp.Interfaces.Repositories;

public interface IPartidoRepository
{
    List<Partido> GetAll();
    void Add(Partido partido);
}