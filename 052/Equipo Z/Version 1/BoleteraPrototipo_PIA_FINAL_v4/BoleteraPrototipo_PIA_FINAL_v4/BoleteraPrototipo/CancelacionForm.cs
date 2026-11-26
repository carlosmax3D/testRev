using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    // Cancelación de ventas con el folio (solo administrador)
    public class CancelacionForm : Form
    {
        readonly VentaRepositorio ventas = new VentaRepositorio();
        TextBox txtFolio, txtMotivo;
        DataGridView dgv;
        Label lblSeleccion;
        string folioElegido = null;

        public CancelacionForm()
        {
            Ui.PrepararForm(this, "Cancelación de ventas", "Cancelación de ventas", 960, 580);

            GroupBox g = Ui.Grupo(this, "1. Buscar la venta por su folio (código de compra)", 12, 70, 936, 290);
            Ui.Etiqueta(g, "Folio (puede ser una parte)", 12, 24);
            txtFolio = Ui.Texto(g, 12, 44, 380, 36);
            txtFolio.KeyDown += delegate (object s, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Buscar(); }
            };
            Ui.Boton(g, "Buscar", 402, 42, 100, 27, Ui.Azul, delegate { Buscar(); });
            Ui.Boton(g, "Ver todas", 510, 42, 100, 27, Ui.Gris, delegate { txtFolio.Clear(); Buscar(); });
            dgv = Ui.Grid(g, 12, 80, 910, 200);
            dgv.CellClick += dgv_CellClick;

            GroupBox c = Ui.Grupo(this, "2. Cancelar", 12, 368, 936, 200);
            lblSeleccion = Ui.Etiqueta(c, "Elige una venta de la tabla.", 12, 24);
            lblSeleccion.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSeleccion.ForeColor = Ui.Navy;
            Ui.Etiqueta(c, "Motivo de la cancelación", 12, 54);
            txtMotivo = Ui.Texto(c, 12, 74, 700, 200);
            Label nota = Ui.Etiqueta(c, "Al cancelar se liberan los boletos y se cancela la factura de la venta.", 12, 106);
            nota.ForeColor = Color.DimGray;
            Ui.Boton(c, "Cancelar venta", 12, 140, 200, 40, Ui.Rojo, btnCancelar_Click);
            Ui.Boton(c, "Cerrar", 222, 140, 120, 40, Ui.Gris, delegate { Close(); });

            Load += delegate
            {
                if (!Sesion.EsAdmin) { Tema.Aviso("Solo un administrador puede cancelar ventas.", "Cancelación"); Close(); return; }
                Buscar();
            };
        }

        void Buscar()
        {
            try
            {
                dgv.DataSource = ventas.Listar(txtFolio.Text);
                Tema.FormatoGrid(dgv);
                folioElegido = null;
                lblSeleccion.Text = "Elige una venta de la tabla.";
            }
            catch (Exception ex) { Tema.Manejar(ex, "Cancelación"); }
        }

        void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow f = dgv.Rows[e.RowIndex];
            folioElegido = Convert.ToString(f.Cells["Folio"].Value);
            lblSeleccion.Text = "Venta " + folioElegido + "  |  " + Convert.ToString(f.Cells["Cliente"].Value)
                + "  |  " + Formato.Dinero(Convert.ToDecimal(f.Cells["Total"].Value)) + "  |  " + Convert.ToString(f.Cells["Estado"].Value);
        }

        void btnCancelar_Click(object sender, EventArgs e)
        {
            Guid folio;
            if (folioElegido == null || !Guid.TryParse(folioElegido, out folio)) { Tema.Aviso("Primero elige una venta de la tabla.", "Cancelación"); return; }
            string motivo = txtMotivo.Text.Trim();
            if (motivo == "") { Tema.Aviso("Escribe el motivo de la cancelación.", "Cancelación"); txtMotivo.Focus(); return; }
            if (!Tema.Pregunta("¿Seguro que quieres cancelar la venta " + folioElegido + "?\nEsta acción no se puede deshacer.", "Cancelación")) return;
            try
            {
                ventas.Cancelar(folio, motivo);
                txtMotivo.Clear();
                Buscar();
                Tema.Info("La venta se canceló. Los boletos quedaron libres y la factura se canceló.", "Cancelación");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Cancelación"); }
        }
    }
}
