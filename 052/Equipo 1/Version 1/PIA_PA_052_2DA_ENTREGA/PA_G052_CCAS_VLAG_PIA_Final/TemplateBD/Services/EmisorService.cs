using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services
{
    public class EmisorService : IEmisorService
    {
        private readonly IEmisorRepository _repository;

        public EmisorService(IEmisorRepository repository)
        {
            _repository = repository;
        }

        public List<Emisor> ObtenerEmisores() => _repository.GetAll();
    }
}
