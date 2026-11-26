using System.Runtime.InteropServices;

namespace TemplateCSharp.Forms;

// Copia el folio de una venta para pegarlo en Factura o en Cancelación
public static class Portapapeles
{
    public static bool CopiarFolio(Guid folio)
    {
        try
        {
            Clipboard.SetText(folio.ToString());
            return true;
        }
        catch (ExternalException)
        {
            // Otro programa tiene ocupado el portapapeles
            return false;
        }
    }

    // Clic derecho → "Copiar folio" o Ctrl+C copian el folio del renglón seleccionado
    public static void HabilitarCopiaFolio(DataGridView tabla, DataGridViewColumn columnaFolio)
    {
        ContextMenuStrip menu = new();
        menu.Items.Add("Copiar folio", null, (_, _) => CopiarSeleccionado(tabla, columnaFolio));
        tabla.ContextMenuStrip = menu;

        tabla.CellMouseDown += (_, e) =>
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                tabla.CurrentCell = tabla.Rows[e.RowIndex].Cells[e.ColumnIndex >= 0 ? e.ColumnIndex : columnaFolio.Index];
            }
        };

        tabla.KeyDown += (_, e) =>
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopiarSeleccionado(tabla, columnaFolio);
                e.Handled = true;
            }
        };
    }

    private static void CopiarSeleccionado(DataGridView tabla, DataGridViewColumn columnaFolio)
    {
        if (tabla.CurrentRow?.Cells[columnaFolio.Index].Value is not Guid folio)
        {
            return;
        }

        if (!CopiarFolio(folio))
        {
            Mensajes.Aviso("No se pudo copiar el folio. Intente de nuevo.");
        }
    }
}
