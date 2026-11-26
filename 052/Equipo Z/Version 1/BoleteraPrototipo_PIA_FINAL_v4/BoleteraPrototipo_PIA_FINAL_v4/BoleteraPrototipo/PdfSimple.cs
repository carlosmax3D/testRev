using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace BoleteraPrototipo
{
    // Generador de PDF muy sencillo (solo texto, líneas y rectángulos) para no depender
    // de librerías externas. Las coordenadas van en puntos, con el origen ARRIBA a la izquierda
    // de una hoja carta (612 x 792).
    class PdfSimple
    {
        public const double Ancho = 612, Alto = 792;
        readonly List<StringBuilder> paginas = new List<StringBuilder>();
        StringBuilder actual;

        public PdfSimple()
        {
            NuevaPagina();
        }

        public void NuevaPagina()
        {
            actual = new StringBuilder();
            paginas.Add(actual);
        }

        static string N(double v)
        {
            return v.ToString("0.##", CultureInfo.InvariantCulture);
        }

        static string Escapar(string t)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in t ?? "")
            {
                if (c == '\\' || c == '(' || c == ')') { sb.Append('\\'); sb.Append(c); }
                else if (c < 32) sb.Append(' ');
                else if (c > 255) sb.Append('?');
                else sb.Append(c);
            }
            return sb.ToString();
        }

        // ancho aproximado del texto en Helvetica (sirve para alinear a la derecha y recortar)
        public static double AnchoTexto(string t, double tam, bool negrita)
        {
            double suma = 0;
            foreach (char c in t ?? "")
            {
                if (char.IsDigit(c) || c == '$') suma += 0.556;
                else if (c == ' ' || c == '.' || c == ',' || c == ':' || c == ';' || c == '\'' || c == '|' || c == '!') suma += 0.278;
                else if (c == 'i' || c == 'l' || c == 'j' || c == 'I' || c == 't' || c == 'f' || c == 'r') suma += 0.28;
                else if (c == 'm' || c == 'w' || c == 'M' || c == 'W') suma += 0.8;
                else if (char.IsUpper(c)) suma += 0.68;
                else suma += 0.53;
            }
            return suma * tam * (negrita ? 1.06 : 1.0);
        }

        public static string Recortar(string t, double ancho, double tam, bool negrita)
        {
            t = t ?? "";
            if (AnchoTexto(t, tam, negrita) <= ancho) return t;
            while (t.Length > 1 && AnchoTexto(t + "...", tam, negrita) > ancho) t = t.Substring(0, t.Length - 1);
            return t + "...";
        }

        // alineacion: 0 izquierda, 1 centro, 2 derecha (x es el punto de referencia)
        public void Texto(double x, double y, string texto, double tam, bool negrita, int alineacion)
        {
            double w = AnchoTexto(texto, tam, negrita);
            if (alineacion == 1) x -= w / 2;
            else if (alineacion == 2) x -= w;
            actual.Append("BT /").Append(negrita ? "F2" : "F1").Append(' ').Append(N(tam)).Append(" Tf ")
                  .Append(N(x)).Append(' ').Append(N(Alto - y)).Append(" Td (")
                  .Append(Escapar(texto)).Append(") Tj ET\n");
        }

        public void Texto(double x, double y, string texto, double tam, bool negrita)
        {
            Texto(x, y, texto, tam, negrita, 0);
        }

        public void Linea(double x1, double y1, double x2, double y2, double grosor)
        {
            actual.Append(N(grosor)).Append(" w ").Append(N(x1)).Append(' ').Append(N(Alto - y1)).Append(" m ")
                  .Append(N(x2)).Append(' ').Append(N(Alto - y2)).Append(" l S\n");
        }

        // rectángulo; si relleno >= 0 se rellena con ese gris (0 negro, 1 blanco); si borde es true se dibuja el contorno
        public void Rect(double x, double y, double w, double h, double relleno, bool borde)
        {
            if (relleno >= 0)
                actual.Append(N(relleno)).Append(" g ").Append(N(x)).Append(' ').Append(N(Alto - y - h)).Append(' ')
                      .Append(N(w)).Append(' ').Append(N(h)).Append(" re f 0 g\n");
            if (borde)
                actual.Append("0.7 w ").Append(N(x)).Append(' ').Append(N(Alto - y - h)).Append(' ')
                      .Append(N(w)).Append(' ').Append(N(h)).Append(" re S\n");
        }

        // texto en blanco (para encabezados con fondo oscuro)
        public void TextoBlanco(double x, double y, string texto, double tam, bool negrita, int alineacion)
        {
            actual.Append("1 g\n");
            Texto(x, y, texto, tam, negrita, alineacion);
            actual.Append("0 g\n");
        }

        public byte[] ObtenerBytes()
        {
            Encoding latin = Encoding.GetEncoding(28591);
            MemoryStream ms = new MemoryStream();
            List<long> offsets = new List<long>();
            Action<string> escribir = delegate (string s)
            {
                byte[] b = latin.GetBytes(s);
                ms.Write(b, 0, b.Length);
            };
            Action<int, string> objeto = delegate (int num, string cuerpo)
            {
                while (offsets.Count < num) offsets.Add(0);
                offsets[num - 1] = ms.Position;
                escribir(num + " 0 obj\n" + cuerpo + "\nendobj\n");
            };

            escribir("%PDF-1.4\n");
            int total = 4 + paginas.Count * 2;
            objeto(1, "<< /Type /Catalog /Pages 2 0 R >>");
            StringBuilder kids = new StringBuilder();
            for (int i = 0; i < paginas.Count; i++) kids.Append(5 + i * 2).Append(" 0 R ");
            objeto(2, "<< /Type /Pages /Kids [" + kids + "] /Count " + paginas.Count + " >>");
            objeto(3, "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>");
            objeto(4, "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica-Bold /Encoding /WinAnsiEncoding >>");
            for (int i = 0; i < paginas.Count; i++)
            {
                int pag = 5 + i * 2, cont = 6 + i * 2;
                objeto(pag, "<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Resources << /Font << /F1 3 0 R /F2 4 0 R >> >> /Contents " + cont + " 0 R >>");
                string contenido = paginas[i].ToString();
                objeto(cont, "<< /Length " + latin.GetByteCount(contenido) + " >>\nstream\n" + contenido + "endstream");
            }
            long xref = ms.Position;
            escribir("xref\n0 " + (total + 1) + "\n0000000000 65535 f \n");
            foreach (long o in offsets) escribir(o.ToString("0000000000") + " 00000 n \n");
            escribir("trailer\n<< /Size " + (total + 1) + " /Root 1 0 R >>\nstartxref\n" + xref + "\n%%EOF\n");
            return ms.ToArray();
        }

        public void Guardar(string ruta)
        {
            File.WriteAllBytes(ruta, ObtenerBytes());
        }
    }
}
