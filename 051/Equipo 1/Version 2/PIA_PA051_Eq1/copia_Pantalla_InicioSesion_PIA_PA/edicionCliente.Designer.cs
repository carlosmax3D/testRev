
namespace Pantalla_EdicionCliente_PIA_PA
{
    partial class pantalla_edicionCliente
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
            datagridview1_edicionCliente = new DataGridView();
            boton2_edicionCliente = new Button();
            boton1_edicionCliente = new Button();
            boton3_edicionCliente = new Button();
            menuStrip1 = new MenuStrip();
            atras_edicionCliente = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)datagridview1_edicionCliente).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // datagridview1_edicionCliente
            // 
            datagridview1_edicionCliente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridview1_edicionCliente.Location = new Point(31, 48);
            datagridview1_edicionCliente.Name = "datagridview1_edicionCliente";
            datagridview1_edicionCliente.RowHeadersWidth = 62;
            datagridview1_edicionCliente.Size = new Size(1325, 247);
            datagridview1_edicionCliente.TabIndex = 0;
            datagridview1_edicionCliente.CellContentClick += datagridview1_edicionCliente_CellContentClick;
            // 
            // boton2_edicionCliente
            // 
            boton2_edicionCliente.BackColor = Color.LightGreen;
            boton2_edicionCliente.Location = new Point(691, 344);
            boton2_edicionCliente.Name = "boton2_edicionCliente";
            boton2_edicionCliente.Size = new Size(112, 34);
            boton2_edicionCliente.TabIndex = 1;
            boton2_edicionCliente.Text = "Editar";
            boton2_edicionCliente.UseVisualStyleBackColor = false;
            boton2_edicionCliente.Click += boton2_edicionCliente_Click;
            // 
            // boton1_edicionCliente
            // 
            boton1_edicionCliente.BackColor = Color.MediumTurquoise;
            boton1_edicionCliente.Location = new Point(1244, 344);
            boton1_edicionCliente.Name = "boton1_edicionCliente";
            boton1_edicionCliente.Size = new Size(112, 34);
            boton1_edicionCliente.TabIndex = 2;
            boton1_edicionCliente.Text = "Guardar";
            boton1_edicionCliente.UseVisualStyleBackColor = false;
            boton1_edicionCliente.Click += boton1_edicionCliente_Click;
            // 
            // boton3_edicionCliente
            // 
            boton3_edicionCliente.BackColor = Color.LightCoral;
            boton3_edicionCliente.Location = new Point(31, 344);
            boton3_edicionCliente.Name = "boton3_edicionCliente";
            boton3_edicionCliente.Size = new Size(112, 34);
            boton3_edicionCliente.TabIndex = 3;
            boton3_edicionCliente.Text = "Eliminar";
            boton3_edicionCliente.UseVisualStyleBackColor = false;
            boton3_edicionCliente.Click += boton3_edicionCliente_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { atras_edicionCliente });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1378, 33);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // atras_edicionCliente
            // 
            atras_edicionCliente.Name = "atras_edicionCliente";
            atras_edicionCliente.Size = new Size(69, 29);
            atras_edicionCliente.Text = "Atras";
            atras_edicionCliente.Click += atras_edicionCliente_Click;
            // 
            // pantalla_edicionCliente
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1378, 450);
            Controls.Add(boton3_edicionCliente);
            Controls.Add(boton1_edicionCliente);
            Controls.Add(boton2_edicionCliente);
            Controls.Add(datagridview1_edicionCliente);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "pantalla_edicionCliente";
            Text = "Edicion de Cliente";
            Load += pantalla_edicionCliente_Load;
            ((System.ComponentModel.ISupportInitialize)datagridview1_edicionCliente).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void atras_edicionCliente_Click(object sender, EventArgs e)
        {
            // Regresar a la pantalla anterior.
            if (historialAnterior != null)
            {
                historialAnterior.Show();
                historialAnterior.Activate();
            }

            // Cerrar la pantalla de edición.
            this.Close();
        }

        #endregion

        private DataGridView datagridview1_edicionCliente;
        private Button boton2_edicionCliente;
        private Button boton1_edicionCliente;
        private Button boton3_edicionCliente;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem atras_edicionCliente;
    }
}
