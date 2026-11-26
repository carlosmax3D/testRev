using System.Text;

namespace TemplateCSharp.Services;

// PDF de texto, tamaño carta, con fuente Helvetica
public class DocumentoPdf
{
    private const float AnchoPagina = 612f;
    private const float AltoPagina = 792f;
    private const float Margen = 50f;

    // Ancho de cada carácter (códigos 32 a 255) en milésimas del tamaño de la letra
    private static readonly short[] AnchosNormal =
    [
        278, 278, 355, 556, 556, 889, 667, 191, 333, 333, 389, 584, 278, 333, 278, 278,
        556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 278, 278, 584, 584, 584, 556,
        1015, 667, 667, 722, 722, 667, 611, 778, 722, 278, 500, 667, 556, 833, 722, 778,
        667, 778, 722, 667, 611, 722, 667, 944, 667, 667, 611, 278, 278, 278, 469, 556,
        333, 556, 556, 500, 556, 556, 278, 556, 556, 222, 222, 500, 222, 833, 556, 556,
        556, 556, 333, 500, 278, 556, 500, 722, 500, 500, 500, 334, 260, 334, 584, 556,
        556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 556,
        556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 556,
        278, 333, 556, 556, 556, 556, 260, 556, 333, 737, 370, 556, 584, 333, 737, 333,
        400, 584, 333, 333, 333, 556, 537, 278, 333, 333, 365, 556, 834, 834, 834, 611,
        667, 667, 667, 667, 667, 667, 1000, 722, 667, 667, 667, 667, 278, 278, 278, 278,
        722, 722, 778, 778, 778, 778, 778, 584, 778, 722, 722, 722, 722, 667, 667, 611,
        556, 556, 556, 556, 556, 556, 889, 500, 556, 556, 556, 556, 278, 278, 278, 278,
        556, 556, 556, 556, 556, 556, 556, 584, 611, 556, 556, 556, 556, 500, 556, 500
    ];

    private static readonly short[] AnchosNegrita =
    [
        278, 333, 474, 556, 556, 889, 722, 238, 333, 333, 389, 584, 278, 333, 278, 278,
        556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 333, 333, 584, 584, 584, 611,
        975, 722, 722, 722, 722, 667, 611, 778, 722, 278, 556, 722, 611, 833, 722, 778,
        667, 778, 722, 667, 611, 722, 667, 944, 667, 667, 611, 333, 278, 333, 584, 556,
        333, 556, 611, 556, 611, 556, 333, 611, 611, 278, 278, 556, 278, 889, 611, 611,
        611, 611, 389, 556, 333, 611, 556, 778, 556, 556, 500, 389, 280, 389, 584, 611,
        611, 611, 611, 611, 611, 611, 611, 611, 611, 611, 611, 611, 611, 611, 611, 611,
        611, 611, 611, 611, 611, 611, 611, 611, 611, 611, 611, 611, 611, 611, 611, 611,
        278, 333, 556, 556, 556, 556, 280, 556, 333, 737, 370, 556, 584, 333, 737, 333,
        400, 584, 333, 333, 333, 611, 556, 278, 333, 333, 365, 556, 834, 834, 834, 611,
        722, 722, 722, 722, 722, 722, 1000, 722, 667, 667, 667, 667, 278, 278, 278, 278,
        722, 722, 778, 778, 778, 778, 778, 584, 778, 722, 722, 722, 722, 667, 667, 611,
        556, 556, 556, 556, 556, 556, 889, 556, 556, 556, 556, 556, 278, 278, 278, 278,
        611, 611, 611, 611, 611, 611, 611, 584, 611, 611, 611, 611, 611, 556, 611, 556
    ];

    private readonly List<StringBuilder> _paginas = [];
    private StringBuilder _pagina = new();
    private float _y;

    public DocumentoPdf()
    {
        NuevaPagina();
    }

    // Escribe el texto en renglones nuevos; si no cabe en el ancho, lo parte por palabras
    public void Escribir(string texto, float tamano = 10f, bool negrita = false)
    {
        float altoRenglon = tamano * 1.4f;
        string fuente = negrita ? "F2" : "F1";

        foreach (string renglon in Partir(Normalizar(texto), tamano, negrita))
        {
            if (_y - altoRenglon < Margen)
            {
                NuevaPagina();
            }

            _y -= altoRenglon;
            _pagina.Append(FormattableString.Invariant(
                $"BT /{fuente} {tamano:0.##} Tf {Margen:0.##} {_y:0.##} Td ({Escapar(renglon)}) Tj ET\n"));
        }
    }

    public void Espacio(float puntos)
    {
        _y -= puntos;
    }

    public void Guardar(string ruta)
    {
        File.WriteAllBytes(ruta, ObtenerBytes());
    }

