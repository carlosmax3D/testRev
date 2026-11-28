namespace TemplateCSharp.Forms;

partial class FrmClientes
{
    private System.ComponentModel.IContainer components = null;

    private Label lblTitulo;
    private Label lblId;
    private TextBox txtId;
    private Label lblNombre;
    private TextBox txtNombre;
    private Label lblApellidos;
    private TextBox txtApellidos;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblTelefono;
    private TextBox txtTelefono;
    private Label lblRfc;
    private TextBox txtRfc;

    private Label lblBuscar;
    private TextBox txtBuscarApellido;
    private Button btnBuscar;
    private Button btnRestablecer;

    private Button btnNuevo;
    private Button btnGuardar;
    private Button btnActualizar;
    private Button btnEliminar;

    private DataGridView dgvClientes;

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
        lblTitulo = new Label();
        lblId = new Label();
        txtId = new TextBox();
        lblNombre = new Label();
        txtNombre = new TextBox();
        lblApellidos = new Label();
        txtApellidos = new TextBox();
        lblEmail = new Label();
        txtEmail = new TextBox();
        lblTelefono = new Label();
        txtTelefono = new TextBox();
        lblRfc = new Label();
        txtRfc = new TextBox();
        lblBuscar = new Label();
        txtBuscarApellido = new TextBox();
        btnBuscar = new Button();
        btnRestablecer = new Button();
        btnNuevo = new Button();
        btnGuardar = new Button();
        btnActualizar = new Button();
        btnEliminar = new Button();
        dgvClientes = new DataGridView();
        label1 = new Label();
        txtCURP = new TextBox();
        ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(29, 20);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(233, 32);
        lblTitulo.TabIndex = 21;
        lblTitulo.Text = "Gestión de Clientes";
        // 
        // lblId
        // 
        lblId.AutoSize = true;
        lblId.Location = new Point(29, 73);
        lblId.Name = "lblId";
        lblId.Size = new Size(27, 20);
        lblId.TabIndex = 20;
        lblId.Text = "ID:";
        // 
        // txtId
        // 
        txtId.Location = new Point(29, 100);
        txtId.Margin = new Padding(3, 4, 3, 4);
        txtId.Name = "txtId";
        txtId.ReadOnly = true;
        txtId.Size = new Size(91, 27);
        txtId.TabIndex = 19;
        // 
        // lblNombre
        // 
        lblNombre.AutoSize = true;
        lblNombre.Location = new Point(137, 73);
        lblNombre.Name = "lblNombre";
        lblNombre.Size = new Size(83, 20);
        lblNombre.TabIndex = 18;
        lblNombre.Text = "Nombre(s):";
        // 
        // txtNombre
        // 
        txtNombre.Location = new Point(137, 100);
        txtNombre.Margin = new Padding(3, 4, 3, 4);
        txtNombre.Name = "txtNombre";
        txtNombre.Size = new Size(148, 27);
        txtNombre.TabIndex = 17;
        // 
        // lblApellidos
        // 
        lblApellidos.AutoSize = true;
        lblApellidos.Location = new Point(303, 73);
        lblApellidos.Name = "lblApellidos";
        lblApellidos.Size = new Size(75, 20);
        lblApellidos.TabIndex = 16;
        lblApellidos.Text = "Apellidos:";
        // 
        // txtApellidos
        // 
        txtApellidos.Location = new Point(303, 100);
        txtApellidos.Margin = new Padding(3, 4, 3, 4);
        txtApellidos.Name = "txtApellidos";
        txtApellidos.Size = new Size(148, 27);
        txtApellidos.TabIndex = 15;
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.Location = new Point(29, 153);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(135, 20);
        lblEmail.TabIndex = 14;
        lblEmail.Text = "Correo Electrónico:";
        // 
        // txtEmail
        // 
        txtEmail.Location = new Point(29, 180);
        txtEmail.Margin = new Padding(3, 4, 3, 4);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(257, 27);
        txtEmail.TabIndex = 13;
        // 
        // lblTelefono
        // 
        lblTelefono.AutoSize = true;
        lblTelefono.Location = new Point(303, 153);
        lblTelefono.Name = "lblTelefono";
        lblTelefono.Size = new Size(70, 20);
        lblTelefono.TabIndex = 12;
        lblTelefono.Text = "Teléfono:";
        // 
        // txtTelefono
        // 
        txtTelefono.Location = new Point(303, 180);
        txtTelefono.Margin = new Padding(3, 4, 3, 4);
        txtTelefono.Name = "txtTelefono";
        txtTelefono.Size = new Size(148, 27);
        txtTelefono.TabIndex = 11;
        // 
        // lblRfc
        // 
        lblRfc.AutoSize = true;
        lblRfc.Location = new Point(29, 233);
        lblRfc.Name = "lblRfc";
        lblRfc.Size = new Size(109, 20);
        lblRfc.TabIndex = 10;
        lblRfc.Text = "RFC (opcional):";
        // 
        // txtRfc
        // 
        txtRfc.Location = new Point(29, 260);
        txtRfc.Margin = new Padding(3, 4, 3, 4);
        txtRfc.Name = "txtRfc";
        txtRfc.Size = new Size(171, 27);
        txtRfc.TabIndex = 9;
        // 
        // lblBuscar
        // 
        lblBuscar.AutoSize = true;
        lblBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblBuscar.Location = new Point(480, 27);
        lblBuscar.Name = "lblBuscar";
        lblBuscar.Size = new Size(150, 20);
        lblBuscar.TabIndex = 4;
        lblBuscar.Text = "Buscar por Apellido:";
        // 
        // txtBuscarApellido
        // 
        txtBuscarApellido.Location = new Point(630, 24);
        txtBuscarApellido.Margin = new Padding(3, 4, 3, 4);
        txtBuscarApellido.Name = "txtBuscarApellido";
        txtBuscarApellido.Size = new Size(205, 27);
        txtBuscarApellido.TabIndex = 3;
        // 
        // btnBuscar
        // 
        btnBuscar.Location = new Point(848, 20);
        btnBuscar.Margin = new Padding(3, 4, 3, 4);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(86, 36);
        btnBuscar.TabIndex = 2;
        btnBuscar.Text = "Buscar";
        btnBuscar.Click += btnBuscar_Click;
        // 
        // btnRestablecer
        // 
        btnRestablecer.Location = new Point(944, 20);
        btnRestablecer.Margin = new Padding(3, 4, 3, 4);
        btnRestablecer.Name = "btnRestablecer";
        btnRestablecer.Size = new Size(97, 36);
        btnRestablecer.TabIndex = 1;
        btnRestablecer.Text = "Ver Todos";
        btnRestablecer.Click += btnRestablecer_Click;
        // 
        // btnNuevo
        // 
        btnNuevo.Location = new Point(29, 333);
        btnNuevo.Margin = new Padding(3, 4, 3, 4);
        btnNuevo.Name = "btnNuevo";
        btnNuevo.Size = new Size(91, 43);
        btnNuevo.TabIndex = 8;
        btnNuevo.Text = "Nuevo";
        btnNuevo.Click += btnNuevo_Click;
        // 
        // btnGuardar
        // 
        btnGuardar.Location = new Point(131, 333);
        btnGuardar.Margin = new Padding(3, 4, 3, 4);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(91, 43);
        btnGuardar.TabIndex = 7;
        btnGuardar.Text = "Guardar";
        btnGuardar.Click += btnGuardar_Click;
        // 
        // btnActualizar
        // 
        btnActualizar.Location = new Point(234, 333);
        btnActualizar.Margin = new Padding(3, 4, 3, 4);
        btnActualizar.Name = "btnActualizar";
        btnActualizar.Size = new Size(103, 43);
        btnActualizar.TabIndex = 6;
        btnActualizar.Text = "Actualizar";
        btnActualizar.Click += btnActualizar_Click;
        // 
        // btnEliminar
        // 
        btnEliminar.Location = new Point(349, 333);
        btnEliminar.Margin = new Padding(3, 4, 3, 4);
        btnEliminar.Name = "btnEliminar";
        btnEliminar.Size = new Size(91, 43);
        btnEliminar.TabIndex = 5;
        btnEliminar.Text = "Eliminar";
        btnEliminar.Click += btnEliminar_Click;
        // 
        // dgvClientes
        // 
        dgvClientes.AllowUserToAddRows = false;
        dgvClientes.AllowUserToDeleteRows = false;
        dgvClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvClientes.Location = new Point(480, 73);
        dgvClientes.Margin = new Padding(3, 4, 3, 4);
        dgvClientes.MultiSelect = false;
        dgvClientes.Name = "dgvClientes";
        dgvClientes.ReadOnly = true;
        dgvClientes.RowHeadersWidth = 51;
        dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvClientes.Size = new Size(560, 387);
        dgvClientes.TabIndex = 0;
        dgvClientes.CellClick += dgvClientes_CellClick;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(257, 233);
        label1.Name = "label1";
        label1.Size = new Size(48, 20);
        label1.TabIndex = 22;
        label1.Text = "CURP:";
        label1.Click += label1_Click;
        // 
        // txtCURP
        // 
        txtCURP.Location = new Point(257, 260);
        txtCURP.Name = "txtCURP";
        txtCURP.Size = new Size(155, 27);
        txtCURP.TabIndex = 23;
        // 
        // FrmClientes
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1069, 487);
        Controls.Add(txtCURP);
        Controls.Add(label1);
        Controls.Add(dgvClientes);
        Controls.Add(btnRestablecer);
        Controls.Add(btnBuscar);
        Controls.Add(txtBuscarApellido);
        Controls.Add(lblBuscar);
        Controls.Add(btnEliminar);
        Controls.Add(btnActualizar);
        Controls.Add(btnGuardar);
        Controls.Add(btnNuevo);
        Controls.Add(txtRfc);
        Controls.Add(lblRfc);
        Controls.Add(txtTelefono);
        Controls.Add(lblTelefono);
        Controls.Add(txtEmail);
        Controls.Add(lblEmail);
        Controls.Add(txtApellidos);
        Controls.Add(lblApellidos);
        Controls.Add(txtNombre);
        Controls.Add(lblNombre);
        Controls.Add(txtId);
        Controls.Add(lblId);
        Controls.Add(lblTitulo);
        Margin = new Padding(3, 4, 3, 4);
        Name = "FrmClientes";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Mantenimiento de Clientes";
        Load += FrmClientes_Load;
        ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label label1;
    private TextBox txtCURP;
}