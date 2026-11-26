namespace PIA_PA
{
    partial class ModificarEmpleado
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
            groupBox1 = new GroupBox();
            btnSeleccionar = new Button();
            txtID2 = new TextBox();
            txtID1 = new TextBox();
            label1 = new Label();
            label3 = new Label();
            label2 = new Label();
            txtCorreo = new TextBox();
            btnModificar = new Button();
            txtNombre4 = new TextBox();
            txtNombre3 = new TextBox();
            txtNombre2 = new TextBox();
            txtNombre1 = new TextBox();
            label8 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtNombre4);
            groupBox1.Controls.Add(txtNombre3);
            groupBox1.Controls.Add(btnSeleccionar);
            groupBox1.Controls.Add(txtNombre2);
            groupBox1.Controls.Add(txtID2);
            groupBox1.Controls.Add(txtNombre1);
            groupBox1.Controls.Add(txtID1);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtCorreo);
            groupBox1.Controls.Add(btnModificar);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(743, 392);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // btnSeleccionar
            // 
            btnSeleccionar.Location = new Point(662, 56);
            btnSeleccionar.Name = "btnSeleccionar";
            btnSeleccionar.Size = new Size(75, 23);
            btnSeleccionar.TabIndex = 5;
            btnSeleccionar.Text = "Seleccionar";
            btnSeleccionar.UseVisualStyleBackColor = true;
            // 
            // txtID2
            // 
            txtID2.Location = new Point(199, 56);
            txtID2.Name = "txtID2";
            txtID2.Size = new Size(457, 23);
            txtID2.TabIndex = 4;
            // 
            // txtID1
            // 
            txtID1.Location = new Point(33, 56);
            txtID1.Name = "txtID1";
            txtID1.Size = new Size(160, 23);
            txtID1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 59);
            label1.Name = "label1";
            label1.Size = new Size(21, 15);
            label1.TabIndex = 2;
            label1.Text = "ID:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(199, 120);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 41;
            label3.Text = "Email:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(211, 30);
            label2.TabIndex = 1;
            label2.Text = "Modificar empleado";
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(275, 117);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(381, 23);
            txtCorreo.TabIndex = 40;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(275, 146);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(381, 23);
            btnModificar.TabIndex = 38;
            btnModificar.Text = "Modificar empleado";
            btnModificar.UseVisualStyleBackColor = true;
            // 
            // txtNombre4
            // 
            txtNombre4.Location = new Point(612, 85);
            txtNombre4.Name = "txtNombre4";
            txtNombre4.Size = new Size(125, 23);
            txtNombre4.TabIndex = 73;
            // 
            // txtNombre3
            // 
            txtNombre3.Location = new Point(481, 85);
            txtNombre3.Name = "txtNombre3";
            txtNombre3.Size = new Size(125, 23);
            txtNombre3.TabIndex = 72;
            // 
            // txtNombre2
            // 
            txtNombre2.Location = new Point(350, 85);
            txtNombre2.Name = "txtNombre2";
            txtNombre2.Size = new Size(125, 23);
            txtNombre2.TabIndex = 71;
            // 
            // txtNombre1
            // 
            txtNombre1.Location = new Point(219, 85);
            txtNombre1.Name = "txtNombre1";
            txtNombre1.Size = new Size(125, 23);
            txtNombre1.TabIndex = 70;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(159, 88);
            label8.Name = "label8";
            label8.Size = new Size(54, 15);
            label8.TabIndex = 69;
            label8.Text = "Nombre:";
            // 
            // ModificarEmpleado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Name = "ModificarEmpleado";
            Size = new Size(744, 394);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label2;
        private TextBox txtID2;
        private TextBox txtID1;
        private Label label1;
        private Button btnSeleccionar;
        private Label label3;
        private TextBox txtCorreo;
        private Button btnModificar;
        private TextBox txtNombre4;
        private TextBox txtNombre3;
        private TextBox txtNombre2;
        private TextBox txtNombre1;
        private Label label8;
    }
}
