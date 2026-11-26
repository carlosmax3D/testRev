namespace PIA_PA
{
    partial class AgregarEmpleado
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
            label7 = new Label();
            txtContrasena = new TextBox();
            label1 = new Label();
            txtCorreo = new TextBox();
            btnGuardar = new Button();
            label2 = new Label();
            txtNombre4 = new TextBox();
            txtNombre3 = new TextBox();
            txtNombre2 = new TextBox();
            txtNombre1 = new TextBox();
            label8 = new Label();
            menuAgregarPartidos.SuspendLayout();
            SuspendLayout();
            // 
            // menuAgregarPartidos
            // 
            menuAgregarPartidos.Controls.Add(txtNombre4);
            menuAgregarPartidos.Controls.Add(label7);
            menuAgregarPartidos.Controls.Add(txtNombre3);
            menuAgregarPartidos.Controls.Add(txtContrasena);
            menuAgregarPartidos.Controls.Add(txtNombre2);
            menuAgregarPartidos.Controls.Add(label1);
            menuAgregarPartidos.Controls.Add(txtNombre1);
            menuAgregarPartidos.Controls.Add(txtCorreo);
            menuAgregarPartidos.Controls.Add(label8);
            menuAgregarPartidos.Controls.Add(btnGuardar);
            menuAgregarPartidos.Controls.Add(label2);
            menuAgregarPartidos.Location = new Point(0, 0);
            menuAgregarPartidos.Margin = new Padding(2);
            menuAgregarPartidos.Name = "menuAgregarPartidos";
            menuAgregarPartidos.Padding = new Padding(0);
            menuAgregarPartidos.Size = new Size(743, 392);
            menuAgregarPartidos.TabIndex = 8;
            menuAgregarPartidos.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(6, 110);
            label7.Name = "label7";
            label7.Size = new Size(70, 15);
            label7.TabIndex = 32;
            label7.Text = "Contraseña:";
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(82, 107);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(283, 23);
            txtContrasena.TabIndex = 31;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(6, 81);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 28;
            label1.Text = "Email:";
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(82, 78);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(283, 23);
            txtCorreo.TabIndex = 27;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(6, 136);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(359, 23);
            btnGuardar.TabIndex = 25;
            btnGuardar.Text = "Agregar empleado";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 13);
            label2.Name = "label2";
            label2.Size = new Size(195, 30);
            label2.TabIndex = 0;
            label2.Text = "Agregar empleado";
            // 
            // txtNombre4
            // 
            txtNombre4.Location = new Point(459, 49);
            txtNombre4.Name = "txtNombre4";
            txtNombre4.Size = new Size(125, 23);
            txtNombre4.TabIndex = 73;
            // 
            // txtNombre3
            // 
            txtNombre3.Location = new Point(328, 49);
            txtNombre3.Name = "txtNombre3";
            txtNombre3.Size = new Size(125, 23);
            txtNombre3.TabIndex = 72;
            // 
            // txtNombre2
            // 
            txtNombre2.Location = new Point(197, 49);
            txtNombre2.Name = "txtNombre2";
            txtNombre2.Size = new Size(125, 23);
            txtNombre2.TabIndex = 71;
            // 
            // txtNombre1
            // 
            txtNombre1.Location = new Point(66, 49);
            txtNombre1.Name = "txtNombre1";
            txtNombre1.Size = new Size(125, 23);
            txtNombre1.TabIndex = 70;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(6, 52);
            label8.Name = "label8";
            label8.Size = new Size(54, 15);
            label8.TabIndex = 69;
            label8.Text = "Nombre:";
            // 
            // AgregarEmpleado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuAgregarPartidos);
            Name = "AgregarEmpleado";
            Size = new Size(744, 394);
            menuAgregarPartidos.ResumeLayout(false);
            menuAgregarPartidos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox menuAgregarPartidos;
        private Label label2;
        private Button btnGuardar;
        private Label label7;
        private TextBox txtContrasena;
        private Label label1;
        private TextBox txtCorreo;
        private TextBox txtNombre4;
        private TextBox txtNombre3;
        private TextBox txtNombre2;
        private TextBox txtNombre1;
        private Label label8;
    }
}
