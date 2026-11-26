using System.Data;
using System.Globalization;
using System.Text;
using TemplateCSharp.Data;

namespace TemplateCSharp.Services;

/// <summary>
/// Exporta la factura a un archivo PDF de una página (tamaño carta).
/// El PDF se escribe directamente, sin librerías externas: usa las fuentes
/// estándar Helvetica y Helvetica-Bold que todos los lectores de PDF incluyen.
/// </summary>
public static class FacturaPdf
{
    private const double AnchoPagina = 612;   // tamaño carta en puntos
    private const double AltoPagina = 792;
    private const double Margen = 50;

    public static void Generar(string rutaArchivo, DataRow encabezado, DataTable detalle)
    {
        string contenido = CrearContenido(encabezado, detalle);

        EscribirArchivo(rutaArchivo, contenido);
    }

    // -----------------------------------------------------------------
    // CONTENIDO DE LA PÁGINA
    // -----------------------------------------------------------------
    private static string CrearContenido(DataRow encabezado, DataTable detalle)
    {
        StringBuilder c = new StringBuilder();

        double derecha = AnchoPagina - Margen;
        double ancho = AnchoPagina - (2 * Margen);
        double y = AltoPagina - Margen;

        bool cancelada = DatosHelper.Texto(encabezado, "Estado") == "Cancelada";
        string folioCompra = DatosHelper.Texto(encabezado, "FolioCompra").ToUpperInvariant();

        // ----- Título -----
        Rectangulo(c, Margen, y - 40, ancho, 44, 0.16, 0.31, 0.60);
        Texto(c, Margen + 12, y - 24, "FACTURA", 20, true, 1, 1, 1);
        TextoDerecha(c, derecha - 12, y - 14, "Folio: " + DatosHelper.Texto(encabezado, "FolioFactura"), 11, true, 1, 1, 1);
        TextoDerecha(
            c,
            derecha - 12,
            y - 30,
            "Emisión: " + Formato.FechaHora(DatosHelper.Fecha(encabezado, "FechaHoraEmision")),
            9,
            false,
            1,
            1,
            1);
        y -= 62;

        if (cancelada)
        {
            Texto(c, Margen, y, "FACTURA CANCELADA - SIN VALIDEZ", 12, true, 0.80, 0.10, 0.10);
            y -= 22;
        }

        // ----- Emisor -----
        y = Seccion(c, y, "DATOS DEL EMISOR");
        y = Renglon(c, y, "Nombre o razón social:", DatosHelper.Texto(encabezado, "NombreEmpresa"));
        y = Renglon(c, y, "RFC:", DatosHelper.Texto(encabezado, "RFCEmpresa"));
        y = Renglon(c, y, "Domicilio:", DatosHelper.Texto(encabezado, "DomicilioEmpresa"));
        y -= 8;

        // ----- Receptor -----
        y = Seccion(c, y, "DATOS DEL RECEPTOR");
        y = Renglon(c, y, "Nombre:", DatosHelper.Texto(encabezado, "NombreReceptor"));
        y = Renglon(c, y, "RFC:", DatosHelper.Texto(encabezado, "RFCReceptor"));
        y = Renglon(c, y, "Domicilio:", DatosHelper.Texto(encabezado, "DomicilioReceptor"));
        y -= 8;

        // ----- Comprobante -----
        y = Seccion(c, y, "DATOS DEL COMPROBANTE");
        y = Renglon(c, y, "Folio de factura:", DatosHelper.Texto(encabezado, "FolioFactura"));
        y = Renglon(c, y, "Folio de compra:", folioCompra);
        y = Renglon(c, y, "Fecha y hora de emisión:", Formato.FechaHora(DatosHelper.Fecha(encabezado, "FechaHoraEmision")));
        y = Renglon(c, y, "Método de pago:", DatosHelper.Texto(encabezado, "MetodoPago"));
        y = Renglon(c, y, "Estado:", DatosHelper.Texto(encabezado, "Estado"));
        y -= 8;

        // ----- Detalle -----
        y = Seccion(c, y, "DETALLE DE LA COMPRA");
        y = Renglon(c, y, "Partido:", DatosHelper.Texto(encabezado, "Partido"));
        y = Renglon(c, y, "Estadio:", DatosHelper.Texto(encabezado, "Estadio"));
        y = Renglon(c, y, "Fecha del partido:", Formato.FechaHora(DatosHelper.Fecha(encabezado, "FechaPartido")));
        y -= 6;

        // Columnas de la tabla: posición inicial y ancho
        double[] x = { Margen, Margen + 118, Margen + 218, Margen + 283, Margen + 358, Margen + 423, Margen + 453 };
        double[] w = { 118, 100, 65, 75, 65, 30, 59 };
        string[] titulos = { "Partido", "Estadio", "Zona", "Asiento", "Precio unitario", "Cant.", "Importe" };

        Rectangulo(c, Margen, y - 5, ancho, 17, 0.16, 0.31, 0.60);

        for (int i = 0; i < titulos.Length; i++)
        {
            if (i >= 4)
            {
                TextoDerecha(c, i == 6 ? derecha - 4 : x[i] + w[i] - 4, y, titulos[i], 8, true, 1, 1, 1);
            }
            else
            {
                Texto(c, x[i] + 4, y, titulos[i], 8, true, 1, 1, 1);
            }
        }

        y -= 18;

        int numero = 0;

        foreach (DataRow fila in detalle.Rows)
        {
            if (numero % 2 == 1)
            {
                Rectangulo(c, Margen, y - 5, ancho, 16, 0.94, 0.95, 0.98);
            }

            Texto(c, x[0] + 4, y, Recortar(DatosHelper.Texto(fila, "Partido"), w[0] - 6, 8), 8, false, 0, 0, 0);
            Texto(c, x[1] + 4, y, Recortar(DatosHelper.Texto(fila, "Estadio"), w[1] - 6, 8), 8, false, 0, 0, 0);
            Texto(c, x[2] + 4, y, Recortar(DatosHelper.Texto(fila, "Zona"), w[2] - 6, 8), 8, false, 0, 0, 0);
            Texto(c, x[3] + 4, y, Recortar(DatosHelper.Texto(fila, "Asiento"), w[3] - 6, 8), 8, false, 0, 0, 0);
            TextoDerecha(c, x[4] + w[4] - 4, y, Formato.Moneda(DatosHelper.Numero(fila, "PrecioUnitario")), 8, false, 0, 0, 0);
            TextoDerecha(c, x[5] + w[5] - 4, y, DatosHelper.Entero(fila, "Cantidad").ToString(CultureInfo.InvariantCulture), 8, false, 0, 0, 0);
            TextoDerecha(c, derecha - 4, y, Formato.Moneda(DatosHelper.Numero(fila, "Importe")), 8, false, 0, 0, 0);

            y -= 16;
            numero++;
        }

        Linea(c, Margen, y + 8, derecha, y + 8);
        y -= 12;

        // ----- Totales -----
        TextoDerecha(c, derecha - 110, y, "Cantidad de boletos:", 10, false, 0, 0, 0);
        TextoDerecha(c, derecha - 4, y, numero.ToString(CultureInfo.InvariantCulture), 10, false, 0, 0, 0);
        y -= 16;
        TextoDerecha(c, derecha - 110, y, "Subtotal:", 10, false, 0, 0, 0);
        TextoDerecha(c, derecha - 4, y, Formato.Moneda(DatosHelper.Numero(encabezado, "Subtotal")), 10, false, 0, 0, 0);
        y -= 16;
        TextoDerecha(c, derecha - 110, y, "IVA (16%):", 10, false, 0, 0, 0);
        TextoDerecha(c, derecha - 4, y, Formato.Moneda(DatosHelper.Numero(encabezado, "IVA")), 10, false, 0, 0, 0);
        y -= 8;
        Linea(c, derecha - 200, y, derecha, y);
        y -= 16;
        TextoDerecha(c, derecha - 110, y, "TOTAL:", 12, true, 0, 0, 0);
        TextoDerecha(c, derecha - 4, y, Formato.Moneda(DatosHelper.Numero(encabezado, "Total")), 12, true, 0, 0, 0);

        // ----- Pie de página -----
        Linea(c, Margen, 62, derecha, 62);
        Texto(c, Margen, 48, "Documento generado por el sistema de venta de boletos. Conserve su folio de compra para cualquier aclaración.", 8, false, 0.35, 0.35, 0.35);

        return c.ToString();
    }

