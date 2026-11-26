namespace TemplateCSharp.Forms;

partial class FrmProductos
{
    private System.ComponentModel.IContainer components = null;

    private Label lblId;
    private Label lblNombre;
    private Label lblPrecio;

    private TextBox txtId;
    private TextBox txtNombre;

    private NumericUpDown nudPrecio;

    private Button btnNuevo;
    private Button btnGuardar;
    private Button btnActualizar;
    private Button btnEliminar;

    private DataGridView dgvProductos;

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
        lblId = new Label();
        lblNombre = new Label();
        lblPrecio = new Label();

        txtId = new TextBox();
        txtNombre = new TextBox();

        nudPrecio = new NumericUpDown();

        btnNuevo = new Button();
        btnGuardar = new Button();
        btnActualizar = new Button();
        btnEliminar = new Button();

        dgvProductos = new DataGridView();

        ((System.ComponentModel.ISupportInitialize)nudPrecio).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();

        SuspendLayout();

        // 
        // lblId
        // 
        lblId.AutoSize = true;
        lblId.Location = new Point(30, 30);
        lblId.Text = "ID:";

        // 
        // txtId
        // 
        txtId.Location = new Point(120, 27);
        txtId.ReadOnly = true;
        txtId.Size = new Size(100, 23);

        // 
        // lblNombre
        // 
        lblNombre.AutoSize = true;
        lblNombre.Location = new Point(30, 70);
        lblNombre.Text = "Nombre:";

        // 
        // txtNombre
        // 
        txtNombre.Location = new Point(120, 67);
        txtNombre.Size = new Size(250, 23);

        // 
        // lblPrecio
        // 
        lblPrecio.AutoSize = true;
        lblPrecio.Location = new Point(30, 110);
        lblPrecio.Text = "Precio:";

        // 
        // nudPrecio
        // 
        nudPrecio.DecimalPlaces = 2;
        nudPrecio.Maximum = 1000000;
        nudPrecio.Location = new Point(120, 107);
        nudPrecio.Size = new Size(120, 23);

        // 
        // btnNuevo
        // 
        btnNuevo.Location = new Point(400, 27);
        btnNuevo.Size = new Size(100, 35);
        btnNuevo.Text = "Nuevo";
        btnNuevo.UseVisualStyleBackColor = true;
        btnNuevo.Click += btnNuevo_Click;

        // 
        // btnGuardar
        // 
        btnGuardar.Location = new Point(400, 70);
        btnGuardar.Size = new Size(100, 35);
        btnGuardar.Text = "Guardar";
        btnGuardar.UseVisualStyleBackColor = true;
        btnGuardar.Click += btnGuardar_Click;

        // 
        // btnActualizar
        // 
        btnActualizar.Location = new Point(510, 70);
        btnActualizar.Size = new Size(100, 35);
        btnActualizar.Text = "Actualizar";
        btnActualizar.UseVisualStyleBackColor = true;
        btnActualizar.Click += btnActualizar_Click;

        // 
        // btnEliminar
        // 
        btnEliminar.Location = new Point(620, 70);
        btnEliminar.Size = new Size(100, 35);
        btnEliminar.Text = "Eliminar";
        btnEliminar.UseVisualStyleBackColor = true;
        btnEliminar.Click += btnEliminar_Click;

        // 
        // dgvProductos
        // 
        dgvProductos.AllowUserToAddRows = false;
        dgvProductos.AllowUserToDeleteRows = false;
        dgvProductos.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;
        dgvProductos.ColumnHeadersHeightSizeMode =
            DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvProductos.Location = new Point(30, 160);
        dgvProductos.MultiSelect = false;
        dgvProductos.ReadOnly = true;
        dgvProductos.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;
        dgvProductos.Size = new Size(690, 280);
        dgvProductos.CellClick += dgvProductos_CellClick;

        // 
        // FrmProductos
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(760, 480);

        Controls.Add(lblId);
        Controls.Add(txtId);

        Controls.Add(lblNombre);
        Controls.Add(txtNombre);

        Controls.Add(lblPrecio);
        Controls.Add(nudPrecio);

        Controls.Add(btnNuevo);
        Controls.Add(btnGuardar);
        Controls.Add(btnActualizar);
        Controls.Add(btnEliminar);

        Controls.Add(dgvProductos);

        Name = "FrmProductos";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Productos";

        Load += FrmProductos_Load;

        ((System.ComponentModel.ISupportInitialize)nudPrecio).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();

        ResumeLayout(false);
        PerformLayout();
    }
}
