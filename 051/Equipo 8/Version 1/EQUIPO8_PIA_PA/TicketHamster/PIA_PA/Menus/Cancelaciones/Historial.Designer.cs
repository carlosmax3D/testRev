namespace PIA_PA
{
    partial class Historial
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
            idPartido = new DataGridViewTextBoxColumn();
            colFecha = new DataGridViewTextBoxColumn();
            colHora = new DataGridViewTextBoxColumn();
            colLocal = new DataGridViewTextBoxColumn();
            colVisitante = new DataGridViewTextBoxColumn();
            colEstadio = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            menuConsultarPartidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCatalogo).BeginInit();
            SuspendLayout();
            // 
            // menuConsultarPartidos
            // 
            menuConsultarPartidos.Controls.Add(btnActualizar);
            menuConsultarPartidos.Controls.Add(btnBuscar);
            menuConsultarPartidos.Controls.Add(cBoxConsultarPartidos);
            menuConsultarPartidos.Controls.Add(txtBoxConsultarPartidos);
            menuConsultarPartidos.Controls.Add(label1);
            menuConsultarPartidos.Controls.Add(gridCatalogo);
            menuConsultarPartidos.Location = new Point(0, 0);
            menuConsultarPartidos.Margin = new Padding(2);
            menuConsultarPartidos.Name = "menuConsultarPartidos";
            menuConsultarPartidos.Padding = new Padding(0);
            menuConsultarPartidos.Size = new Size(743, 392);
            menuConsultarPartidos.TabIndex = 3;
            menuConsultarPartidos.TabStop = false;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(651, 361);
            btnActualizar.Margin = new Padding(2);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(83, 22);
            btnActualizar.TabIndex = 6;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(473, 361);
            btnBuscar.Margin = new Padding(2);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(72, 22);
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
            cBoxConsultarPartidos.ItemHeight = 15;
            cBoxConsultarPartidos.Items.AddRange(new object[] { "ID", "Fecha", "Hora", "Local", "Visitante", "Estadio", "Estado" });
            cBoxConsultarPartidos.Location = new Point(83, 363);
            cBoxConsultarPartidos.Margin = new Padding(2);
            cBoxConsultarPartidos.Name = "cBoxConsultarPartidos";
            cBoxConsultarPartidos.Size = new Size(129, 23);
            cBoxConsultarPartidos.TabIndex = 4;
            // 
            // txtBoxConsultarPartidos
            // 
            txtBoxConsultarPartidos.Location = new Point(215, 363);
            txtBoxConsultarPartidos.Margin = new Padding(2);
            txtBoxConsultarPartidos.Name = "txtBoxConsultarPartidos";
            txtBoxConsultarPartidos.Size = new Size(255, 23);
            txtBoxConsultarPartidos.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 362);
            label1.Margin = new Padding(0, 0, 2, 12);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 2;
            label1.Text = "Buscar por:";
            // 
            // gridCatalogo
            // 
            gridCatalogo.AllowUserToOrderColumns = true;
            gridCatalogo.ColumnHeadersHeight = 34;
            gridCatalogo.Columns.AddRange(new DataGridViewColumn[] { idPartido, colFecha, colHora, colLocal, colVisitante, colEstadio, colEstado });
            gridCatalogo.Location = new Point(9, 16);
            gridCatalogo.Margin = new Padding(7, 2, 7, 42);
            gridCatalogo.Name = "gridCatalogo";
            gridCatalogo.RowHeadersWidth = 62;
            gridCatalogo.Size = new Size(724, 334);
            gridCatalogo.TabIndex = 1;
            // 
            // idPartido
            // 
            idPartido.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            idPartido.Frozen = true;
            idPartido.HeaderText = "ID";
            idPartido.MinimumWidth = 8;
            idPartido.Name = "idPartido";
            idPartido.Width = 43;
            // 
            // colFecha
            // 
            colFecha.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colFecha.Frozen = true;
            colFecha.HeaderText = "Fecha";
            colFecha.MinimumWidth = 8;
            colFecha.Name = "colFecha";
            colFecha.Width = 63;
            // 
            // colHora
            // 
            colHora.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colHora.Frozen = true;
            colHora.HeaderText = "Hora";
            colHora.MinimumWidth = 8;
            colHora.Name = "colHora";
            colHora.Width = 58;
            // 
            // colLocal
            // 
            colLocal.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colLocal.HeaderText = "Local";
            colLocal.MinimumWidth = 8;
            colLocal.Name = "colLocal";
            // 
            // colVisitante
            // 
            colVisitante.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colVisitante.HeaderText = "Visitante";
            colVisitante.MinimumWidth = 8;
            colVisitante.Name = "colVisitante";
            // 
            // colEstadio
            // 
            colEstadio.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colEstadio.HeaderText = "Estadio";
            colEstadio.MinimumWidth = 8;
            colEstadio.Name = "colEstadio";
            // 
            // colEstado
            // 
            colEstado.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colEstado.HeaderText = "Estado";
            colEstado.MinimumWidth = 8;
            colEstado.Name = "colEstado";
            // 
            // ConsultarPartidos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuConsultarPartidos);
            Name = "ConsultarPartidos";
            Size = new Size(744, 394);
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
        private DataGridViewTextBoxColumn idPartido;
        private DataGridViewTextBoxColumn colFecha;
        private DataGridViewTextBoxColumn colHora;
        private DataGridViewTextBoxColumn colLocal;
        private DataGridViewTextBoxColumn colVisitante;
        private DataGridViewTextBoxColumn colEstadio;
        private DataGridViewTextBoxColumn colEstado;
    }
}
