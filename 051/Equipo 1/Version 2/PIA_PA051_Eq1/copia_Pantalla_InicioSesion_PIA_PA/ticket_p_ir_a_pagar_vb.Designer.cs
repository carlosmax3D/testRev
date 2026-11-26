
namespace Menu_p_Operativo
{
    partial class ticket_d_compra_p_pagar
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Required method for disposing of resources.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            etiqueta_folio_ticket = new Label();
            etiqueta_subtotal = new Label();
            etiqueta_iva = new Label();
            etiqueta_total_venta = new Label();
            etiqueta_total_boletos = new Label();
            etiqueta_rfc_cliente_ticket = new Label();
            etiqueta_nom_op_ticket = new Label();
            etiqueta_nom_admin_ticket = new Label();
            etiqueta_rfc_emisor = new Label();
            rfc_emisor_ticket = new Label();
            nom_admin_ticket = new Label();
            nom_op_ticket = new Label();
            rfc_client_ticket = new Label();
            tot_d_boletos = new Label();
            tot_d_venta = new Label();
            iva_compra = new Label();
            subtotal_ticket = new Label();
            folio_d_ticket = new Label();
            cancelar_siempre_no = new Button();
            ir_a_pagar = new Button();
            SuspendLayout();
            // 
            // etiqueta_folio_ticket
            // 
            etiqueta_folio_ticket.AutoSize = true;
            etiqueta_folio_ticket.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_folio_ticket.ForeColor = SystemColors.Window;
            etiqueta_folio_ticket.Location = new Point(199, 50);
            etiqueta_folio_ticket.Margin = new Padding(4, 0, 4, 0);
            etiqueta_folio_ticket.Name = "etiqueta_folio_ticket";
            etiqueta_folio_ticket.Size = new Size(87, 29);
            etiqueta_folio_ticket.TabIndex = 0;
            etiqueta_folio_ticket.Text = "Folio: ";
            // 
            // etiqueta_subtotal
            // 
            etiqueta_subtotal.AutoSize = true;
            etiqueta_subtotal.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_subtotal.ForeColor = SystemColors.Window;
            etiqueta_subtotal.Location = new Point(199, 111);
            etiqueta_subtotal.Margin = new Padding(4, 0, 4, 0);
            etiqueta_subtotal.Name = "etiqueta_subtotal";
            etiqueta_subtotal.Size = new Size(123, 29);
            etiqueta_subtotal.TabIndex = 1;
            etiqueta_subtotal.Text = "Subtotal:";
            // 
            // etiqueta_iva
            // 
            etiqueta_iva.AutoSize = true;
            etiqueta_iva.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_iva.ForeColor = SystemColors.Window;
            etiqueta_iva.Location = new Point(199, 168);
            etiqueta_iva.Margin = new Padding(4, 0, 4, 0);
            etiqueta_iva.Name = "etiqueta_iva";
            etiqueta_iva.Size = new Size(73, 29);
            etiqueta_iva.TabIndex = 2;
            etiqueta_iva.Text = "IVA: ";
            // 
            // etiqueta_total_venta
            // 
            etiqueta_total_venta.AutoSize = true;
            etiqueta_total_venta.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_total_venta.ForeColor = SystemColors.Window;
            etiqueta_total_venta.Location = new Point(199, 231);
            etiqueta_total_venta.Margin = new Padding(4, 0, 4, 0);
            etiqueta_total_venta.Name = "etiqueta_total_venta";
            etiqueta_total_venta.Size = new Size(223, 29);
            etiqueta_total_venta.TabIndex = 3;
            etiqueta_total_venta.Text = "Total de la Venta:";
            // 
            // etiqueta_total_boletos
            // 
            etiqueta_total_boletos.AutoSize = true;
            etiqueta_total_boletos.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_total_boletos.ForeColor = SystemColors.Window;
            etiqueta_total_boletos.Location = new Point(199, 305);
            etiqueta_total_boletos.Margin = new Padding(4, 0, 4, 0);
            etiqueta_total_boletos.Name = "etiqueta_total_boletos";
            etiqueta_total_boletos.Size = new Size(222, 29);
            etiqueta_total_boletos.TabIndex = 4;
            etiqueta_total_boletos.Text = "Total de Boletos: ";
            // 
            // etiqueta_rfc_cliente_ticket
            // 
            etiqueta_rfc_cliente_ticket.AutoSize = true;
            etiqueta_rfc_cliente_ticket.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_rfc_cliente_ticket.ForeColor = SystemColors.Window;
            etiqueta_rfc_cliente_ticket.Location = new Point(199, 370);
            etiqueta_rfc_cliente_ticket.Margin = new Padding(4, 0, 4, 0);
            etiqueta_rfc_cliente_ticket.Name = "etiqueta_rfc_cliente_ticket";
            etiqueta_rfc_cliente_ticket.Size = new Size(202, 29);
            etiqueta_rfc_cliente_ticket.TabIndex = 5;
            etiqueta_rfc_cliente_ticket.Text = "RFC del Cliente:";
            // 
            // etiqueta_nom_op_ticket
            // 
            etiqueta_nom_op_ticket.AutoSize = true;
            etiqueta_nom_op_ticket.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_nom_op_ticket.ForeColor = SystemColors.Window;
            etiqueta_nom_op_ticket.Location = new Point(199, 432);
            etiqueta_nom_op_ticket.Margin = new Padding(4, 0, 4, 0);
            etiqueta_nom_op_ticket.Name = "etiqueta_nom_op_ticket";
            etiqueta_nom_op_ticket.Size = new Size(283, 29);
            etiqueta_nom_op_ticket.TabIndex = 6;
            etiqueta_nom_op_ticket.Text = "Nomina del Operador: ";
            // 
            // etiqueta_nom_admin_ticket
            // 
            etiqueta_nom_admin_ticket.AutoSize = true;
            etiqueta_nom_admin_ticket.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_nom_admin_ticket.ForeColor = SystemColors.Window;
            etiqueta_nom_admin_ticket.Location = new Point(199, 492);
            etiqueta_nom_admin_ticket.Margin = new Padding(4, 0, 4, 0);
            etiqueta_nom_admin_ticket.Name = "etiqueta_nom_admin_ticket";
            etiqueta_nom_admin_ticket.Size = new Size(341, 29);
            etiqueta_nom_admin_ticket.TabIndex = 7;
            etiqueta_nom_admin_ticket.Text = "Nomina del Administrador: ";
            // 
            // etiqueta_rfc_emisor
            // 
            etiqueta_rfc_emisor.AutoSize = true;
            etiqueta_rfc_emisor.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_rfc_emisor.ForeColor = SystemColors.Window;
            etiqueta_rfc_emisor.Location = new Point(199, 554);
            etiqueta_rfc_emisor.Margin = new Padding(4, 0, 4, 0);
            etiqueta_rfc_emisor.Name = "etiqueta_rfc_emisor";
            etiqueta_rfc_emisor.Size = new Size(201, 29);
            etiqueta_rfc_emisor.TabIndex = 8;
            etiqueta_rfc_emisor.Text = "RFC del Emisor:";
            // 
            // rfc_emisor_ticket
            // 
            rfc_emisor_ticket.AutoSize = true;
            rfc_emisor_ticket.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            rfc_emisor_ticket.ForeColor = SystemColors.Window;
            rfc_emisor_ticket.Location = new Point(626, 554);
            rfc_emisor_ticket.Margin = new Padding(4, 0, 4, 0);
            rfc_emisor_ticket.Name = "rfc_emisor_ticket";
            rfc_emisor_ticket.Size = new Size(192, 29);
            rfc_emisor_ticket.TabIndex = 17;
            rfc_emisor_ticket.Text = "RFC del Emisor";
            rfc_emisor_ticket.Click += rfc_emisor_ticket_Click;
            // 
            // nom_admin_ticket
            // 
            nom_admin_ticket.AutoSize = true;
            nom_admin_ticket.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            nom_admin_ticket.ForeColor = SystemColors.Window;
            nom_admin_ticket.Location = new Point(626, 492);
            nom_admin_ticket.Margin = new Padding(4, 0, 4, 0);
            nom_admin_ticket.Name = "nom_admin_ticket";
            nom_admin_ticket.Size = new Size(332, 29);
            nom_admin_ticket.TabIndex = 16;
            nom_admin_ticket.Text = "Nomina del Administrador ";
            nom_admin_ticket.Click += nom_admin_ticket_Click;
            // 
            // nom_op_ticket
            // 
            nom_op_ticket.AutoSize = true;
            nom_op_ticket.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            nom_op_ticket.ForeColor = SystemColors.Window;
            nom_op_ticket.Location = new Point(626, 432);
            nom_op_ticket.Margin = new Padding(4, 0, 4, 0);
            nom_op_ticket.Name = "nom_op_ticket";
            nom_op_ticket.Size = new Size(274, 29);
            nom_op_ticket.TabIndex = 15;
            nom_op_ticket.Text = "Nomina del Operador ";
            nom_op_ticket.Click += nom_op_ticket_Click;
            // 
            // rfc_client_ticket
            // 
            rfc_client_ticket.AutoSize = true;
            rfc_client_ticket.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            rfc_client_ticket.ForeColor = SystemColors.Window;
            rfc_client_ticket.Location = new Point(626, 370);
            rfc_client_ticket.Margin = new Padding(4, 0, 4, 0);
            rfc_client_ticket.Name = "rfc_client_ticket";
            rfc_client_ticket.Size = new Size(193, 29);
            rfc_client_ticket.TabIndex = 14;
            rfc_client_ticket.Text = "RFC del Cliente";
            rfc_client_ticket.Click += rfc_client_ticket_Click;
            // 
            // tot_d_boletos
            // 
            tot_d_boletos.AutoSize = true;
            tot_d_boletos.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            tot_d_boletos.ForeColor = SystemColors.Window;
            tot_d_boletos.Location = new Point(626, 305);
            tot_d_boletos.Margin = new Padding(4, 0, 4, 0);
            tot_d_boletos.Name = "tot_d_boletos";
            tot_d_boletos.Size = new Size(206, 29);
            tot_d_boletos.TabIndex = 13;
            tot_d_boletos.Text = "Total de Boletos";
            tot_d_boletos.Click += tot_d_boletos_Click;
            // 
            // tot_d_venta
            // 
            tot_d_venta.AutoSize = true;
            tot_d_venta.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            tot_d_venta.ForeColor = SystemColors.Window;
            tot_d_venta.Location = new Point(626, 231);
            tot_d_venta.Margin = new Padding(4, 0, 4, 0);
            tot_d_venta.Name = "tot_d_venta";
            tot_d_venta.Size = new Size(214, 29);
            tot_d_venta.TabIndex = 12;
            tot_d_venta.Text = "Total de la Venta";
            tot_d_venta.Click += tot_d_venta_Click;
            // 
            // iva_compra
            // 
            iva_compra.AutoSize = true;
            iva_compra.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            iva_compra.ForeColor = SystemColors.Window;
            iva_compra.Location = new Point(626, 168);
            iva_compra.Margin = new Padding(4, 0, 4, 0);
            iva_compra.Name = "iva_compra";
            iva_compra.Size = new Size(64, 29);
            iva_compra.TabIndex = 11;
            iva_compra.Text = "IVA ";
            iva_compra.Click += iva_compra_Click;
            // 
            // subtotal_ticket
            // 
            subtotal_ticket.AutoSize = true;
            subtotal_ticket.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            subtotal_ticket.ForeColor = SystemColors.Window;
            subtotal_ticket.Location = new Point(626, 111);
            subtotal_ticket.Margin = new Padding(4, 0, 4, 0);
            subtotal_ticket.Name = "subtotal_ticket";
            subtotal_ticket.Size = new Size(114, 29);
            subtotal_ticket.TabIndex = 10;
            subtotal_ticket.Text = "Subtotal";
            subtotal_ticket.Click += subtotal_ticket_Click;
            // 
            // folio_d_ticket
            // 
            folio_d_ticket.AutoSize = true;
            folio_d_ticket.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            folio_d_ticket.ForeColor = SystemColors.Window;
            folio_d_ticket.Location = new Point(626, 50);
            folio_d_ticket.Margin = new Padding(4, 0, 4, 0);
            folio_d_ticket.Name = "folio_d_ticket";
            folio_d_ticket.Size = new Size(78, 29);
            folio_d_ticket.TabIndex = 9;
            folio_d_ticket.Text = "Folio ";
            folio_d_ticket.Click += folio_d_ticket_Click;
            // 
            // cancelar_siempre_no
            // 
            cancelar_siempre_no.ForeColor = SystemColors.HotTrack;
            cancelar_siempre_no.Location = new Point(361, 661);
            cancelar_siempre_no.Margin = new Padding(4);
            cancelar_siempre_no.Name = "cancelar_siempre_no";
            cancelar_siempre_no.Size = new Size(135, 74);
            cancelar_siempre_no.TabIndex = 18;
            cancelar_siempre_no.Text = "Cancelar";
            cancelar_siempre_no.UseVisualStyleBackColor = true;
            cancelar_siempre_no.Click += cancelar_siempre_no_Click;
            // 
            // ir_a_pagar
            // 
            ir_a_pagar.ForeColor = SystemColors.HotTrack;
            ir_a_pagar.Location = new Point(610, 661);
            ir_a_pagar.Margin = new Padding(4);
            ir_a_pagar.Name = "ir_a_pagar";
            ir_a_pagar.Size = new Size(135, 72);
            ir_a_pagar.TabIndex = 19;
            ir_a_pagar.Text = "Pagar";
            ir_a_pagar.UseVisualStyleBackColor = true;
            ir_a_pagar.Click += ir_a_pagar_Click;
            // 
            // ticket_d_compra_p_pagar
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1086, 784);
            Controls.Add(ir_a_pagar);
            Controls.Add(cancelar_siempre_no);
            Controls.Add(rfc_emisor_ticket);
            Controls.Add(nom_admin_ticket);
            Controls.Add(nom_op_ticket);
            Controls.Add(rfc_client_ticket);
            Controls.Add(tot_d_boletos);
            Controls.Add(tot_d_venta);
            Controls.Add(iva_compra);
            Controls.Add(subtotal_ticket);
            Controls.Add(folio_d_ticket);
            Controls.Add(etiqueta_rfc_emisor);
            Controls.Add(etiqueta_nom_admin_ticket);
            Controls.Add(etiqueta_nom_op_ticket);
            Controls.Add(etiqueta_rfc_cliente_ticket);
            Controls.Add(etiqueta_total_boletos);
            Controls.Add(etiqueta_total_venta);
            Controls.Add(etiqueta_iva);
            Controls.Add(etiqueta_subtotal);
            Controls.Add(etiqueta_folio_ticket);
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "ticket_d_compra_p_pagar";
            Text = "Ticket";
            Load += ticket_d_compra_p_pagar_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_folio_ticket;
        private Label etiqueta_subtotal;
        private Label etiqueta_iva;
        private Label etiqueta_total_venta;
        private Label etiqueta_total_boletos;
        private Label etiqueta_rfc_cliente_ticket;
        private Label etiqueta_nom_op_ticket;
        private Label etiqueta_nom_admin_ticket;
        private Label etiqueta_rfc_emisor;
        private Label rfc_emisor_ticket;
        private Label nom_admin_ticket;
        private Label nom_op_ticket;
        private Label rfc_client_ticket;
        private Label tot_d_boletos;
        private Label tot_d_venta;
        private Label iva_compra;
        private Label subtotal_ticket;
        private Label folio_d_ticket;
        private Button cancelar_siempre_no;
        private Button ir_a_pagar;
    }
}
