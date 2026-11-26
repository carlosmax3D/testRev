namespace TemplateCSharp.Forms
{
    partial class FrmEstadios
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
            txtGestEstd = new Label();
            BotonBuscarEstd = new Button();
            txtBuscEstd = new Label();
            buscarEstd = new TextBox();
            DGVEstd = new DataGridView();
            botonNuevoEstd = new Button();
            botonEditEstd = new Button();
            botonElimEstd = new Button();
            botonRegresarEstd = new Button();
            ((System.ComponentModel.ISupportInitialize)DGVEstd).BeginInit();
            SuspendLayout();
            // 
            // txtGestEstd
            // 
            txtGestEstd.AutoSize = true;
            txtGestEstd.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtGestEstd.ForeColor = Color.MediumVioletRed;
            txtGestEstd.Location = new Point(12, 9);
            txtGestEstd.Name = "txtGestEstd";
            txtGestEstd.Size = new Size(279, 30);
            txtGestEstd.TabIndex = 0;
            txtGestEstd.Text = "Gestión de Estadios";
            // 
            // BotonBuscarEstd
            // 
            BotonBuscarEstd.Location = new Point(306, 89);
            BotonBuscarEstd.Name = "BotonBuscarEstd";
            BotonBuscarEstd.Size = new Size(75, 23);
            BotonBuscarEstd.TabIndex = 1;
            BotonBuscarEstd.Text = "Buscar";
            BotonBuscarEstd.UseVisualStyleBackColor = true;
            // 
            // txtBuscEstd
            // 
            txtBuscEstd.AutoSize = true;
            txtBuscEstd.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscEstd.Location = new Point(12, 69);
            txtBuscEstd.Name = "txtBuscEstd";
            txtBuscEstd.Size = new Size(167, 17);
            txtBuscEstd.TabIndex = 2;
            txtBuscEstd.Text = "Buscar Estadio por ID";
            // 
            // buscarEstd
            // 
            buscarEstd.Location = new Point(12, 89);
            buscarEstd.Name = "buscarEstd";
            buscarEstd.PlaceholderText = "Ingrese el ID del Estadio";
            buscarEstd.Size = new Size(279, 23);
            buscarEstd.TabIndex = 3;
            // 
            // DGVEstd
            // 
            DGVEstd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVEstd.Location = new Point(12, 118);
            DGVEstd.Name = "DGVEstd";
            DGVEstd.Size = new Size(613, 121);
            DGVEstd.TabIndex = 4;
            // 
            // botonNuevoEstd
            // 
            botonNuevoEstd.Location = new Point(11, 245);
            botonNuevoEstd.Name = "botonNuevoEstd";
            botonNuevoEstd.Size = new Size(124, 34);
            botonNuevoEstd.TabIndex = 5;
            botonNuevoEstd.Text = "Nuevo Estadio";
            botonNuevoEstd.UseVisualStyleBackColor = true;
            // 
            // botonEditEstd
            // 
            botonEditEstd.Location = new Point(150, 245);
            botonEditEstd.Name = "botonEditEstd";
            botonEditEstd.Size = new Size(124, 34);
            botonEditEstd.TabIndex = 6;
            botonEditEstd.Text = "Editar";
            botonEditEstd.UseVisualStyleBackColor = true;
            // 
            // botonElimEstd
            // 
            botonElimEstd.Location = new Point(289, 245);
            botonElimEstd.Name = "botonElimEstd";
            botonElimEstd.Size = new Size(124, 34);
            botonElimEstd.TabIndex = 7;
            botonElimEstd.Text = "Eliminar";
            botonElimEstd.UseVisualStyleBackColor = true;
            // 
            // botonRegresarEstd
            // 
            botonRegresarEstd.Location = new Point(501, 274);
            botonRegresarEstd.Name = "botonRegresarEstd";
            botonRegresarEstd.Size = new Size(124, 34);
            botonRegresarEstd.TabIndex = 8;
            botonRegresarEstd.Text = "Regresar al Menu";
            botonRegresarEstd.UseVisualStyleBackColor = true;
            // 
            // FrmEstadios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(637, 320);
            Controls.Add(botonRegresarEstd);
            Controls.Add(botonElimEstd);
            Controls.Add(botonEditEstd);
            Controls.Add(botonNuevoEstd);
            Controls.Add(DGVEstd);
            Controls.Add(buscarEstd);
            Controls.Add(txtBuscEstd);
            Controls.Add(BotonBuscarEstd);
            Controls.Add(txtGestEstd);
            Name = "FrmEstadios";
            Text = "FrmEstadios";
            ((System.ComponentModel.ISupportInitialize)DGVEstd).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtGestEstd;
        private Button BotonBuscarEstd;
        private Label txtBuscEstd;
        private TextBox buscarEstd;
        private DataGridView DGVEstd;
        private Button botonNuevoEstd;
        private Button botonEditEstd;
        private Button botonElimEstd;
        private Button botonRegresarEstd;
    }
}