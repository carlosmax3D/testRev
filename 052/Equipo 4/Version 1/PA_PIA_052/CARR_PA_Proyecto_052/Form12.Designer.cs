namespace CARR_PA_Proyecto_052.Forms
{
    partial class Form12
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.TableLayoutPanel layoutPrincipal = null!;
        private System.Windows.Forms.Label lblTitulo = null!;
        private System.Windows.Forms.GroupBox grpVentas = null!;
        private System.Windows.Forms.GroupBox grpMotivo = null!;
        private System.Windows.Forms.GroupBox grpHistorial = null!;
        private System.Windows.Forms.TableLayoutPanel layoutMotivo = null!;
        private System.Windows.Forms.Label lblCodigoCompra = null!;
        private System.Windows.Forms.TextBox txtCodigoCompra = null!;
        private System.Windows.Forms.Label lblMotivo = null!;
        private System.Windows.Forms.DataGridView dgvVentas = null!;
        private System.Windows.Forms.DataGridView dgvCancelaciones = null!;
        private System.Windows.Forms.TextBox txtMotivo = null!;
        private System.Windows.Forms.Button btnCancelarBoleto = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5 = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            layoutPrincipal = new TableLayoutPanel();
            lblTitulo = new Label();
            grpVentas = new GroupBox();
            dgvVentas = new DataGridView();
            grpMotivo = new GroupBox();
            layoutMotivo = new TableLayoutPanel();
            lblCodigoCompra = new Label();
            txtCodigoCompra = new TextBox();
            lblMotivo = new Label();
            txtMotivo = new TextBox();
            btnCancelarBoleto = new Button();
            grpHistorial = new GroupBox();
            dgvCancelaciones = new DataGridView();
            layoutPrincipal.SuspendLayout();
            grpVentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            grpMotivo.SuspendLayout();
            layoutMotivo.SuspendLayout();
            grpHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCancelaciones).BeginInit();
            SuspendLayout();
            // 
            // layoutPrincipal
            // 
            layoutPrincipal.ColumnCount = 1;
            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPrincipal.Controls.Add(lblTitulo, 0, 0);
            layoutPrincipal.Controls.Add(grpVentas, 0, 1);
            layoutPrincipal.Controls.Add(grpMotivo, 0, 2);
            layoutPrincipal.Controls.Add(grpHistorial, 0, 3);
            layoutPrincipal.Dock = DockStyle.Fill;
            layoutPrincipal.Location = new Point(0, 0);
            layoutPrincipal.Name = "layoutPrincipal";
            layoutPrincipal.Padding = new Padding(14);
            layoutPrincipal.RowCount = 4;
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 156F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));
            layoutPrincipal.Size = new Size(1080, 760);
            layoutPrincipal.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(17, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1046, 42);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cancelación de boletos";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpVentas
            // 
            grpVentas.Controls.Add(dgvVentas);
            grpVentas.Dock = DockStyle.Fill;
            grpVentas.Location = new Point(17, 59);
            grpVentas.Name = "grpVentas";
            grpVentas.Padding = new Padding(8);
            grpVentas.Size = new Size(1046, 250);
            grpVentas.TabIndex = 1;
            grpVentas.TabStop = false;
            grpVentas.Text = "Ventas disponibles para cancelar";
            // 
            // dgvVentas
            // 
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.AllowUserToDeleteRows = false;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.Dock = DockStyle.Fill;
            dgvVentas.Location = new Point(8, 24);
            dgvVentas.MultiSelect = false;
            dgvVentas.Name = "dgvVentas";
            dgvVentas.ReadOnly = true;
            dgvVentas.RowHeadersVisible = false;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.Size = new Size(1030, 218);
            dgvVentas.TabIndex = 0;
            // 
            // grpMotivo
            // 
            grpMotivo.Controls.Add(layoutMotivo);
            grpMotivo.Dock = DockStyle.Fill;
            grpMotivo.Location = new Point(17, 315);
            grpMotivo.Name = "grpMotivo";
            grpMotivo.Padding = new Padding(8);
            grpMotivo.Size = new Size(1046, 150);
            grpMotivo.TabIndex = 2;
            grpMotivo.TabStop = false;
            grpMotivo.Text = "Datos de cancelación";
            // 
            // layoutMotivo
            // 
            layoutMotivo.ColumnCount = 2;
            layoutMotivo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMotivo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            layoutMotivo.Controls.Add(lblCodigoCompra, 0, 0);
            layoutMotivo.Controls.Add(txtCodigoCompra, 0, 1);
            layoutMotivo.Controls.Add(lblMotivo, 0, 2);
            layoutMotivo.Controls.Add(txtMotivo, 0, 3);
            layoutMotivo.Controls.Add(btnCancelarBoleto, 1, 3);
            layoutMotivo.Dock = DockStyle.Fill;
            layoutMotivo.Location = new Point(8, 24);
            layoutMotivo.Name = "layoutMotivo";
            layoutMotivo.RowCount = 4;
            layoutMotivo.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layoutMotivo.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            layoutMotivo.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layoutMotivo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutMotivo.Size = new Size(1030, 118);
            layoutMotivo.TabIndex = 0;
            // 
            // lblCodigoCompra
            // 
            layoutMotivo.SetColumnSpan(lblCodigoCompra, 2);
            lblCodigoCompra.Dock = DockStyle.Fill;
            lblCodigoCompra.Location = new Point(3, 0);
            lblCodigoCompra.Name = "lblCodigoCompra";
            lblCodigoCompra.Size = new Size(1024, 24);
            lblCodigoCompra.TabIndex = 0;
            lblCodigoCompra.Text = "Código de compra (GUID):";
            lblCodigoCompra.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtCodigoCompra
            // 
            layoutMotivo.SetColumnSpan(txtCodigoCompra, 2);
            txtCodigoCompra.Dock = DockStyle.Fill;
            txtCodigoCompra.Location = new Point(3, 27);
            txtCodigoCompra.MaxLength = 36;
            txtCodigoCompra.Name = "txtCodigoCompra";
            txtCodigoCompra.Size = new Size(1024, 23);
            txtCodigoCompra.TabIndex = 1;
            txtCodigoCompra.TextChanged += txtCodigoCompra_TextChanged;
            // 
            // lblMotivo
            // 
            layoutMotivo.SetColumnSpan(lblMotivo, 2);
            lblMotivo.Dock = DockStyle.Fill;
            lblMotivo.Location = new Point(3, 52);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.Size = new Size(1024, 24);
            lblMotivo.TabIndex = 0;
            lblMotivo.Text = "Indica por qué se cancela la compra (máximo 255 caracteres):";
            lblMotivo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtMotivo
            // 
            txtMotivo.Dock = DockStyle.Fill;
            txtMotivo.Location = new Point(3, 79);
            txtMotivo.MaxLength = 255;
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(834, 23);
            txtMotivo.TabIndex = 1;
            txtMotivo.TextChanged += txtMotivo_TextChanged;
            // 
            // btnCancelarBoleto
            // 
            btnCancelarBoleto.Dock = DockStyle.Fill;
            btnCancelarBoleto.Enabled = false;
            btnCancelarBoleto.Location = new Point(843, 79);
            btnCancelarBoleto.Name = "btnCancelarBoleto";
            btnCancelarBoleto.Size = new Size(184, 36);
            btnCancelarBoleto.TabIndex = 2;
            btnCancelarBoleto.Text = "Registrar cancelación";
            btnCancelarBoleto.UseVisualStyleBackColor = true;
            btnCancelarBoleto.Click += btnCancelarBoleto_Click;
            // 
            // grpHistorial
            // 
            grpHistorial.Controls.Add(dgvCancelaciones);
            grpHistorial.Dock = DockStyle.Fill;
            grpHistorial.Location = new Point(17, 471);
            grpHistorial.Name = "grpHistorial";
            grpHistorial.Padding = new Padding(8);
            grpHistorial.Size = new Size(1046, 272);
            grpHistorial.TabIndex = 3;
            grpHistorial.TabStop = false;
            grpHistorial.Text = "Cancelaciones registradas";
            // 
            // dgvCancelaciones
            // 
            dgvCancelaciones.AllowUserToAddRows = false;
            dgvCancelaciones.AllowUserToDeleteRows = false;
            dgvCancelaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCancelaciones.Dock = DockStyle.Fill;
            dgvCancelaciones.Location = new Point(8, 24);
            dgvCancelaciones.MultiSelect = false;
            dgvCancelaciones.Name = "dgvCancelaciones";
            dgvCancelaciones.ReadOnly = true;
            dgvCancelaciones.RowHeadersVisible = false;
            dgvCancelaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCancelaciones.Size = new Size(1030, 240);
            dgvCancelaciones.TabIndex = 0;
            // 
            // Form12
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 760);
            Controls.Add(layoutPrincipal);
            MinimumSize = new Size(900, 650);
            Name = "Form12";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cancelaciones de boletos";
            Load += Form12_Load;
            layoutPrincipal.ResumeLayout(false);
            grpVentas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            grpMotivo.ResumeLayout(false);
            layoutMotivo.ResumeLayout(false);
            layoutMotivo.PerformLayout();
            grpHistorial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCancelaciones).EndInit();
            ResumeLayout(false);
        }

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}