    private static double Seccion(StringBuilder c, double y, string titulo)
    {
        Rectangulo(c, Margen, y - 5, AnchoPagina - (2 * Margen), 17, 0.88, 0.91, 0.96);
        Texto(c, Margen + 6, y, titulo, 10, true, 0.16, 0.31, 0.60);

        return y - 22;
    }

    /// <summary>Etiqueta en negritas y valor; el valor se divide en varias líneas si no cabe.</summary>
    private static double Renglon(StringBuilder c, double y, string etiqueta, string valor)
    {
        Texto(c, Margen + 8, y, etiqueta, 9, true, 0, 0, 0);

        List<string> lineas = Dividir(valor, AnchoPagina - (2 * Margen) - 150, 9);

        foreach (string linea in lineas)
        {
            Texto(c, Margen + 140, y, linea, 9, false, 0, 0, 0);
            y -= 13;
        }

        return y;
    }

    // -----------------------------------------------------------------
    // OPERADORES DE DIBUJO DE PDF
    // -----------------------------------------------------------------
    private static string N(double valor)
    {
        return valor.ToString("0.###", CultureInfo.InvariantCulture);
    }

    private static string Escapar(string texto)
    {
        StringBuilder limpio = new StringBuilder();

        foreach (char letra in texto)
        {
            if (letra == '\\' || letra == '(' || letra == ')')
            {
                limpio.Append('\\').Append(letra);
            }
            else if (letra < 32 || letra > 255)
            {
                limpio.Append(' ');     // caracteres que la fuente estándar no tiene
            }
            else
            {
                limpio.Append(letra);
            }
        }

        return limpio.ToString();
    }

