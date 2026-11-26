namespace PIA_PA
{
    partial class Ventas
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
            monthCalendar1 = new MonthCalendar();
            btnGenerarTXT = new Button();
            label2 = new Label();
            btnGuardarTXT = new Button();
            txtReporte = new TextBox();
            menuAgregarPartidos.SuspendLayout();
            SuspendLayout();
            // 
            // menuAgregarPartidos
            // 
            menuAgregarPartidos.Controls.Add(btnGuardarTXT);
            menuAgregarPartidos.Controls.Add(monthCalendar1);
            menuAgregarPartidos.Controls.Add(txtReporte);
            menuAgregarPartidos.Controls.Add(btnGenerarTXT);
            menuAgregarPartidos.Controls.Add(label2);
            menuAgregarPartidos.Location = new Point(0, 0);
            menuAgregarPartidos.Name = "menuAgregarPartidos";
            menuAgregarPartidos.Padding = new Padding(0);
            menuAgregarPartidos.Size = new Size(1061, 653);
            menuAgregarPartidos.TabIndex = 8;
            menuAgregarPartidos.TabStop = false;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(13, 80);
            monthCalendar1.Margin = new Padding(13, 15, 13, 15);
            monthCalendar1.MaxSelectionCount = 31;
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 17;
            // 
            // btnGenerarTXT
            // 
            btnGenerarTXT.Location = new Point(9, 370);
            btnGenerarTXT.Margin = new Padding(4, 5, 4, 5);
            btnGenerarTXT.Name = "btnGenerarTXT";
            btnGenerarTXT.Size = new Size(359, 38);
            btnGenerarTXT.TabIndex = 16;
            btnGenerarTXT.Text = "Generar TXT";
            btnGenerarTXT.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(9, 22);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(412, 45);
            label2.TabIndex = 0;
            label2.Text = "Generar reporte de ventas";
            // 
            // btnGuardarTXT
            // 
            btnGuardarTXT.Location = new Point(9, 418);
            btnGuardarTXT.Margin = new Padding(4, 5, 4, 5);
            btnGuardarTXT.Name = "btnGuardarTXT";
            btnGuardarTXT.Size = new Size(359, 38);
            btnGuardarTXT.TabIndex = 23;
            btnGuardarTXT.Text = "Guardar TXT";
            btnGuardarTXT.UseVisualStyleBackColor = true;
            // 
            // txtReporte
            // 
            txtReporte.Location = new Point(376, 80);
            txtReporte.Multiline = true;
            txtReporte.Name = "txtReporte";
            txtReporte.Size = new Size(670, 545);
            txtReporte.TabIndex = 22;
            // 
            // Ventas
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuAgregarPartidos);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Ventas";
            Size = new Size(1063, 657);
            menuAgregarPartidos.ResumeLayout(false);
            menuAgregarPartidos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox menuAgregarPartidos;
        private Label label2;
        private Button btnGenerarTXT;
        private MonthCalendar monthCalendar1;
        private Button btnGuardarTXT;
        private TextBox txtReporte;
    }
}
