using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;


namespace TemplateCSharp.Services;

public class EstadioService : IEstadioService
{
    private readonly IEstadioRepository _repository;

    public EstadioService(IEstadioRepository repository)
    {
        _repository = repository;
    }

    public List<Estadio> ObtenerEstadios() => _repository.GetAll();

    public void RegistrarEstadio(Estadio estadio)
    {
        if (string.IsNullOrWhiteSpace(estadio.Nombre))
            throw new ArgumentException("El nombre del estadio es obligatorio.");

        if (string.IsNullOrWhiteSpace(estadio.Ciudad))
            throw new ArgumentException("La ciudad del estadio es obligatoria.");

        if (string.IsNullOrWhiteSpace(estadio.Direccion))
            throw new ArgumentException("La dirección del estadio es obligatoria.");

        if (estadio.Capacidad <= 0)
            throw new ArgumentException("La capacidad total debe ser mayor a cero.");

        if (string.IsNullOrWhiteSpace(estadio.MapaEstadio))
            throw new ArgumentException("Debe indicar el mapa o ruta de imagen del estadio.");

        estadio.UsuarioAuditoria = SesionActual.UsuarioLogueado?.Correo ?? "Sistema";
        _repository.Add(estadio);
    }
}