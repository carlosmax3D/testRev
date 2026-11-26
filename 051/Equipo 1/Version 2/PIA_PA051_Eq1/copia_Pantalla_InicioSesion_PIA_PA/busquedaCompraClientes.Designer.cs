namespace Pantalla_BusquedaCompraCliente_PIA_PA
{
    partial class pantalla_busquedaClientes
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            label1_busquedaClientes = new Label();
            txtbox1_busquedaClientes = new TextBox();
            boton1_busquedaClientes = new Button();
            boton2_busquedaClientes = new Button();
            SuspendLayout();
            // 
            // label1_busquedaClientes
            // 
            label1_busquedaClientes.AutoSize = true;
            label1_busquedaClientes.Font = new Font("Sitka Subheading", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1_busquedaClientes.Location = new Point(51, 43);
            label1_busquedaClientes.Name = "label1_busquedaClientes";
            label1_busquedaClientes.Size = new Size(325, 29);
            label1_busquedaClientes.TabIndex = 0;
            label1_busquedaClientes.Text = "Ingresa Primer Apellido del cliente:";
            label1_busquedaClientes.Click += label1_busquedaClientes_Click;
            // 
            // txtbox1_busquedaClientes
            // 
            txtbox1_busquedaClientes.Location = new Point(382, 43);
            txtbox1_busquedaClientes.Name = "txtbox1_busquedaClientes";
            txtbox1_busquedaClientes.PlaceholderText = "Primer Apellido";
            txtbox1_busquedaClientes.Size = new Size(274, 31);
            txtbox1_busquedaClientes.TabIndex = 1;
            txtbox1_busquedaClientes.TextChanged += txtbox1_busquedaClientes_TextChanged;
            // 
            // boton1_busquedaClientes
            // 
            boton1_busquedaClientes.BackColor = Color.PaleTurquoise;
            boton1_busquedaClientes.Location = new Point(544, 152);
            boton1_busquedaClientes.Name = "boton1_busquedaClientes";
            boton1_busquedaClientes.Size = new Size(112, 34);
            boton1_busquedaClientes.TabIndex = 2;
            boton1_busquedaClientes.Text = "Buscar";
            boton1_busquedaClientes.UseVisualStyleBackColor = false;
            boton1_busquedaClientes.Click += boton1_busquedaClientes_Click;
            // 
            // boton2_busquedaClientes
            // 
            boton2_busquedaClientes.BackColor = Color.LightCoral;
            boton2_busquedaClientes.Location = new Point(51, 152);
            boton2_busquedaClientes.Name = "boton2_busquedaClientes";
            boton2_busquedaClientes.Size = new Size(112, 34);
            boton2_busquedaClientes.TabIndex = 3;
            boton2_busquedaClientes.Text = "Cancelar";
            boton2_busquedaClientes.UseVisualStyleBackColor = false;
            boton2_busquedaClientes.Click += boton2_busquedaClientes_Click;
            // 
            // pantalla_busquedaClientes
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(758, 245);
            Controls.Add(boton2_busquedaClientes);
            Controls.Add(boton1_busquedaClientes);
            Controls.Add(txtbox1_busquedaClientes);
            Controls.Add(label1_busquedaClientes);
            Name = "pantalla_busquedaClientes";
            Text = "Busqueda de Clientes";
            Load += pantalla_busquedaClientes_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1_busquedaClientes;
        private TextBox txtbox1_busquedaClientes;
        private Button boton1_busquedaClientes;
        private Button boton2_busquedaClientes;
    }
}
