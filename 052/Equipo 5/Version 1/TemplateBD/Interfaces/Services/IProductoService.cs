using TemplateCSharp.Models;
namespace TemplateCSharp.Interfaces.Services;

public interface IProductoService
{
    List<Producto> ObtenerProductos();
    Producto? ObtenerProducto(int id);
    void CrearProducto(Producto producto);
    void ActualizarProducto(Producto producto);
    void EliminarProducto(int id);
}