    private byte[] ObtenerBytes()
    {
        // Objetos: 1 catálogo, 2 páginas, 3 y 4 fuentes, y por cada hoja su página y su contenido
        int totalObjetos = 4 + _paginas.Count * 2;
        long[] posiciones = new long[totalObjetos + 1];
        using MemoryStream pdf = new();

        Agregar(pdf, "%PDF-1.4\n");

        List<string> hojas = [];
        for (int i = 0; i < _paginas.Count; i++)
        {
            hojas.Add($"{5 + i * 2} 0 R");
        }

        AgregarObjeto(pdf, posiciones, 1, "<< /Type /Catalog /Pages 2 0 R >>");
        AgregarObjeto(pdf, posiciones, 2, $"<< /Type /Pages /Kids [{string.Join(" ", hojas)}] /Count {_paginas.Count} >>");
        AgregarObjeto(pdf, posiciones, 3, "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>");
        AgregarObjeto(pdf, posiciones, 4, "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica-Bold /Encoding /WinAnsiEncoding >>");

        for (int i = 0; i < _paginas.Count; i++)
        {
            int numeroPagina = 5 + i * 2;
            int numeroContenido = numeroPagina + 1;

            AgregarObjeto(pdf, posiciones, numeroPagina,
                $"<< /Type /Page /Parent 2 0 R /MediaBox [0 0 {AnchoPagina:0} {AltoPagina:0}] " +
                $"/Resources << /Font << /F1 3 0 R /F2 4 0 R >> >> /Contents {numeroContenido} 0 R >>");

            byte[] contenido = Encoding.Latin1.GetBytes(_paginas[i].ToString());
            posiciones[numeroContenido] = pdf.Position;
            Agregar(pdf, $"{numeroContenido} 0 obj\n<< /Length {contenido.Length} >>\nstream\n");
            pdf.Write(contenido, 0, contenido.Length);
            Agregar(pdf, "\nendstream\nendobj\n");
        }

        // Tabla de referencias: posición en bytes de cada objeto
        long inicioTabla = pdf.Position;
        Agregar(pdf, $"xref\n0 {totalObjetos + 1}\n0000000000 65535 f \n");
        for (int n = 1; n <= totalObjetos; n++)
        {
            Agregar(pdf, $"{posiciones[n]:D10} 00000 n \n");
        }

        Agregar(pdf, $"trailer\n<< /Size {totalObjetos + 1} /Root 1 0 R >>\nstartxref\n{inicioTabla}\n%%EOF\n");
        return pdf.ToArray();
    }

    private void NuevaPagina()
    {
        _pagina = new StringBuilder();
        _paginas.Add(_pagina);
        _y = AltoPagina - Margen;
    }

    // Parte el texto en renglones que caben en el ancho de la página. Corta por palabras y,
    // si una palabra sola no cabe, la corta por caracteres.
    private static IEnumerable<string> Partir(string texto, float tamano, bool negrita)
    {
        float maximo = AnchoPagina - 2 * Margen;

        foreach (string linea in texto.Split('\n'))
        {
            string actual = string.Empty;
            foreach (string palabra in linea.Split(' '))
            {
                string propuesta = actual.Length == 0 ? palabra : actual + " " + palabra;
                if (Ancho(propuesta, tamano, negrita) <= maximo)
                {
                    actual = propuesta;
                    continue;
                }

                if (actual.Length > 0)
                {
                    yield return actual;
                }

                actual = palabra;
                while (Ancho(actual, tamano, negrita) > maximo)
                {
                    int caben = 1;
                    while (caben < actual.Length && Ancho(actual[..(caben + 1)], tamano, negrita) <= maximo)
                    {
                        caben++;
                    }

                    yield return actual[..caben];
                    actual = actual[caben..];
                }
            }

            yield return actual;
        }
    }

    // Ancho en puntos del texto ya normalizado
    private static float Ancho(string texto, float tamano, bool negrita)
    {
        short[] anchos = negrita ? AnchosNegrita : AnchosNormal;
        int total = 0;
        foreach (char c in texto)
        {
            total += anchos[c - ' '];
        }

        return total * tamano / 1000f;
    }

    // Deja solo caracteres que Helvetica puede mostrar con WinAnsiEncoding; los demás se cambian por ?
    private static string Normalizar(string texto)
    {
        StringBuilder resultado = new();
        foreach (char c in texto.Replace("\r\n", "\n").Replace('\t', ' '))
        {
            bool imprimible = c == '\n' || (c >= ' ' && c < '\u007F') || (c >= '\u00A0' && c <= '\u00FF');
            resultado.Append(imprimible ? c : '?');
        }

        return resultado.ToString();
    }

    // Paréntesis y diagonal invertida tienen significado especial dentro de un texto PDF
    private static string Escapar(string texto)
    {
        StringBuilder resultado = new();
        foreach (char c in texto)
        {
            if (c == '\\' || c == '(' || c == ')')
            {
                resultado.Append('\\');
            }

            resultado.Append(c);
        }

        return resultado.ToString();
    }

    private static void AgregarObjeto(MemoryStream pdf, long[] posiciones, int numero, string cuerpo)
    {
        posiciones[numero] = pdf.Position;
        Agregar(pdf, $"{numero} 0 obj\n{cuerpo}\nendobj\n");
    }

    private static void Agregar(MemoryStream pdf, string texto)
    {
        byte[] bytes = Encoding.Latin1.GetBytes(texto);
        pdf.Write(bytes, 0, bytes.Length);
    }
}
