using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    // Arma el PDF de una factura a partir de lo que regresa sp_Factura_Obtener
    static class FacturaPdf
    {
        public static void Generar(DataSet ds, string ruta)
        {
            DataRow h = ds.Tables[0].Rows[0];
            DataTable det = ds.Tables[1];
            PdfSimple pdf = new PdfSimple();
            double izq = 40, der = 572;

            // encabezado
            pdf.Rect(0, 0, 612, 70, 0.14, false);
            pdf.TextoBlanco(izq, 38, "FACTURA DE VENTA DE BOLETOS", 18, true, 0);
            pdf.TextoBlanco(izq, 56, Convert.ToString(h["nombre_emisor"]), 10, false, 0);
            string estado = Convert.ToString(h["estado"]);
            pdf.TextoBlanco(der, 38, estado.ToUpper(), 14, true, 2);

            // datos del comprobante
            double y = 100;
            DateTime fecha = Convert.ToDateTime(h["fecha_emision"]);
            string hora = Convert.ToString(h["hora_emision"]);
            if (hora.Length >= 5) hora = hora.Substring(0, 5);
            pdf.Texto(izq, y, "Folio de factura:", 9, true);
            pdf.Texto(izq + 90, y, Convert.ToString(h["folio_factura"]), 9, false);
            pdf.Texto(izq, y + 14, "Folio de venta:", 9, true);
            pdf.Texto(izq + 90, y + 14, Convert.ToString(h["folio_venta"]), 9, false);
            pdf.Texto(izq, y + 28, "Fecha y hora de emisión:", 9, true);
            pdf.Texto(izq + 120, y + 28, Formato.Fecha(fecha) + " " + hora, 9, false);
            pdf.Texto(izq, y + 42, "Método de pago:", 9, true);
            pdf.Texto(izq + 90, y + 42, Convert.ToString(h["metodo_pago"]).Replace("Credito", "Crédito").Replace("Debito", "Débito"), 9, false);

            // emisor y receptor
            y = 170;
            pdf.Rect(izq, y, 256, 80, -1, true);
            pdf.Rect(izq + 276, y, 256, 80, -1, true);
            pdf.Texto(izq + 8, y + 16, "DATOS DEL EMISOR", 9, true);
            pdf.Texto(izq + 8, y + 32, PdfSimple.Recortar(Convert.ToString(h["nombre_emisor"]), 240, 9, false), 9, false);
            pdf.Texto(izq + 8, y + 46, "RFC: " + Convert.ToString(h["rfc_emisor"]), 9, false);
            pdf.Texto(izq + 8, y + 60, PdfSimple.Recortar("Domicilio: " + Convert.ToString(h["domicilio_emisor"]), 240, 8, false), 8, false);
            pdf.Texto(izq + 284, y + 16, "DATOS DEL RECEPTOR", 9, true);
            pdf.Texto(izq + 284, y + 32, PdfSimple.Recortar(Convert.ToString(h["nombre_receptor"]), 240, 9, false), 9, false);
            pdf.Texto(izq + 284, y + 46, "RFC: " + Convert.ToString(h["rfc_receptor"]), 9, false);
            pdf.Texto(izq + 284, y + 60, PdfSimple.Recortar("Domicilio: " + Convert.ToString(h["domicilio_receptor"]), 240, 8, false), 8, false);

            // tabla de detalle
            y = 275;
            double[] w = { 120, 90, 62, 82, 52, 30, 52, 44 };   // partido, estadio, zona, asiento, p.unit, cant, subtotal, iva
            string[] titulos = { "Partido", "Estadio", "Zona", "Asiento", "P. unitario", "Cant.", "Subtotal", "IVA" };
            pdf.Rect(izq, y, der - izq, 18, 0.14, false);
            double x = izq;
            for (int i = 0; i < titulos.Length; i++)
            {
                pdf.TextoBlanco(x + 3, y + 12, titulos[i], 8, true, 0);
                x += w[i];
            }
            y += 18;
            foreach (DataRow r in det.Rows)
            {
                x = izq;
                string[] celdas = {
                    Convert.ToString(r["Partido"]), Convert.ToString(r["Estadio"]), Convert.ToString(r["Zona"]),
                    Convert.ToString(r["Asiento"]), Formato.Dinero(Convert.ToDecimal(r["Precio unitario"])),
                    Convert.ToString(r["Cantidad"]), Formato.Dinero(Convert.ToDecimal(r["Subtotal"])),
                    Formato.Dinero(Convert.ToDecimal(r["IVA"]))
                };
                for (int i = 0; i < celdas.Length; i++)
                {
                    if (i >= 4) pdf.Texto(x + w[i] - 3, y + 13, celdas[i], 8, false, 2);
                    else pdf.Texto(x + 3, y + 13, PdfSimple.Recortar(celdas[i], w[i] - 6, 8, false), 8, false);
                    x += w[i];
                }
                pdf.Linea(izq, y + 19, der, y + 19, 0.4);
                y += 19;
                if (y > 700) { pdf.NuevaPagina(); y = 60; }
            }

            // totales
            y += 14;
            double xl = der - 190, xr = der;
            pdf.Texto(xl, y, "Subtotal:", 10, false);
            pdf.Texto(xr, y, Formato.Dinero(Convert.ToDecimal(h["subtotal"])), 10, false, 2);
            pdf.Texto(xl, y + 16, "IVA (16%):", 10, false);
            pdf.Texto(xr, y + 16, Formato.Dinero(Convert.ToDecimal(h["iva"])), 10, false, 2);
            pdf.Linea(xl, y + 24, xr, y + 24, 0.8);
            pdf.Texto(xl, y + 40, "TOTAL:", 12, true);
            pdf.Texto(xr, y + 40, Formato.Dinero(Convert.ToDecimal(h["total"])), 12, true, 2);

            pdf.Texto(PdfSimple.Ancho / 2, 760, "Documento generado por BoleteraPrototipo", 8, false, 1);
            pdf.Guardar(ruta);
        }

        // Pide dónde guardar, genera el PDF y lo abre
        public static void ExportarDeVenta(int idVenta, string folioVenta, IWin32Window dueno)
        {
            try
            {
                DataSet ds = new VentaRepositorio().Factura(idVenta);
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "Archivo PDF (*.pdf)|*.pdf";
                    dlg.Title = "Guardar factura";
                    string corto = (folioVenta ?? "").Length >= 8 ? folioVenta.Substring(0, 8) : "venta" + idVenta;
                    dlg.FileName = "Factura_" + corto + ".pdf";
                    if (dlg.ShowDialog(dueno) != DialogResult.OK) return;
                    Generar(ds, dlg.FileName);
                    if (Tema.Pregunta("Factura guardada. ¿Quieres abrirla?", "Factura"))
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(dlg.FileName) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                Tema.Manejar(ex, "Factura");
            }
        }
    }
}
