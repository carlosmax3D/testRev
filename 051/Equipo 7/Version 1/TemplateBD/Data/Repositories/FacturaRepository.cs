using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;


namespace TemplateCSharp.Data.Repositories;

public class FacturaRepository : IFacturaRepository
{
    private readonly DatabaseConnection _database;

    public FacturaRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public List<CompraResumen> ListarCompras()
    {
        List<CompraResumen> lista = [];
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ListarComprasFacturas", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new CompraResumen
            {
                IdCompra = reader.GetInt32(reader.GetOrdinal("idCompra")),
                FolioCompra = reader.GetString(reader.GetOrdinal("folioCompra")),
                FolioFactura = reader.GetString(reader.GetOrdinal("folioFactura")),
                FechaCompra = reader.GetString(reader.GetOrdinal("fechaCompra")),
                Cliente = reader.GetString(reader.GetOrdinal("cliente")),
                Total = reader.GetDecimal(reader.GetOrdinal("total")),
                MetodoPago = reader.GetString(reader.GetOrdinal("metodoPago")),
                EstadoCompra = reader.GetString(reader.GetOrdinal("estadoCompra")),
                EstadoFactura = reader.GetString(reader.GetOrdinal("estadoFactura"))
            });
        }

        return lista;
    }

    public FacturaCompleta ObtenerFacturaPorFolio(string folioCompra)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_ObtenerFacturaPorFolio", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@FolioCompra", SqlDbType.VarChar, 50).Value = folioCompra;

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
            throw new InvalidOperationException("No se pudo recuperar la información de la factura.");

        FacturaCompleta factura = new()
        {
            FolioCompra = reader.GetString(reader.GetOrdinal("folioCompra")),
            FolioFactura = reader.GetString(reader.GetOrdinal("folioFactura")),
            FechaFactura = reader.GetDateTime(reader.GetOrdinal("fechaFactura")),
            MetodoPago = reader.GetString(reader.GetOrdinal("metodoPago")),
            EstadoCompra = reader.GetString(reader.GetOrdinal("estadoCompra")),
            EstadoFactura = reader.GetString(reader.GetOrdinal("estadoFactura")),
            Subtotal = reader.GetDecimal(reader.GetOrdinal("subtotal")),
            IVA = reader.GetDecimal(reader.GetOrdinal("iva")),
            Total = reader.GetDecimal(reader.GetOrdinal("total")),
            ReceptorNombre = reader.GetString(reader.GetOrdinal("receptorNombre")),
            ReceptorRFC = reader.GetString(reader.GetOrdinal("receptorRFC")),
            ReceptorDomicilio = reader.GetString(reader.GetOrdinal("receptorDomicilio")),
            Partido = reader.GetString(reader.GetOrdinal("partido")),
            Estadio = reader.GetString(reader.GetOrdinal("estadio")),
            FechaPartido = reader.GetString(reader.GetOrdinal("fechaPartido")),
            Zona = reader.GetString(reader.GetOrdinal("zona"))
        };

        if (reader.NextResult())
        {
            while (reader.Read())
            {
                factura.Detalles.Add(new DetalleFacturaItem
                {
                    Zona = reader.GetString(reader.GetOrdinal("zona")),
                    Asiento = reader.GetString(reader.GetOrdinal("asiento")),
                    Cantidad = reader.GetInt32(reader.GetOrdinal("cantidad")),
                    PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("precioUnitario")),
                    Subtotal = reader.GetDecimal(reader.GetOrdinal("subtotal"))
                });
            }
        }

        return factura;
    }

    public void CancelarCompra(string folioCompra, string motivo, string usuarioAuditoria)
    {
        using SqlConnection connection = _database.CreateConnection();
        using SqlCommand command = new("sp_CancelarCompra", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@FolioCompra", SqlDbType.VarChar, 50).Value = folioCompra;
        command.Parameters.Add("@Motivo", SqlDbType.VarChar, 255).Value = motivo;
        command.Parameters.Add("@UsuarioAuditoria", SqlDbType.VarChar, 100).Value = usuarioAuditoria;

        connection.Open();
        command.ExecuteNonQuery();
    }
}