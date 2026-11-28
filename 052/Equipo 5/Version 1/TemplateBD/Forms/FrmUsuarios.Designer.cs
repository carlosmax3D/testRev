namespace TemplateCSharp.Forms;

partial class FrmUsuarios
{
    private System.ComponentModel.IContainer components = null;

    private Label lblTitulo;
    private Label lblId;
    private TextBox txtId;
    private Label lblNombre;
    private TextBox txtNombre;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblNomina;
    private TextBox txtNomina;
    private Label lblContrasena;
    private TextBox txtContrasena;
    private Label lblRol;
    private ComboBox cmbRol;
    private Label lblFechaNac;
    private DateTimePicker dtpFechaNac;

    private Button btnNuevo;
    private Button btnGuardar;
    private Button btnActualizar;
    private Button btnEliminar;

    private DataGridView dgvUsuarios;

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
        lblEmail = new Label();
        txtEmail = new TextBox();
        lblNomina = new Label();
        txtNomina = new TextBox();
        lblContrasena = new Label();
        txtContrasena = new TextBox();
        lblRol = new Label();
        cmbRol = new ComboBox();
        lblFechaNac = new Label();
        dtpFechaNac = new DateTimePicker();

        btnNuevo = new Button();
        btnGuardar = new Button();
        btnActualizar = new Button();
        btnEliminar = new Button();

        dgvUsuarios = new DataGridView();
        ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
        SuspendLayout();

        // lblTitulo
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(25, 15);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(196, 25);
        lblTitulo.Text = "Gestión de Usuarios";

        // lblId / txtId
        lblId.AutoSize = true;
        lblId.Location = new Point(25, 55);
        lblId.Text = "ID:";
        txtId.Location = new Point(25, 75);
        txtId.Size = new Size(80, 23);
        txtId.ReadOnly = true;

        // lblNombre / txtNombre
        lblNombre.AutoSize = true;
        lblNombre.Location = new Point(120, 55);
        lblNombre.Text = "Nombre Completo:";
        txtNombre.Location = new Point(120, 75);
        txtNombre.Size = new Size(240, 23);

        // lblEmail / txtEmail
        lblEmail.AutoSize = true;
        lblEmail.Location = new Point(25, 115);
        lblEmail.Text = "Correo Electrónico:";
        txtEmail.Location = new Point(25, 135);
        txtEmail.Size = new Size(200, 23);

        // lblNomina / txtNomina
        lblNomina.AutoSize = true;
        lblNomina.Location = new Point(240, 115);
        lblNomina.Text = "Nº Nómina:";
        txtNomina.Location = new Point(240, 135);
        txtNomina.Size = new Size(120, 23);

        // lblContrasena / txtContrasena
        lblContrasena.AutoSize = true;
        lblContrasena.Location = new Point(25, 175);
        lblContrasena.Text = "Contraseña (dejar vacío si no cambia):";
        txtContrasena.Location = new Point(25, 195);
        txtContrasena.Size = new Size(200, 23);
        txtContrasena.UseSystemPasswordChar = true;

        // lblRol / cmbRol
        lblRol.AutoSize = true;
        lblRol.Location = new Point(240, 175);
        lblRol.Text = "Rol:";
        cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbRol.FormattingEnabled = true;
        cmbRol.Items.AddRange(new object[] { "Administrador", "Operativo" });
        cmbRol.Location = new Point(240, 195);
        cmbRol.Size = new Size(120, 23);

        // lblFechaNac / dtpFechaNac
        lblFechaNac.AutoSize = true;
        lblFechaNac.Location = new Point(25, 235);
        lblFechaNac.Text = "Fecha de Nacimiento:";
        dtpFechaNac.Format = DateTimePickerFormat.Short;
        dtpFechaNac.Location = new Point(25, 255);
        dtpFechaNac.Size = new Size(150, 23);

        // Botones
        btnNuevo.Location = new Point(25, 305);
        btnNuevo.Size = new Size(75, 32);
        btnNuevo.Text = "Nuevo";
        btnNuevo.UseVisualStyleBackColor = true;
        btnNuevo.Click += btnNuevo_Click;

        btnGuardar.Location = new Point(110, 305);
        btnGuardar.Size = new Size(75, 32);
        btnGuardar.Text = "Guardar";
        btnGuardar.UseVisualStyleBackColor = true;
        btnGuardar.Click += btnGuardar_Click;

        btnActualizar.Location = new Point(195, 305);
        btnActualizar.Size = new Size(80, 32);
        btnActualizar.Text = "Actualizar";
        btnActualizar.UseVisualStyleBackColor = true;
        btnActualizar.Click += btnActualizar_Click;

        btnEliminar.Location = new Point(285, 305);
        btnEliminar.Size = new Size(75, 32);
        btnEliminar.Text = "Eliminar";
        btnEliminar.UseVisualStyleBackColor = true;
        btnEliminar.Click += btnEliminar_Click;

        // DataGridView
        dgvUsuarios.AllowUserToAddRows = false;
        dgvUsuarios.AllowUserToDeleteRows = false;
        dgvUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvUsuarios.Location = new Point(380, 25);
        dgvUsuarios.MultiSelect = false;
        dgvUsuarios.ReadOnly = true;
        dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvUsuarios.Size = new Size(520, 325);
        dgvUsuarios.CellClick += dgvUsuarios_CellClick;

        // FrmUsuarios
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(920, 370);
        Controls.Add(dgvUsuarios);
        Controls.Add(btnEliminar);
        Controls.Add(btnActualizar);
        Controls.Add(btnGuardar);
        Controls.Add(btnNuevo);
        Controls.Add(dtpFechaNac);
        Controls.Add(lblFechaNac);
        Controls.Add(cmbRol);
        Controls.Add(lblRol);
        Controls.Add(txtContrasena);
        Controls.Add(lblContrasena);
        Controls.Add(txtNomina);
        Controls.Add(lblNomina);
        Controls.Add(txtEmail);
        Controls.Add(lblEmail);
        Controls.Add(txtNombre);
        Controls.Add(lblNombre);
        Controls.Add(txtId);
        Controls.Add(lblId);
        Controls.Add(lblTitulo);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Mantenimiento de Usuarios";
        Load += FrmUsuarios_Load;

        ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}