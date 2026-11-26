#nullable disable
namespace TemplateCSharp.Forms;
partial class FrmClientes
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitulo;
    private GroupBox grpDatos;
    private Label lblNombre;
    private TextBox txtNombre;
    private Label lblPaterno;
    private TextBox txtPaterno;
    private Label lblMaterno;
    private TextBox txtMaterno;
    private Label lblCalle;
    private TextBox txtCalle;
    private Label lblNumExterior;
    private TextBox txtNumExterior;
    private Label lblColonia;
    private TextBox txtColonia;
    private Label lblCodigoPostal;
    private TextBox txtCodigoPostal;
    private Label lblCiudad;
    private TextBox txtCiudad;
    private Label lblEstado;
    private TextBox txtEstado;
    private Label lblRfc;
    private TextBox txtRfc;
    private Label lblCorreo;
    private TextBox txtCorreo;
    private Label lblTelCasa;
    private TextBox txtTelCasa;
    private Label lblTelCelular;
    private TextBox txtTelCelular;
    private Label lblNacimiento;
    private DateTimePicker dtpNacimiento;
    private Label lblBuscar;
    private TextBox txtBuscar;
    private Button btnBuscar;
    private DataGridView dgvClientes;
    private DataGridViewTextBoxColumn colNombre;
    private DataGridViewTextBoxColumn colRfc;
    private DataGridViewTextBoxColumn colCorreo;
    private DataGridViewTextBoxColumn colRegistro;
    private Label lblAuditoria;
    private Button btnRegistrar;
    private Button btnActualizar;
    private Button btnDarDeBaja;
    private Button btnReactivar;
    private CheckBox chkInactivos;
    private DataGridViewTextBoxColumn colEstado;
    private Button btnLimpiar;
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
        grpDatos = new GroupBox();
        lblNombre = new Label();
        txtNombre = new TextBox();
        lblPaterno = new Label();
        txtPaterno = new TextBox();
        lblMaterno = new Label();
        txtMaterno = new TextBox();
        lblCalle = new Label();
        txtCalle = new TextBox();
        lblNumExterior = new Label();
        txtNumExterior = new TextBox();
        lblColonia = new Label();
        txtColonia = new TextBox();
        lblCodigoPostal = new Label();
        txtCodigoPostal = new TextBox();
        lblCiudad = new Label();
        txtCiudad = new TextBox();
        lblEstado = new Label();
        txtEstado = new TextBox();
        lblRfc = new Label();
        txtRfc = new TextBox();
        lblCorreo = new Label();
        txtCorreo = new TextBox();
        lblTelCasa = new Label();
        txtTelCasa = new TextBox();
        lblTelCelular = new Label();
        txtTelCelular = new TextBox();
        lblNacimiento = new Label();
        dtpNacimiento = new DateTimePicker();
        lblBuscar = new Label();
        txtBuscar = new TextBox();
        btnBuscar = new Button();
        dgvClientes = new DataGridView();
        colNombre = new DataGridViewTextBoxColumn();
        colRfc = new DataGridViewTextBoxColumn();
        colCorreo = new DataGridViewTextBoxColumn();
        colRegistro = new DataGridViewTextBoxColumn();
        lblAuditoria = new Label();
        btnRegistrar = new Button();
        btnActualizar = new Button();
        btnDarDeBaja = new Button();
        btnReactivar = new Button();
        chkInactivos = new CheckBox();
        colEstado = new DataGridViewTextBoxColumn();
        btnLimpiar = new Button();
        btnCerrar = new Button();
        grpDatos.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblTitulo.Location = new Point(26, 14);
        lblTitulo.Margin = new Padding(4, 0, 4, 0);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(119, 37);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Clientes";
        // 
        // grpDatos
        // 
        grpDatos.Controls.Add(lblNombre);
        grpDatos.Controls.Add(txtNombre);
        grpDatos.Controls.Add(lblPaterno);
        grpDatos.Controls.Add(txtPaterno);
        grpDatos.Controls.Add(lblMaterno);
        grpDatos.Controls.Add(txtMaterno);
        grpDatos.Controls.Add(lblCalle);
        grpDatos.Controls.Add(txtCalle);
        grpDatos.Controls.Add(lblNumExterior);
        grpDatos.Controls.Add(txtNumExterior);
        grpDatos.Controls.Add(lblColonia);
        grpDatos.Controls.Add(txtColonia);
        grpDatos.Controls.Add(lblCodigoPostal);
        grpDatos.Controls.Add(txtCodigoPostal);
        grpDatos.Controls.Add(lblCiudad);
        grpDatos.Controls.Add(txtCiudad);
        grpDatos.Controls.Add(lblEstado);
        grpDatos.Controls.Add(txtEstado);
        grpDatos.Controls.Add(lblRfc);
        grpDatos.Controls.Add(txtRfc);
        grpDatos.Controls.Add(lblCorreo);
        grpDatos.Controls.Add(txtCorreo);
        grpDatos.Controls.Add(lblTelCasa);
        grpDatos.Controls.Add(txtTelCasa);
        grpDatos.Controls.Add(lblTelCelular);
        grpDatos.Controls.Add(txtTelCelular);
        grpDatos.Controls.Add(lblNacimiento);
        grpDatos.Controls.Add(dtpNacimiento);
        grpDatos.Location = new Point(26, 77);
        grpDatos.Margin = new Padding(4, 4, 4, 4);
        grpDatos.Name = "grpDatos";
        grpDatos.Padding = new Padding(4, 4, 4, 4);
        grpDatos.Size = new Size(643, 700);
        grpDatos.TabIndex = 1;
        grpDatos.TabStop = false;
        grpDatos.Text = "Datos del cliente";
        // 
        // lblNombre
        // 
        lblNombre.AutoSize = true;
        lblNombre.Location = new Point(19, 43);
        lblNombre.Margin = new Padding(4, 0, 4, 0);
        lblNombre.Name = "lblNombre";
        lblNombre.Size = new Size(85, 21);
        lblNombre.TabIndex = 0;
        lblNombre.Text = "Nombre(s)";
        // 
        // txtNombre
        // 
        txtNombre.Location = new Point(283, 35);
        txtNombre.Margin = new Padding(4, 4, 4, 4);
        txtNombre.MaxLength = 60;
        txtNombre.Name = "txtNombre";
        txtNombre.Size = new Size(333, 29);
        txtNombre.TabIndex = 1;
        // 
        // lblPaterno
        // 
        lblPaterno.AutoSize = true;
        lblPaterno.Location = new Point(19, 87);
        lblPaterno.Margin = new Padding(4, 0, 4, 0);
        lblPaterno.Name = "lblPaterno";
        lblPaterno.Size = new Size(125, 21);
        lblPaterno.TabIndex = 2;
        lblPaterno.Text = "Apellido paterno";
        // 
        // txtPaterno
        // 
        txtPaterno.Location = new Point(283, 78);
        txtPaterno.Margin = new Padding(4, 4, 4, 4);
        txtPaterno.MaxLength = 40;
        txtPaterno.Name = "txtPaterno";
        txtPaterno.Size = new Size(333, 29);
        txtPaterno.TabIndex = 3;
        // 
        // lblMaterno
        // 
        lblMaterno.AutoSize = true;
        lblMaterno.Location = new Point(19, 130);
        lblMaterno.Margin = new Padding(4, 0, 4, 0);
        lblMaterno.Name = "lblMaterno";
        lblMaterno.Size = new Size(130, 21);
        lblMaterno.TabIndex = 4;
        lblMaterno.Text = "Apellido materno";
        // 
        // txtMaterno
        // 
        txtMaterno.Location = new Point(283, 122);
        txtMaterno.Margin = new Padding(4, 4, 4, 4);
        txtMaterno.MaxLength = 40;
        txtMaterno.Name = "txtMaterno";
        txtMaterno.Size = new Size(333, 29);
        txtMaterno.TabIndex = 5;
        // 
        // lblCalle
        // 
        lblCalle.AutoSize = true;
        lblCalle.Location = new Point(19, 174);
        lblCalle.Margin = new Padding(4, 0, 4, 0);
        lblCalle.Name = "lblCalle";
        lblCalle.Size = new Size(44, 21);
        lblCalle.TabIndex = 6;
        lblCalle.Text = "Calle";
        // 
        // txtCalle
        // 
        txtCalle.Location = new Point(283, 165);
        txtCalle.Margin = new Padding(4, 4, 4, 4);
        txtCalle.MaxLength = 80;
        txtCalle.Name = "txtCalle";
        txtCalle.Size = new Size(333, 29);
        txtCalle.TabIndex = 7;
        // 
        // lblNumExterior
        // 
        lblNumExterior.AutoSize = true;
        lblNumExterior.Location = new Point(19, 217);
        lblNumExterior.Margin = new Padding(4, 0, 4, 0);
        lblNumExterior.Name = "lblNumExterior";
        lblNumExterior.Size = new Size(125, 21);
        lblNumExterior.TabIndex = 8;
        lblNumExterior.Text = "Número exterior";
        // 
        // txtNumExterior
        // 
        txtNumExterior.Location = new Point(283, 209);
        txtNumExterior.Margin = new Padding(4, 4, 4, 4);
        txtNumExterior.MaxLength = 10;
        txtNumExterior.Name = "txtNumExterior";
        txtNumExterior.Size = new Size(333, 29);
        txtNumExterior.TabIndex = 9;
        // 
        // lblColonia
        // 
        lblColonia.AutoSize = true;
        lblColonia.Location = new Point(19, 260);
        lblColonia.Margin = new Padding(4, 0, 4, 0);
        lblColonia.Name = "lblColonia";
        lblColonia.Size = new Size(63, 21);
        lblColonia.TabIndex = 10;
        lblColonia.Text = "Colonia";
        // 
        // txtColonia
        // 
        txtColonia.Location = new Point(283, 252);
        txtColonia.Margin = new Padding(4, 4, 4, 4);
        txtColonia.MaxLength = 80;
        txtColonia.Name = "txtColonia";
        txtColonia.Size = new Size(333, 29);
        txtColonia.TabIndex = 11;
        // 
        // lblCodigoPostal
        // 
        lblCodigoPostal.AutoSize = true;
        lblCodigoPostal.Location = new Point(19, 304);
        lblCodigoPostal.Margin = new Padding(4, 0, 4, 0);
        lblCodigoPostal.Name = "lblCodigoPostal";
        lblCodigoPostal.Size = new Size(106, 21);
        lblCodigoPostal.TabIndex = 12;
        lblCodigoPostal.Text = "Código postal";
        // 
        // txtCodigoPostal
        // 
        txtCodigoPostal.Location = new Point(283, 295);
        txtCodigoPostal.Margin = new Padding(4, 4, 4, 4);
        txtCodigoPostal.MaxLength = 5;
        txtCodigoPostal.Name = "txtCodigoPostal";
        txtCodigoPostal.Size = new Size(333, 29);
        txtCodigoPostal.TabIndex = 13;
        txtCodigoPostal.TextChanged += txtCodigoPostal_TextChanged;
        // 
        // lblCiudad
        // 
        lblCiudad.AutoSize = true;
        lblCiudad.Location = new Point(19, 347);
        lblCiudad.Margin = new Padding(4, 0, 4, 0);
        lblCiudad.Name = "lblCiudad";
        lblCiudad.Size = new Size(212, 21);
        lblCiudad.TabIndex = 14;
        lblCiudad.Text = "Ciudad (según código postal)";
        // 
        // txtCiudad
        // 
        txtCiudad.Location = new Point(283, 339);
        txtCiudad.Margin = new Padding(4, 4, 4, 4);
        txtCiudad.MaxLength = 60;
        txtCiudad.Name = "txtCiudad";
        txtCiudad.Size = new Size(333, 29);
        txtCiudad.TabIndex = 15;
        // 
        // lblEstado
        // 
        lblEstado.AutoSize = true;
        lblEstado.Location = new Point(19, 391);
        lblEstado.Margin = new Padding(4, 0, 4, 0);
        lblEstado.Name = "lblEstado";
        lblEstado.Size = new Size(209, 21);
        lblEstado.TabIndex = 16;
        lblEstado.Text = "Estado (según código postal)";
        // 
        // txtEstado
        // 
        txtEstado.Location = new Point(283, 382);
        txtEstado.Margin = new Padding(4, 4, 4, 4);
        txtEstado.MaxLength = 50;
        txtEstado.Name = "txtEstado";
        txtEstado.Size = new Size(333, 29);
        txtEstado.TabIndex = 17;
        // 
        // lblRfc
        // 
        lblRfc.AutoSize = true;
        lblRfc.Location = new Point(19, 434);
        lblRfc.Margin = new Padding(4, 0, 4, 0);
        lblRfc.Name = "lblRfc";
        lblRfc.Size = new Size(38, 21);
        lblRfc.TabIndex = 18;
        lblRfc.Text = "RFC";
        // 
        // txtRfc
        // 
        txtRfc.Location = new Point(283, 426);
        txtRfc.Margin = new Padding(4, 4, 4, 4);
        txtRfc.MaxLength = 13;
        txtRfc.Name = "txtRfc";
        txtRfc.Size = new Size(333, 29);
        txtRfc.TabIndex = 19;
        // 
        // lblCorreo
        // 
        lblCorreo.AutoSize = true;
        lblCorreo.Location = new Point(19, 477);
        lblCorreo.Margin = new Padding(4, 0, 4, 0);
        lblCorreo.Name = "lblCorreo";
        lblCorreo.Size = new Size(138, 21);
        lblCorreo.TabIndex = 20;
        lblCorreo.Text = "Correo electrónico";
        // 
        // txtCorreo
        // 
        txtCorreo.Location = new Point(283, 469);
        txtCorreo.Margin = new Padding(4, 4, 4, 4);
        txtCorreo.MaxLength = 100;
        txtCorreo.Name = "txtCorreo";
        txtCorreo.Size = new Size(333, 29);
        txtCorreo.TabIndex = 21;
        // 
        // lblTelCasa
        // 
        lblTelCasa.AutoSize = true;
        lblTelCasa.Location = new Point(19, 521);
        lblTelCasa.Margin = new Padding(4, 0, 4, 0);
        lblTelCasa.Name = "lblTelCasa";
        lblTelCasa.Size = new Size(123, 21);
        lblTelCasa.TabIndex = 22;
        lblTelCasa.Text = "Teléfono de casa";
        // 
        // txtTelCasa
        // 
        txtTelCasa.Location = new Point(283, 512);
        txtTelCasa.Margin = new Padding(4, 4, 4, 4);
        txtTelCasa.MaxLength = 10;
        txtTelCasa.Name = "txtTelCasa";
        txtTelCasa.Size = new Size(333, 29);
        txtTelCasa.TabIndex = 23;
        // 
        // lblTelCelular
        // 
        lblTelCelular.AutoSize = true;
        lblTelCelular.Location = new Point(19, 564);
        lblTelCelular.Margin = new Padding(4, 0, 4, 0);
        lblTelCelular.Name = "lblTelCelular";
        lblTelCelular.Size = new Size(118, 21);
        lblTelCelular.TabIndex = 24;
        lblTelCelular.Text = "Teléfono celular";
        // 
        // txtTelCelular
        // 
        txtTelCelular.Location = new Point(283, 556);
        txtTelCelular.Margin = new Padding(4, 4, 4, 4);
        txtTelCelular.MaxLength = 10;
        txtTelCelular.Name = "txtTelCelular";
        txtTelCelular.Size = new Size(333, 29);
        txtTelCelular.TabIndex = 25;
        // 
        // lblNacimiento
        // 
        lblNacimiento.AutoSize = true;
        lblNacimiento.Location = new Point(19, 608);
        lblNacimiento.Margin = new Padding(4, 0, 4, 0);
        lblNacimiento.Name = "lblNacimiento";
        lblNacimiento.Size = new Size(152, 21);
        lblNacimiento.TabIndex = 26;
        lblNacimiento.Text = "Fecha de nacimiento";
        // 
        // dtpNacimiento
        // 
        dtpNacimiento.CustomFormat = "dd/MMM/yyyy";
        dtpNacimiento.Format = DateTimePickerFormat.Custom;
        dtpNacimiento.Location = new Point(283, 599);
        dtpNacimiento.Margin = new Padding(4, 4, 4, 4);
        dtpNacimiento.Name = "dtpNacimiento";
        dtpNacimiento.Size = new Size(333, 29);
        dtpNacimiento.TabIndex = 27;
        dtpNacimiento.Value = new DateTime(1990, 1, 1, 0, 0, 0, 0);
        // 
        // lblBuscar
        // 
        lblBuscar.AutoSize = true;
        lblBuscar.Location = new Point(694, 85);
        lblBuscar.Margin = new Padding(4, 0, 4, 0);
        lblBuscar.Name = "lblBuscar";
        lblBuscar.Size = new Size(212, 21);
        lblBuscar.TabIndex = 2;
        lblBuscar.Text = "Buscar por apellidos o correo";
        // 
        // txtBuscar
        // 
        txtBuscar.Location = new Point(694, 115);
        txtBuscar.Margin = new Padding(4, 4, 4, 4);
        txtBuscar.MaxLength = 100;
        txtBuscar.Name = "txtBuscar";
        txtBuscar.Size = new Size(307, 29);
        txtBuscar.TabIndex = 3;
        // 
        // btnBuscar
        // 
        btnBuscar.Location = new Point(1016, 112);
        btnBuscar.Margin = new Padding(4, 4, 4, 4);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(116, 45);
        btnBuscar.TabIndex = 4;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = true;
        btnBuscar.Click += btnBuscar_Click;
        // 
        // dgvClientes
        // 
        dgvClientes.AllowUserToAddRows = false;
        dgvClientes.AllowUserToDeleteRows = false;
        dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvClientes.BackgroundColor = SystemColors.Window;
        dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        colEstado.DataPropertyName = "EstadoTexto";
        colEstado.HeaderText = "Estado";
        colEstado.MinimumWidth = 7;
        colEstado.Name = "colEstado";
        colEstado.ReadOnly = true;
        dgvClientes.Columns.AddRange(new DataGridViewColumn[] { colNombre, colRfc, colCorreo, colEstado, colRegistro });
        dgvClientes.Location = new Point(694, 184);
        dgvClientes.Margin = new Padding(4, 4, 4, 4);
        dgvClientes.MultiSelect = false;
        dgvClientes.Name = "dgvClientes";
        dgvClientes.ReadOnly = true;
        dgvClientes.RowHeadersVisible = false;
        dgvClientes.RowHeadersWidth = 53;
        dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvClientes.Size = new Size(437, 384);
        dgvClientes.TabIndex = 5;
        dgvClientes.CellClick += dgvClientes_CellClick;
        // 
        // colNombre
        // 
        colNombre.DataPropertyName = "NombreParaMostrar";
        colNombre.HeaderText = "Nombre";
        colNombre.MinimumWidth = 7;
        colNombre.Name = "colNombre";
        colNombre.ReadOnly = true;
        // 
        // colRfc
        // 
        colRfc.DataPropertyName = "RFC";
        colRfc.HeaderText = "RFC";
        colRfc.MinimumWidth = 7;
        colRfc.Name = "colRfc";
        colRfc.ReadOnly = true;
        // 
        // colCorreo
        // 
        colCorreo.DataPropertyName = "Correo";
        colCorreo.HeaderText = "Correo";
        colCorreo.MinimumWidth = 7;
        colCorreo.Name = "colCorreo";
        colCorreo.ReadOnly = true;
        // 
        // colRegistro
        // 
        colRegistro.DataPropertyName = "FechaRegistroTexto";
        colRegistro.HeaderText = "Fecha de registro";
        colRegistro.MinimumWidth = 7;
        colRegistro.Name = "colRegistro";
        colRegistro.ReadOnly = true;
        // 
        // lblAuditoria
        // 
        lblAuditoria.Location = new Point(694, 576);
        lblAuditoria.Margin = new Padding(4, 0, 4, 0);
        lblAuditoria.Name = "lblAuditoria";
        lblAuditoria.Size = new Size(437, 72);
        lblAuditoria.TabIndex = 11;
        // 
        // btnRegistrar
        // 
        btnRegistrar.Location = new Point(694, 658);
        btnRegistrar.Margin = new Padding(4, 4, 4, 4);
        btnRegistrar.Name = "btnRegistrar";
        btnRegistrar.Size = new Size(129, 45);
        btnRegistrar.TabIndex = 6;
        btnRegistrar.Text = "Registrar";
        btnRegistrar.UseVisualStyleBackColor = true;
        btnRegistrar.Click += btnRegistrar_Click;
        // 
        // btnActualizar
        // 
        btnActualizar.Location = new Point(836, 658);
        btnActualizar.Margin = new Padding(4, 4, 4, 4);
        btnActualizar.Name = "btnActualizar";
        btnActualizar.Size = new Size(141, 45);
        btnActualizar.TabIndex = 7;
        btnActualizar.Text = "Actualizar";
        btnActualizar.UseVisualStyleBackColor = true;
        btnActualizar.Click += btnActualizar_Click;
        // 
        // btnDarDeBaja
        // 
        btnDarDeBaja.Location = new Point(990, 658);
        btnDarDeBaja.Margin = new Padding(4, 4, 4, 4);
        btnDarDeBaja.Name = "btnDarDeBaja";
        btnDarDeBaja.Size = new Size(141, 45);
        btnDarDeBaja.TabIndex = 8;
        btnDarDeBaja.Text = "Dar de baja";
        btnDarDeBaja.UseVisualStyleBackColor = true;
        btnDarDeBaja.Click += btnEliminar_Click;
        btnReactivar.Click += btnReactivar_Click;
        chkInactivos.CheckedChanged += chkInactivos_CheckedChanged;
        // 
        // btnReactivar
        // 
        btnReactivar.Location = new Point(836, 721);
        btnReactivar.Margin = new Padding(4, 4, 4, 4);
        btnReactivar.Name = "btnReactivar";
        btnReactivar.Size = new Size(141, 45);
        btnReactivar.TabIndex = 30;
        btnReactivar.Text = "Reactivar";
        btnReactivar.UseVisualStyleBackColor = true;
        // 
        // chkInactivos
        // 
        chkInactivos.AutoSize = true;
        chkInactivos.Location = new Point(694, 150);
        chkInactivos.Name = "chkInactivos";
        chkInactivos.TabIndex = 31;
        chkInactivos.Text = "Mostrar dados de baja";
        chkInactivos.UseVisualStyleBackColor = true;
        // 
        // btnLimpiar
        // 
        btnLimpiar.Location = new Point(694, 721);
        btnLimpiar.Margin = new Padding(4, 4, 4, 4);
        btnLimpiar.Name = "btnLimpiar";
        btnLimpiar.Size = new Size(129, 45);
        btnLimpiar.TabIndex = 9;
        btnLimpiar.Text = "Limpiar";
        btnLimpiar.UseVisualStyleBackColor = true;
        btnLimpiar.Click += btnLimpiar_Click;
        // 
        // btnCerrar
        // 
        btnCerrar.Location = new Point(1003, 721);
        btnCerrar.Margin = new Padding(4, 4, 4, 4);
        btnCerrar.Name = "btnCerrar";
        btnCerrar.Size = new Size(129, 45);
        btnCerrar.TabIndex = 10;
        btnCerrar.Text = "Cerrar";
        btnCerrar.UseVisualStyleBackColor = true;
        btnCerrar.Click += btnCerrar_Click;
        // 
        // FrmClientes
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1157, 840);
        Controls.Add(lblTitulo);
        Controls.Add(grpDatos);
        Controls.Add(lblBuscar);
        Controls.Add(txtBuscar);
        Controls.Add(btnBuscar);
        Controls.Add(dgvClientes);
        Controls.Add(lblAuditoria);
        Controls.Add(btnRegistrar);
        Controls.Add(btnActualizar);
        Controls.Add(btnDarDeBaja);
        Controls.Add(btnReactivar);
        Controls.Add(chkInactivos);
        Controls.Add(btnLimpiar);
        Controls.Add(btnCerrar);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 4, 4, 4);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmClientes";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Gestión de clientes";
        Load += FrmClientes_Load;
        grpDatos.ResumeLayout(false);
        grpDatos.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
