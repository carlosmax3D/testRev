using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DatabaseConnection _database;

    public UsuarioRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public ResultadoInicioSesion ValidarAcceso(string correo,string contrasena)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
            SELECT
                IdUsuario,
                Correo,
                NombreCompleto,
                TipoUsuario,
                EstadoUsuario,
                NumeroNomina,
                Contrasena
            FROM Usuario
            WHERE Correo = @Correo
            """;

        using SqlCommand command = new(sql, connection);

        command.Parameters.Add("@Correo",SqlDbType.VarChar,50).Value = correo.Trim();

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return new ResultadoInicioSesion
            {
                AccesoPermitido = false,
                UsuarioInactivo = false,
                Usuario = null
            };
        }

        string contrasenaGuardada = reader.GetString(reader.GetOrdinal("Contrasena"));

        if (contrasenaGuardada != contrasena)
        {
            return new ResultadoInicioSesion
            {
                AccesoPermitido = false,
                UsuarioInactivo = false,
                Usuario = null
            };
        }

        string estadoUsuario = reader.GetString(reader.GetOrdinal("EstadoUsuario"));

        if (estadoUsuario != "Activo")
        {
            return new ResultadoInicioSesion
            {
                AccesoPermitido = false,
                UsuarioInactivo = true,
                Usuario = CrearUsuario(reader)
            };
        }

        return new ResultadoInicioSesion
        {
            AccesoPermitido = true,
            UsuarioInactivo = false,
            Usuario = CrearUsuario(reader)
        };
    }

    private static Usuario CrearUsuario(SqlDataReader reader)
    {
        return new Usuario
        {
            IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),

            Correo = reader.GetString(reader.GetOrdinal("Correo")),

            NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),

            TipoUsuario = reader.GetString(reader.GetOrdinal("TipoUsuario")),

            EstadoUsuario = reader.GetString(reader.GetOrdinal("EstadoUsuario")),

            NumeroNomina = reader.GetString(reader.GetOrdinal("NumeroNomina"))
        };
    }
    public List<Usuario> ObtenerUsuarios()
    {
        List<Usuario> usuarios = new List<Usuario>();

        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
    SELECT
        U.IdUsuario,
        U.Correo,
        U.NombreCompleto,
        U.TipoUsuario,
        U.EstadoUsuario,
        U.NumeroNomina,
        U.FechaNacimiento,
        U.FechaHoraRegistro,
        U.IdAdministradorReg,
        A.ClaveAdmin,
        O.CajaAsignada
    FROM Usuario U
    LEFT JOIN Administrador A
        ON U.IdUsuario = A.IdUsuario
    LEFT JOIN Operativo O
        ON U.IdUsuario = O.IdUsuario
    ORDER BY U.IdUsuario ASC;
    """;

        using SqlCommand command = new SqlCommand(sql, connection);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Usuario usuario = new Usuario
            {
                IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),

                Correo = reader.GetString(reader.GetOrdinal("Correo")),

                NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),

                TipoUsuario = reader.GetString(reader.GetOrdinal("TipoUsuario")),

                EstadoUsuario = reader.GetString(reader.GetOrdinal("EstadoUsuario")),

                NumeroNomina = reader.GetString(reader.GetOrdinal("NumeroNomina")),

                FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")),

                FechaHoraRegistro = reader.GetDateTime(reader.GetOrdinal("FechaHoraRegistro")),

                IdAdministradorReg = reader.IsDBNull(reader.GetOrdinal("IdAdministradorReg"))? null: reader.GetInt32(reader.GetOrdinal("IdAdministradorReg")),

                ClaveAdmin = reader.IsDBNull(reader.GetOrdinal("ClaveAdmin"))? string.Empty: reader.GetString(reader.GetOrdinal("ClaveAdmin")),

                CajaAsignada = reader.IsDBNull(reader.GetOrdinal("CajaAsignada"))? string.Empty: reader.GetString(reader.GetOrdinal("CajaAsignada"))
            };

            usuarios.Add(usuario);
        }

        return usuarios;
    }

    public Usuario ObtenerUsuarioPorId(int idUsuario)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
        SELECT
            U.IdUsuario,
            U.Correo,
            U.NombreCompleto,
            U.TipoUsuario,
            U.EstadoUsuario,
            U.NumeroNomina,
            U.FechaNacimiento,
            U.FechaHoraRegistro,
            U.IdAdministradorReg,
            U.Contrasena,
            A.ClaveAdmin,
            O.CajaAsignada
        FROM Usuario U
        LEFT JOIN Administrador A
            ON U.IdUsuario = A.IdUsuario
        LEFT JOIN Operativo O
            ON U.IdUsuario = O.IdUsuario
        WHERE U.IdUsuario = @IdUsuario;
        """;

        using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            throw new Exception("No se encontró el usuario solicitado.");
        }

        Usuario usuario = new Usuario
        {
            IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
            Correo = reader.GetString(reader.GetOrdinal("Correo")),
            NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
            TipoUsuario = reader.GetString(reader.GetOrdinal("TipoUsuario")),
            EstadoUsuario = reader.GetString(reader.GetOrdinal("EstadoUsuario")),
            NumeroNomina = reader.GetString(reader.GetOrdinal("NumeroNomina")),
            FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")),
            FechaHoraRegistro = reader.GetDateTime(reader.GetOrdinal("FechaHoraRegistro")),
            IdAdministradorReg = reader.IsDBNull(reader.GetOrdinal("IdAdministradorReg"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("IdAdministradorReg")),
            Contrasena = reader.GetString(reader.GetOrdinal("Contrasena")),
            ClaveAdmin = reader.IsDBNull(reader.GetOrdinal("ClaveAdmin"))
                ? string.Empty
                : reader.GetString(reader.GetOrdinal("ClaveAdmin")),
            CajaAsignada = reader.IsDBNull(reader.GetOrdinal("CajaAsignada"))
                ? string.Empty
                : reader.GetString(reader.GetOrdinal("CajaAsignada"))
        };

        return usuario;
    }

    public List<Usuario> BuscarUsuarios(string criterio, string texto)
    {
        string columna = criterio switch
        {
            "Correo" => "U.Correo",
            "Nombre" => "U.NombreCompleto",
            "Numero de Nómina" => "U.NumeroNomina",
            _ => throw new ArgumentException("El criterio de búsqueda no es válido.")
        };

        List<Usuario> usuarios = new List<Usuario>();

        using SqlConnection connection = _database.CreateConnection();

        string sql = $"""
        SELECT
            U.IdUsuario,
            U.Correo,
            U.NombreCompleto,
            U.TipoUsuario,
            U.EstadoUsuario,
            U.NumeroNomina,
            U.FechaNacimiento,
            U.FechaHoraRegistro,
            U.IdAdministradorReg,
            A.ClaveAdmin,
            O.CajaAsignada
        FROM Usuario U
        LEFT JOIN Administrador A
            ON U.IdUsuario = A.IdUsuario
        LEFT JOIN Operativo O
            ON U.IdUsuario = O.IdUsuario
        WHERE {columna} LIKE @Texto
        ORDER BY U.NombreCompleto;
        """;

        using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.Add("@Texto", SqlDbType.VarChar, 100).Value =
            "%" + texto.Trim() + "%";

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Usuario usuario = new Usuario
            {
                IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),

                Correo = reader.GetString(reader.GetOrdinal("Correo")),

                NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),

                TipoUsuario = reader.GetString(reader.GetOrdinal("TipoUsuario")),

                EstadoUsuario = reader.GetString(reader.GetOrdinal("EstadoUsuario")),

                NumeroNomina = reader.GetString(reader.GetOrdinal("NumeroNomina")),

                FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")),

                FechaHoraRegistro = reader.GetDateTime(reader.GetOrdinal("FechaHoraRegistro")),

                IdAdministradorReg = reader.IsDBNull(reader.GetOrdinal("IdAdministradorReg"))? null: reader.GetInt32(reader.GetOrdinal("IdAdministradorReg")),

                ClaveAdmin = reader.IsDBNull(reader.GetOrdinal("ClaveAdmin"))? string.Empty: reader.GetString(reader.GetOrdinal("ClaveAdmin")),

                CajaAsignada = reader.IsDBNull(reader.GetOrdinal("CajaAsignada"))? string.Empty: reader.GetString(reader.GetOrdinal("CajaAsignada"))
            };

            usuarios.Add(usuario);
        }

        return usuarios;
    }
    public bool ExisteNumeroNomina(string numeroNomina)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
        SELECT COUNT(*)
        FROM Usuario
        WHERE NumeroNomina = @NumeroNomina;
        """;

        using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.Add("@NumeroNomina", SqlDbType.VarChar, 50).Value =
            numeroNomina.Trim();

        connection.Open();

        int cantidad = (int)command.ExecuteScalar();

        return cantidad > 0;
    }
    public bool ExisteCorreo(string correo)
    {
        using SqlConnection connection = _database.CreateConnection();

        const string sql = """
        SELECT COUNT(*)
        FROM Usuario
        WHERE Correo = @Correo;
        """;

        using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.Add("@Correo", SqlDbType.VarChar, 50).Value =
            correo.Trim();

        connection.Open();

        int cantidad = (int)command.ExecuteScalar();

        return cantidad > 0;
    }
    public int RegistrarUsuario(Usuario usuario, string claveAdmin, string cajaAsignada)
    {
        using SqlConnection connection = _database.CreateConnection();

        connection.Open();

        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            const string sqlUsuario = """
            INSERT INTO Usuario
            (
                TipoUsuario,
                EstadoUsuario,
                IdAdministradorReg,
                Correo,
                Contrasena,
                NombreCompleto,
                NumeroNomina,
                FechaNacimiento
            )
            OUTPUT INSERTED.IdUsuario
            VALUES
            (
                @TipoUsuario,
                @EstadoUsuario,
                @IdAdministradorReg,
                @Correo,
                @Contrasena,
                @NombreCompleto,
                @NumeroNomina,
                @FechaNacimiento
            );
            """;

            using SqlCommand commandUsuario =
                new SqlCommand(sqlUsuario, connection, transaction);

            commandUsuario.Parameters.Add("@TipoUsuario", SqlDbType.VarChar, 20)
                .Value = usuario.TipoUsuario;

            commandUsuario.Parameters.Add("@EstadoUsuario", SqlDbType.VarChar, 20)
                .Value = usuario.EstadoUsuario;

            commandUsuario.Parameters.Add("@IdAdministradorReg", SqlDbType.Int)
                .Value = usuario.IdAdministradorReg.HasValue
                    ? usuario.IdAdministradorReg.Value
                    : DBNull.Value;

            commandUsuario.Parameters.Add("@Correo", SqlDbType.VarChar, 50)
                .Value = usuario.Correo;

            commandUsuario.Parameters.Add("@Contrasena", SqlDbType.VarChar, 255)
                .Value = usuario.Contrasena;

            commandUsuario.Parameters.Add("@NombreCompleto", SqlDbType.VarChar, 100)
                .Value = usuario.NombreCompleto;

            commandUsuario.Parameters.Add("@NumeroNomina", SqlDbType.VarChar, 20)
                .Value = usuario.NumeroNomina;

            commandUsuario.Parameters.Add("@FechaNacimiento", SqlDbType.Date)
                .Value = usuario.FechaNacimiento;

            int idUsuario = (int)commandUsuario.ExecuteScalar();

            if (usuario.TipoUsuario == "Administrador")
            {
                const string sqlAdministrador = """
                INSERT INTO Administrador (IdUsuario, ClaveAdmin)
                VALUES (@IdUsuario, @ClaveAdmin);
                """;

                using SqlCommand commandAdministrador =
                    new SqlCommand(sqlAdministrador, connection, transaction);

                commandAdministrador.Parameters.Add("@IdUsuario", SqlDbType.Int)
                    .Value = idUsuario;

                commandAdministrador.Parameters.Add("@ClaveAdmin", SqlDbType.VarChar, 50)
                    .Value = claveAdmin.Trim();

                commandAdministrador.ExecuteNonQuery();
            }
            else if (usuario.TipoUsuario == "Operativo")
            {
                const string sqlOperativo = """
                INSERT INTO Operativo (IdUsuario, CajaAsignada)
                VALUES (@IdUsuario, @CajaAsignada);
                """;

                using SqlCommand commandOperativo =
                    new SqlCommand(sqlOperativo, connection, transaction);

                commandOperativo.Parameters.Add("@IdUsuario", SqlDbType.Int)
                    .Value = idUsuario;

                commandOperativo.Parameters.Add("@CajaAsignada", SqlDbType.VarChar, 50)
                    .Value = cajaAsignada.Trim();

                commandOperativo.ExecuteNonQuery();
            }
            else
            {
                throw new InvalidOperationException("El tipo de usuario no es válido.");
            }

            transaction.Commit();

            return idUsuario;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
    public void ActualizarUsuario(Usuario usuario, string claveAdmin, string cajaAsignada)
    {
        using SqlConnection connection = _database.CreateConnection();

        connection.Open();

        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            const string sqlContrasenaActual = """
            SELECT Contrasena
            FROM Usuario
            WHERE IdUsuario = @IdUsuario;
            """;

            string contrasenaActual;

            using (SqlCommand commandContrasena =
                new SqlCommand(sqlContrasenaActual, connection, transaction))
            {
                commandContrasena.Parameters.Add("@IdUsuario", SqlDbType.Int)
                    .Value = usuario.IdUsuario;

                object resultado = commandContrasena.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                {
                    throw new Exception("No se encontró la contraseña actual del usuario.");
                }

                contrasenaActual = resultado.ToString();
            }

            bool cambioContrasena = contrasenaActual != usuario.Contrasena;

            if (cambioContrasena)
            {
                const string sqlHistorial = """
                SELECT TOP 2 ContrasenaH
                FROM Historial_Contrasena
                WHERE IdUsuario = @IdUsuario
                ORDER BY FechaCambio DESC, IdHistorial DESC;
                """;

                using SqlCommand commandHistorial =
                    new SqlCommand(sqlHistorial, connection, transaction);

                commandHistorial.Parameters.Add("@IdUsuario", SqlDbType.Int)
                    .Value = usuario.IdUsuario;

                using SqlDataReader reader = commandHistorial.ExecuteReader();

                while (reader.Read())
                {
                    string contrasenaAnterior = reader["ContrasenaH"].ToString();

                    if (contrasenaAnterior == usuario.Contrasena)
                    {
                        throw new InvalidOperationException(
                            "No puedes reutilizar ninguna de tus últimas dos contraseñas.");
                    }
                }
            }
            if (cambioContrasena)
            {
                const string sqlGuardarHistorial = """
                INSERT INTO Historial_Contrasena (IdUsuario, ContrasenaH)
                VALUES (@IdUsuario, @ContrasenaAnterior);
                """;

                using SqlCommand commandGuardarHistorial =
                    new SqlCommand(sqlGuardarHistorial, connection, transaction);

                commandGuardarHistorial.Parameters.Add("@IdUsuario", SqlDbType.Int)
                    .Value = usuario.IdUsuario;

                commandGuardarHistorial.Parameters.Add(
                    "@ContrasenaAnterior", SqlDbType.VarChar, 255)
                    .Value = contrasenaActual;

                commandGuardarHistorial.ExecuteNonQuery();

                const string sqlLimpiarHistorial = """
                WITH HistorialOrdenado AS
                (
                    SELECT
                        IdHistorial,
                        ROW_NUMBER() OVER (
                            ORDER BY FechaCambio DESC, IdHistorial DESC
                        ) AS NumeroFila
                    FROM Historial_Contrasena
                    WHERE IdUsuario = @IdUsuario
                )
                DELETE FROM HistorialOrdenado
                WHERE NumeroFila > 2;
                """;

                using SqlCommand commandLimpiarHistorial =
                    new SqlCommand(sqlLimpiarHistorial, connection, transaction);

                commandLimpiarHistorial.Parameters.Add("@IdUsuario", SqlDbType.Int)
                    .Value = usuario.IdUsuario;

                commandLimpiarHistorial.ExecuteNonQuery();
            }

            const string sqlUsuario = """
            UPDATE Usuario
            SET
                TipoUsuario = @TipoUsuario,
                EstadoUsuario = @EstadoUsuario,
                Correo = @Correo,
                Contrasena = @Contrasena,
                NombreCompleto = @NombreCompleto,
                NumeroNomina = @NumeroNomina,
                FechaNacimiento = @FechaNacimiento
            WHERE IdUsuario = @IdUsuario;
            """;

            using SqlCommand commandUsuario =
                new SqlCommand(sqlUsuario, connection, transaction);

            commandUsuario.Parameters.Add("@TipoUsuario", SqlDbType.VarChar, 20)
                .Value = usuario.TipoUsuario;

            commandUsuario.Parameters.Add("@EstadoUsuario", SqlDbType.VarChar, 20)
                .Value = usuario.EstadoUsuario;

            commandUsuario.Parameters.Add("@Correo", SqlDbType.VarChar, 50)
                .Value = usuario.Correo;

            commandUsuario.Parameters.Add("@Contrasena", SqlDbType.VarChar, 255)
                .Value = usuario.Contrasena;

            commandUsuario.Parameters.Add("@NombreCompleto", SqlDbType.VarChar, 100)
                .Value = usuario.NombreCompleto;

            commandUsuario.Parameters.Add("@NumeroNomina", SqlDbType.VarChar, 20)
                .Value = usuario.NumeroNomina;

            commandUsuario.Parameters.Add("@FechaNacimiento", SqlDbType.Date)
                .Value = usuario.FechaNacimiento;

            commandUsuario.Parameters.Add("@IdUsuario", SqlDbType.Int)
                .Value = usuario.IdUsuario;

            int filasActualizadas = commandUsuario.ExecuteNonQuery();

            if (filasActualizadas == 0)
            {
                throw new Exception("No se encontró el usuario que quieres editar.");
            }

            if (usuario.TipoUsuario == "Administrador")
            {
                const string sqlEliminarOperativo = """
                DELETE FROM Operativo
                WHERE IdUsuario = @IdUsuario;
                """;

                using SqlCommand commandEliminar =
                    new SqlCommand(sqlEliminarOperativo, connection, transaction);

                commandEliminar.Parameters.Add("@IdUsuario", SqlDbType.Int)
                    .Value = usuario.IdUsuario;

                commandEliminar.ExecuteNonQuery();

                const string sqlActualizarAdmin = """
                UPDATE Administrador
                SET ClaveAdmin = @ClaveAdmin
                WHERE IdUsuario = @IdUsuario;
                """;

                using SqlCommand commandAdmin =
                    new SqlCommand(sqlActualizarAdmin, connection, transaction);

                commandAdmin.Parameters.Add("@IdUsuario", SqlDbType.Int)
                    .Value = usuario.IdUsuario;

                commandAdmin.Parameters.Add("@ClaveAdmin", SqlDbType.VarChar, 50)
                    .Value = claveAdmin.Trim();

                int filasAdmin = commandAdmin.ExecuteNonQuery();

                if (filasAdmin == 0)
                {
                    const string sqlInsertarAdmin = """
                    INSERT INTO Administrador (IdUsuario, ClaveAdmin)
                    VALUES (@IdUsuario, @ClaveAdmin);
                    """;

                    using SqlCommand commandInsertar =
                        new SqlCommand(sqlInsertarAdmin, connection, transaction);

                    commandInsertar.Parameters.Add("@IdUsuario", SqlDbType.Int)
                        .Value = usuario.IdUsuario;

                    commandInsertar.Parameters.Add("@ClaveAdmin", SqlDbType.VarChar, 50)
                        .Value = claveAdmin.Trim();

                    commandInsertar.ExecuteNonQuery();
                }
            }
            else if (usuario.TipoUsuario == "Operativo")
            {
                const string sqlEliminarAdmin = """
                DELETE FROM Administrador
                WHERE IdUsuario = @IdUsuario;
                """;

                using SqlCommand commandEliminar =
                    new SqlCommand(sqlEliminarAdmin, connection, transaction);

                commandEliminar.Parameters.Add("@IdUsuario", SqlDbType.Int)
                    .Value = usuario.IdUsuario;

                commandEliminar.ExecuteNonQuery();

                const string sqlActualizarOperativo = """
                UPDATE Operativo
                SET CajaAsignada = @CajaAsignada
                WHERE IdUsuario = @IdUsuario;
                """;

                using SqlCommand commandOperativo =
                    new SqlCommand(sqlActualizarOperativo, connection, transaction);

                commandOperativo.Parameters.Add("@IdUsuario", SqlDbType.Int)
                    .Value = usuario.IdUsuario;

                commandOperativo.Parameters.Add("@CajaAsignada", SqlDbType.VarChar, 50)
                    .Value = cajaAsignada.Trim();

                int filasOperativo = commandOperativo.ExecuteNonQuery();

                if (filasOperativo == 0)
                {
                    const string sqlInsertarOperativo = """
                    INSERT INTO Operativo (IdUsuario, CajaAsignada)
                    VALUES (@IdUsuario, @CajaAsignada);
                    """;

                    using SqlCommand commandInsertar =
                        new SqlCommand(sqlInsertarOperativo, connection, transaction);

                    commandInsertar.Parameters.Add("@IdUsuario", SqlDbType.Int)
                        .Value = usuario.IdUsuario;

                    commandInsertar.Parameters.Add("@CajaAsignada", SqlDbType.VarChar, 50)
                        .Value = cajaAsignada.Trim();

                    commandInsertar.ExecuteNonQuery();
                }
            }
            else
            {
                throw new InvalidOperationException("El tipo de usuario no es válido.");
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
    public void EliminarUsuario(int idUsuario)
    {
        using SqlConnection connection = _database.CreateConnection();

        connection.Open();

        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            const string sqlEliminarAdmin = """
            DELETE FROM Administrador
            WHERE IdUsuario = @IdUsuario;
            """;

            using SqlCommand commandAdmin =
                new SqlCommand(sqlEliminarAdmin, connection, transaction);

            commandAdmin.Parameters.Add("@IdUsuario", SqlDbType.Int)
                .Value = idUsuario;

            commandAdmin.ExecuteNonQuery();

            const string sqlEliminarOperativo = """
            DELETE FROM Operativo
            WHERE IdUsuario = @IdUsuario;
            """;

            using SqlCommand commandOperativo =
                new SqlCommand(sqlEliminarOperativo, connection, transaction);

            commandOperativo.Parameters.Add("@IdUsuario", SqlDbType.Int)
                .Value = idUsuario;

            commandOperativo.ExecuteNonQuery();

            const string sqlEliminarUsuario = """
            DELETE FROM Usuario
            WHERE IdUsuario = @IdUsuario;
            """;

            using SqlCommand commandUsuario =
                new SqlCommand(sqlEliminarUsuario, connection, transaction);

            commandUsuario.Parameters.Add("@IdUsuario", SqlDbType.Int)
                .Value = idUsuario;

            int filasEliminadas = commandUsuario.ExecuteNonQuery();

            if (filasEliminadas == 0)
            {
                throw new Exception("No se encontró el usuario que quieres eliminar.");
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}