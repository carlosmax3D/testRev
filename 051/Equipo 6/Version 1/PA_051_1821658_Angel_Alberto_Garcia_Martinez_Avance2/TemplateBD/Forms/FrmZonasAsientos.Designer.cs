#nullable disable
namespace TemplateCSharp.Forms;
partial class FrmZonasAsientos
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitulo;
    private Label lblEstadio;
    private ComboBox cboEstadio;
    private Label lblZona;
    private ComboBox cboZona;
    private Label lblDetalleZona;
    private DataGridView dgvAsientos;
    private DataGridViewTextBoxColumn colFila;
    private DataGridViewTextBoxColumn colAsiento;
    private Button btnCerrar;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitulo = new Label();
        lblEstadio = new Label();
        cboEstadio = new ComboBox();
        lblZona = new Label();
        cboZona = new ComboBox();
        lblDetalleZona = new Label();
        dgvAsientos = new DataGridView();
        colFila = new DataGridViewTextBoxColumn();
        colAsiento = new DataGridViewTextBoxColumn();
        btnCerrar = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvAsientos).BeginInit();
        SuspendLayout();
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(560, 540);
        Text = "Asientos por zona";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        lblTitulo.Name = "lblTitulo";
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Asientos por zona";
        lblTitulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblTitulo.AutoSize = true;
        lblTitulo.Location = new Point(20, 10);
        lblEstadio.Name = "lblEstadio";
        lblEstadio.TabIndex = 1;
        lblEstadio.Text = "Estadio";
        lblEstadio.AutoSize = true;
        lblEstadio.Location = new Point(20, 63);
        cboEstadio.Name = "cboEstadio";
        cboEstadio.TabIndex = 2;
        cboEstadio.Location = new Point(110, 57);
        cboEstadio.Size = new Size(430, 23);
        cboEstadio.DropDownStyle = ComboBoxStyle.DropDownList;
        lblZona.Name = "lblZona";
        lblZona.TabIndex = 3;
        lblZona.Text = "Zona";
        lblZona.AutoSize = true;
        lblZona.Location = new Point(20, 103);
        cboZona.Name = "cboZona";
        cboZona.TabIndex = 4;
        cboZona.Location = new Point(110, 97);
        cboZona.Size = new Size(430, 23);
        cboZona.DropDownStyle = ComboBoxStyle.DropDownList;
        lblDetalleZona.Name = "lblDetalleZona";
        lblDetalleZona.TabIndex = 5;
        lblDetalleZona.Text = string.Empty;
        lblDetalleZona.AutoSize = true;
        lblDetalleZona.Location = new Point(20, 138);
        dgvAsientos.Name = "dgvAsientos";
        dgvAsientos.TabIndex = 6;
        dgvAsientos.Location = new Point(20, 180);
        dgvAsientos.Size = new Size(520, 295);
        dgvAsientos.AllowUserToAddRows = false;
        dgvAsientos.AllowUserToDeleteRows = false;
        dgvAsientos.ReadOnly = true;
        dgvAsientos.MultiSelect = false;
        dgvAsientos.RowHeadersVisible = false;
        dgvAsientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvAsientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvAsientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvAsientos.BackgroundColor = SystemColors.Window;
        colFila.HeaderText = "Fila";
        colFila.DataPropertyName = "Fila";
        colFila.Name = "col0";
        colFila.ReadOnly = true;
        colAsiento.HeaderText = "Asiento";
        colAsiento.DataPropertyName = "Numero";
        colAsiento.Name = "col1";
        colAsiento.ReadOnly = true;
        dgvAsientos.Columns.AddRange(new DataGridViewColumn[] { colFila, colAsiento });
        btnCerrar.Name = "btnCerrar";
        btnCerrar.TabIndex = 7;
        btnCerrar.Text = "Cerrar";
        btnCerrar.Location = new Point(420, 490);
        btnCerrar.Size = new Size(120, 32);
        btnCerrar.UseVisualStyleBackColor = true;
        Controls.Add(lblTitulo);
        Controls.Add(lblEstadio);
        Controls.Add(cboEstadio);
        Controls.Add(lblZona);
        Controls.Add(cboZona);
        Controls.Add(lblDetalleZona);
        Controls.Add(dgvAsientos);
        Controls.Add(btnCerrar);
        Name = "FrmZonasAsientos";
        cboEstadio.SelectedIndexChanged += cboEstadio_SelectedIndexChanged;
        cboZona.SelectedIndexChanged += cboZona_SelectedIndexChanged;
        btnCerrar.Click += btnCerrar_Click;
        Load += FrmZonasAsientos_Load;
        ((System.ComponentModel.ISupportInitialize)dgvAsientos).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
