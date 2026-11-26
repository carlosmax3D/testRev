namespace PIA_PA.Entidades.Pagador;

public class Pagador
{
    public string Pagador_Tipo { get; set; } = string.Empty;
public string Pagador_RFC { get; set; } = string.Empty;
public string Pagador_Nombre_Legal { get; set; } = string.Empty;
public string Pagador_CURP { get; set; } = string.Empty;
public int Pagador_Regimen_Fiscal { get; set; }
public int Pagador_Direccion { get; set; }
}
