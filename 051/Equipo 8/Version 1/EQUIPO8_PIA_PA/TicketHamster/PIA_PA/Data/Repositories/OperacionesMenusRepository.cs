using System;
using System.Data;
using Microsoft.Data.SqlClient;
using PIA_PA.Comun.Data;

namespace PIA_PA.Comun.Data;

// Acceso compartido a SQL; se utiliza únicamente desde los repositorios.
internal sealed class OperacionesMenusRepository
{
    private readonly DatabaseConnection _conexion;
    public OperacionesMenusRepository(DatabaseConnection conexion) => _conexion = conexion;

    private static void Parametros(SqlCommand comando, (string,object?)[] args)
    {
        foreach (var (nombre, valor) in args)
            comando.Parameters.AddWithValue(nombre, valor ?? DBNull.Value);
    }
    public DataTable Tabla(int operacion, params (string,object?)[] args)
    {
        using var c = _conexion.GetConnection();
        using var cmd = new SqlCommand(SqlCatalogoMenus.Obtener(operacion), c);
        Parametros(cmd,args);
        using var adaptador = new SqlDataAdapter(cmd);
        var tabla = new DataTable();adaptador.Fill(tabla);return tabla;
    }
    public object? Valor(int operacion, params (string,object?)[] args)
    {
        using var c = _conexion.GetConnection();
        using var cmd = new SqlCommand(SqlCatalogoMenus.Obtener(operacion), c);
        Parametros(cmd,args);c.Open();return cmd.ExecuteScalar();
    }
    public int Ejecutar(int operacion, params (string,object?)[] args)
    {
        using var c = _conexion.GetConnection();
        using var cmd = new SqlCommand(SqlCatalogoMenus.Obtener(operacion), c);
        Parametros(cmd,args);c.Open();return cmd.ExecuteNonQuery();
    }
    public void Transaccion(Action<IOperacionTransaccional> accion)
    {
        using var c = _conexion.GetConnection();c.Open();
        using var t = c.BeginTransaction(IsolationLevel.Serializable);
        try { accion(new Contexto(c,t));t.Commit(); }
        catch {t.Rollback();throw;}
    }
    private sealed class Contexto : IOperacionTransaccional
    {
        private readonly SqlConnection _conexion;
        private readonly SqlTransaction _transaccion;
        public Contexto(SqlConnection conexion,SqlTransaction transaccion)
        { _conexion=conexion;_transaccion=transaccion; }
        public object? SQL(int operacion,params (string,object?)[] args)
        {
            using var cmd = new SqlCommand(SqlCatalogoMenus.Obtener(operacion),_conexion,_transaccion);
            Parametros(cmd,args);return cmd.ExecuteScalar();
        }
        public int Siguiente(string tabla,string columna)
        {
            // Lista cerrada de identificadores permitidos: no aceptar nombres del usuario.
            if (!((tabla=="Direccion"&&columna=="Direccion_ID") ||
                  (tabla=="Telefono"&&columna=="Telefono_ID") ||
                  (tabla=="Cliente"&&columna=="Cliente_ID") ||
                  (tabla=="Contrasena"&&columna=="Contrasena_ID") ||
                  (tabla=="Usuario"&&columna=="Usuario_ID") ||
                  (tabla=="Pago"&&columna=="Pago_ID") ||
                  (tabla=="Venta_Info"&&columna=="Venta_Info_ID") ||
                  (tabla=="Ticket"&&columna=="Ticket_ID") ||
                  (tabla=="Cancelacion"&&columna=="Cancelacion_ID")))
                throw new ArgumentException("Tabla o columna no permitida.");
            using var cmd = new SqlCommand($"SELECT ISNULL(MAX([{columna}]),0)+1 FROM [{tabla}] WITH (TABLOCKX,HOLDLOCK)",_conexion,_transaccion);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
