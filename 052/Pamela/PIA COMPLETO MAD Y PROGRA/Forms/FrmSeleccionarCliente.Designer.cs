namespace TemplateCSharp.Forms
{
    partial class FrmSeleccionarCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtTituloSC = new Label();
            txtIndicacionSC = new Label();
            dGVClientesSC = new DataGridView();
            botonSeleccionarSC = new Button();
            botonCancelarSC = new Button();
            ((System.ComponentModel.ISupportInitialize)dGVClientesSC).BeginInit();
            SuspendLayout();
            // 
            // txtTituloSC
            // 
            txtTituloSC.AutoSize = true;
            txtTituloSC.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTituloSC.ForeColor = Color.RoyalBlue;
            txtTituloSC.Location = new Point(12, 9);
            txtTituloSC.Name = "txtTituloSC";
            txtTituloSC.Size = new Size(291, 30);
            txtTituloSC.TabIndex = 0;
            txtTituloSC.Text = "Seleccionar cliente";
            // 
            // txtIndicacionSC
            // 
            txtIndicacionSC.AutoSize = true;
            txtIndicacionSC.Location = new Point(12, 50);
            txtIndicacionSC.Name = "txtIndicacionSC";
            txtIndicacionSC.Size = new Size(380, 15);
            txtIndicacionSC.TabIndex = 1;
            txtIndicacionSC.Text = "Se encontraron varios clientes. Selecciona el correcto:";
            // 
            // dGVClientesSC
            // 
            dGVClientesSC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGVClientesSC.Location = new Point(12, 74);
            dGVClientesSC.Name = "dGVClientesSC";
            dGVClientesSC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dGVClientesSC.Size = new Size(640, 200);
            dGVClientesSC.TabIndex = 2;
            dGVClientesSC.CellDoubleClick += dGVClientesSC_CellDoubleClick;
            // 
            // botonSeleccionarSC
            // 
            botonSeleccionarSC.Location = new Point(402, 286);
            botonSeleccionarSC.Name = "botonSeleccionarSC";
            botonSeleccionarSC.Size = new Size(120, 36);
            botonSeleccionarSC.TabIndex = 3;
            botonSeleccionarSC.Text = "Seleccionar";
            botonSeleccionarSC.UseVisualStyleBackColor = true;
            botonSeleccionarSC.Click += botonSeleccionarSC_Click;
            // 
            // botonCancelarSC
            // 
            botonCancelarSC.Location = new Point(532, 286);
            botonCancelarSC.Name = "botonCancelarSC";
            botonCancelarSC.Size = new Size(120, 36);
            botonCancelarSC.TabIndex = 4;
            botonCancelarSC.Text = "Cancelar";
            botonCancelarSC.UseVisualStyleBackColor = true;
            botonCancelarSC.Click += botonCancelarSC_Click;
            // 
            // FrmSeleccionarCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(664, 334);
            Controls.Add(botonCancelarSC);
            Controls.Add(botonSeleccionarSC);
            Controls.Add(dGVClientesSC);
            Controls.Add(txtIndicacionSC);
            Controls.Add(txtTituloSC);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSeleccionarCliente";
            Text = "Seleccionar cliente";
            ((System.ComponentModel.ISupportInitialize)dGVClientesSC).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtTituloSC;
        private Label txtIndicacionSC;
        private DataGridView dGVClientesSC;
        private Button botonSeleccionarSC;
        private Button botonCancelarSC;
    }
}
