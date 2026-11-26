

namespace Pantalla_Facturacion_PIA_PA
{
    partial class pantalla_Facturación
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            boton1_Facturacion = new Button();
            label1_Facturación = new Label();
            textBox1_Facturación = new TextBox();
            boton2_buscarFacturacion = new Button();
            SuspendLayout();
            // 
            // boton1_Facturacion
            // 
            boton1_Facturacion.BackColor = Color.LightCoral;
            boton1_Facturacion.Location = new Point(32, 260);
            boton1_Facturacion.Name = "boton1_Facturacion";
            boton1_Facturacion.Size = new Size(112, 34);
            boton1_Facturacion.TabIndex = 2;
            boton1_Facturacion.Text = "Cancelar";
            boton1_Facturacion.UseVisualStyleBackColor = false;
            boton1_Facturacion.Click += boton1_Facturacion_Click;
            // 
            // label1_Facturación
            // 
            label1_Facturación.AutoSize = true;
            label1_Facturación.Font = new Font("Sitka Banner", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1_Facturación.Location = new Point(71, 51);
            label1_Facturación.Name = "label1_Facturación";
            label1_Facturación.Size = new Size(252, 35);
            label1_Facturación.TabIndex = 3;
            label1_Facturación.Text = "Numero de folio de ticket:";
            // 
            // textBox1_Facturación
            // 
            textBox1_Facturación.Location = new Point(52, 136);
            textBox1_Facturación.Name = "textBox1_Facturación";
            textBox1_Facturación.PlaceholderText = "Ingresa numero de folio de ticket";
            textBox1_Facturación.Size = new Size(298, 31);
            textBox1_Facturación.TabIndex = 4;
            textBox1_Facturación.TextChanged += textBox1_Facturación_TextChanged;
            // 
            // boton2_buscarFacturacion
            // 
            boton2_buscarFacturacion.BackColor = Color.LightGreen;
            boton2_buscarFacturacion.Location = new Point(238, 260);
            boton2_buscarFacturacion.Name = "boton2_buscarFacturacion";
            boton2_buscarFacturacion.Size = new Size(112, 34);
            boton2_buscarFacturacion.TabIndex = 5;
            boton2_buscarFacturacion.Text = "Buscar";
            boton2_buscarFacturacion.UseVisualStyleBackColor = false;
            boton2_buscarFacturacion.Click += boton2_buscarFacturacion_Click;
            // 
            // pantalla_Facturación
            // 
            this.AutoScaleDimensions = new SizeF(10F, 25F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(432, 345);
            this.Controls.Add(boton2_buscarFacturacion);
            this.Controls.Add(textBox1_Facturación);
            this.Controls.Add(label1_Facturación);
            this.Controls.Add(boton1_Facturacion);
            this.Name = "pantalla_Facturación";
            this.Text = "Facturación";
            Load += pantalla_Facturacion_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void boton1_Facturacion_Click(object sender, EventArgs e)
        {
           // Pregunta si el usuario desea cancelar la facturación.
            DialogResult respuesta = MessageBox.Show(
                "¿Estás seguro de que deseas cancelar la facturación?",
                "Cancelar facturación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            // Cierra el formulario si el usuario confirma.
            if (respuesta == DialogResult.Yes)
            {
                this.Close();
            }
        }
        
        #endregion
        private Button boton1_Facturacion;
        private Label label1_Facturación;
        private TextBox textBox1_Facturación;
        private Button boton2_buscarFacturacion;
    }
}
