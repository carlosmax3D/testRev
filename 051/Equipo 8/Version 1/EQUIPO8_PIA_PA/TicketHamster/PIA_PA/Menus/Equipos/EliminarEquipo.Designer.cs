namespace PIA_PA.Menus
{
    partial class EliminarEquipo
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
            cBDeporte = new ComboBox();
            label2 = new Label();
            btnEliminar = new Button();
            btnSeleccionar = new Button();
            label4 = new Label();
            txtID2 = new TextBox();
            txtNombre = new TextBox();
            txtID1 = new TextBox();
            txtEstatus = new TextBox();
            label1 = new Label();
            label5 = new Label();
            label3 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cBDeporte);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnEliminar);
            groupBox1.Controls.Add(btnSeleccionar);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtID2);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Controls.Add(txtID1);
            groupBox1.Controls.Add(txtEstatus);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(743, 392);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // cBDeporte
            // 
            cBDeporte.FormattingEnabled = true;
            cBDeporte.Location = new Point(298, 147);
            cBDeporte.Name = "cBDeporte";
            cBDeporte.Size = new Size(358, 23);
            cBDeporte.TabIndex = 40;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(168, 30);
            label2.TabIndex = 36;
            label2.Text = "Eliminar equipo";
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(298, 174);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(358, 23);
            btnEliminar.TabIndex = 39;
            btnEliminar.Text = "Eliminar equipo";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnSeleccionar
            // 
            btnSeleccionar.Location = new Point(662, 52);
            btnSeleccionar.Name = "btnSeleccionar";
            btnSeleccionar.Size = new Size(75, 23);
            btnSeleccionar.TabIndex = 25;
            btnSeleccionar.Text = "Seleccionar";
            btnSeleccionar.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(199, 150);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 38;
            label4.Text = "Deporte:";
            // 
            // txtID2
            // 
            txtID2.Location = new Point(199, 52);
            txtID2.Name = "txtID2";
            txtID2.Size = new Size(457, 23);
            txtID2.TabIndex = 24;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(298, 84);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(358, 23);
            txtNombre.TabIndex = 37;
            // 
            // txtID1
            // 
            txtID1.Location = new Point(33, 52);
            txtID1.Name = "txtID1";
            txtID1.Size = new Size(160, 23);
            txtID1.TabIndex = 23;
            // 
            // txtEstatus
            // 
            txtEstatus.Location = new Point(298, 113);
            txtEstatus.Name = "txtEstatus";
            txtEstatus.Size = new Size(358, 23);
            txtEstatus.TabIndex = 36;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 55);
            label1.Name = "label1";
            label1.Size = new Size(21, 15);
            label1.TabIndex = 22;
            label1.Text = "ID:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(199, 116);
            label5.Name = "label5";
            label5.Size = new Size(44, 15);
            label5.TabIndex = 35;
            label5.Text = "Estatus";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(199, 87);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 34;
            label3.Text = "Nombre:";
            // 
            // EliminarEquipo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Name = "EliminarEquipo";
            Size = new Size(744, 394);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnSeleccionar;
        private TextBox txtID2;
        private TextBox txtID1;
        private Label label1;
        private Label label2;
        private ComboBox cBDeporte;
        private Button btnEliminar;
        private Label label4;
        private TextBox txtNombre;
        private TextBox txtEstatus;
        private Label label5;
        private Label label3;
    }
}
