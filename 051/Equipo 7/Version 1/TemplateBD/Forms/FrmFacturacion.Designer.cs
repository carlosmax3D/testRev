namespace TemplateCSharp.Forms
{
    partial class FrmFacturacion
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
            label1 = new Label();
            btnExportarPDF = new Button();
            txtVistaPrevia = new TextBox();
            dgvCompras = new DataGridView();
            btnBuscar = new Button();
            txtFolioCompra = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvCompras).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(83, 26);
            label1.Name = "label1";
            label1.Size = new Size(121, 20);
            label1.TabIndex = 11;
            label1.Text = "Folio de compra:";
            // 
            // btnExportarPDF
            // 
            btnExportarPDF.Location = new Point(788, 427);
            btnExportarPDF.Name = "btnExportarPDF";
            btnExportarPDF.Size = new Size(143, 65);
            btnExportarPDF.TabIndex = 10;
            btnExportarPDF.Text = "Exportar PDF";
            btnExportarPDF.UseVisualStyleBackColor = true;
            btnExportarPDF.Click += btnExportarPDF_Click;
            // 
            // txtVistaPrevia
            // 
            txtVistaPrevia.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtVistaPrevia.Location = new Point(633, 104);
            txtVistaPrevia.Multiline = true;
            txtVistaPrevia.Name = "txtVistaPrevia";
            txtVistaPrevia.ReadOnly = true;
            txtVistaPrevia.ScrollBars = ScrollBars.Vertical;
            txtVistaPrevia.Size = new Size(466, 317);
            txtVistaPrevia.TabIndex = 9;
            // 
            // dgvCompras
            // 
            dgvCompras.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCompras.Location = new Point(83, 103);
            dgvCompras.Name = "dgvCompras";
            dgvCompras.RowHeadersWidth = 51;
            dgvCompras.Size = new Size(480, 318);
            dgvCompras.TabIndex = 8;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(344, 61);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 7;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtFolioCompra
            // 
            txtFolioCompra.Location = new Point(83, 61);
            txtFolioCompra.Name = "txtFolioCompra";
            txtFolioCompra.Size = new Size(244, 27);
            txtFolioCompra.TabIndex = 6;
            // 
            // FrmFacturacion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1202, 505);
            Controls.Add(label1);
            Controls.Add(btnExportarPDF);
            Controls.Add(txtVistaPrevia);
            Controls.Add(dgvCompras);
            Controls.Add(btnBuscar);
            Controls.Add(txtFolioCompra);
            Name = "FrmFacturacion";
            Text = "FrmFacturacion";
            Load += FrmFacturacion_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCompras).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnExportarPDF;
        private TextBox txtVistaPrevia;
        private DataGridView dgvCompras;
        private Button btnBuscar;
        private TextBox txtFolioCompra;
    }
}