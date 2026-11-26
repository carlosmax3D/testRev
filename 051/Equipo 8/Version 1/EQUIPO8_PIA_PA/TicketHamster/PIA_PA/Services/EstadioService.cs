using System;
using System.Collections.Generic;
using System.Linq;

namespace PIA_PA.Entidades.Estadio;

public class EstadioService : IEstadioService
{
    private readonly IEstadioRepository _repository;

    public EstadioService(IEstadioRepository repository)
    {
        _repository = repository;
    }

    public List<Estadio> ObtenerTodos()
        => _repository.ObtenerTodos();

    public Estadio? ObtenerPorId(int estadio_ID)
    {
        if (estadio_ID <= 0)
            throw new ArgumentException(
                "La clave Estadio_ID debe ser mayor que cero.");

        return _repository.ObtenerPorId(estadio_ID);
    }

    public void Insertar(Estadio entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _repository.Insertar(entity);
    }

    public void Actualizar(Estadio entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _repository.Actualizar(entity);
    }

    public void Eliminar(int estadio_ID)
    {
        if (estadio_ID <= 0)
            throw new ArgumentException(
                "La clave Estadio_ID debe ser mayor que cero.");

        _repository.Eliminar(estadio_ID);
    }

    public void InsertarCompleto(
        Estadio estadio,
        IReadOnlyList<AreaRegistro> areas)
    {
        if (estadio == null)
            throw new ArgumentNullException(nameof(estadio));

        if (string.IsNullOrWhiteSpace(estadio.Estadio_Nombre))
            throw new ArgumentException(
                "Debe ingresar el nombre del estadio.");

        if (string.IsNullOrWhiteSpace(estadio.Estadio_Ciudad))
            throw new ArgumentException(
                "Debe ingresar la ciudad del estadio.");

        if (areas == null || areas.Count == 0)
            throw new ArgumentException(
                "Debe registrar al menos un área.");

        if (areas.Any(a =>
            string.IsNullOrWhiteSpace(a.Nombre) ||
            a.Filas <= 0 ||
            a.Columnas <= 0))
        {
            throw new ArgumentException(
                "Todas las áreas deben tener nombre, filas y columnas válidas.");
        }

        int capacidadTotal = 0;

        foreach (AreaRegistro area in areas)
        {
            capacidadTotal = checked(
                capacidadTotal + checked(area.Filas * area.Columnas));
        }

        estadio.Estadio_Capacidad = capacidadTotal;

        _repository.InsertarCompleto(estadio, areas);
    }

    public System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros) => _repository.TablaOperacion(id,parametros);
    public object? ValorOperacion(int id, params (string,object?)[] parametros) => _repository.ValorOperacion(id,parametros);
    public int EjecutarOperacion(int id, params (string,object?)[] parametros) => _repository.EjecutarOperacion(id,parametros);
    public void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion) => _repository.TransaccionOperacion(accion);
}