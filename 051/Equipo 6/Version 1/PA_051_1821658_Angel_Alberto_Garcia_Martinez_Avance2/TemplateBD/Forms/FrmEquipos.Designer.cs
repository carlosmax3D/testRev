#nullable disable
namespace TemplateCSharp.Forms;
partial class FrmEquipos
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitulo;
    private Label lblNombre;
    private TextBox txtNombre;
    private Button btnGuardar;
    private Label lblBuscar;
    private TextBox txtBuscar;
    private Button btnBuscar;
    private Button btnListarTodos;
    private DataGridView dgvEquipos;
    private DataGridViewTextBoxColumn colEquipo;
    private DataGridViewTextBoxColumn colRegistradoPor;
    private DataGridViewTextBoxColumn colFechaRegistro;
    private DataGridViewTextBoxColumn colEstado;
    private Button btnLimpiar;
    private Button btnModificar;
    private Button btnEliminar;
    private Button btnReactivar;
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
        lblNombre = new Label();
        txtNombre = new TextBox();
        btnGuardar = new Button();
        lblBuscar = new Label();
        txtBuscar = new TextBox();
        btnBuscar = new Button();
        btnListarTodos = new Button();
        dgvEquipos = new DataGridView();
        colEquipo = new DataGridViewTextBoxColumn();
        colRegistradoPor = new DataGridViewTextBoxColumn();
        colFechaRegistro = new DataGridViewTextBoxColumn();
        colEstado = new DataGridViewTextBoxColumn();
        btnLimpiar = new Button();
        btnModificar = new Button();
        btnEliminar = new Button();
        btnReactivar = new Button();
        btnCerrar = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvEquipos).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblTitulo.Location = new Point(26, 14);
        lblTitulo.Margin = new Padding(4, 0, 4, 0);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(118, 37);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Equipos";
        // 
        // lblNombre
        // 
        lblNombre.AutoSize = true;
        lblNombre.Location = new Point(26, 88);
        lblNombre.Margin = new Padding(4, 0, 4, 0);
        lblNombre.Name = "lblNombre";
        lblNombre.Size = new Size(145, 21);
        lblNombre.TabIndex = 1;
        lblNombre.Text = "Nombre del equipo";
        // 
        // txtNombre
        // 
        txtNombre.Location = new Point(231, 80);
        txtNombre.Margin = new Padding(4, 4, 4, 4);
        txtNombre.MaxLength = 80;
        txtNombre.Name = "txtNombre";
        txtNombre.Size = new Size(571, 29);
        txtNombre.TabIndex = 2;
        // 
        // btnGuardar
        // 
        btnGuardar.Location = new Point(823, 77);
        btnGuardar.Margin = new Padding(4, 4, 4, 4);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(167, 45);
        btnGuardar.TabIndex = 3;
        btnGuardar.Text = "Registrar";
        btnGuardar.UseVisualStyleBackColor = true;
        btnGuardar.Click += Guardar;
        // 
        // lblBuscar
        // 
        lblBuscar.AutoSize = true;
        lblBuscar.Location = new Point(26, 158);
        lblBuscar.Margin = new Padding(4, 0, 4, 0);
        lblBuscar.Name = "lblBuscar";
        lblBuscar.Size = new Size(143, 21);
        lblBuscar.TabIndex = 4;
        lblBuscar.Text = "Buscar por nombre";
        // 
        // txtBuscar
        // 
        txtBuscar.Location = new Point(231, 150);
        txtBuscar.Margin = new Padding(4, 4, 4, 4);
        txtBuscar.MaxLength = 80;
        txtBuscar.Name = "txtBuscar";
        txtBuscar.Size = new Size(385, 29);
        txtBuscar.TabIndex = 5;
        // 
        // btnBuscar
        // 
        btnBuscar.Location = new Point(630, 147);
        btnBuscar.Margin = new Padding(4, 4, 4, 4);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(167, 45);
        btnBuscar.TabIndex = 6;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = true;
        btnBuscar.Click += btnBuscar_Click;
        // 
        // btnListarTodos
        // 
        btnListarTodos.Location = new Point(823, 147);
        btnListarTodos.Margin = new Padding(4, 4, 4, 4);
        btnListarTodos.Name = "btnListarTodos";
        btnListarTodos.Size = new Size(167, 45);
        btnListarTodos.TabIndex = 7;
        btnListarTodos.Text = "Listar todos";
        btnListarTodos.UseVisualStyleBackColor = true;
        btnListarTodos.Click += btnListarTodos_Click;
        // 
        // dgvEquipos
        // 
        dgvEquipos.AllowUserToAddRows = false;
        dgvEquipos.AllowUserToDeleteRows = false;
        dgvEquipos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvEquipos.BackgroundColor = SystemColors.Window;
        dgvEquipos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvEquipos.Columns.AddRange(new DataGridViewColumn[] { colEquipo, colEstado, colRegistradoPor, colFechaRegistro });
        dgvEquipos.Location = new Point(26, 224);
        dgvEquipos.Margin = new Padding(4, 4, 4, 4);
        dgvEquipos.MultiSelect = false;
        dgvEquipos.Name = "dgvEquipos";
        dgvEquipos.ReadOnly = true;
        dgvEquipos.RowHeadersVisible = false;
        dgvEquipos.RowHeadersWidth = 53;
        dgvEquipos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvEquipos.Size = new Size(964, 322);
        dgvEquipos.TabIndex = 8;
        dgvEquipos.CellClick += dgvEquipos_CellClick;
        // 
        // colEquipo
        // 
        colEquipo.DataPropertyName = "Nombre";
        colEquipo.HeaderText = "Equipo";
        colEquipo.MinimumWidth = 7;
        colEquipo.Name = "colEquipo";
        colEquipo.ReadOnly = true;
        // 
        // colRegistradoPor
        // 
        colRegistradoPor.DataPropertyName = "RegistradoPor";
        colRegistradoPor.HeaderText = "Registrado por";
        colRegistradoPor.MinimumWidth = 7;
        colRegistradoPor.Name = "colRegistradoPor";
        colRegistradoPor.ReadOnly = true;
        // 
        // colFechaRegistro
        // 
        colFechaRegistro.DataPropertyName = "FechaRegistroTexto";
        colFechaRegistro.HeaderText = "Fecha de registro";
        colFechaRegistro.MinimumWidth = 7;
        colFechaRegistro.Name = "colFechaRegistro";
        colFechaRegistro.ReadOnly = true;
        // 
        // colEstado
        // 
        colEstado.DataPropertyName = "EstadoTexto";
        colEstado.HeaderText = "Estado";
        colEstado.MinimumWidth = 7;
        colEstado.Name = "colEstado";
        colEstado.ReadOnly = true;
        // 
        // btnLimpiar
        // 
        btnLimpiar.Location = new Point(26, 574);
        btnLimpiar.Margin = new Padding(4, 4, 4, 4);
        btnLimpiar.Name = "btnLimpiar";
        btnLimpiar.Size = new Size(167, 45);
        btnLimpiar.TabIndex = 9;
        btnLimpiar.Text = "Limpiar";
        btnLimpiar.UseVisualStyleBackColor = true;
        btnLimpiar.Click += btnLimpiar_Click;
        // 
        // btnModificar
        // 
        btnModificar.Location = new Point(231, 574);
        btnModificar.Margin = new Padding(4, 4, 4, 4);
        btnModificar.Name = "btnModificar";
        btnModificar.Size = new Size(167, 45);
        btnModificar.TabIndex = 10;
        btnModificar.Text = "Modificar";
        btnModificar.UseVisualStyleBackColor = true;
        btnModificar.Click += Modificar;
        // 
        // btnEliminar
        // 
        btnEliminar.Location = new Point(424, 574);
        btnEliminar.Margin = new Padding(4, 4, 4, 4);
        btnEliminar.Name = "btnEliminar";
        btnEliminar.Size = new Size(167, 45);
        btnEliminar.TabIndex = 11;
        btnEliminar.Text = "Eliminar";
        btnEliminar.UseVisualStyleBackColor = true;
        btnEliminar.Click += Eliminar;
        // 
        // btnReactivar
        // 
        btnReactivar.Location = new Point(617, 574);
        btnReactivar.Margin = new Padding(4, 4, 4, 4);
        btnReactivar.Name = "btnReactivar";
        btnReactivar.Size = new Size(167, 45);
        btnReactivar.TabIndex = 12;
        btnReactivar.Text = "Reactivar";
        btnReactivar.UseVisualStyleBackColor = true;
        btnReactivar.Click += Reactivar;
        // 
        // btnCerrar
        // 
        btnCerrar.Location = new Point(823, 574);
        btnCerrar.Margin = new Padding(4, 4, 4, 4);
        btnCerrar.Name = "btnCerrar";
        btnCerrar.Size = new Size(167, 45);
        btnCerrar.TabIndex = 13;
        btnCerrar.Text = "Cerrar";
        btnCerrar.UseVisualStyleBackColor = true;
        btnCerrar.Click += btnCerrar_Click;
        // 
        // FrmEquipos
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1029, 658);
        Controls.Add(lblTitulo);
        Controls.Add(lblNombre);
        Controls.Add(txtNombre);
        Controls.Add(btnGuardar);
        Controls.Add(lblBuscar);
        Controls.Add(txtBuscar);
        Controls.Add(btnBuscar);
        Controls.Add(btnListarTodos);
        Controls.Add(dgvEquipos);
        Controls.Add(btnLimpiar);
        Controls.Add(btnModificar);
        Controls.Add(btnEliminar);
        Controls.Add(btnReactivar);
        Controls.Add(btnCerrar);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 4, 4, 4);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmEquipos";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Catálogo de equipos";
        Load += FrmEquipos_Load;
        ((System.ComponentModel.ISupportInitialize)dgvEquipos).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
