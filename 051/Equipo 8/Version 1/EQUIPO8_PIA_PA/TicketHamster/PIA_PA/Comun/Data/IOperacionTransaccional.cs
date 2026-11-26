namespace PIA_PA.Comun.Data;

public interface IOperacionTransaccional
{
    object? SQL(int operacion, params (string, object?)[] parametros);
    int Siguiente(string tabla, string columna);
}
