namespace TemplateCSharp.Forms
{
    partial class FrmClientes
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
            txtGestCte = new Label();
            buscarCliente = new TextBox();
            botonBuscadorCte = new Button();
            DGVCte = new DataGridView();
            txtBuscCte = new Label();
            botonNuevoCte = new Button();
            botonEditCte = new Button();
            botonElimCte = new Button();
            botonRegresarCte = new Button();
            botonHistCte = new Button();
            botonAudiCte = new Button();
            ((System.ComponentModel.ISupportInitialize)DGVCte).BeginInit();
            SuspendLayout();
            // 
            // txtGestCte
            // 
            txtGestCte.AutoSize = true;
            txtGestCte.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtGestCte.ForeColor = Color.RoyalBlue;
            txtGestCte.Location = new Point(12, 9);
            txtGestCte.Name = "txtGestCte";
            txtGestCte.Size = new Size(273, 30);
            txtGestCte.TabIndex = 0;
            txtGestCte.Text = "Gestión de Clientes";
            // 
            // buscarCliente
            // 
            buscarCliente.Location = new Point(12, 90);
            buscarCliente.Name = "buscarCliente";
            buscarCliente.PlaceholderText = "Ingrese el ID del cliente";
            buscarCliente.Size = new Size(273, 23);
            buscarCliente.TabIndex = 1;
            // 
            // botonBuscadorCte
            // 
            botonBuscadorCte.Location = new Point(301, 90);
            botonBuscadorCte.Name = "botonBuscadorCte";
            botonBuscadorCte.Size = new Size(95, 23);
            botonBuscadorCte.TabIndex = 2;
            botonBuscadorCte.Text = "Buscar";
            botonBuscadorCte.UseVisualStyleBackColor = true;
            // 
            // DGVCte
            // 
            DGVCte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVCte.Location = new Point(12, 119);
            DGVCte.MultiSelect = false;
            DGVCte.Name = "DGVCte";
            DGVCte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVCte.Size = new Size(599, 133);
            DGVCte.TabIndex = 3;
            // 
            // txtBuscCte
            // 
            txtBuscCte.AutoSize = true;
            txtBuscCte.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscCte.Location = new Point(12, 70);
            txtBuscCte.Name = "txtBuscCte";
            txtBuscCte.Size = new Size(163, 17);
            txtBuscCte.TabIndex = 4;
            txtBuscCte.Text = "Buscar Cliente por ID";
            // 
            // botonNuevoCte
            // 
            botonNuevoCte.Location = new Point(10, 258);
            botonNuevoCte.Name = "botonNuevoCte";
            botonNuevoCte.Size = new Size(114, 32);
            botonNuevoCte.TabIndex = 5;
            botonNuevoCte.Text = "Nuevo Cliente";
            botonNuevoCte.UseVisualStyleBackColor = true;
            // 
            // botonEditCte
            // 
            botonEditCte.Location = new Point(140, 258);
            botonEditCte.Name = "botonEditCte";
            botonEditCte.Size = new Size(114, 32);
            botonEditCte.TabIndex = 6;
            botonEditCte.Text = "Editar";
            botonEditCte.UseVisualStyleBackColor = true;
            botonEditCte.Click += button1_Click;
            // 
            // botonElimCte
            // 
            botonElimCte.Location = new Point(270, 258);
            botonElimCte.Name = "botonElimCte";
            botonElimCte.Size = new Size(114, 32);
            botonElimCte.TabIndex = 7;
            botonElimCte.Text = "Eliminar";
            botonElimCte.UseVisualStyleBackColor = true;
            // 
            // botonRegresarCte
            // 
            botonRegresarCte.Location = new Point(488, 354);
            botonRegresarCte.Name = "botonRegresarCte";
            botonRegresarCte.Size = new Size(114, 32);
            botonRegresarCte.TabIndex = 8;
            botonRegresarCte.Text = "Regresar al Menu";
            botonRegresarCte.UseVisualStyleBackColor = true;
            // 
            // botonHistCte
            // 
            botonHistCte.Location = new Point(12, 305);
            botonHistCte.Name = "botonHistCte";
            botonHistCte.Size = new Size(121, 32);
            botonHistCte.TabIndex = 9;
            botonHistCte.Text = "Historial del Cliente";
            botonHistCte.UseVisualStyleBackColor = true;
            // 
            // botonAudiCte
            // 
            botonAudiCte.Location = new Point(140, 305);
            botonAudiCte.Name = "botonAudiCte";
            botonAudiCte.Size = new Size(114, 32);
            botonAudiCte.TabIndex = 10;
            botonAudiCte.Text = "Auditoría";
            botonAudiCte.UseVisualStyleBackColor = true;
            // 
            // FrmClientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(623, 398);
            Controls.Add(botonAudiCte);
            Controls.Add(botonHistCte);
            Controls.Add(botonRegresarCte);
            Controls.Add(botonElimCte);
            Controls.Add(botonEditCte);
            Controls.Add(botonNuevoCte);
            Controls.Add(txtBuscCte);
            Controls.Add(DGVCte);
            Controls.Add(botonBuscadorCte);
            Controls.Add(buscarCliente);
            Controls.Add(txtGestCte);
            Name = "FrmClientes";
            Text = "FrmClientes";
            ((System.ComponentModel.ISupportInitialize)DGVCte).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtGestCte;
        private TextBox buscarCliente;
        private Button botonBuscadorCte;
        private DataGridView DGVCte;
        private Label txtBuscCte;
        private Button botonNuevoCte;
        private Button botonEditCte;
        private Button botonElimCte;
        private Button botonRegresarCte;
        private Button botonHistCte;
        private Button botonAudiCte;
    }
}