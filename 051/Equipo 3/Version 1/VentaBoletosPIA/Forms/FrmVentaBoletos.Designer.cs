namespace VentaBoletosApp.Forms
{
    partial class FrmVentaBoletos
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>Required method for Designer support - do not modify the contents of this method with the code editor.</summary>
        private void InitializeComponent()
        {
            this.grpCliente = new System.Windows.Forms.GroupBox();
            this.lblBuscarCliente = new System.Windows.Forms.Label();
            this.txtBuscarCliente = new System.Windows.Forms.TextBox();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.btnNuevoCliente = new System.Windows.Forms.Button();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.grpPartido = new System.Windows.Forms.GroupBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.cmbEstadio = new System.Windows.Forms.ComboBox();
            this.txtEquipo = new System.Windows.Forms.TextBox();
            this.btnBuscarPartido = new System.Windows.Forms.Button();
            this.lblPista = new System.Windows.Forms.Label();
            this.dgvPartidos = new System.Windows.Forms.DataGridView();
            this.colPartidos1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartidos2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartidos3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpZona = new System.Windows.Forms.GroupBox();
            this.dgvZonas = new System.Windows.Forms.DataGridView();
            this.colZonas1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZonas2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZonas3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZonas4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAsientos = new System.Windows.Forms.Label();
            this.lstAsientos = new System.Windows.Forms.CheckedListBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.grpCarrito = new System.Windows.Forms.GroupBox();
            this.dgvCarrito = new System.Windows.Forms.DataGridView();
            this.colCarrito1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarrito2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarrito3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnVaciar = new System.Windows.Forms.Button();
            this.grpPago = new System.Windows.Forms.GroupBox();
            this.lblMetodo = new System.Windows.Forms.Label();
            this.cmbMetodo = new System.Windows.Forms.ComboBox();
            this.lblTitular = new System.Windows.Forms.Label();
            this.txtTitular = new System.Windows.Forms.TextBox();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.lblExpira = new System.Windows.Forms.Label();
            this.txtExpira = new System.Windows.Forms.TextBox();
            this.lblCcv = new System.Windows.Forms.Label();
            this.txtCcv = new System.Windows.Forms.TextBox();
            this.lblSimulado = new System.Windows.Forms.Label();
            this.grpResumen = new System.Windows.Forms.GroupBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblIva = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZonas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).BeginInit();
            this.grpCliente.SuspendLayout();
            this.grpPartido.SuspendLayout();
            this.grpZona.SuspendLayout();
            this.grpCarrito.SuspendLayout();
            this.grpPago.SuspendLayout();
            this.grpResumen.SuspendLayout();
            this.SuspendLayout();
            //
            // grpCliente
            //
            this.grpCliente.Controls.Add(this.lblBuscarCliente);
            this.grpCliente.Controls.Add(this.txtBuscarCliente);
            this.grpCliente.Controls.Add(this.btnBuscarCliente);
            this.grpCliente.Controls.Add(this.btnNuevoCliente);
            this.grpCliente.Controls.Add(this.cmbCliente);
            this.grpCliente.Text = "1. Cliente";
            this.grpCliente.Location = new System.Drawing.Point(10, 8);
            this.grpCliente.Size = new System.Drawing.Size(600, 108);
            this.grpCliente.Name = "grpCliente";
            this.grpCliente.TabIndex = 0;
            //
            // lblBuscarCliente
            //
            this.lblBuscarCliente.Text = "Buscar (apellido o correo):";
            this.lblBuscarCliente.Location = new System.Drawing.Point(15, 30);
            this.lblBuscarCliente.AutoSize = true;
            this.lblBuscarCliente.Name = "lblBuscarCliente";
            this.lblBuscarCliente.TabIndex = 1;
            //
            // txtBuscarCliente
            //
            this.txtBuscarCliente.Location = new System.Drawing.Point(190, 26);
            this.txtBuscarCliente.Size = new System.Drawing.Size(170, 23);
            this.txtBuscarCliente.Name = "txtBuscarCliente";
            this.txtBuscarCliente.TabIndex = 2;
            //
            // btnBuscarCliente
            //
            this.btnBuscarCliente.Text = "Buscar";
            this.btnBuscarCliente.Location = new System.Drawing.Point(370, 22);
            this.btnBuscarCliente.Size = new System.Drawing.Size(80, 32);
            this.btnBuscarCliente.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnBuscarCliente.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnBuscarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarCliente.UseVisualStyleBackColor = false;
            this.btnBuscarCliente.FlatAppearance.BorderSize = 0;
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.TabIndex = 3;
            this.btnBuscarCliente.Click += new System.EventHandler(this.BtnBuscarCliente_Click);
            //
            // btnNuevoCliente
            //
            this.btnNuevoCliente.Text = "Nuevo cliente";
            this.btnNuevoCliente.Location = new System.Drawing.Point(460, 22);
            this.btnNuevoCliente.Size = new System.Drawing.Size(120, 32);
            this.btnNuevoCliente.BackColor = System.Drawing.Color.FromArgb(96, 110, 128);
            this.btnNuevoCliente.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnNuevoCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoCliente.UseVisualStyleBackColor = false;
            this.btnNuevoCliente.FlatAppearance.BorderSize = 0;
            this.btnNuevoCliente.Name = "btnNuevoCliente";
            this.btnNuevoCliente.TabIndex = 4;
            this.btnNuevoCliente.Click += new System.EventHandler(this.BtnNuevoCliente_Click);
            //
            // cmbCliente
            //
            this.cmbCliente.Location = new System.Drawing.Point(15, 66);
            this.cmbCliente.Size = new System.Drawing.Size(570, 23);
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCliente.FormattingEnabled = true;
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.TabIndex = 5;
            //
            // grpPartido
            //
            this.grpPartido.Controls.Add(this.dtpFecha);
            this.grpPartido.Controls.Add(this.cmbEstadio);
            this.grpPartido.Controls.Add(this.txtEquipo);
            this.grpPartido.Controls.Add(this.btnBuscarPartido);
            this.grpPartido.Controls.Add(this.lblPista);
            this.grpPartido.Controls.Add(this.dgvPartidos);
            this.grpPartido.Text = "2. Partido";
            this.grpPartido.Location = new System.Drawing.Point(10, 120);
            this.grpPartido.Size = new System.Drawing.Size(600, 245);
            this.grpPartido.Name = "grpPartido";
            this.grpPartido.TabIndex = 6;
            //
            // dtpFecha
            //
            this.dtpFecha.Location = new System.Drawing.Point(15, 28);
            this.dtpFecha.Size = new System.Drawing.Size(135, 23);
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.CustomFormat = "dd-MMM-yyyy";
            this.dtpFecha.ShowCheckBox = true;
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.TabIndex = 7;
            //
            // cmbEstadio
            //
            this.cmbEstadio.Location = new System.Drawing.Point(158, 28);
            this.cmbEstadio.Size = new System.Drawing.Size(170, 23);
            this.cmbEstadio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadio.FormattingEnabled = true;
            this.cmbEstadio.Name = "cmbEstadio";
            this.cmbEstadio.TabIndex = 8;
            //
            // txtEquipo
            //
            this.txtEquipo.Location = new System.Drawing.Point(336, 28);
            this.txtEquipo.Size = new System.Drawing.Size(120, 23);
            this.txtEquipo.Name = "txtEquipo";
            this.txtEquipo.TabIndex = 9;
            //
            // btnBuscarPartido
            //
            this.btnBuscarPartido.Text = "Buscar";
            this.btnBuscarPartido.Location = new System.Drawing.Point(465, 24);
            this.btnBuscarPartido.Size = new System.Drawing.Size(120, 32);
            this.btnBuscarPartido.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnBuscarPartido.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnBuscarPartido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarPartido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarPartido.UseVisualStyleBackColor = false;
            this.btnBuscarPartido.FlatAppearance.BorderSize = 0;
            this.btnBuscarPartido.Name = "btnBuscarPartido";
            this.btnBuscarPartido.TabIndex = 10;
            this.btnBuscarPartido.Click += new System.EventHandler(this.BtnBuscarPartido_Click);
            //
            // lblPista
            //
            this.lblPista.Text = "Fecha (opcional)  ·  Estadio  ·  Equipo";
            this.lblPista.Location = new System.Drawing.Point(15, 56);
            this.lblPista.AutoSize = true;
            this.lblPista.ForeColor = System.Drawing.Color.FromArgb(96, 110, 128);
            this.lblPista.Name = "lblPista";
            this.lblPista.TabIndex = 11;
            //
            // dgvPartidos
            //
            this.dgvPartidos.AllowUserToAddRows = false;
            this.dgvPartidos.AllowUserToDeleteRows = false;
            this.dgvPartidos.AllowUserToResizeRows = false;
            this.dgvPartidos.AutoGenerateColumns = false;
            this.dgvPartidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPartidos.BackgroundColor = System.Drawing.Color.White;
            this.dgvPartidos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvPartidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartidos.MultiSelect = false;
            this.dgvPartidos.ReadOnly = true;
            this.dgvPartidos.RowHeadersVisible = false;
            this.dgvPartidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPartidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colPartidos1, this.colPartidos2, this.colPartidos3 });
            this.dgvPartidos.Location = new System.Drawing.Point(15, 76);
            this.dgvPartidos.Size = new System.Drawing.Size(570, 155);
            this.dgvPartidos.Name = "dgvPartidos";
            this.dgvPartidos.TabIndex = 12;
            this.dgvPartidos.SelectionChanged += new System.EventHandler(this.DgvPartidos_SelectionChanged);
            //
            // colPartidos1
            //
            this.colPartidos1.DataPropertyName = "DescripcionCorta";
            this.colPartidos1.FillWeight = 110F;
            this.colPartidos1.HeaderText = "Partido";
            this.colPartidos1.Name = "colPartidos1";
            this.colPartidos1.ReadOnly = true;
            this.colPartidos1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colPartidos2
            //
            this.colPartidos2.DataPropertyName = "FechaHora";
            this.colPartidos2.FillWeight = 80F;
            this.colPartidos2.HeaderText = "Fecha y hora";
            this.colPartidos2.Name = "colPartidos2";
            this.colPartidos2.ReadOnly = true;
            this.colPartidos2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPartidos2.Tag = "hora";
            //
            // colPartidos3
            //
            this.colPartidos3.DataPropertyName = "NombreEstadio";
            this.colPartidos3.FillWeight = 90F;
            this.colPartidos3.HeaderText = "Estadio";
            this.colPartidos3.Name = "colPartidos3";
            this.colPartidos3.ReadOnly = true;
            this.colPartidos3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // grpZona
            //
            this.grpZona.Controls.Add(this.dgvZonas);
            this.grpZona.Controls.Add(this.lblAsientos);
            this.grpZona.Controls.Add(this.lstAsientos);
            this.grpZona.Controls.Add(this.btnAgregar);
            this.grpZona.Text = "3. Zona y asientos";
            this.grpZona.Location = new System.Drawing.Point(10, 370);
            this.grpZona.Size = new System.Drawing.Size(600, 345);
            this.grpZona.Name = "grpZona";
            this.grpZona.TabIndex = 13;
            //
            // dgvZonas
            //
            this.dgvZonas.AllowUserToAddRows = false;
            this.dgvZonas.AllowUserToDeleteRows = false;
            this.dgvZonas.AllowUserToResizeRows = false;
            this.dgvZonas.AutoGenerateColumns = false;
            this.dgvZonas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvZonas.BackgroundColor = System.Drawing.Color.White;
            this.dgvZonas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvZonas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvZonas.MultiSelect = false;
            this.dgvZonas.ReadOnly = true;
            this.dgvZonas.RowHeadersVisible = false;
            this.dgvZonas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvZonas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colZonas1, this.colZonas2, this.colZonas3, this.colZonas4 });
            this.dgvZonas.Location = new System.Drawing.Point(15, 26);
            this.dgvZonas.Size = new System.Drawing.Size(570, 110);
            this.dgvZonas.Name = "dgvZonas";
            this.dgvZonas.TabIndex = 14;
            this.dgvZonas.SelectionChanged += new System.EventHandler(this.DgvZonas_SelectionChanged);
            //
            // colZonas1
            //
            this.colZonas1.DataPropertyName = "NombreZona";
            this.colZonas1.FillWeight = 100F;
            this.colZonas1.HeaderText = "Zona";
            this.colZonas1.Name = "colZonas1";
            this.colZonas1.ReadOnly = true;
            this.colZonas1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colZonas2
            //
            this.colZonas2.DataPropertyName = "Precio";
            this.colZonas2.FillWeight = 70F;
            this.colZonas2.HeaderText = "Precio";
            this.colZonas2.Name = "colZonas2";
            this.colZonas2.ReadOnly = true;
            this.colZonas2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colZonas3
            //
            this.colZonas3.DataPropertyName = "AsientosDisponibles";
            this.colZonas3.FillWeight = 70F;
            this.colZonas3.HeaderText = "Disponibles";
            this.colZonas3.Name = "colZonas3";
            this.colZonas3.ReadOnly = true;
            this.colZonas3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colZonas4
            //
            this.colZonas4.DataPropertyName = "AsientosTotales";
            this.colZonas4.FillWeight = 55F;
            this.colZonas4.HeaderText = "Total";
            this.colZonas4.Name = "colZonas4";
            this.colZonas4.ReadOnly = true;
            this.colZonas4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // lblAsientos
            //
            this.lblAsientos.Text = "Asientos disponibles (máximo 4 boletos por compra):";
            this.lblAsientos.Location = new System.Drawing.Point(15, 144);
            this.lblAsientos.AutoSize = true;
            this.lblAsientos.Name = "lblAsientos";
            this.lblAsientos.TabIndex = 15;
            //
            // lstAsientos
            //
            this.lstAsientos.Location = new System.Drawing.Point(15, 166);
            this.lstAsientos.Size = new System.Drawing.Size(570, 130);
            this.lstAsientos.CheckOnClick = true;
            this.lstAsientos.MultiColumn = true;
            this.lstAsientos.ColumnWidth = 140;
            this.lstAsientos.IntegralHeight = false;
            this.lstAsientos.FormattingEnabled = true;
            this.lstAsientos.Name = "lstAsientos";
            this.lstAsientos.TabIndex = 16;
            //
            // btnAgregar
            //
            this.btnAgregar.Text = "Agregar al carrito";
            this.btnAgregar.Location = new System.Drawing.Point(15, 302);
            this.btnAgregar.Size = new System.Drawing.Size(170, 32);
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnAgregar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.TabIndex = 17;
            this.btnAgregar.Click += new System.EventHandler(this.BtnAgregar_Click);
            //
            // grpCarrito
            //
            this.grpCarrito.Controls.Add(this.dgvCarrito);
            this.grpCarrito.Controls.Add(this.btnQuitar);
            this.grpCarrito.Controls.Add(this.btnVaciar);
            this.grpCarrito.Text = "Carrito";
            this.grpCarrito.Location = new System.Drawing.Point(620, 8);
            this.grpCarrito.Size = new System.Drawing.Size(600, 240);
            this.grpCarrito.Name = "grpCarrito";
            this.grpCarrito.TabIndex = 18;
            //
            // dgvCarrito
            //
            this.dgvCarrito.AllowUserToAddRows = false;
            this.dgvCarrito.AllowUserToDeleteRows = false;
            this.dgvCarrito.AllowUserToResizeRows = false;
            this.dgvCarrito.AutoGenerateColumns = false;
            this.dgvCarrito.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCarrito.BackgroundColor = System.Drawing.Color.White;
            this.dgvCarrito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvCarrito.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarrito.MultiSelect = false;
            this.dgvCarrito.ReadOnly = true;
            this.dgvCarrito.RowHeadersVisible = false;
            this.dgvCarrito.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCarrito.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colCarrito1, this.colCarrito2, this.colCarrito3 });
            this.dgvCarrito.Location = new System.Drawing.Point(15, 26);
            this.dgvCarrito.Size = new System.Drawing.Size(570, 160);
            this.dgvCarrito.Name = "dgvCarrito";
            this.dgvCarrito.TabIndex = 19;
            //
            // colCarrito1
            //
            this.colCarrito1.DataPropertyName = "NombreZona";
            this.colCarrito1.FillWeight = 100F;
            this.colCarrito1.HeaderText = "Zona";
            this.colCarrito1.Name = "colCarrito1";
            this.colCarrito1.ReadOnly = true;
            this.colCarrito1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colCarrito2
            //
            this.colCarrito2.DataPropertyName = "DescripcionAsiento";
            this.colCarrito2.FillWeight = 100F;
            this.colCarrito2.HeaderText = "Asiento";
            this.colCarrito2.Name = "colCarrito2";
            this.colCarrito2.ReadOnly = true;
            this.colCarrito2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colCarrito3
            //
            this.colCarrito3.DataPropertyName = "Precio";
            this.colCarrito3.FillWeight = 70F;
            this.colCarrito3.HeaderText = "Precio";
            this.colCarrito3.Name = "colCarrito3";
            this.colCarrito3.ReadOnly = true;
            this.colCarrito3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // btnQuitar
            //
            this.btnQuitar.Text = "Quitar seleccionado";
            this.btnQuitar.Location = new System.Drawing.Point(15, 195);
            this.btnQuitar.Size = new System.Drawing.Size(170, 32);
            this.btnQuitar.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnQuitar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.FlatAppearance.BorderSize = 0;
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.TabIndex = 20;
            this.btnQuitar.Click += new System.EventHandler(this.BtnQuitar_Click);
            //
            // btnVaciar
            //
            this.btnVaciar.Text = "Vaciar";
            this.btnVaciar.Location = new System.Drawing.Point(195, 195);
            this.btnVaciar.Size = new System.Drawing.Size(100, 32);
            this.btnVaciar.BackColor = System.Drawing.Color.FromArgb(96, 110, 128);
            this.btnVaciar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnVaciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVaciar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVaciar.UseVisualStyleBackColor = false;
            this.btnVaciar.FlatAppearance.BorderSize = 0;
            this.btnVaciar.Name = "btnVaciar";
            this.btnVaciar.TabIndex = 21;
            this.btnVaciar.Click += new System.EventHandler(this.BtnVaciar_Click);
            //
            // grpPago
            //
            this.grpPago.Controls.Add(this.lblMetodo);
            this.grpPago.Controls.Add(this.cmbMetodo);
            this.grpPago.Controls.Add(this.lblTitular);
            this.grpPago.Controls.Add(this.txtTitular);
            this.grpPago.Controls.Add(this.lblNumero);
            this.grpPago.Controls.Add(this.txtNumero);
            this.grpPago.Controls.Add(this.lblExpira);
            this.grpPago.Controls.Add(this.txtExpira);
            this.grpPago.Controls.Add(this.lblCcv);
            this.grpPago.Controls.Add(this.txtCcv);
            this.grpPago.Controls.Add(this.lblSimulado);
            this.grpPago.Text = "4. Pago";
            this.grpPago.Location = new System.Drawing.Point(620, 252);
            this.grpPago.Size = new System.Drawing.Size(600, 215);
            this.grpPago.Name = "grpPago";
            this.grpPago.TabIndex = 22;
            //
            // lblMetodo
            //
            this.lblMetodo.Text = "Método de pago:";
            this.lblMetodo.Location = new System.Drawing.Point(15, 32);
            this.lblMetodo.AutoSize = true;
            this.lblMetodo.Name = "lblMetodo";
            this.lblMetodo.TabIndex = 23;
            //
            // cmbMetodo
            //
            this.cmbMetodo.Location = new System.Drawing.Point(140, 28);
            this.cmbMetodo.Size = new System.Drawing.Size(200, 23);
            this.cmbMetodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMetodo.FormattingEnabled = true;
            this.cmbMetodo.Name = "cmbMetodo";
            this.cmbMetodo.TabIndex = 24;
            this.cmbMetodo.SelectedIndexChanged += new System.EventHandler(this.CmbMetodo_SelectedIndexChanged);
            //
            // lblTitular
            //
            this.lblTitular.Text = "Titular:";
            this.lblTitular.Location = new System.Drawing.Point(15, 70);
            this.lblTitular.AutoSize = true;
            this.lblTitular.Name = "lblTitular";
            this.lblTitular.TabIndex = 25;
            //
            // txtTitular
            //
            this.txtTitular.Location = new System.Drawing.Point(140, 66);
            this.txtTitular.Size = new System.Drawing.Size(300, 23);
            this.txtTitular.MaxLength = 100;
            this.txtTitular.Name = "txtTitular";
            this.txtTitular.TabIndex = 26;
            //
            // lblNumero
            //
            this.lblNumero.Text = "Núm. tarjeta:";
            this.lblNumero.Location = new System.Drawing.Point(15, 104);
            this.lblNumero.AutoSize = true;
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.TabIndex = 27;
            //
            // txtNumero
            //
            this.txtNumero.Location = new System.Drawing.Point(140, 100);
            this.txtNumero.Size = new System.Drawing.Size(220, 23);
            this.txtNumero.MaxLength = 23;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.TabIndex = 28;
            //
            // lblExpira
            //
            this.lblExpira.Text = "Expira (MM/AA):";
            this.lblExpira.Location = new System.Drawing.Point(15, 138);
            this.lblExpira.AutoSize = true;
            this.lblExpira.Name = "lblExpira";
            this.lblExpira.TabIndex = 29;
            //
            // txtExpira
            //
            this.txtExpira.Location = new System.Drawing.Point(140, 134);
            this.txtExpira.Size = new System.Drawing.Size(80, 23);
            this.txtExpira.MaxLength = 5;
            this.txtExpira.Name = "txtExpira";
            this.txtExpira.TabIndex = 30;
            //
            // lblCcv
            //
            this.lblCcv.Text = "CCV:";
            this.lblCcv.Location = new System.Drawing.Point(240, 138);
            this.lblCcv.AutoSize = true;
            this.lblCcv.Name = "lblCcv";
            this.lblCcv.TabIndex = 31;
            //
            // txtCcv
            //
            this.txtCcv.Location = new System.Drawing.Point(285, 134);
            this.txtCcv.Size = new System.Drawing.Size(70, 23);
            this.txtCcv.MaxLength = 4;
            this.txtCcv.UseSystemPasswordChar = true;
            this.txtCcv.Name = "txtCcv";
            this.txtCcv.TabIndex = 32;
            //
            // lblSimulado
            //
            this.lblSimulado.Text = "Pago simulado: los datos de la tarjeta se validan pero no se almacenan.";
            this.lblSimulado.Location = new System.Drawing.Point(15, 178);
            this.lblSimulado.AutoSize = true;
            this.lblSimulado.ForeColor = System.Drawing.Color.FromArgb(96, 110, 128);
            this.lblSimulado.Name = "lblSimulado";
            this.lblSimulado.TabIndex = 33;
            //
            // grpResumen
            //
            this.grpResumen.Controls.Add(this.lblCantidad);
            this.grpResumen.Controls.Add(this.lblSubtotal);
            this.grpResumen.Controls.Add(this.lblIva);
            this.grpResumen.Controls.Add(this.lblTotal);
            this.grpResumen.Controls.Add(this.btnConfirmar);
            this.grpResumen.Controls.Add(this.btnLimpiar);
            this.grpResumen.Text = "Resumen de compra";
            this.grpResumen.Location = new System.Drawing.Point(620, 472);
            this.grpResumen.Size = new System.Drawing.Size(600, 243);
            this.grpResumen.Name = "grpResumen";
            this.grpResumen.TabIndex = 34;
            //
            // lblCantidad
            //
            this.lblCantidad.Text = "Boletos: 0";
            this.lblCantidad.Location = new System.Drawing.Point(20, 32);
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.TabIndex = 35;
            //
            // lblSubtotal
            //
            this.lblSubtotal.Text = "Subtotal: $0.00";
            this.lblSubtotal.Location = new System.Drawing.Point(20, 62);
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.TabIndex = 36;
            //
            // lblIva
            //
            this.lblIva.Text = "IVA (16%): $0.00";
            this.lblIva.Location = new System.Drawing.Point(20, 92);
            this.lblIva.AutoSize = true;
            this.lblIva.Name = "lblIva";
            this.lblIva.TabIndex = 37;
            //
            // lblTotal
            //
            this.lblTotal.Text = "Total: $0.00";
            this.lblTotal.Location = new System.Drawing.Point(20, 125);
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 38;
            //
            // btnConfirmar
            //
            this.btnConfirmar.Text = "Confirmar compra";
            this.btnConfirmar.Location = new System.Drawing.Point(20, 180);
            this.btnConfirmar.Size = new System.Drawing.Size(220, 42);
            this.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnConfirmar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.TabIndex = 39;
            this.btnConfirmar.Click += new System.EventHandler(this.BtnConfirmar_Click);
            //
            // btnLimpiar
            //
            this.btnLimpiar.Text = "Limpiar todo";
            this.btnLimpiar.Location = new System.Drawing.Point(260, 185);
            this.btnLimpiar.Size = new System.Drawing.Size(140, 32);
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(96, 110, 128);
            this.btnLimpiar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.TabIndex = 40;
            this.btnLimpiar.Click += new System.EventHandler(this.BtnLimpiar_Click);
            //
            // FrmVentaBoletos
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.ClientSize = new System.Drawing.Size(1240, 730);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(1240, 730);
            this.Controls.Add(this.grpCliente);
            this.Controls.Add(this.grpPartido);
            this.Controls.Add(this.grpZona);
            this.Controls.Add(this.grpCarrito);
            this.Controls.Add(this.grpPago);
            this.Controls.Add(this.grpResumen);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmVentaBoletos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Venta de boletos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZonas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).EndInit();
            this.grpCliente.ResumeLayout(false);
            this.grpCliente.PerformLayout();
            this.grpPartido.ResumeLayout(false);
            this.grpPartido.PerformLayout();
            this.grpZona.ResumeLayout(false);
            this.grpZona.PerformLayout();
            this.grpCarrito.ResumeLayout(false);
            this.grpCarrito.PerformLayout();
            this.grpPago.ResumeLayout(false);
            this.grpPago.PerformLayout();
            this.grpResumen.ResumeLayout(false);
            this.grpResumen.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpCliente;
        private System.Windows.Forms.Label lblBuscarCliente;
        private System.Windows.Forms.TextBox txtBuscarCliente;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.Button btnNuevoCliente;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.GroupBox grpPartido;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.ComboBox cmbEstadio;
        private System.Windows.Forms.TextBox txtEquipo;
        private System.Windows.Forms.Button btnBuscarPartido;
        private System.Windows.Forms.Label lblPista;
        private System.Windows.Forms.DataGridView dgvPartidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos3;
        private System.Windows.Forms.GroupBox grpZona;
        private System.Windows.Forms.DataGridView dgvZonas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZonas1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZonas2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZonas3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZonas4;
        private System.Windows.Forms.Label lblAsientos;
        private System.Windows.Forms.CheckedListBox lstAsientos;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.GroupBox grpCarrito;
        private System.Windows.Forms.DataGridView dgvCarrito;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarrito1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarrito2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarrito3;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnVaciar;
        private System.Windows.Forms.GroupBox grpPago;
        private System.Windows.Forms.Label lblMetodo;
        private System.Windows.Forms.ComboBox cmbMetodo;
        private System.Windows.Forms.Label lblTitular;
        private System.Windows.Forms.TextBox txtTitular;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label lblExpira;
        private System.Windows.Forms.TextBox txtExpira;
        private System.Windows.Forms.Label lblCcv;
        private System.Windows.Forms.TextBox txtCcv;
        private System.Windows.Forms.Label lblSimulado;
        private System.Windows.Forms.GroupBox grpResumen;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnLimpiar;
    }
}
