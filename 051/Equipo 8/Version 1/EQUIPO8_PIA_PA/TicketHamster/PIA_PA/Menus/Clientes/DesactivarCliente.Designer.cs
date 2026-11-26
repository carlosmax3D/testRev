namespace PIA_PA.Menus
{
    partial class DesactivarCliente
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
            label2 = new Label();
            btnSeleccionar = new Button();
            txtID2 = new TextBox();
            txtID1 = new TextBox();
            label3 = new Label();
            label1 = new Label();
            txtCorreo = new TextBox();
            label5 = new Label();
            btnEliminar = new Button();
            txtRFC = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnSeleccionar);
            groupBox1.Controls.Add(txtID2);
            groupBox1.Controls.Add(txtID1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtCorreo);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(btnEliminar);
            groupBox1.Controls.Add(txtRFC);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(743, 392);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(184, 30);
            label2.TabIndex = 36;
            label2.Text = "Desactivar cliente";
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
            // txtID2
            // 
            txtID2.Location = new Point(199, 52);
            txtID2.Name = "txtID2";
            txtID2.Size = new Size(457, 23);
            txtID2.TabIndex = 24;
            // 
            // txtID1
            // 
            txtID1.Location = new Point(33, 52);
            txtID1.Name = "txtID1";
            txtID1.Size = new Size(160, 23);
            txtID1.TabIndex = 23;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(199, 113);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 41;
            label3.Text = "Email:";
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
            // txtCorreo
            // 
            txtCorreo.Location = new Point(275, 110);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(381, 23);
            txtCorreo.TabIndex = 40;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(199, 84);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 34;
            label5.Text = "RFC:";
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(275, 139);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(381, 23);
            btnEliminar.TabIndex = 38;
            btnEliminar.Text = "Desactivar cliente";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // txtRFC
            // 
            txtRFC.Location = new Point(275, 81);
            txtRFC.Name = "txtRFC";
            txtRFC.Size = new Size(381, 23);
            txtRFC.TabIndex = 35;
            // 
            // DesactivarCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Name = "DesactivarCliente";
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
        private Label label3;
        private TextBox txtCorreo;
        private Label label5;
        private Button btnEliminar;
        private TextBox txtRFC;
    }
}
