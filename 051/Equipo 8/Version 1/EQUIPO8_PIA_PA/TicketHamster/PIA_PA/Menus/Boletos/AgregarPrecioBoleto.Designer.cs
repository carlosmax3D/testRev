namespace PIA_PA
{
    partial class AgregarPrecioBoleto
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            menuAgregarPartidos = new GroupBox();
            cBoxAsientoArea = new ComboBox();
            label1 = new Label();
            cBoxPartido = new ComboBox();
            btnAgregarPrecioArea = new Button();
            txtPartido = new Label();
            label2 = new Label();
            label3 = new Label();
            txtBoxPrecio = new TextBox();
            menuAgregarPartidos.SuspendLayout();
            SuspendLayout();
            // 
            // menuAgregarPartidos
            // 
            menuAgregarPartidos.Controls.Add(txtBoxPrecio);
            menuAgregarPartidos.Controls.Add(label3);
            menuAgregarPartidos.Controls.Add(cBoxAsientoArea);
            menuAgregarPartidos.Controls.Add(label1);
            menuAgregarPartidos.Controls.Add(cBoxPartido);
            menuAgregarPartidos.Controls.Add(btnAgregarPrecioArea);
            menuAgregarPartidos.Controls.Add(txtPartido);
            menuAgregarPartidos.Controls.Add(label2);
            menuAgregarPartidos.Location = new Point(0, 0);
            menuAgregarPartidos.Margin = new Padding(2);
            menuAgregarPartidos.Name = "menuAgregarPartidos";
            menuAgregarPartidos.Padding = new Padding(0);
            menuAgregarPartidos.Size = new Size(743, 392);
            menuAgregarPartidos.TabIndex = 8;
            menuAgregarPartidos.TabStop = false;
            // 
            // cBoxAsientoArea
            // 
            cBoxAsientoArea.FormattingEnabled = true;
            cBoxAsientoArea.Location = new Point(82, 78);
            cBoxAsientoArea.Name = "cBoxAsientoArea";
            cBoxAsientoArea.Size = new Size(283, 23);
            cBoxAsientoArea.TabIndex = 35;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(6, 81);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 34;
            label1.Text = "Area:";
            // 
            // cBoxPartido
            // 
            cBoxPartido.FormattingEnabled = true;
            cBoxPartido.Location = new Point(82, 49);
            cBoxPartido.Name = "cBoxPartido";
            cBoxPartido.Size = new Size(283, 23);
            cBoxPartido.TabIndex = 33;
            // 
            // btnAgregarPrecioArea
            // 
            btnAgregarPrecioArea.Location = new Point(6, 136);
            btnAgregarPrecioArea.Name = "btnAgregarPrecioArea";
            btnAgregarPrecioArea.Size = new Size(359, 23);
            btnAgregarPrecioArea.TabIndex = 25;
            btnAgregarPrecioArea.Text = "Agregar Precio Area";
            btnAgregarPrecioArea.UseVisualStyleBackColor = true;
            // 
            // txtPartido
            // 
            txtPartido.AutoSize = true;
            txtPartido.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPartido.Location = new Point(6, 52);
            txtPartido.Name = "txtPartido";
            txtPartido.Size = new Size(48, 15);
            txtPartido.TabIndex = 1;
            txtPartido.Text = "Partido:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 13);
            label2.Name = "label2";
            label2.Size = new Size(83, 30);
            label2.TabIndex = 0;
            label2.Text = "Precios";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(6, 110);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 36;
            label3.Text = "Precio:";
            // 
            // txtBoxPrecio
            // 
            txtBoxPrecio.Location = new Point(82, 107);
            txtBoxPrecio.Name = "txtBoxPrecio";
            txtBoxPrecio.Size = new Size(283, 23);
            txtBoxPrecio.TabIndex = 37;
            // 
            // AgregarPrecioBoleto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuAgregarPartidos);
            Name = "AgregarPrecioBoleto";
            Size = new Size(744, 394);
            menuAgregarPartidos.ResumeLayout(false);
            menuAgregarPartidos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox menuAgregarPartidos;
        private Label txtPartido;
        private Label label2;
        private Button btnAgregarPrecioArea;
        private ComboBox cBoxAsientoArea;
        private Label label1;
        private ComboBox cBoxPartido;
        private TextBox txtBoxPrecio;
        private Label label3;
    }
}
