namespace PIA_PA
{
    partial class AgregarEquipo
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
            cBDeporte = new ComboBox();
            btnGuardar = new Button();
            label4 = new Label();
            txtNombre = new TextBox();
            txtEstatus = new TextBox();
            label5 = new Label();
            label3 = new Label();
            label2 = new Label();
            menuAgregarPartidos.SuspendLayout();
            SuspendLayout();
            // 
            // menuAgregarPartidos
            // 
            menuAgregarPartidos.Controls.Add(cBDeporte);
            menuAgregarPartidos.Controls.Add(btnGuardar);
            menuAgregarPartidos.Controls.Add(label4);
            menuAgregarPartidos.Controls.Add(txtNombre);
            menuAgregarPartidos.Controls.Add(txtEstatus);
            menuAgregarPartidos.Controls.Add(label5);
            menuAgregarPartidos.Controls.Add(label3);
            menuAgregarPartidos.Controls.Add(label2);
            menuAgregarPartidos.Location = new Point(0, 0);
            menuAgregarPartidos.Margin = new Padding(2);
            menuAgregarPartidos.Name = "menuAgregarPartidos";
            menuAgregarPartidos.Padding = new Padding(0);
            menuAgregarPartidos.Size = new Size(743, 392);
            menuAgregarPartidos.TabIndex = 8;
            menuAgregarPartidos.TabStop = false;
            // 
            // cBDeporte
            // 
            cBDeporte.FormattingEnabled = true;
            cBDeporte.Location = new Point(105, 112);
            cBDeporte.Name = "cBDeporte";
            cBDeporte.Size = new Size(227, 23);
            cBDeporte.TabIndex = 26;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(6, 139);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(326, 23);
            btnGuardar.TabIndex = 25;
            btnGuardar.Text = "Agregar equipo";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(6, 115);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 18;
            label4.Text = "Deporte:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(105, 49);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(227, 23);
            txtNombre.TabIndex = 13;
            // 
            // txtEstatus
            // 
            txtEstatus.Location = new Point(105, 78);
            txtEstatus.Name = "txtEstatus";
            txtEstatus.Size = new Size(227, 23);
            txtEstatus.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(6, 81);
            label5.Name = "label5";
            label5.Size = new Size(47, 15);
            label5.TabIndex = 5;
            label5.Text = "Estatus:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(6, 52);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 1;
            label3.Text = "Nombre:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 13);
            label2.Name = "label2";
            label2.Size = new Size(167, 30);
            label2.TabIndex = 0;
            label2.Text = "Agregar equipo";
            // 
            // AgregarEquipo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuAgregarPartidos);
            Name = "AgregarEquipo";
            Size = new Size(744, 394);
            menuAgregarPartidos.ResumeLayout(false);
            menuAgregarPartidos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox menuAgregarPartidos;
        private Label label5;
        private Label label3;
        private Label label2;
        private TextBox txtNombre;
        private TextBox txtEstatus;
        private Button btnGuardar;
        private Label label4;
        private ComboBox cBDeporte;
    }
}
