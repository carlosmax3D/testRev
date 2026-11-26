using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _repository;

    public ProductoService(IProductoRepository repository)
    {
        _repository = repository;
    }

    public List<Producto> ObtenerProductos()
    {
        return _repository.GetAll();
    }

    public Producto? ObtenerProducto(int id)
    {
        ValidarId(id);

        return _repository.GetById(id);
    }

    public void CrearProducto(Producto producto)
    {
        ValidarProducto(producto);

        _repository.Add(producto);
    }

    public void ActualizarProducto(Producto producto)
    {
        ValidarProducto(producto);

        if (producto.Id <= 0)
        {
            throw new ArgumentException(
                "El ID del producto no es válido.");
        }

        _repository.Update(producto);
    }

    public void EliminarProducto(int id)
    {
        ValidarId(id);

        _repository.Delete(id);
    }

    private static void ValidarProducto(Producto producto)
    {
        if (producto is null)
        {
            throw new ArgumentNullException(nameof(producto));
        }

        if (string.IsNullOrWhiteSpace(producto.Nombre))
        {
            throw new ArgumentException(
                "El nombre del producto es obligatorio.");
        }

        if (producto.Nombre.Length > 100)
        {
            throw new ArgumentException(
                "El nombre del producto no puede superar los 100 caracteres.");
        }

        if (producto.Precio < 0)
        {
            throw new ArgumentException(
                "El precio no puede ser negativo.");
        }
    }

    private static void ValidarId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException(
                "El ID debe ser mayor que cero.",
                nameof(id));
        }
    }
}
