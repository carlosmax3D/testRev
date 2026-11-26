namespace TemplateCSharp.Forms
{
    partial class FrmPartidos
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
            txtGestPart = new Label();
            txtBuscPart = new Label();
            buscarPart = new TextBox();
            botonBuscPart = new Button();
            DGVPart = new DataGridView();
            botonNuevoPart = new Button();
            botonEditPart = new Button();
            botonElimPart = new Button();
            botonRegresarPart = new Button();
            ((System.ComponentModel.ISupportInitialize)DGVPart).BeginInit();
            botonLimpiarPart = new Button();
            SuspendLayout();
            // 
            // txtGestPart
            // 
            txtGestPart.AutoSize = true;
            txtGestPart.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtGestPart.ForeColor = Color.Yellow;
            txtGestPart.Location = new Point(12, 9);
            txtGestPart.Name = "txtGestPart";
            txtGestPart.Size = new Size(286, 30);
            txtGestPart.TabIndex = 0;
            txtGestPart.Text = "Gestión de Partidos";
            // 
            // txtBuscPart
            // 
            txtBuscPart.AutoSize = true;
            txtBuscPart.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscPart.Location = new Point(12, 65);
            txtBuscPart.Name = "txtBuscPart";
            txtBuscPart.Size = new Size(175, 17);
            txtBuscPart.TabIndex = 1;
            txtBuscPart.Text = "Buscar partido por equipo o estadio:";
            // 
            // buscarPart
            // 
            buscarPart.Location = new Point(12, 85);
            buscarPart.Name = "buscarPart";
            buscarPart.PlaceholderText = "Equipo o estadio (vacío = todos)";
            buscarPart.Size = new Size(286, 23);
            buscarPart.TabIndex = 2;
            // 
            // botonBuscPart
            // 
            botonBuscPart.Location = new Point(313, 85);
            botonBuscPart.Name = "botonBuscPart";
            botonBuscPart.Size = new Size(75, 23);
            botonBuscPart.TabIndex = 3;
            botonBuscPart.Text = "Buscar";
            botonBuscPart.UseVisualStyleBackColor = true;
            // 
            // DGVPart
            // 
            DGVPart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVPart.Location = new Point(12, 114);
            DGVPart.Name = "DGVPart";
            DGVPart.Size = new Size(611, 127);
            DGVPart.TabIndex = 4;
            // 
            // botonNuevoPart
            // 
            botonNuevoPart.Location = new Point(12, 247);
            botonNuevoPart.Name = "botonNuevoPart";
            botonNuevoPart.Size = new Size(115, 40);
            botonNuevoPart.TabIndex = 5;
            botonNuevoPart.Text = "Nuevo Partido";
            botonNuevoPart.UseVisualStyleBackColor = true;
            // 
            // botonEditPart
            // 
            botonEditPart.Location = new Point(142, 247);
            botonEditPart.Name = "botonEditPart";
            botonEditPart.Size = new Size(115, 40);
            botonEditPart.TabIndex = 6;
            botonEditPart.Text = "Editar";
            botonEditPart.UseVisualStyleBackColor = true;
            // 
            // botonElimPart
            // 
            botonElimPart.Location = new Point(273, 247);
            botonElimPart.Name = "botonElimPart";
            botonElimPart.Size = new Size(115, 40);
            botonElimPart.TabIndex = 7;
            botonElimPart.Text = "Eliminar";
            botonElimPart.UseVisualStyleBackColor = true;
            // 
            // botonRegresarPart
            // 
            botonRegresarPart.Location = new Point(508, 274);
            botonRegresarPart.Name = "botonRegresarPart";
            botonRegresarPart.Size = new Size(115, 40);
            botonRegresarPart.TabIndex = 8;
            botonRegresarPart.Text = "Regresar al menú";
            botonRegresarPart.UseVisualStyleBackColor = true;
            botonRegresarPart.Click += button4_Click;
            // 
            // botonLimpiarPart
            // 
            botonLimpiarPart.Location = new Point(394, 85);
            botonLimpiarPart.Name = "botonLimpiarPart";
            botonLimpiarPart.Size = new Size(75, 23);
            botonLimpiarPart.TabIndex = 50;
            botonLimpiarPart.Text = "Limpiar";
            botonLimpiarPart.UseVisualStyleBackColor = true;
            botonLimpiarPart.Click += botonLimpiarPart_Click;
            // 
            // FrmPartidos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(635, 335);
            Controls.Add(botonLimpiarPart);
            Controls.Add(botonRegresarPart);
            Controls.Add(botonElimPart);
            Controls.Add(botonEditPart);
            Controls.Add(botonNuevoPart);
            Controls.Add(DGVPart);
            Controls.Add(botonBuscPart);
            Controls.Add(buscarPart);
            Controls.Add(txtBuscPart);
            Controls.Add(txtGestPart);
            Name = "FrmPartidos";
            Text = "Gestión de partidos";
            ((System.ComponentModel.ISupportInitialize)DGVPart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button botonLimpiarPart;

        private Label txtGestPart;
        private Label txtBuscPart;
        private TextBox buscarPart;
        private Button botonBuscPart;
        private DataGridView DGVPart;
        private Button botonNuevoPart;
        private Button botonEditPart;
        private Button botonElimPart;
        private Button botonRegresarPart;
    }
}