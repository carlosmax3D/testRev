using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Services;

public interface IEstadioService
{
    // Se llama antes de leer el archivo, para no cargar en memoria una imagen demasiado grande
    void ValidarTamanoMapa(long bytes);

    // Activos e inactivos
    List<Estadio> ObtenerEstadios();

    // Solo activos, para programar partidos
    List<Estadio> ObtenerEstadiosActivos();

    void RegistrarEstadio(Estadio estadio);

    // Sin mapa nuevo se conserva el actual
    void ActualizarEstadio(Estadio estadio);

    // Borrado lógico: solo un estadio sin partidos programados; sus zonas y asientos se conservan
    void EliminarEstadio(Estadio estadio);

    void ReactivarEstadio(Estadio estadio);

    // Imagen del mapa guardada en la base (también de un estadio inactivo)
    byte[] ObtenerMapa(int idEstadio);

    List<Zona> ObtenerZonas(int idEstadio);

    void RegistrarZona(Zona zona, int asientosPorFila);

    // Cambia nombre y precio base; el cupo no se modifica
    void ActualizarZona(Zona zona);

    // Borrado lógico: solo una zona sin boletos vendidos para partidos programados
    void EliminarZona(Zona zona);

    // El estadio debe estar activo y la zona caber en su capacidad
    void ReactivarZona(Zona zona);

    List<Asiento> ObtenerAsientos(int idZona);
}
