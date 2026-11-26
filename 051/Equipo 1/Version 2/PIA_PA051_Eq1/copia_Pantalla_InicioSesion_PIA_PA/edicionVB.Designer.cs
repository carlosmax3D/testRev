namespace Pantalla_EdicionInfoVB_PIA_PA
{
    partial class pantalla_edicionVB
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
            datagridview1_edicionVB = new DataGridView();
            boton3_edicionVB = new Button();
            botn1_edicionVB = new Button();
            menuStrip1 = new MenuStrip();
            atras_edicionVB = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)datagridview1_edicionVB).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // datagridview1_edicionVB
            // 
            datagridview1_edicionVB.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridview1_edicionVB.Location = new Point(41, 74);
            datagridview1_edicionVB.Name = "datagridview1_edicionVB";
            datagridview1_edicionVB.RowHeadersWidth = 62;
            datagridview1_edicionVB.Size = new Size(713, 175);
            datagridview1_edicionVB.TabIndex = 0;
            datagridview1_edicionVB.CellContentClick += datagridview1_edicionVB_CellContentClick;
            // 
            // boton3_edicionVB
            // 
            boton3_edicionVB.BackColor = Color.LightCoral;
            boton3_edicionVB.Location = new Point(41, 306);
            boton3_edicionVB.Name = "boton3_edicionVB";
            boton3_edicionVB.Size = new Size(112, 34);
            boton3_edicionVB.TabIndex = 2;
            boton3_edicionVB.Text = "Eliminar";
            boton3_edicionVB.UseVisualStyleBackColor = false;
            boton3_edicionVB.Click += boton3_edicionVB_Click;
            // 
            // botn1_edicionVB
            // 
            botn1_edicionVB.BackColor = Color.LightGreen;
            botn1_edicionVB.Location = new Point(642, 306);
            botn1_edicionVB.Name = "botn1_edicionVB";
            botn1_edicionVB.Size = new Size(112, 34);
            botn1_edicionVB.TabIndex = 3;
            botn1_edicionVB.Text = "Guardar";
            botn1_edicionVB.UseVisualStyleBackColor = false;
            botn1_edicionVB.Click += boton1_edicionVB_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { atras_edicionVB });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 33);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // atras_edicionVB
            // 
            atras_edicionVB.Name = "atras_edicionVB";
            atras_edicionVB.Size = new Size(69, 29);
            atras_edicionVB.Text = "Atras";
            atras_edicionVB.Click += atras_edicionVB_Click;
            // 
            // pantalla_edicionVB
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 396);
            Controls.Add(botn1_edicionVB);
            Controls.Add(boton3_edicionVB);
            Controls.Add(datagridview1_edicionVB);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "pantalla_edicionVB";
            Text = "Edicion Venta de Boleto";
            Load += pantalla_edicionVB_Load;
            ((System.ComponentModel.ISupportInitialize)datagridview1_edicionVB).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView datagridview1_edicionVB;
        private Button boton3_edicionVB;
        private Button botn1_edicionVB;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem atras_edicionVB;
    }
}
