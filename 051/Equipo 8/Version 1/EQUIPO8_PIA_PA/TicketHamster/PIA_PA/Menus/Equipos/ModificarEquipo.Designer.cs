namespace PIA_PA
{
    partial class ModificarEquipo
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
            btnModificar = new Button();
            label4 = new Label();
            txtNombre = new TextBox();
            txtEstatus = new TextBox();
            label5 = new Label();
            label3 = new Label();
            btnSeleccionar = new Button();
            txtID2 = new TextBox();
            txtID1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cBDeporte);
            groupBox1.Controls.Add(btnModificar);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Controls.Add(txtEstatus);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnSeleccionar);
            groupBox1.Controls.Add(txtID2);
            groupBox1.Controls.Add(txtID1);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(743, 392);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // cBDeporte
            // 
            cBDeporte.FormattingEnabled = true;
            cBDeporte.Location = new Point(298, 151);
            cBDeporte.Name = "cBDeporte";
            cBDeporte.Size = new Size(358, 23);
            cBDeporte.TabIndex = 33;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(298, 178);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(358, 23);
            btnModificar.TabIndex = 32;
            btnModificar.Text = "Modificar equipo";
            btnModificar.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(199, 154);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 31;
            label4.Text = "Deporte:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(298, 88);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(358, 23);
            txtNombre.TabIndex = 30;
            // 
            // txtEstatus
            // 
            txtEstatus.Location = new Point(298, 117);
            txtEstatus.Name = "txtEstatus";
            txtEstatus.Size = new Size(358, 23);
            txtEstatus.TabIndex = 29;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(199, 120);
            label5.Name = "label5";
            label5.Size = new Size(44, 15);
            label5.TabIndex = 28;
            label5.Text = "Estatus";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(199, 91);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 27;
            label3.Text = "Nombre:";
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(183, 30);
            label2.TabIndex = 1;
            label2.Text = "Modificar equipo";
            // 
            // ModificarEquipo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Name = "ModificarEquipo";
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
        private ComboBox cBDeporte;
        private Button btnModificar;
        private Label label4;
        private TextBox txtNombre;
        private TextBox txtEstatus;
        private Label label5;
        private Label label3;
    }
}
