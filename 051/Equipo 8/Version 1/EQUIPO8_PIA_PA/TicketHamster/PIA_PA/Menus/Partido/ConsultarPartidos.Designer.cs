namespace PIA_PA
{
    partial class ConsultarPartidos
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
            menuConsultarPartidos = new GroupBox();
            btnActualizar = new Button();
            btnBuscar = new Button();
            cBoxConsultarPartidos = new ComboBox();
            txtBoxConsultarPartidos = new TextBox();
            label1 = new Label();
            gridCatalogo = new DataGridView();
            btnProbarconn = new Button();
            menuConsultarPartidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCatalogo).BeginInit();
            SuspendLayout();
            // 
            // menuConsultarPartidos
            // 
            menuConsultarPartidos.Controls.Add(btnProbarconn);
            menuConsultarPartidos.Controls.Add(btnActualizar);
            menuConsultarPartidos.Controls.Add(btnBuscar);
            menuConsultarPartidos.Controls.Add(cBoxConsultarPartidos);
            menuConsultarPartidos.Controls.Add(txtBoxConsultarPartidos);
            menuConsultarPartidos.Controls.Add(label1);
            menuConsultarPartidos.Controls.Add(gridCatalogo);
            menuConsultarPartidos.Location = new Point(0, 0);
            menuConsultarPartidos.Name = "menuConsultarPartidos";
            menuConsultarPartidos.Padding = new Padding(0);
            menuConsultarPartidos.Size = new Size(1061, 653);
            menuConsultarPartidos.TabIndex = 3;
            menuConsultarPartidos.TabStop = false;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(930, 602);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(119, 37);
            btnActualizar.TabIndex = 6;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(676, 602);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(103, 37);
            btnBuscar.TabIndex = 5;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnConsultarPartidos_Click;
            // 
            // cBoxConsultarPartidos
            // 
            cBoxConsultarPartidos.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxConsultarPartidos.FlatStyle = FlatStyle.System;
            cBoxConsultarPartidos.FormattingEnabled = true;
            cBoxConsultarPartidos.ItemHeight = 25;
            cBoxConsultarPartidos.Items.AddRange(new object[] { "ID", "Fecha", "Hora", "Local", "Visitante", "Estadio", "Estado" });
            cBoxConsultarPartidos.Location = new Point(119, 605);
            cBoxConsultarPartidos.Name = "cBoxConsultarPartidos";
            cBoxConsultarPartidos.Size = new Size(183, 33);
            cBoxConsultarPartidos.TabIndex = 4;
            // 
            // txtBoxConsultarPartidos
            // 
            txtBoxConsultarPartidos.Location = new Point(307, 605);
            txtBoxConsultarPartidos.Name = "txtBoxConsultarPartidos";
            txtBoxConsultarPartidos.Size = new Size(363, 31);
            txtBoxConsultarPartidos.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 603);
            label1.Margin = new Padding(0, 0, 3, 20);
            label1.Name = "label1";
            label1.Size = new Size(100, 25);
            label1.TabIndex = 2;
            label1.Text = "Buscar por:";
            // 
            // gridCatalogo
            // 
            gridCatalogo.AllowUserToOrderColumns = true;
            gridCatalogo.ColumnHeadersHeight = 34;
            gridCatalogo.Location = new Point(13, 27);
            gridCatalogo.Margin = new Padding(10, 3, 10, 70);
            gridCatalogo.Name = "gridCatalogo";
            gridCatalogo.RowHeadersWidth = 62;
            gridCatalogo.Size = new Size(1034, 557);
            gridCatalogo.TabIndex = 1;
            // 
            // btnProbarconn
            // 
            btnProbarconn.Location = new Point(785, 605);
            btnProbarconn.Name = "btnProbarconn";
            btnProbarconn.Size = new Size(139, 34);
            btnProbarconn.TabIndex = 7;
            btnProbarconn.Text = "Probar conn";
            btnProbarconn.UseVisualStyleBackColor = true;
            btnProbarconn.Click += button1_Click;
            // 
            // ConsultarPartidos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuConsultarPartidos);
            Margin = new Padding(4, 5, 4, 5);
            Name = "ConsultarPartidos";
            Size = new Size(1063, 657);
            menuConsultarPartidos.ResumeLayout(false);
            menuConsultarPartidos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridCatalogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox menuConsultarPartidos;
        private Button btnActualizar;
        private Button btnBuscar;
        private ComboBox cBoxConsultarPartidos;
        private TextBox txtBoxConsultarPartidos;
        private Label label1;
        private DataGridView gridCatalogo;
        private Button btnProbarconn;
    }
}
