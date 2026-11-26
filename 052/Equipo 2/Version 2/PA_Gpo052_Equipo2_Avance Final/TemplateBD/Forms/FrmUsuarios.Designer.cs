namespace TemplateCSharp.Forms
{
    partial class FrmUsuarios
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
            panel1 = new Panel();
            txtId = new TextBox();
            label9 = new Label();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            btnGuardarUsuario = new FontAwesome.Sharp.IconButton();
            txtPWConfirmar = new TextBox();
            lblPWConfirmar = new Label();
            txtPW = new TextBox();
            lblPW = new Label();
            txtEmailUsuario = new TextBox();
            lblEmailUsuario = new Label();
            cbEstatusUsuario = new ComboBox();
            cbTipoUsuario = new ComboBox();
            dtpFechaNac = new DateTimePicker();
            lblEstatusUsuario = new Label();
            lblTipoUsuario = new Label();
            txtNumNominaUsuario = new TextBox();
            lblNumNominaUsuario = new Label();
            lblFechaNac = new Label();
            txtApellidosUsuario = new TextBox();
            lblApellidosUsuario = new Label();
            txtNombreUsuario = new TextBox();
            lblNombreUsuario = new Label();
            panel2 = new Panel();
            dgvDataUsuario = new DataGridView();
            btnSeleccionar = new DataGridViewButtonColumn();
            Id_usuario = new DataGridViewTextBoxColumn();
            Email = new DataGridViewTextBoxColumn();
            PW = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Apellidos = new DataGridViewTextBoxColumn();
            NumNomina = new DataGridViewTextBoxColumn();
            FechaNac = new DataGridViewTextBoxColumn();
            Id_tipo = new DataGridViewTextBoxColumn();
            TipoUsuario = new DataGridViewTextBoxColumn();
            Estatus = new DataGridViewTextBoxColumn();
            EstatusValor = new DataGridViewTextBoxColumn();
            Id_usuarioReg = new DataGridViewTextBoxColumn();
            UsuarioReg = new DataGridViewTextBoxColumn();
            FechaRegistro = new DataGridViewTextBoxColumn();
            HoraRegistro = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDataUsuario).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(txtId);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(iconButton3);
            panel1.Controls.Add(iconButton2);
            panel1.Controls.Add(btnGuardarUsuario);
            panel1.Controls.Add(txtPWConfirmar);
            panel1.Controls.Add(lblPWConfirmar);
            panel1.Controls.Add(txtPW);
            panel1.Controls.Add(lblPW);
            panel1.Controls.Add(txtEmailUsuario);
            panel1.Controls.Add(lblEmailUsuario);
            panel1.Controls.Add(cbEstatusUsuario);
            panel1.Controls.Add(cbTipoUsuario);
            panel1.Controls.Add(dtpFechaNac);
            panel1.Controls.Add(lblEstatusUsuario);
            panel1.Controls.Add(lblTipoUsuario);
            panel1.Controls.Add(txtNumNominaUsuario);
            panel1.Controls.Add(lblNumNominaUsuario);
            panel1.Controls.Add(lblFechaNac);
            panel1.Controls.Add(txtApellidosUsuario);
            panel1.Controls.Add(lblApellidosUsuario);
            panel1.Controls.Add(txtNombreUsuario);
            panel1.Controls.Add(lblNombreUsuario);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1548, 338);
            panel1.TabIndex = 0;
            // 
            // txtId
            // 
            txtId.Location = new Point(424, 31);
            txtId.Name = "txtId";
            txtId.Size = new Size(40, 31);
            txtId.TabIndex = 30;
            txtId.Visible = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(41, 31);
            label9.Name = "label9";
            label9.Size = new Size(360, 43);
            label9.TabIndex = 29;
            label9.Text = "Detalles del usuario";
            // 
            // iconButton3
            // 
            iconButton3.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Save;
            iconButton3.IconColor = Color.Black;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.Location = new Point(1212, 144);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(213, 71);
            iconButton3.TabIndex = 28;
            iconButton3.Text = "Editar";
            iconButton3.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton3.UseVisualStyleBackColor = true;
            // 
            // iconButton2
            // 
            iconButton2.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Save;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.Location = new Point(1212, 243);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(213, 71);
            iconButton2.TabIndex = 27;
            iconButton2.Text = "Eliminar";
            iconButton2.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton2.UseVisualStyleBackColor = true;
            // 
            // btnGuardarUsuario
            // 
            btnGuardarUsuario.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGuardarUsuario.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardarUsuario.IconColor = Color.Black;
            btnGuardarUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardarUsuario.Location = new Point(1212, 49);
            btnGuardarUsuario.Name = "btnGuardarUsuario";
            btnGuardarUsuario.Size = new Size(213, 71);
            btnGuardarUsuario.TabIndex = 26;
            btnGuardarUsuario.Text = "Guardar";
            btnGuardarUsuario.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardarUsuario.UseVisualStyleBackColor = true;
            btnGuardarUsuario.Click += btnGuardarUsuario_Click;
            // 
            // txtPWConfirmar
            // 
            txtPWConfirmar.Font = new Font("Segoe UI", 10F);
            txtPWConfirmar.Location = new Point(784, 276);
            txtPWConfirmar.Name = "txtPWConfirmar";
            txtPWConfirmar.Size = new Size(318, 34);
            txtPWConfirmar.TabIndex = 25;
            // 
            // lblPWConfirmar
            // 
            lblPWConfirmar.AutoSize = true;
            lblPWConfirmar.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPWConfirmar.Location = new Point(784, 243);
            lblPWConfirmar.Name = "lblPWConfirmar";
            lblPWConfirmar.Size = new Size(228, 23);
            lblPWConfirmar.TabIndex = 24;
            lblPWConfirmar.Text = "Confirmar contraseña:";
            // 
            // txtPW
            // 
            txtPW.Font = new Font("Segoe UI", 10F);
            txtPW.Location = new Point(784, 194);
            txtPW.Name = "txtPW";
            txtPW.Size = new Size(318, 34);
            txtPW.TabIndex = 23;
            // 
            // lblPW
            // 
            lblPW.AutoSize = true;
            lblPW.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPW.Location = new Point(784, 168);
            lblPW.Name = "lblPW";
            lblPW.Size = new Size(130, 23);
            lblPW.TabIndex = 22;
            lblPW.Text = "Contraseña:";
            // 
            // txtEmailUsuario
            // 
            txtEmailUsuario.Font = new Font("Segoe UI", 10F);
            txtEmailUsuario.Location = new Point(784, 123);
            txtEmailUsuario.Name = "txtEmailUsuario";
            txtEmailUsuario.Size = new Size(318, 34);
            txtEmailUsuario.TabIndex = 21;
            // 
            // lblEmailUsuario
            // 
            lblEmailUsuario.AutoSize = true;
            lblEmailUsuario.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmailUsuario.Location = new Point(784, 97);
            lblEmailUsuario.Name = "lblEmailUsuario";
            lblEmailUsuario.Size = new Size(199, 23);
            lblEmailUsuario.TabIndex = 20;
            lblEmailUsuario.Text = "Correo electrónico:";
            // 
            // cbEstatusUsuario
            // 
            cbEstatusUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEstatusUsuario.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbEstatusUsuario.FormattingEnabled = true;
            cbEstatusUsuario.Location = new Point(412, 276);
            cbEstatusUsuario.Name = "cbEstatusUsuario";
            cbEstatusUsuario.Size = new Size(318, 31);
            cbEstatusUsuario.TabIndex = 19;
            // 
            // cbTipoUsuario
            // 
            cbTipoUsuario.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbTipoUsuario.FormattingEnabled = true;
            cbTipoUsuario.Location = new Point(412, 194);
            cbTipoUsuario.Name = "cbTipoUsuario";
            cbTipoUsuario.Size = new Size(318, 31);
            cbTipoUsuario.TabIndex = 18;
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFechaNac.Location = new Point(41, 272);
            dtpFechaNac.Name = "dtpFechaNac";
            dtpFechaNac.Size = new Size(318, 32);
            dtpFechaNac.TabIndex = 17;
            // 
            // lblEstatusUsuario
            // 
            lblEstatusUsuario.AutoSize = true;
            lblEstatusUsuario.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEstatusUsuario.Location = new Point(412, 243);
            lblEstatusUsuario.Name = "lblEstatusUsuario";
            lblEstatusUsuario.Size = new Size(82, 23);
            lblEstatusUsuario.TabIndex = 15;
            lblEstatusUsuario.Text = "Estatus:";
            // 
            // lblTipoUsuario
            // 
            lblTipoUsuario.AutoSize = true;
            lblTipoUsuario.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTipoUsuario.Location = new Point(412, 168);
            lblTipoUsuario.Name = "lblTipoUsuario";
            lblTipoUsuario.Size = new Size(160, 23);
            lblTipoUsuario.TabIndex = 13;
            lblTipoUsuario.Text = "Tipo de usuario:";
            // 
            // txtNumNominaUsuario
            // 
            txtNumNominaUsuario.Font = new Font("Segoe UI", 10F);
            txtNumNominaUsuario.Location = new Point(412, 123);
            txtNumNominaUsuario.Name = "txtNumNominaUsuario";
            txtNumNominaUsuario.Size = new Size(318, 34);
            txtNumNominaUsuario.TabIndex = 12;
            // 
            // lblNumNominaUsuario
            // 
            lblNumNominaUsuario.AutoSize = true;
            lblNumNominaUsuario.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNumNominaUsuario.Location = new Point(412, 97);
            lblNumNominaUsuario.Name = "lblNumNominaUsuario";
            lblNumNominaUsuario.Size = new Size(205, 23);
            lblNumNominaUsuario.TabIndex = 11;
            lblNumNominaUsuario.Text = "Número de nómina:";
            // 
            // lblFechaNac
            // 
            lblFechaNac.AutoSize = true;
            lblFechaNac.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFechaNac.Location = new Point(41, 243);
            lblFechaNac.Name = "lblFechaNac";
            lblFechaNac.Size = new Size(225, 23);
            lblFechaNac.TabIndex = 9;
            lblFechaNac.Text = "Fecha de nacimiento:";
            // 
            // txtApellidosUsuario
            // 
            txtApellidosUsuario.Font = new Font("Segoe UI", 10F);
            txtApellidosUsuario.Location = new Point(41, 194);
            txtApellidosUsuario.Name = "txtApellidosUsuario";
            txtApellidosUsuario.Size = new Size(318, 34);
            txtApellidosUsuario.TabIndex = 8;
            // 
            // lblApellidosUsuario
            // 
            lblApellidosUsuario.AutoSize = true;
            lblApellidosUsuario.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblApellidosUsuario.Location = new Point(41, 168);
            lblApellidosUsuario.Name = "lblApellidosUsuario";
            lblApellidosUsuario.Size = new Size(105, 23);
            lblApellidosUsuario.TabIndex = 7;
            lblApellidosUsuario.Text = "Apellidos:";
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Font = new Font("Segoe UI", 10F);
            txtNombreUsuario.Location = new Point(41, 123);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(318, 34);
            txtNombreUsuario.TabIndex = 6;
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.AutoSize = true;
            lblNombreUsuario.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNombreUsuario.Location = new Point(41, 97);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new Size(95, 23);
            lblNombreUsuario.TabIndex = 5;
            lblNombreUsuario.Text = "Nombre:";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaption;
            panel2.Controls.Add(dgvDataUsuario);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 338);
            panel2.Name = "panel2";
            panel2.Size = new Size(1548, 536);
            panel2.TabIndex = 1;
            // 
            // dgvDataUsuario
            // 
            dgvDataUsuario.AllowUserToAddRows = false;
            dgvDataUsuario.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDataUsuario.Columns.AddRange(new DataGridViewColumn[] { btnSeleccionar, Id_usuario, Email, PW, Nombre, Apellidos, NumNomina, FechaNac, Id_tipo, TipoUsuario, Estatus, EstatusValor, Id_usuarioReg, UsuarioReg, FechaRegistro, HoraRegistro });
            dgvDataUsuario.Location = new Point(41, 112);
            dgvDataUsuario.MultiSelect = false;
            dgvDataUsuario.Name = "dgvDataUsuario";
            dgvDataUsuario.ReadOnly = true;
            dgvDataUsuario.RowHeadersWidth = 62;
            dgvDataUsuario.Size = new Size(1457, 390);
            dgvDataUsuario.TabIndex = 0;
            // 
            // btnSeleccionar
            // 
            btnSeleccionar.HeaderText = "";
            btnSeleccionar.MinimumWidth = 8;
            btnSeleccionar.Name = "btnSeleccionar";
            btnSeleccionar.ReadOnly = true;
            btnSeleccionar.Width = 30;
            // 
            // Id_usuario
            // 
            Id_usuario.HeaderText = "ID Usuario";
            Id_usuario.MinimumWidth = 8;
            Id_usuario.Name = "Id_usuario";
            Id_usuario.ReadOnly = true;
            Id_usuario.Visible = false;
            Id_usuario.Width = 150;
            // 
            // Email
            // 
            Email.HeaderText = "Correo electrónico";
            Email.MinimumWidth = 8;
            Email.Name = "Email";
            Email.ReadOnly = true;
            Email.Width = 150;
            // 
            // PW
            // 
            PW.HeaderText = "PasswordActual";
            PW.MinimumWidth = 8;
            PW.Name = "PW";
            PW.ReadOnly = true;
            PW.Visible = false;
            PW.Width = 150;
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre(s)";
            Nombre.MinimumWidth = 8;
            Nombre.Name = "Nombre";
            Nombre.ReadOnly = true;
            Nombre.Width = 150;
            // 
            // Apellidos
            // 
            Apellidos.HeaderText = "Apellidos";
            Apellidos.MinimumWidth = 8;
            Apellidos.Name = "Apellidos";
            Apellidos.ReadOnly = true;
            Apellidos.Width = 150;
            // 
            // NumNomina
            // 
            NumNomina.HeaderText = "Número de nómina";
            NumNomina.MinimumWidth = 8;
            NumNomina.Name = "NumNomina";
            NumNomina.ReadOnly = true;
            NumNomina.Width = 150;
            // 
            // FechaNac
            // 
            FechaNac.HeaderText = "Fecha de nacimiento";
            FechaNac.MinimumWidth = 8;
            FechaNac.Name = "FechaNac";
            FechaNac.ReadOnly = true;
            FechaNac.Width = 150;
            // 
            // Id_tipo
            // 
            Id_tipo.HeaderText = "IdTipo";
            Id_tipo.MinimumWidth = 8;
            Id_tipo.Name = "Id_tipo";
            Id_tipo.ReadOnly = true;
            Id_tipo.Visible = false;
            Id_tipo.Width = 150;
            // 
            // TipoUsuario
            // 
            TipoUsuario.HeaderText = "Tipo de usuario";
            TipoUsuario.MinimumWidth = 8;
            TipoUsuario.Name = "TipoUsuario";
            TipoUsuario.ReadOnly = true;
            TipoUsuario.Width = 150;
            // 
            // Estatus
            // 
            Estatus.HeaderText = "Estatus";
            Estatus.MinimumWidth = 8;
            Estatus.Name = "Estatus";
            Estatus.ReadOnly = true;
            Estatus.Width = 150;
            // 
            // EstatusValor
            // 
            EstatusValor.HeaderText = "EstatusValor";
            EstatusValor.MinimumWidth = 8;
            EstatusValor.Name = "EstatusValor";
            EstatusValor.ReadOnly = true;
            EstatusValor.Visible = false;
            EstatusValor.Width = 150;
            // 
            // Id_usuarioReg
            // 
            Id_usuarioReg.HeaderText = "IdUsuarioReg";
            Id_usuarioReg.MinimumWidth = 8;
            Id_usuarioReg.Name = "Id_usuarioReg";
            Id_usuarioReg.ReadOnly = true;
            Id_usuarioReg.Visible = false;
            Id_usuarioReg.Width = 150;
            // 
            // UsuarioReg
            // 
            UsuarioReg.HeaderText = "Usuario registrador";
            UsuarioReg.MinimumWidth = 8;
            UsuarioReg.Name = "UsuarioReg";
            UsuarioReg.ReadOnly = true;
            UsuarioReg.Width = 150;
            // 
            // FechaRegistro
            // 
            FechaRegistro.HeaderText = "Fecha de registro";
            FechaRegistro.MinimumWidth = 8;
            FechaRegistro.Name = "FechaRegistro";
            FechaRegistro.ReadOnly = true;
            FechaRegistro.Width = 150;
            // 
            // HoraRegistro
            // 
            HoraRegistro.HeaderText = "Hora de registro";
            HoraRegistro.MinimumWidth = 8;
            HoraRegistro.Name = "HoraRegistro";
            HoraRegistro.ReadOnly = true;
            HoraRegistro.Visible = false;
            HoraRegistro.Width = 150;
            // 
            // FrmUsuarios
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1548, 874);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmUsuarios";
            Text = "FrmUsuarios";
            Load += FrmUsuarios_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDataUsuario).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox txtNombreUsuario;
        private Label lblNombreUsuario;
        private TextBox txtApellidosUsuario;
        private Label lblApellidosUsuario;
        private Label lblFechaNac;
        private ComboBox cbEstatusUsuario;
        private ComboBox cbTipoUsuario;
        private DateTimePicker dtpFechaNac;
        private Label lblEstatusUsuario;
        private Label lblTipoUsuario;
        private TextBox txtNumNominaUsuario;
        private Label lblNumNominaUsuario;
        private TextBox txtPWConfirmar;
        private Label lblPWConfirmar;
        private TextBox txtPW;
        private Label lblPW;
        private TextBox txtEmailUsuario;
        private Label lblEmailUsuario;
        private FontAwesome.Sharp.IconButton btnGuardarUsuario;
        private FontAwesome.Sharp.IconButton iconButton2;
        private Label label9;
        private FontAwesome.Sharp.IconButton iconButton3;
        private Panel panel2;
        private DataGridView dgvDataUsuario;
        private TextBox txtId;
        private DataGridViewButtonColumn btnSeleccionar;
        private DataGridViewTextBoxColumn Id_usuario;
        private DataGridViewTextBoxColumn Email;
        private DataGridViewTextBoxColumn PW;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Apellidos;
        private DataGridViewTextBoxColumn NumNomina;
        private DataGridViewTextBoxColumn FechaNac;
        private DataGridViewTextBoxColumn Id_tipo;
        private DataGridViewTextBoxColumn TipoUsuario;
        private DataGridViewTextBoxColumn Estatus;
        private DataGridViewTextBoxColumn EstatusValor;
        private DataGridViewTextBoxColumn Id_usuarioReg;
        private DataGridViewTextBoxColumn UsuarioReg;
        private DataGridViewTextBoxColumn FechaRegistro;
        private DataGridViewTextBoxColumn HoraRegistro;
    }
}