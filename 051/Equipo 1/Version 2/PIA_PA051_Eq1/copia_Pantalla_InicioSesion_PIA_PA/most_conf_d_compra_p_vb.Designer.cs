namespace Menu_p_Operativo
{
    partial class mostrar_conf_compra
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            etiqueta_indica_zona = new Label();
            etiqueta_indica_fila = new Label();
            etiqueta_indica_asiento = new Label();
            etiqueta_indica_partido = new Label();
            etiqueta_indica_pbase_asient = new Label();
            etiqueta_indica_pbase_total = new Label();
            precio_base_total = new Label();
            precio_base_asiento = new Label();
            nom_partido = new Label();
            num_asiento = new Label();
            nom_d_fila = new Label();
            nom_d_zona = new Label();
            regresar_asientos_disp = new Button();
            aceptar_conf_compra = new Button();
            SuspendLayout();
            // 
            // etiqueta_indica_zona
            // 
            etiqueta_indica_zona.AutoSize = true;
            etiqueta_indica_zona.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_indica_zona.ForeColor = SystemColors.Window;
            etiqueta_indica_zona.Location = new Point(190, 34);
            etiqueta_indica_zona.Name = "etiqueta_indica_zona";
            etiqueta_indica_zona.Size = new Size(88, 29);
            etiqueta_indica_zona.TabIndex = 0;
            etiqueta_indica_zona.Text = "Zona: ";
            etiqueta_indica_zona.Click += etiqueta_indica_zona_Click;
            // 
            // etiqueta_indica_fila
            // 
            etiqueta_indica_fila.AutoSize = true;
            etiqueta_indica_fila.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_indica_fila.ForeColor = SystemColors.Window;
            etiqueta_indica_fila.Location = new Point(190, 81);
            etiqueta_indica_fila.Name = "etiqueta_indica_fila";
            etiqueta_indica_fila.Size = new Size(64, 29);
            etiqueta_indica_fila.TabIndex = 1;
            etiqueta_indica_fila.Text = "Fila:";
            etiqueta_indica_fila.Click += etiqueta_indica_fila_Click;
            // 
            // etiqueta_indica_asiento
            // 
            etiqueta_indica_asiento.AutoSize = true;
            etiqueta_indica_asiento.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_indica_asiento.ForeColor = SystemColors.Window;
            etiqueta_indica_asiento.Location = new Point(190, 130);
            etiqueta_indica_asiento.Name = "etiqueta_indica_asiento";
            etiqueta_indica_asiento.Size = new Size(111, 29);
            etiqueta_indica_asiento.TabIndex = 2;
            etiqueta_indica_asiento.Text = "Asiento:";
            etiqueta_indica_asiento.Click += etiqueta_indica_asiento_Click;
            // 
            // etiqueta_indica_partido
            // 
            etiqueta_indica_partido.AutoSize = true;
            etiqueta_indica_partido.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_indica_partido.ForeColor = SystemColors.Window;
            etiqueta_indica_partido.Location = new Point(190, 180);
            etiqueta_indica_partido.Name = "etiqueta_indica_partido";
            etiqueta_indica_partido.Size = new Size(109, 29);
            etiqueta_indica_partido.TabIndex = 3;
            etiqueta_indica_partido.Text = "Partido:";
            etiqueta_indica_partido.Click += etiqueta_indica_partido_Click;
            // 
            // etiqueta_indica_pbase_asient
            // 
            etiqueta_indica_pbase_asient.AutoSize = true;
            etiqueta_indica_pbase_asient.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_indica_pbase_asient.ForeColor = SystemColors.Window;
            etiqueta_indica_pbase_asient.Location = new Point(190, 227);
            etiqueta_indica_pbase_asient.Name = "etiqueta_indica_pbase_asient";
            etiqueta_indica_pbase_asient.Size = new Size(263, 29);
            etiqueta_indica_pbase_asient.TabIndex = 4;
            etiqueta_indica_pbase_asient.Text = "Precio Base Asiento: ";
            etiqueta_indica_pbase_asient.Click += etiqueta_indica_pbase_asient_Click;
            // 
            // etiqueta_indica_pbase_total
            // 
            etiqueta_indica_pbase_total.AutoSize = true;
            etiqueta_indica_pbase_total.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_indica_pbase_total.ForeColor = SystemColors.Window;
            etiqueta_indica_pbase_total.Location = new Point(190, 279);
            etiqueta_indica_pbase_total.Name = "etiqueta_indica_pbase_total";
            etiqueta_indica_pbase_total.Size = new Size(235, 29);
            etiqueta_indica_pbase_total.TabIndex = 5;
            etiqueta_indica_pbase_total.Text = "Precio Base Total: ";
            etiqueta_indica_pbase_total.Click += etiqueta_indica_pbase_total_Click;
            // 
            // precio_base_total
            // 
            precio_base_total.AutoSize = true;
            precio_base_total.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            precio_base_total.ForeColor = SystemColors.Window;
            precio_base_total.Location = new Point(474, 279);
            precio_base_total.Name = "precio_base_total";
            precio_base_total.Size = new Size(219, 29);
            precio_base_total.TabIndex = 11;
            precio_base_total.Text = "Precio Base Total";
            precio_base_total.Click += precio_base_total_Click;
            // 
            // precio_base_asiento
            // 
            precio_base_asiento.AutoSize = true;
            precio_base_asiento.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            precio_base_asiento.ForeColor = SystemColors.Window;
            precio_base_asiento.Location = new Point(474, 227);
            precio_base_asiento.Name = "precio_base_asiento";
            precio_base_asiento.Size = new Size(247, 29);
            precio_base_asiento.TabIndex = 10;
            precio_base_asiento.Text = "Precio Base Asiento";
            precio_base_asiento.Click += precio_base_asiento_Click;
            // 
            // nom_partido
            // 
            nom_partido.AutoSize = true;
            nom_partido.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            nom_partido.ForeColor = SystemColors.Window;
            nom_partido.Location = new Point(474, 180);
            nom_partido.Name = "nom_partido";
            nom_partido.Size = new Size(100, 29);
            nom_partido.TabIndex = 9;
            nom_partido.Text = "Partido";
            nom_partido.Click += nom_partido_Click;
            // 
            // num_asiento
            // 
            num_asiento.AutoSize = true;
            num_asiento.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            num_asiento.ForeColor = SystemColors.Window;
            num_asiento.Location = new Point(474, 130);
            num_asiento.Name = "num_asiento";
            num_asiento.Size = new Size(102, 29);
            num_asiento.TabIndex = 8;
            num_asiento.Text = "Asiento";
            num_asiento.Click += num_asiento_Click;
            // 
            // nom_d_fila
            // 
            nom_d_fila.AutoSize = true;
            nom_d_fila.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            nom_d_fila.ForeColor = SystemColors.Window;
            nom_d_fila.Location = new Point(474, 81);
            nom_d_fila.Name = "nom_d_fila";
            nom_d_fila.Size = new Size(55, 29);
            nom_d_fila.TabIndex = 7;
            nom_d_fila.Text = "Fila";
            nom_d_fila.Click += nom_d_fila_Click;
            // 
            // nom_d_zona
            // 
            nom_d_zona.AutoSize = true;
            nom_d_zona.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            nom_d_zona.ForeColor = SystemColors.Window;
            nom_d_zona.Location = new Point(474, 34);
            nom_d_zona.Name = "nom_d_zona";
            nom_d_zona.Size = new Size(72, 29);
            nom_d_zona.TabIndex = 6;
            nom_d_zona.Text = "Zona";
            nom_d_zona.Click += nom_d_zona_Click;
            // 
            // regresar_asientos_disp
            // 
            regresar_asientos_disp.ForeColor = SystemColors.HotTrack;
            regresar_asientos_disp.Location = new Point(231, 353);
            regresar_asientos_disp.Name = "regresar_asientos_disp";
            regresar_asientos_disp.Size = new Size(112, 56);
            regresar_asientos_disp.TabIndex = 12;
            regresar_asientos_disp.Text = "Regresar";
            regresar_asientos_disp.UseVisualStyleBackColor = true;
            regresar_asientos_disp.Click += regresar_asientos_disp_Click;
            // 
            // aceptar_conf_compra
            // 
            aceptar_conf_compra.ForeColor = SystemColors.HotTrack;
            aceptar_conf_compra.Location = new Point(488, 353);
            aceptar_conf_compra.Name = "aceptar_conf_compra";
            aceptar_conf_compra.Size = new Size(112, 55);
            aceptar_conf_compra.TabIndex = 13;
            aceptar_conf_compra.Text = "Aceptar";
            aceptar_conf_compra.UseVisualStyleBackColor = true;
            aceptar_conf_compra.Click += aceptar_conf_compra_Click;
            // 
            // mostrar_conf_compra
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(959, 450);
            Controls.Add(aceptar_conf_compra);
            Controls.Add(regresar_asientos_disp);
            Controls.Add(precio_base_total);
            Controls.Add(precio_base_asiento);
            Controls.Add(nom_partido);
            Controls.Add(num_asiento);
            Controls.Add(nom_d_fila);
            Controls.Add(nom_d_zona);
            Controls.Add(etiqueta_indica_pbase_total);
            Controls.Add(etiqueta_indica_pbase_asient);
            Controls.Add(etiqueta_indica_partido);
            Controls.Add(etiqueta_indica_asiento);
            Controls.Add(etiqueta_indica_fila);
            Controls.Add(etiqueta_indica_zona);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.ActiveCaptionText;
            Name = "mostrar_conf_compra";
            Text = "Confirmacion de Compra";
            Load += mostrar_conf_compra_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_indica_zona;
        private Label etiqueta_indica_fila;
        private Label etiqueta_indica_asiento;
        private Label etiqueta_indica_partido;
        private Label etiqueta_indica_pbase_asient;
        private Label etiqueta_indica_pbase_total;
        private Label precio_base_total;
        private Label precio_base_asiento;
        private Label nom_partido;
        private Label num_asiento;
        private Label nom_d_fila;
        private Label nom_d_zona;
        private Button regresar_asientos_disp;
        private Button aceptar_conf_compra;
    }
}