    private static void Texto(StringBuilder c, double x, double y, string texto, double tamano, bool negrita, double r, double g, double b)
    {
        c.Append("BT ");
        c.Append(negrita ? "/F2 " : "/F1 ").Append(N(tamano)).Append(" Tf ");
        c.Append(N(r)).Append(' ').Append(N(g)).Append(' ').Append(N(b)).Append(" rg ");
        c.Append(N(x)).Append(' ').Append(N(y)).Append(" Td ");
        c.Append('(').Append(Escapar(texto)).Append(") Tj ET\n");
    }

    private static void TextoDerecha(StringBuilder c, double xDerecha, double y, string texto, double tamano, bool negrita, double r, double g, double b)
    {
        Texto(c, xDerecha - AnchoTexto(texto, tamano, negrita), y, texto, tamano, negrita, r, g, b);
    }

    private static void Rectangulo(StringBuilder c, double x, double y, double ancho, double alto, double r, double g, double b)
    {
        c.Append(N(r)).Append(' ').Append(N(g)).Append(' ').Append(N(b)).Append(" rg ");
        c.Append(N(x)).Append(' ').Append(N(y)).Append(' ').Append(N(ancho)).Append(' ').Append(N(alto)).Append(" re f\n");
    }

    private static void Linea(StringBuilder c, double x1, double y1, double x2, double y2)
    {
        c.Append("0.6 0.6 0.6 RG 0.7 w ");
        c.Append(N(x1)).Append(' ').Append(N(y1)).Append(" m ");
        c.Append(N(x2)).Append(' ').Append(N(y2)).Append(" l S\n");
    }

    // -----------------------------------------------------------------
    // MEDIDAS DE TEXTO (anchos aproximados de la fuente Helvetica)
    // -----------------------------------------------------------------
    private static double AnchoLetra(char letra)
    {
        if (letra >= '0' && letra <= '9')
        {
            return 556;
        }

        switch (letra)
        {
            case ' ':
            case ',':
            case '.':
            case ':':
            case ';':
            case '/':
            case '!':
            case 'I':
            case 'f':
            case 't':
                return 278;
            case 'i':
            case 'j':
            case 'l':
            case '\'':
                return 222;
            case '-':
            case '(':
            case ')':
            case 'r':
                return 333;
            case '$':
            case '#':
                return 556;
            case '%':
                return 889;
            case 'm':
            case 'M':
                return 833;
            case 'W':
                return 944;
            case 'w':
                return 722;
            case 'c':
            case 'k':
            case 's':
            case 'v':
            case 'x':
            case 'y':
            case 'z':
            case 'J':
                return 500;
        }

        if (char.IsUpper(letra))
        {
            return 690;
        }

        return 556;
    }

