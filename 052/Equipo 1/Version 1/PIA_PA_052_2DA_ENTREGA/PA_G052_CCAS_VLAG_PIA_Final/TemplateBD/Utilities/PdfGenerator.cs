using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TemplateCSharp.Utilities
{
    public static class PdfGenerator
    {
        //PDF simple a partir de una lista de líneas de texto.
        public static void GenerarPdf(string rutaArchivo, List<string> lineas)
        {
            var sb = new StringBuilder();
            var offsets = new List<int>();

            //guarda la posición (byte offset) antes de escribir cada objeto
            void AddObject(string contenido)
            {
                offsets.Add(sb.Length);
                sb.Append(contenido);
            }

            //Header
            sb.Append("%PDF-1.4\n");

            //Catálogo
            AddObject("1 0 obj\n<< /Type /Catalog /Pages 2 0 R >>\nendobj\n");

            //Lista de páginas
            AddObject("2 0 obj\n<< /Type /Pages /Kids [3 0 R] /Count 1 >>\nendobj\n");

            //Pagina
            AddObject("3 0 obj\n<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Contents 4 0 R /Resources << /Font << /F1 5 0 R >> >> >>\nendobj\n");

            //Construir el contenido (texto)
            var content = new StringBuilder();
            content.Append("BT\n/F1 11 Tf\n40 760 Td\n14 TL\n");
            foreach (var linea in lineas)
            {
                content.Append($"({Escapar(linea)}) Tj\nT*\n");
            }
            content.Append("ET\n");
            string stream = content.ToString();

            //Contenido de la página
            AddObject($"4 0 obj\n<< /Length {stream.Length} >>\nstream\n{stream}endstream\nendobj\n");

            //Fuente (Courier para que las columnas se alineen)
            AddObject("5 0 obj\n<< /Type /Font /Subtype /Type1 /BaseFont /Courier /Encoding /WinAnsiEncoding >>\nendobj\n");

            //Tabla xref (posiciones de los objetos)
            int xrefOffset = sb.Length;
            sb.Append("xref\n0 6\n");
            sb.Append("0000000000 65535 f \n");
            foreach (var offset in offsets)
            {
                sb.Append(offset.ToString("D10")).Append(" 00000 n \n");
            }

            //Trailer
            sb.Append("trailer\n<< /Size 6 /Root 1 0 R >>\n");
            sb.Append($"startxref\n{xrefOffset}\n%%EOF");

            //Escribir el archivo con File.WriteAllText
            File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.Latin1);
        }

        //Escapa caracteres especiales del formato PDF
        private static string Escapar(string texto)
        {
            return texto
                .Replace("\\", "\\\\")
                .Replace("(", "\\(")
                .Replace(")", "\\)");
        }
    }
}
