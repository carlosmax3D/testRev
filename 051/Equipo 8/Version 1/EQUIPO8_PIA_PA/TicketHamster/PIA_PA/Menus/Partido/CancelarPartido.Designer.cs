namespace PIA_PA.Menus
{
    partial class CancelarPartido
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
            btnCancelar = new Button();
            label4 = new Label();
            label6 = new Label();
            label5 = new Label();
            dTimeFecha = new DateTimePicker();
            label3 = new Label();
            txtBuscar = new Button();
            txtID2 = new TextBox();
            txtID1 = new TextBox();
            label1 = new Label();
            txtGpoLocal = new TextBox();
            txtGpoVisitante = new TextBox();
            txtEstadio = new TextBox();
            txtHora = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtHora);
            groupBox1.Controls.Add(txtEstadio);
            groupBox1.Controls.Add(txtGpoVisitante);
            groupBox1.Controls.Add(txtGpoLocal);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnCancelar);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(dTimeFecha);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtBuscar);
            groupBox1.Controls.Add(txtID2);
            groupBox1.Controls.Add(txtID1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new Size(1061, 653);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(9, 32);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(268, 45);
            label2.TabIndex = 36;
            label2.Text = "Cancelar partido";
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(426, 333);
            btnCancelar.Margin = new Padding(4, 5, 4, 5);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(511, 38);
            btnCancelar.TabIndex = 35;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(284, 285);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(74, 25);
            label4.TabIndex = 33;
            label4.Text = "Estadio:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(284, 237);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(136, 25);
            label6.TabIndex = 30;
            label6.Text = "Grupo visitante:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(284, 188);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(107, 25);
            label5.TabIndex = 29;
            label5.Text = "Grupo local:";
            // 
            // dTimeFecha
            // 
            dTimeFecha.Location = new Point(426, 140);
            dTimeFecha.Margin = new Padding(4, 5, 4, 5);
            dTimeFecha.Name = "dTimeFecha";
            dTimeFecha.Size = new Size(371, 31);
            dTimeFecha.TabIndex = 27;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(284, 145);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(116, 25);
            label3.TabIndex = 26;
            label3.Text = "Fecha y hora:";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(946, 87);
            txtBuscar.Margin = new Padding(4, 5, 4, 5);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(107, 38);
            txtBuscar.TabIndex = 25;
            txtBuscar.Text = "Buscar";
            txtBuscar.UseVisualStyleBackColor = true;
            // 
            // txtID2
            // 
            txtID2.Location = new Point(284, 87);
            txtID2.Margin = new Padding(4, 5, 4, 5);
            txtID2.Name = "txtID2";
            txtID2.Size = new Size(651, 31);
            txtID2.TabIndex = 24;
            // 
            // txtID1
            // 
            txtID1.Location = new Point(47, 87);
            txtID1.Margin = new Padding(4, 5, 4, 5);
            txtID1.Name = "txtID1";
            txtID1.Size = new Size(227, 31);
            txtID1.TabIndex = 23;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 92);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(34, 25);
            label1.TabIndex = 22;
            label1.Text = "ID:";
            // 
            // txtGpoLocal
            // 
            txtGpoLocal.Location = new Point(426, 185);
            txtGpoLocal.Name = "txtGpoLocal";
            txtGpoLocal.Size = new Size(509, 31);
            txtGpoLocal.TabIndex = 37;
            // 
            // txtGpoVisitante
            // 
            txtGpoVisitante.Location = new Point(428, 231);
            txtGpoVisitante.Name = "txtGpoVisitante";
            txtGpoVisitante.Size = new Size(509, 31);
            txtGpoVisitante.TabIndex = 38;
            // 
            // txtEstadio
            // 
            txtEstadio.Location = new Point(426, 279);
            txtEstadio.Name = "txtEstadio";
            txtEstadio.Size = new Size(509, 31);
            txtEstadio.TabIndex = 39;
            // 
            // txtHora
            // 
            txtHora.Location = new Point(804, 142);
            txtHora.Name = "txtHora";
            txtHora.Size = new Size(133, 31);
            txtHora.TabIndex = 40;
            // 
            // CancelarPartido
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "CancelarPartido";
            Size = new Size(1063, 657);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnCancelar;
        private Label label4;
        private Label label6;
        private Label label5;
        private DateTimePicker dTimeFecha;
        private Label label3;
        private Button txtBuscar;
        private TextBox txtID2;
        private TextBox txtID1;
        private Label label1;
        private Label label2;
        private TextBox txtHora;
        private TextBox txtEstadio;
        private TextBox txtGpoVisitante;
        private TextBox txtGpoLocal;
    }
}