    private static double AnchoTexto(string texto, double tamano, bool negrita)
    {
        double suma = 0;

        foreach (char letra in texto)
        {
            double anchoLetra = AnchoLetra(letra);

            // En negritas las letras son un poco más anchas (los dígitos miden lo mismo)
            if (negrita && char.IsLetter(letra))
            {
                anchoLetra *= 1.07;
            }

            suma += anchoLetra;
        }

        return suma * tamano / 1000.0;
    }

    private static string Recortar(string texto, double anchoMaximo, double tamano)
    {
        if (AnchoTexto(texto, tamano, false) <= anchoMaximo)
        {
            return texto;
        }

        string recortado = texto;

        while (recortado.Length > 1 && AnchoTexto(recortado + "...", tamano, false) > anchoMaximo)
        {
            recortado = recortado.Substring(0, recortado.Length - 1);
        }

        return recortado.TrimEnd() + "...";
    }

    private static List<string> Dividir(string texto, double anchoMaximo, double tamano)
    {
        List<string> lineas = new List<string>();
        string actual = string.Empty;

        foreach (string palabra in texto.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            string prueba = actual.Length == 0 ? palabra : actual + " " + palabra;

            if (actual.Length > 0 && AnchoTexto(prueba, tamano, false) > anchoMaximo)
            {
                lineas.Add(actual);
                actual = palabra;
            }
            else
            {
                actual = prueba;
            }
        }

        lineas.Add(actual);

        return lineas;
    }

    // -----------------------------------------------------------------
    // ESTRUCTURA DEL ARCHIVO PDF
    // -----------------------------------------------------------------
    private static void EscribirArchivo(string rutaArchivo, string contenido)
    {
        Encoding latin = Encoding.Latin1;
        byte[] flujo = latin.GetBytes(contenido);

        string[] objetos =
        {
            "<< /Type /Catalog /Pages 2 0 R >>",
            "<< /Type /Pages /Kids [3 0 R] /Count 1 >>",
            "<< /Type /Page /Parent 2 0 R /MediaBox [0 0 " + N(AnchoPagina) + " " + N(AltoPagina) + "] " +
                "/Resources << /Font << /F1 4 0 R /F2 5 0 R >> >> /Contents 6 0 R >>",
            "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>",
            "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica-Bold /Encoding /WinAnsiEncoding >>"
        };

        using MemoryStream memoria = new MemoryStream();

        List<long> posiciones = new List<long>();

        Escribir(memoria, latin, "%PDF-1.4\n");

        for (int i = 0; i < objetos.Length; i++)
        {
            posiciones.Add(memoria.Position);
            Escribir(memoria, latin, (i + 1) + " 0 obj\n" + objetos[i] + "\nendobj\n");
        }

        // Objeto 6: contenido de la página
        posiciones.Add(memoria.Position);
        Escribir(memoria, latin, "6 0 obj\n<< /Length " + flujo.Length + " >>\nstream\n");
        memoria.Write(flujo, 0, flujo.Length);
        Escribir(memoria, latin, "\nendstream\nendobj\n");

        // Tabla de referencias cruzadas
        long inicioTabla = memoria.Position;
        int totalObjetos = posiciones.Count + 1;

        Escribir(memoria, latin, "xref\n0 " + totalObjetos + "\n");
        Escribir(memoria, latin, "0000000000 65535 f \n");

        foreach (long posicion in posiciones)
        {
            Escribir(memoria, latin, posicion.ToString("0000000000", CultureInfo.InvariantCulture) + " 00000 n \n");
        }

        Escribir(memoria, latin, "trailer\n<< /Size " + totalObjetos + " /Root 1 0 R >>\nstartxref\n" + inicioTabla + "\n%%EOF\n");

        File.WriteAllBytes(rutaArchivo, memoria.ToArray());
    }

    private static void Escribir(Stream destino, Encoding codificacion, string texto)
    {
        byte[] bytes = codificacion.GetBytes(texto);
        destino.Write(bytes, 0, bytes.Length);
    }
}
