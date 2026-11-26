namespace PIA_PA
{
    partial class AgregarPartido
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
            btnGuardar = new Button();
            cBEstadio = new ComboBox();
            label4 = new Label();
            cBCrupoVisitante = new ComboBox();
            cBGrupoLocal = new ComboBox();
            label6 = new Label();
            label5 = new Label();
            dTFecha = new DateTimePicker();
            label3 = new Label();
            label2 = new Label();
            txtHora = new TextBox();
            menuAgregarPartidos.SuspendLayout();
            SuspendLayout();
            // 
            // menuAgregarPartidos
            // 
            menuAgregarPartidos.Controls.Add(txtHora);
            menuAgregarPartidos.Controls.Add(btnGuardar);
            menuAgregarPartidos.Controls.Add(cBEstadio);
            menuAgregarPartidos.Controls.Add(label4);
            menuAgregarPartidos.Controls.Add(cBCrupoVisitante);
            menuAgregarPartidos.Controls.Add(cBGrupoLocal);
            menuAgregarPartidos.Controls.Add(label6);
            menuAgregarPartidos.Controls.Add(label5);
            menuAgregarPartidos.Controls.Add(dTFecha);
            menuAgregarPartidos.Controls.Add(label3);
            menuAgregarPartidos.Controls.Add(label2);
            menuAgregarPartidos.Location = new Point(0, 0);
            menuAgregarPartidos.Name = "menuAgregarPartidos";
            menuAgregarPartidos.Padding = new Padding(0);
            menuAgregarPartidos.Size = new Size(1061, 653);
            menuAgregarPartidos.TabIndex = 8;
            menuAgregarPartidos.TabStop = false;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(150, 275);
            btnGuardar.Margin = new Padding(4, 5, 4, 5);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(466, 38);
            btnGuardar.TabIndex = 11;
            btnGuardar.Text = "Crear";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // cBEstadio
            // 
            cBEstadio.FormattingEnabled = true;
            cBEstadio.Location = new Point(150, 227);
            cBEstadio.Margin = new Padding(4, 5, 4, 5);
            cBEstadio.Name = "cBEstadio";
            cBEstadio.Size = new Size(464, 33);
            cBEstadio.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(9, 227);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(74, 25);
            label4.TabIndex = 9;
            label4.Text = "Estadio:";
            // 
            // cBCrupoVisitante
            // 
            cBCrupoVisitante.FormattingEnabled = true;
            cBCrupoVisitante.Location = new Point(150, 178);
            cBCrupoVisitante.Margin = new Padding(4, 5, 4, 5);
            cBCrupoVisitante.Name = "cBCrupoVisitante";
            cBCrupoVisitante.Size = new Size(464, 33);
            cBCrupoVisitante.TabIndex = 8;
            // 
            // cBGrupoLocal
            // 
            cBGrupoLocal.FormattingEnabled = true;
            cBGrupoLocal.Location = new Point(150, 130);
            cBGrupoLocal.Margin = new Padding(4, 5, 4, 5);
            cBGrupoLocal.Name = "cBGrupoLocal";
            cBGrupoLocal.Size = new Size(464, 33);
            cBGrupoLocal.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(9, 178);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(136, 25);
            label6.TabIndex = 6;
            label6.Text = "Grupo visitante:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(9, 130);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(107, 25);
            label5.TabIndex = 5;
            label5.Text = "Grupo local:";
            // 
            // dTFecha
            // 
            dTFecha.Location = new Point(150, 82);
            dTFecha.Margin = new Padding(4, 5, 4, 5);
            dTFecha.Name = "dTFecha";
            dTFecha.Size = new Size(325, 31);
            dTFecha.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(9, 87);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(116, 25);
            label3.TabIndex = 1;
            label3.Text = "Fecha y hora:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(9, 22);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(221, 45);
            label2.TabIndex = 0;
            label2.Text = "Crear partido";
            // 
            // txtHora
            // 
            txtHora.Location = new Point(482, 84);
            txtHora.Name = "txtHora";
            txtHora.Size = new Size(132, 31);
            txtHora.TabIndex = 12;
            // 
            // AgregarPartido
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuAgregarPartidos);
            Margin = new Padding(4, 5, 4, 5);
            Name = "AgregarPartido";
            Size = new Size(1063, 657);
            menuAgregarPartidos.ResumeLayout(false);
            menuAgregarPartidos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox menuAgregarPartidos;
        private Button btnGuardar;
        private ComboBox cBEstadio;
        private Label label4;
        private ComboBox cBCrupoVisitante;
        private ComboBox cBGrupoLocal;
        private Label label6;
        private Label label5;
        private DateTimePicker dTFecha;
        private Label label3;
        private Label label2;
        private TextBox txtHora;
    }
}
