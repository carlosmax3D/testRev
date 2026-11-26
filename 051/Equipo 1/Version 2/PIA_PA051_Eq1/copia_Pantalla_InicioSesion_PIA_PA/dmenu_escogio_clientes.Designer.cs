namespace Menu_p_Operativo
{
    partial class dmenu_eligio_clientes
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
            c_opcion_regisclient = new Button();
            c_opcion_buscarclient = new Button();
            etiqueta_qdh = new Label();
            regresar_a_menu_oper = new Button();
            SuspendLayout();
            // 
            // c_opcion_regisclient
            // 
            c_opcion_regisclient.Location = new Point(119, 135);
            c_opcion_regisclient.Margin = new Padding(4, 4, 4, 4);
            c_opcion_regisclient.Name = "c_opcion_regisclient";
            c_opcion_regisclient.Size = new Size(150, 86);
            c_opcion_regisclient.TabIndex = 0;
            c_opcion_regisclient.Text = "Registrar Clientes";
            c_opcion_regisclient.UseVisualStyleBackColor = true;
            c_opcion_regisclient.Click += c_opcion_regisclient_Click;
            // 
            // c_opcion_buscarclient
            // 
            c_opcion_buscarclient.Location = new Point(364, 135);
            c_opcion_buscarclient.Margin = new Padding(4, 4, 4, 4);
            c_opcion_buscarclient.Name = "c_opcion_buscarclient";
            c_opcion_buscarclient.Size = new Size(150, 89);
            c_opcion_buscarclient.TabIndex = 1;
            c_opcion_buscarclient.Text = "Buscar Cliente";
            c_opcion_buscarclient.UseVisualStyleBackColor = true;
            c_opcion_buscarclient.Click += c_opcion_buscarclient_Click;
            // 
            // etiqueta_qdh
            // 
            etiqueta_qdh.AutoSize = true;
            etiqueta_qdh.ForeColor = SystemColors.Window;
            etiqueta_qdh.Location = new Point(232, 41);
            etiqueta_qdh.Margin = new Padding(4, 0, 4, 0);
            etiqueta_qdh.Name = "etiqueta_qdh";
            etiqueta_qdh.Size = new Size(159, 25);
            etiqueta_qdh.TabIndex = 2;
            etiqueta_qdh.Text = "¿Que desea hacer?";
            // 
            // regresar_a_menu_oper
            // 
            regresar_a_menu_oper.Location = new Point(254, 345);
            regresar_a_menu_oper.Margin = new Padding(4, 4, 4, 4);
            regresar_a_menu_oper.Name = "regresar_a_menu_oper";
            regresar_a_menu_oper.Size = new Size(118, 36);
            regresar_a_menu_oper.TabIndex = 3;
            regresar_a_menu_oper.Text = "Regresar";
            regresar_a_menu_oper.UseVisualStyleBackColor = true;
            regresar_a_menu_oper.Click += regresar_a_menu_oper_Click;
            // 
            // dmenu_eligio_clientes
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(650, 442);
            Controls.Add(regresar_a_menu_oper);
            Controls.Add(etiqueta_qdh);
            Controls.Add(c_opcion_buscarclient);
            Controls.Add(c_opcion_regisclient);
            ForeColor = SystemColors.HotTrack;
            Margin = new Padding(4, 4, 4, 4);
            Name = "dmenu_eligio_clientes";
            Text = "Clientes";
            Load += dmenu_eligio_clientes_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button c_opcion_regisclient;
        private Button c_opcion_buscarclient;
        private Label etiqueta_qdh;
        private Button regresar_a_menu_oper;
    }
}
