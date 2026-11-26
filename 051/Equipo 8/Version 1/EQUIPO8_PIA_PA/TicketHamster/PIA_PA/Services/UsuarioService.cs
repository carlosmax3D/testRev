namespace PIA_PA.Entidades.Usuario;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public List<Usuario> ObtenerTodos() => _repository.ObtenerTodos();

    public Usuario? ObtenerPorId(int usuario_ID)
    {
        if (usuario_ID <= 0) throw new ArgumentException("La clave Usuario_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(usuario_ID);
    }

    public void Insertar(Usuario entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Usuario entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int usuario_ID)
    {
        if (usuario_ID <= 0) throw new ArgumentException("La clave Usuario_ID debe ser mayor que cero.");
        _repository.Eliminar(usuario_ID);
    }

    public bool ValidarCredenciales(string email, string contrasena)
    {
        return _repository.ValidarCredenciales(email, contrasena);
    }

    public System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros) => _repository.TablaOperacion(id,parametros);
    public object? ValorOperacion(int id, params (string,object?)[] parametros) => _repository.ValorOperacion(id,parametros);
    public int EjecutarOperacion(int id, params (string,object?)[] parametros) => _repository.EjecutarOperacion(id,parametros);
    public void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion) => _repository.TransaccionOperacion(accion);
}
