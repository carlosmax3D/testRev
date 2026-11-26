namespace TemplateCSharp.Forms
{
    partial class FrmDetalleEstadio
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
            txtDetalleEstadio = new Label();
            txtNomEstdDE = new Label();
            tBNomEstdDE = new TextBox();
            txtCdEstdDE = new Label();
            textBox1 = new TextBox();
            txtCapEstdDE = new Label();
            textBox2 = new TextBox();
            txtZonasDE = new Label();
            dGVZonasDE = new DataGridView();
            BotonAgreZonaDE = new Button();
            botonElimZonaDE = new Button();
            botonGuardDE = new Button();
            botonCancelDE = new Button();
            txtMapaDE = new Label();
            tBMapaDE = new TextBox();
            botonMapaDE = new Button();
            botonVerMapaDE = new Button();
            ((System.ComponentModel.ISupportInitialize)dGVZonasDE).BeginInit();
            txtVistaMapaDE = new Label();
            pBMapaDE = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pBMapaDE).BeginInit();
            SuspendLayout();
            // 
            // txtDetalleEstadio
            // 
            txtDetalleEstadio.AutoSize = true;
            txtDetalleEstadio.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtDetalleEstadio.ForeColor = Color.FromArgb(0, 192, 0);
            txtDetalleEstadio.Location = new Point(79, 9);
            txtDetalleEstadio.Name = "txtDetalleEstadio";
            txtDetalleEstadio.Size = new Size(269, 30);
            txtDetalleEstadio.TabIndex = 0;
            txtDetalleEstadio.Text = "Detalle de Estadio";
            // 
            // txtNomEstdDE
            // 
            txtNomEstdDE.AutoSize = true;
            txtNomEstdDE.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNomEstdDE.Location = new Point(31, 55);
            txtNomEstdDE.Name = "txtNomEstdDE";
            txtNomEstdDE.Size = new Size(67, 17);
            txtNomEstdDE.TabIndex = 1;
            txtNomEstdDE.Text = "Nombre:";
            // 
            // tBNomEstdDE
            // 
            tBNomEstdDE.Location = new Point(104, 54);
            tBNomEstdDE.Name = "tBNomEstdDE";
            tBNomEstdDE.Size = new Size(374, 23);
            tBNomEstdDE.TabIndex = 2;
            // 
            // txtCdEstdDE
            // 
            txtCdEstdDE.AutoSize = true;
            txtCdEstdDE.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCdEstdDE.Location = new Point(39, 97);
            txtCdEstdDE.Name = "txtCdEstdDE";
            txtCdEstdDE.Size = new Size(63, 17);
            txtCdEstdDE.TabIndex = 3;
            txtCdEstdDE.Text = "Ciudad:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(104, 96);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(374, 23);
            textBox1.TabIndex = 4;
            // 
            // txtCapEstdDE
            // 
            txtCapEstdDE.AutoSize = true;
            txtCapEstdDE.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCapEstdDE.Location = new Point(16, 135);
            txtCapEstdDE.Name = "txtCapEstdDE";
            txtCapEstdDE.Size = new Size(86, 17);
            txtCapEstdDE.TabIndex = 5;
            txtCapEstdDE.Text = "Capacidad:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(104, 134);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(374, 23);
            textBox2.TabIndex = 6;
            // 
            // txtZonasDE
            // 
            txtZonasDE.AutoSize = true;
            txtZonasDE.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtZonasDE.ForeColor = Color.Red;
            txtZonasDE.Location = new Point(12, 178);
            txtZonasDE.Name = "txtZonasDE";
            txtZonasDE.Size = new Size(61, 20);
            txtZonasDE.TabIndex = 7;
            txtZonasDE.Text = "Zonas";
            // 
            // dGVZonasDE
            // 
            dGVZonasDE.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGVZonasDE.Location = new Point(16, 201);
            dGVZonasDE.Name = "dGVZonasDE";
            dGVZonasDE.Size = new Size(462, 150);
            dGVZonasDE.TabIndex = 8;
            // 
            // BotonAgreZonaDE
            // 
            BotonAgreZonaDE.Location = new Point(16, 357);
            BotonAgreZonaDE.Name = "BotonAgreZonaDE";
            BotonAgreZonaDE.Size = new Size(113, 39);
            BotonAgreZonaDE.TabIndex = 9;
            BotonAgreZonaDE.Text = "Agregar Zona";
            BotonAgreZonaDE.UseVisualStyleBackColor = true;
            // 
            // botonElimZonaDE
            // 
            botonElimZonaDE.Location = new Point(148, 357);
            botonElimZonaDE.Name = "botonElimZonaDE";
            botonElimZonaDE.Size = new Size(113, 39);
            botonElimZonaDE.TabIndex = 10;
            botonElimZonaDE.Text = "Eliminar Zona";
            botonElimZonaDE.UseVisualStyleBackColor = true;
            // 
            // botonGuardDE
            // 
            botonGuardDE.Location = new Point(79, 447);
            botonGuardDE.Name = "botonGuardDE";
            botonGuardDE.Size = new Size(113, 39);
            botonGuardDE.TabIndex = 11;
            botonGuardDE.Text = "Guardar";
            botonGuardDE.UseVisualStyleBackColor = true;
            // 
            // botonCancelDE
            // 
            botonCancelDE.Location = new Point(235, 447);
            botonCancelDE.Name = "botonCancelDE";
            botonCancelDE.Size = new Size(113, 39);
            botonCancelDE.TabIndex = 12;
            botonCancelDE.Text = "Cancelar";
            botonCancelDE.UseVisualStyleBackColor = true;
            // 
            // txtMapaDE
            // 
            txtMapaDE.AutoSize = true;
            txtMapaDE.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMapaDE.Location = new Point(16, 412);
            txtMapaDE.Name = "txtMapaDE";
            txtMapaDE.Size = new Size(50, 17);
            txtMapaDE.TabIndex = 13;
            txtMapaDE.Text = "Mapa:";
            // 
            // tBMapaDE
            // 
            tBMapaDE.Location = new Point(104, 409);
            tBMapaDE.Name = "tBMapaDE";
            tBMapaDE.ReadOnly = true;
            tBMapaDE.Size = new Size(190, 23);
            tBMapaDE.TabIndex = 14;
            tBMapaDE.TabStop = false;
            // 
            // botonMapaDE
            // 
            botonMapaDE.Location = new Point(300, 405);
            botonMapaDE.Name = "botonMapaDE";
            botonMapaDE.Size = new Size(88, 30);
            botonMapaDE.TabIndex = 15;
            botonMapaDE.Text = "Examinar...";
            botonMapaDE.UseVisualStyleBackColor = true;
            // 
            // botonVerMapaDE
            // 
            botonVerMapaDE.Location = new Point(394, 405);
            botonVerMapaDE.Name = "botonVerMapaDE";
            botonVerMapaDE.Size = new Size(84, 30);
            botonVerMapaDE.TabIndex = 16;
            botonVerMapaDE.Text = "Ver mapa";
            botonVerMapaDE.UseVisualStyleBackColor = true;
            // 
            // txtVistaMapaDE
            // 
            txtVistaMapaDE.AutoSize = true;
            txtVistaMapaDE.Location = new Point(496, 54);
            txtVistaMapaDE.Name = "txtVistaMapaDE";
            txtVistaMapaDE.Size = new Size(150, 15);
            txtVistaMapaDE.TabIndex = 17;
            txtVistaMapaDE.Text = "Vista previa del mapa:";
            // 
            // pBMapaDE
            // 
            pBMapaDE.BackColor = Color.White;
            pBMapaDE.BorderStyle = BorderStyle.FixedSingle;
            pBMapaDE.Location = new Point(496, 76);
            pBMapaDE.Name = "pBMapaDE";
            pBMapaDE.Size = new Size(282, 320);
            pBMapaDE.SizeMode = PictureBoxSizeMode.Zoom;
            pBMapaDE.TabIndex = 18;
            pBMapaDE.TabStop = false;
            // 
            // FrmDetalleEstadio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 500);
            Controls.Add(pBMapaDE);
            Controls.Add(txtVistaMapaDE);
            Controls.Add(botonVerMapaDE);
            Controls.Add(botonMapaDE);
            Controls.Add(tBMapaDE);
            Controls.Add(txtMapaDE);
            Controls.Add(botonCancelDE);
            Controls.Add(botonGuardDE);
            Controls.Add(botonElimZonaDE);
            Controls.Add(BotonAgreZonaDE);
            Controls.Add(dGVZonasDE);
            Controls.Add(txtZonasDE);
            Controls.Add(textBox2);
            Controls.Add(txtCapEstdDE);
            Controls.Add(textBox1);
            Controls.Add(txtCdEstdDE);
            Controls.Add(tBNomEstdDE);
            Controls.Add(txtNomEstdDE);
            Controls.Add(txtDetalleEstadio);
            Name = "FrmDetalleEstadio";
            Text = "Detalle del estadio";
            ((System.ComponentModel.ISupportInitialize)dGVZonasDE).EndInit();
            ((System.ComponentModel.ISupportInitialize)pBMapaDE).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pBMapaDE;

        private Label txtVistaMapaDE;

        private Label txtDetalleEstadio;
        private Label txtNomEstdDE;
        private TextBox tBNomEstdDE;
        private Label txtCdEstdDE;
        private TextBox textBox1;
        private Label txtCapEstdDE;
        private TextBox textBox2;
        private Label txtZonasDE;
        private DataGridView dGVZonasDE;
        private Button BotonAgreZonaDE;
        private Button botonElimZonaDE;
        private Button botonGuardDE;
        private Button botonCancelDE;
        private Label txtMapaDE;
        private TextBox tBMapaDE;
        private Button botonMapaDE;
        private Button botonVerMapaDE;
    }
}