using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models.Dtos;

namespace VentaBoletosApp.Services;

/// <summary>Genera el PDF de la factura con QuestPDF (licencia Community).</summary>
public class FacturaPdfGenerator : IFacturaPdfGenerator
{
    private const string ColorPrincipal = "#1F3A5F";
    private const string ColorSuave = "#EEF2F7";

    static FacturaPdfGenerator()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public void Generar(FacturaCompleta factura, string rutaArchivo)
    {
        Document.Create(document =>
        {
            document.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(t => t.FontSize(9).FontFamily("Arial"));

                page.Header().Element(c => Encabezado(c, factura));
                page.Content().PaddingVertical(10).Element(c => Contenido(c, factura));

                page.Footer().AlignCenter().Text(t =>
                {
                    t.DefaultTextStyle(s => s.FontSize(8).FontColor(Colors.Grey.Darken1));
                    t.Span("Página ");
                    t.CurrentPageNumber();
                    t.Span(" de ");
                    t.TotalPages();
                });
            });
        }).GeneratePdf(rutaArchivo);
    }

    private static void Encabezado(IContainer container, FacturaCompleta f)
    {
        container.Column(col =>
        {
            col.Item().Row(row =>
            {
                row.RelativeItem().Column(c =>
                {
                    c.Item().Text("FACTURA").FontSize(22).Bold().FontColor(ColorPrincipal);
                    c.Item().Text($"Folio: {f.Factura.Folio}").Bold();
                    c.Item().Text($"Fecha de emisión: {Formato.FechaHora(f.Factura.Fecha)}");
                    c.Item().Text($"Método de pago: {f.Venta.MetodoPago}");
                    c.Item().Text($"Folio de compra: {f.Venta.Folio}").FontSize(8);
                });

                if (!f.Factura.EstaVigente)
                {
                    row.ConstantItem(140).AlignRight().AlignMiddle()
                        .Border(2).BorderColor(Colors.Red.Medium).Padding(6)
                        .Text("CANCELADA").FontSize(18).Bold().FontColor(Colors.Red.Medium);
                }
            });

            col.Item().PaddingTop(6).LineHorizontal(1).LineColor(ColorPrincipal);
        });
    }

    private static void Contenido(IContainer container, FacturaCompleta f)
    {
        container.Column(col =>
        {
            col.Spacing(10);

            col.Item().Row(row =>
            {
                row.RelativeItem().Element(c => Caja(c, "EMISOR",
                    f.Emisor.RazonSocial, $"RFC: {f.Emisor.RFC}", f.Emisor.DomicilioCompleto));

                row.ConstantItem(12);

                row.RelativeItem().Element(c => Caja(c, "RECEPTOR",
                    f.Receptor.NombreCompleto, $"RFC: {f.Receptor.RFC}", f.Receptor.DomicilioCompleto));
            });

            col.Item().Table(tabla =>
            {
                tabla.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(3);   // Partido
                    c.RelativeColumn(2.2f);// Estadio
                    c.RelativeColumn(1.6f);// Zona
                    c.RelativeColumn(1.2f);// Asiento
                    c.RelativeColumn(1.5f);// P. unitario
                    c.ConstantColumn(30);  // Cant
                    c.RelativeColumn(1.5f);// Subtotal
                    c.RelativeColumn(1.4f);// IVA
                    c.RelativeColumn(1.6f);// Total
                });

                tabla.Header(h =>
                {
                    foreach (string titulo in new[]
                             { "Partido", "Estadio", "Zona", "Asiento", "P. unitario", "Cant.", "Subtotal", "IVA", "Total" })
                    {
                        h.Cell().Background(ColorPrincipal).Padding(4)
                            .Text(titulo).FontColor(Colors.White).Bold().FontSize(8);
                    }
                });

                string partido = $"{f.Venta.EquipoLocal} vs {f.Venta.EquipoVisitante}\n{Formato.FechaHora(f.Venta.FechaPartido)}";

                bool alterno = false;

                foreach (var d in f.Detalles)
                {
                    decimal iva = Math.Round(d.PrecioUnitario * VentaService.TasaIva, 2, MidpointRounding.AwayFromZero);
                    string fondo = alterno ? ColorSuave : Colors.White;
                    alterno = !alterno;

                    Celda(tabla, fondo, partido);
                    Celda(tabla, fondo, f.Venta.NombreEstadio);
                    Celda(tabla, fondo, d.NombreZona);
                    Celda(tabla, fondo, Formato.Asiento(d.Fila, d.Numero));
                    Celda(tabla, fondo, Formato.Moneda(d.PrecioUnitario), true);
                    Celda(tabla, fondo, "1", true);
                    Celda(tabla, fondo, Formato.Moneda(d.PrecioUnitario), true);
                    Celda(tabla, fondo, Formato.Moneda(iva), true);
                    Celda(tabla, fondo, Formato.Moneda(d.PrecioUnitario + iva), true);
                }
            });

            col.Item().AlignRight().Width(200).Column(t =>
            {
                Total(t, "Subtotal", Formato.Moneda(f.Venta.Subtotal));
                Total(t, "IVA (16%)", Formato.Moneda(f.Venta.IVA));
                t.Item().LineHorizontal(1).LineColor(ColorPrincipal);
                Total(t, "TOTAL", Formato.Moneda(f.Venta.Total), true);
            });

            col.Item().PaddingTop(10).Text("Este documento es un comprobante de compra de boletos. Conserve su folio para cualquier aclaración.")
                .FontSize(8).FontColor(Colors.Grey.Darken1);
        });
    }

    private static void Caja(IContainer c, string titulo, string nombre, string rfc, string domicilio)
    {
        c.Border(1).BorderColor(Colors.Grey.Lighten1).Padding(8).Column(col =>
        {
            col.Item().Text(titulo).Bold().FontColor(ColorPrincipal).FontSize(10);
            col.Item().PaddingTop(3).Text(nombre).Bold();
            col.Item().Text(rfc);
            col.Item().Text(domicilio);
        });
    }

    private static void Celda(TableDescriptor tabla, string fondo, string texto, bool derecha = false)
    {
        var cell = tabla.Cell().Background(fondo)
            .BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4);

        if (derecha)
        {
            cell.AlignRight().Text(texto).FontSize(8);
        }
        else
        {
            cell.Text(texto).FontSize(8);
        }
    }

    private static void Total(ColumnDescriptor col, string etiqueta, string valor, bool resaltar = false)
    {
        col.Item().PaddingVertical(2).Row(r =>
        {
            var e = r.RelativeItem().Text(etiqueta);
            var v = r.RelativeItem().AlignRight().Text(valor);

            if (resaltar)
            {
                e.Bold().FontSize(11);
                v.Bold().FontSize(11);
            }
        });
    }
}
