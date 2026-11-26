using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IEstadioRepository
{
    List<Estadio> Consultar(int idAdministrador);

    int Insertar(Estadio estadio, int idAdministrador);

    // Un MapaEstadio vacío conserva el mapa guardado
    void Actualizar(Estadio estadio, int idAdministrador);

    // Borrado lógico (Activo = 0)
    void Eliminar(int idEstadio, int idAdministrador);

    void Reactivar(int idEstadio, int idAdministrador);

    byte[] ObtenerMapa(int idEstadio, int idUsuario);

    List<Zona> ConsultarZonas(int idEstadio, int idAdministrador);

    int InsertarZona(Zona zona, int asientosPorFila, int idAdministrador);

    void ActualizarZona(Zona zona, int idAdministrador);

    // Borrado lógico (Activo = 0)
    void EliminarZona(int idZona, int idAdministrador);

    void ReactivarZona(int idZona, int idAdministrador);

    List<Asiento> ConsultarAsientos(int idZona, int idAdministrador);
}
