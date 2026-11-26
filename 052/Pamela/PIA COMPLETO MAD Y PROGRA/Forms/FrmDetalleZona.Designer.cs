namespace TemplateCSharp.Forms
{
    partial class FrmDetalleZona
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
            txtAgreZona = new Label();
            txtNomZonaAZ = new Label();
            tBNomZonaAZ = new TextBox();
            tBCupoZonaAZ = new TextBox();
            txtCupoZonaAZ = new Label();
            tBPrecioBaseAZ = new TextBox();
            txtPrecioBaseAZ = new Label();
            botonGuardarAZ = new Button();
            botonCancelAZ = new Button();
            SuspendLayout();
            // 
            // txtAgreZona
            // 
            txtAgreZona.AutoSize = true;
            txtAgreZona.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAgreZona.ForeColor = Color.Red;
            txtAgreZona.Location = new Point(54, 9);
            txtAgreZona.Name = "txtAgreZona";
            txtAgreZona.Size = new Size(199, 30);
            txtAgreZona.TabIndex = 0;
            txtAgreZona.Text = "Agregar Zona";
            // 
            // txtNomZonaAZ
            // 
            txtNomZonaAZ.AutoSize = true;
            txtNomZonaAZ.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNomZonaAZ.Location = new Point(39, 60);
            txtNomZonaAZ.Name = "txtNomZonaAZ";
            txtNomZonaAZ.Size = new Size(67, 17);
            txtNomZonaAZ.TabIndex = 1;
            txtNomZonaAZ.Text = "Nombre:";
            // 
            // tBNomZonaAZ
            // 
            tBNomZonaAZ.Location = new Point(112, 54);
            tBNomZonaAZ.Name = "tBNomZonaAZ";
            tBNomZonaAZ.Size = new Size(193, 23);
            tBNomZonaAZ.TabIndex = 2;
            // 
            // tBCupoZonaAZ
            // 
            tBCupoZonaAZ.Location = new Point(112, 112);
            tBCupoZonaAZ.Name = "tBCupoZonaAZ";
            tBCupoZonaAZ.Size = new Size(193, 23);
            tBCupoZonaAZ.TabIndex = 4;
            // 
            // txtCupoZonaAZ
            // 
            txtCupoZonaAZ.AutoSize = true;
            txtCupoZonaAZ.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCupoZonaAZ.Location = new Point(54, 118);
            txtCupoZonaAZ.Name = "txtCupoZonaAZ";
            txtCupoZonaAZ.Size = new Size(49, 17);
            txtCupoZonaAZ.TabIndex = 3;
            txtCupoZonaAZ.Text = "Cupo:";
            // 
            // tBPrecioBaseAZ
            // 
            tBPrecioBaseAZ.Location = new Point(112, 83);
            tBPrecioBaseAZ.Name = "tBPrecioBaseAZ";
            tBPrecioBaseAZ.Size = new Size(193, 23);
            tBPrecioBaseAZ.TabIndex = 6;
            // 
            // txtPrecioBaseAZ
            // 
            txtPrecioBaseAZ.AutoSize = true;
            txtPrecioBaseAZ.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPrecioBaseAZ.Location = new Point(12, 89);
            txtPrecioBaseAZ.Name = "txtPrecioBaseAZ";
            txtPrecioBaseAZ.Size = new Size(94, 17);
            txtPrecioBaseAZ.TabIndex = 5;
            txtPrecioBaseAZ.Text = "Precio base:";
            // 
            // botonGuardarAZ
            // 
            botonGuardarAZ.Location = new Point(139, 151);
            botonGuardarAZ.Name = "botonGuardarAZ";
            botonGuardarAZ.Size = new Size(81, 40);
            botonGuardarAZ.TabIndex = 7;
            botonGuardarAZ.Text = "Guardar";
            botonGuardarAZ.UseVisualStyleBackColor = true;
            // 
            // botonCancelAZ
            // 
            botonCancelAZ.Location = new Point(226, 151);
            botonCancelAZ.Name = "botonCancelAZ";
            botonCancelAZ.Size = new Size(79, 40);
            botonCancelAZ.TabIndex = 8;
            botonCancelAZ.Text = "Cancelar";
            botonCancelAZ.UseVisualStyleBackColor = true;
            // 
            // FrmDetalleZona
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(332, 214);
            Controls.Add(botonCancelAZ);
            Controls.Add(botonGuardarAZ);
            Controls.Add(tBPrecioBaseAZ);
            Controls.Add(txtPrecioBaseAZ);
            Controls.Add(tBCupoZonaAZ);
            Controls.Add(txtCupoZonaAZ);
            Controls.Add(tBNomZonaAZ);
            Controls.Add(txtNomZonaAZ);
            Controls.Add(txtAgreZona);
            Name = "FrmDetalleZona";
            Text = "Agregar zona";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtAgreZona;
        private Label txtNomZonaAZ;
        private TextBox tBNomZonaAZ;
        private TextBox tBCupoZonaAZ;
        private Label txtCupoZonaAZ;
        private TextBox tBPrecioBaseAZ;
        private Label txtPrecioBaseAZ;
        private Button botonGuardarAZ;
        private Button botonCancelAZ;
    }
}