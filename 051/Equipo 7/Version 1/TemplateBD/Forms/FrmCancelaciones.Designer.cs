namespace TemplateCSharp.Forms
{
    partial class FrmCancelaciones
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtFolioCompra = new MaskedTextBox();
            btnBuscar = new Button();
            dgvCompras = new DataGridView();
            lblResumenCompra = new Label();
            txtMotivo = new MaskedTextBox();
            btnCancelarCompra = new Button();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCompras).BeginInit();
            SuspendLayout();
            // 
            // txtFolioCompra
            // 
            txtFolioCompra.Location = new Point(45, 34);
            txtFolioCompra.Name = "txtFolioCompra";
            txtFolioCompra.Size = new Size(418, 27);
            txtFolioCompra.TabIndex = 0;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(484, 34);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // dgvCompras
            // 
            dgvCompras.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCompras.Location = new Point(45, 88);
            dgvCompras.Name = "dgvCompras";
            dgvCompras.RowHeadersWidth = 51;
            dgvCompras.Size = new Size(575, 339);
            dgvCompras.TabIndex = 2;
            // 
            // lblResumenCompra
            // 
            lblResumenCompra.AutoSize = true;
            lblResumenCompra.Location = new Point(668, 88);
            lblResumenCompra.Name = "lblResumenCompra";
            lblResumenCompra.Size = new Size(0, 20);
            lblResumenCompra.TabIndex = 3;
            // 
            // txtMotivo
            // 
            txtMotivo.Location = new Point(668, 327);
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(283, 27);
            txtMotivo.TabIndex = 4;
            // 
            // btnCancelarCompra
            // 
            btnCancelarCompra.BackColor = Color.Red;
            btnCancelarCompra.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancelarCompra.ForeColor = SystemColors.Control;
            btnCancelarCompra.Location = new Point(1028, 366);
            btnCancelarCompra.Name = "btnCancelarCompra";
            btnCancelarCompra.Size = new Size(132, 72);
            btnCancelarCompra.TabIndex = 5;
            btnCancelarCompra.Text = "Cancelar";
            btnCancelarCompra.UseVisualStyleBackColor = false;
            btnCancelarCompra.Click += btnCancelarCompra_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 9);
            label2.Name = "label2";
            label2.Size = new Size(139, 20);
            label2.TabIndex = 6;
            label2.Text = "FOLIO DE COMPRA:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(668, 292);
            label3.Name = "label3";
            label3.Size = new Size(178, 20);
            label3.TabIndex = 7;
            label3.Text = "Motivo de la cancelacion:";
            // 
            // FrmCancelaciones
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1191, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnCancelarCompra);
            Controls.Add(txtMotivo);
            Controls.Add(lblResumenCompra);
            Controls.Add(dgvCompras);
            Controls.Add(btnBuscar);
            Controls.Add(txtFolioCompra);
            Name = "FrmCancelaciones";
            Text = "FrmCancelaciones";
            Load += FrmCancelaciones_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCompras).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaskedTextBox txtFolioCompra;
        private Button btnBuscar;
        private DataGridView dgvCompras;
        private Label lblResumenCompra;
        private MaskedTextBox txtMotivo;
        private Button btnCancelarCompra;
        private Label label2;
        private Label label3;
    }
}