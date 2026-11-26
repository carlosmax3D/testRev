using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TemplateCSharp.Data;

// Lee la cadena DefaultConnection de App.config
public static class Configuracion
{
    public static IConfiguration Cargar()
    {
        string ruta = Path.Combine(AppContext.BaseDirectory, "App.config");
        XDocument documento = XDocument.Load(ruta);

        string? cadena = null;
        XElement? conexiones = documento.Root?.Element("connectionStrings");

        if (conexiones != null)
        {
            foreach (XElement elemento in conexiones.Elements("add"))
            {
                if ((string?)elemento.Attribute("name") == "DefaultConnection")
                {
                    cadena = (string?)elemento.Attribute("connectionString");
                }
            }
        }

        if (string.IsNullOrWhiteSpace(cadena))
        {
            throw new InvalidOperationException("App.config no tiene la cadena de conexión DefaultConnection.");
        }

        if (string.Equals(new SqlConnectionStringBuilder(cadena).DataSource, "SERVIDOR", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("App.config todavía tiene el servidor de ejemplo SERVIDOR.");
        }

        return new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = cadena
            })
            .Build();
    }
}
