using System.Drawing;
using System.Drawing.Imaging;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class EstadioService : IEstadioService
{
    private const int BytesPorMegabyte = 1024 * 1024;
    private const int MaximoBytesPermitidos = 5 * BytesPorMegabyte;

    // Igual que en sp_Estadio_Insertar y sp_Estadio_Actualizar
    private const int CapacidadMaxima = 200000;

    // Mayor valor que cabe en DECIMAL(10,2)
    private const decimal PrecioMaximo = 99999999.99m;

    private const string SeleccioneEstadio = "Seleccione un estadio de la lista.";
    private const string SeleccioneZona = "Seleccione una zona de la lista.";

    private readonly IEstadioRepository _repository;
    private readonly ISesionUsuario _sesion;

    public EstadioService(IEstadioRepository repository, ISesionUsuario sesion)
    {
        _repository = repository;
        _sesion = sesion;
    }

    public void ValidarTamanoMapa(long bytes)
    {
        if (bytes > MaximoBytesPermitidos)
        {
            throw new ArgumentException($"La imagen del mapa no puede pesar más de {MaximoBytesPermitidos / BytesPorMegabyte} MB.");
        }
    }

    public List<Estadio> ObtenerEstadios()
    {
        return _repository.Consultar(ObtenerAdministrador());
    }

    public List<Estadio> ObtenerEstadiosActivos()
    {
        return ObtenerEstadios().FindAll(estadio => estadio.Activo);
    }

    public void RegistrarEstadio(Estadio estadio)
    {
        if (estadio.MapaEstadio.Length == 0)
        {
            throw new ArgumentException("Seleccione la imagen del mapa del estadio.");
        }

        ValidarEstadio(estadio);
        estadio.IdEstadio = _repository.Insertar(estadio, ObtenerAdministrador());
    }

    public void ActualizarEstadio(Estadio estadio)
    {
        ExigirEstadioActivo(estadio);
        ValidarEstadio(estadio);
        _repository.Actualizar(estadio, ObtenerAdministrador());
    }

    public void EliminarEstadio(Estadio estadio)
    {
        ExigirEstadioActivo(estadio);
        _repository.Eliminar(estadio.IdEstadio, ObtenerAdministrador());
    }

    public void ReactivarEstadio(Estadio estadio)
    {
        EstadoRegistro.ExigirInactivo(estadio, estadio?.IdEstadio ?? 0, SeleccioneEstadio, "El estadio ya está activo.");
        _repository.Reactivar(estadio!.IdEstadio, ObtenerAdministrador());
    }

    private static void ExigirEstadioActivo(Estadio? estadio)
    {
        EstadoRegistro.ExigirActivo(estadio, estadio?.IdEstadio ?? 0, SeleccioneEstadio,
            "El estadio está inactivo. Reactívelo para modificarlo o eliminarlo.");
    }

    // También de un estadio inactivo: el mapa se conserva con la baja lógica.
    // Lo usan el catálogo de estadios y la venta de boletos, así que basta un usuario con sesión
    public byte[] ObtenerMapa(int idEstadio)
    {
        if (idEstadio <= 0)
        {
            throw new ArgumentException(SeleccioneEstadio);
        }

        if (_sesion.UsuarioActual is null)
        {
            throw new UnauthorizedAccessException("Inicie sesión para consultar el mapa del estadio.");
        }

        byte[] mapa = _repository.ObtenerMapa(idEstadio, _sesion.UsuarioActual.IdUsuario);
        if (mapa.Length == 0 || !EsImagenValida(mapa))
        {
            throw new InvalidOperationException("El mapa guardado de este estadio no es una imagen válida. Un administrador debe elegir otra imagen en Estadios.");
        }

        return mapa;
    }

    public List<Zona> ObtenerZonas(int idEstadio)
    {
        if (idEstadio <= 0)
        {
            throw new ArgumentException(SeleccioneEstadio);
        }

        return _repository.ConsultarZonas(idEstadio, ObtenerAdministrador());
    }

    // El procedimiento crea los asientos de la zona según el cupo y los asientos por fila
    public void RegistrarZona(Zona zona, int asientosPorFila)
    {
        if (zona.IdEstadio <= 0)
        {
            throw new ArgumentException("Seleccione primero un estadio de la lista.");
        }

        ValidarZona(zona);

        if (zona.Cupo <= 0)
        {
            throw new ArgumentException("El cupo debe ser un número entero mayor que cero.");
        }

        if (asientosPorFila <= 0 || asientosPorFila > zona.Cupo)
        {
            throw new ArgumentException("Los asientos por fila deben ser mayores que cero y no superar el cupo.");
        }

        zona.IdZona = _repository.InsertarZona(zona, asientosPorFila, ObtenerAdministrador());
    }

    public void ActualizarZona(Zona zona)
    {
        ExigirZonaActiva(zona);
        ValidarZona(zona);
        _repository.ActualizarZona(zona, ObtenerAdministrador());
    }

    public void EliminarZona(Zona zona)
    {
        ExigirZonaActiva(zona);
        _repository.EliminarZona(zona.IdZona, ObtenerAdministrador());
    }

    public void ReactivarZona(Zona zona)
    {
        EstadoRegistro.ExigirInactivo(zona, zona?.IdZona ?? 0, SeleccioneZona, "La zona ya está activa.");
        _repository.ReactivarZona(zona!.IdZona, ObtenerAdministrador());
    }

    private static void ExigirZonaActiva(Zona? zona)
    {
        EstadoRegistro.ExigirActivo(zona, zona?.IdZona ?? 0, SeleccioneZona,
            "La zona está inactiva. Reactívela para modificarla o eliminarla.");
    }

    private static void ValidarZona(Zona zona)
    {
        zona.NombreZona = zona.NombreZona.Trim();

        if (!Validador.TieneLetrasODigitos(zona.NombreZona))
        {
            throw new ArgumentException("Capture el nombre de la zona, sin emojis ni símbolos especiales.");
        }

        if (zona.NombreZona.Length > Longitudes.NombreZona)
        {
            throw new ArgumentException($"El nombre de la zona no puede superar los {Longitudes.NombreZona} caracteres.");
        }

        if (zona.PrecioBase <= 0 || zona.PrecioBase > PrecioMaximo)
        {
            throw new ArgumentException($"El precio base debe ser mayor que cero y no superar {Formato.Moneda(PrecioMaximo)}.");
        }

        if (decimal.Round(zona.PrecioBase, 2) != zona.PrecioBase)
        {
            throw new ArgumentException("El precio base admite como máximo dos decimales.");
        }
    }

    public List<Asiento> ObtenerAsientos(int idZona)
    {
        if (idZona <= 0)
        {
            throw new ArgumentException("Seleccione una zona.");
        }

        return _repository.ConsultarAsientos(idZona, ObtenerAdministrador());
    }

    // Un mapa vacío solo se permite al modificar: conserva el guardado
    private void ValidarEstadio(Estadio estadio)
    {
        estadio.Nombre = estadio.Nombre.Trim();
        estadio.Ciudad = estadio.Ciudad.Trim();

        if (!Validador.TieneLetrasODigitos(estadio.Nombre) || !Validador.TieneLetrasODigitos(estadio.Ciudad))
        {
            throw new ArgumentException("Capture el nombre y la ciudad del estadio, sin emojis ni símbolos especiales.");
        }

        if (estadio.Nombre.Length > Longitudes.NombreEstadio)
        {
            throw new ArgumentException($"El nombre del estadio no puede superar los {Longitudes.NombreEstadio} caracteres.");
        }

        if (estadio.Ciudad.Length > Longitudes.CiudadEstadio)
        {
            throw new ArgumentException($"La ciudad no puede superar los {Longitudes.CiudadEstadio} caracteres.");
        }

        if (estadio.CapacidadTotal <= 0 || estadio.CapacidadTotal > CapacidadMaxima)
        {
            throw new ArgumentException($"La capacidad debe ser un número entero mayor que cero y no superar {CapacidadMaxima:N0}.");
        }

        if (estadio.MapaEstadio.Length > 0)
        {
            ValidarTamanoMapa(estadio.MapaEstadio.Length);

            if (!EsImagenValida(estadio.MapaEstadio))
            {
                throw new ArgumentException("El archivo del mapa no es una imagen PNG, JPG o BMP válida.");
            }
        }
    }

    // Revisa que el contenido sea una imagen PNG, JPG o BMP
    private static bool EsImagenValida(byte[] datos)
    {
        try
        {
            using MemoryStream flujo = new(datos);
            using Image imagen = Image.FromStream(flujo, useEmbeddedColorManagement: false, validateImageData: true);
            return imagen.RawFormat.Equals(ImageFormat.Png)
                || imagen.RawFormat.Equals(ImageFormat.Jpeg)
                || imagen.RawFormat.Equals(ImageFormat.Bmp);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is OutOfMemoryException || ex is System.Runtime.InteropServices.ExternalException)
        {
            return false;
        }
    }

    private int ObtenerAdministrador()
    {
        if (_sesion.UsuarioActual == null || !_sesion.UsuarioActual.EsAdministrador)
        {
            throw new UnauthorizedAccessException("Solo un administrador puede gestionar estadios, zonas y asientos.");
        }

        return _sesion.UsuarioActual.IdUsuario;
    }
}
