
namespace Menu_p_Operativo
{
    partial class menu_operador
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
            opcion_ventaboletos = new Button();
            opcion_clientes = new Button();
            opcion_evaluaciongerencial = new Button();
            opcion_reportes = new Button();
            etiqueta_queprocede = new Label();
            cerrar_secion_operativo = new Button();
            SuspendLayout();
            // 
            // opcion_ventaboletos
            // 
            opcion_ventaboletos.Location = new Point(164, 246);
            opcion_ventaboletos.Margin = new Padding(4, 4, 4, 4);
            opcion_ventaboletos.Name = "opcion_ventaboletos";
            opcion_ventaboletos.Size = new Size(131, 84);
            opcion_ventaboletos.TabIndex = 0;
            opcion_ventaboletos.Text = "Venta Boletos";
            opcion_ventaboletos.UseVisualStyleBackColor = true;
            opcion_ventaboletos.Click += opcion_ventaboletos_Click;
            // 
            // opcion_clientes
            // 
            opcion_clientes.Location = new Point(359, 246);
            opcion_clientes.Margin = new Padding(4, 4, 4, 4);
            opcion_clientes.Name = "opcion_clientes";
            opcion_clientes.Size = new Size(130, 84);
            opcion_clientes.TabIndex = 1;
            opcion_clientes.Text = "Clientes";
            opcion_clientes.UseVisualStyleBackColor = true;
            opcion_clientes.Click += opcion_clientes_Click;
            // 
            // opcion_evaluaciongerencial
            // 
            opcion_evaluaciongerencial.Location = new Point(731, 246);
            opcion_evaluaciongerencial.Margin = new Padding(4, 4, 4, 4);
            opcion_evaluaciongerencial.Name = "opcion_evaluaciongerencial";
            opcion_evaluaciongerencial.Size = new Size(130, 89);
            opcion_evaluaciongerencial.TabIndex = 2;
            opcion_evaluaciongerencial.Text = "Evaluacion Gerencial";
            opcion_evaluaciongerencial.UseVisualStyleBackColor = true;
            opcion_evaluaciongerencial.Click += opcion_evaluaciongerencial_Click;
            // 
            // opcion_reportes
            // 
            opcion_reportes.Location = new Point(541, 246);
            opcion_reportes.Margin = new Padding(4, 4, 4, 4);
            opcion_reportes.Name = "opcion_reportes";
            opcion_reportes.Size = new Size(130, 88);
            opcion_reportes.TabIndex = 3;
            opcion_reportes.Text = "Reportes";
            opcion_reportes.UseVisualStyleBackColor = true;
            opcion_reportes.Click += opcion_reportes_Click;
            // 
            // etiqueta_queprocede
            // 
            etiqueta_queprocede.AutoSize = true;
            etiqueta_queprocede.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            etiqueta_queprocede.ForeColor = SystemColors.Window;
            etiqueta_queprocede.Location = new Point(395, 124);
            etiqueta_queprocede.Margin = new Padding(4, 0, 4, 0);
            etiqueta_queprocede.Name = "etiqueta_queprocede";
            etiqueta_queprocede.Size = new Size(216, 29);
            etiqueta_queprocede.TabIndex = 4;
            etiqueta_queprocede.Text = "¿Que desea hacer?";
            // 
            // cerrar_secion_operativo
            // 
            cerrar_secion_operativo.Location = new Point(418, 448);
            cerrar_secion_operativo.Margin = new Padding(4, 4, 4, 4);
            cerrar_secion_operativo.Name = "cerrar_secion_operativo";
            cerrar_secion_operativo.Size = new Size(166, 62);
            cerrar_secion_operativo.TabIndex = 5;
            cerrar_secion_operativo.Text = "Cerrar Sesion";
            cerrar_secion_operativo.UseVisualStyleBackColor = true;
            cerrar_secion_operativo.Click += cerrar_secion_operativo_Click;
            // 
            // menu_operador
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1000, 562);
            Controls.Add(cerrar_secion_operativo);
            Controls.Add(etiqueta_queprocede);
            Controls.Add(opcion_reportes);
            Controls.Add(opcion_evaluaciongerencial);
            Controls.Add(opcion_clientes);
            Controls.Add(opcion_ventaboletos);
            ForeColor = SystemColors.HotTrack;
            Margin = new Padding(4, 4, 4, 4);
            Name = "menu_operador";
            Text = "Menu Operador";
            Load += menu_operador_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void menu_operador_Load(object sender, EventArgs e)
        {
            // No se requieren instrucciones al cargar el menú.
        }

        #endregion

        private Button opcion_ventaboletos;
        private Button opcion_clientes;
        private Button opcion_evaluaciongerencial;
        private Button opcion_reportes;
        private Label etiqueta_queprocede;
        private Button cerrar_secion_operativo;
    }
}
