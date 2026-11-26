namespace TemplateCSharp.Forms
{
    partial class FrmPrincipal
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
            panel3 = new Panel();
            btnSalirPrincipal = new FontAwesome.Sharp.IconButton();
            btnReportes = new FontAwesome.Sharp.IconButton();
            btnFacturacion = new FontAwesome.Sharp.IconButton();
            btnCancelaciones = new FontAwesome.Sharp.IconButton();
            btnBoletos = new FontAwesome.Sharp.IconButton();
            btnVentas = new FontAwesome.Sharp.IconButton();
            btnPartidos = new FontAwesome.Sharp.IconButton();
            btnEquipos = new FontAwesome.Sharp.IconButton();
            btnCatalogo = new FontAwesome.Sharp.IconButton();
            btnClientes = new FontAwesome.Sharp.IconButton();
            btnUsuarios = new FontAwesome.Sharp.IconButton();
            panel2 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            lblNUsuario = new Label();
            lblUsuario = new Label();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            label1 = new Label();
            panelContenedor = new Panel();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.DeepSkyBlue;
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 1024);
            panel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BackColor = Color.DeepSkyBlue;
            panel3.Controls.Add(btnSalirPrincipal);
            panel3.Controls.Add(btnReportes);
            panel3.Controls.Add(btnFacturacion);
            panel3.Controls.Add(btnCancelaciones);
            panel3.Controls.Add(btnBoletos);
            panel3.Controls.Add(btnVentas);
            panel3.Controls.Add(btnPartidos);
            panel3.Controls.Add(btnEquipos);
            panel3.Controls.Add(btnCatalogo);
            panel3.Controls.Add(btnClientes);
            panel3.Controls.Add(btnUsuarios);
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(25, 300);
            panel3.Name = "panel3";
            panel3.Size = new Size(325, 724);
            panel3.TabIndex = 2;
            // 
            // btnSalirPrincipal
            // 
            btnSalirPrincipal.Dock = DockStyle.Bottom;
            btnSalirPrincipal.FlatAppearance.BorderSize = 0;
            btnSalirPrincipal.FlatStyle = FlatStyle.Flat;
            btnSalirPrincipal.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSalirPrincipal.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            btnSalirPrincipal.IconColor = Color.Black;
            btnSalirPrincipal.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSalirPrincipal.ImageAlign = ContentAlignment.MiddleLeft;
            btnSalirPrincipal.Location = new Point(0, 669);
            btnSalirPrincipal.Name = "btnSalirPrincipal";
            btnSalirPrincipal.Size = new Size(325, 55);
            btnSalirPrincipal.TabIndex = 10;
            btnSalirPrincipal.Text = "Salir";
            btnSalirPrincipal.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSalirPrincipal.UseVisualStyleBackColor = true;
            btnSalirPrincipal.Click += btnSalirPrincipal_Click;
            // 
            // btnReportes
            // 
            btnReportes.Dock = DockStyle.Top;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReportes.IconChar = FontAwesome.Sharp.IconChar.Clipboard;
            btnReportes.IconColor = Color.Black;
            btnReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnReportes.ImageAlign = ContentAlignment.MiddleLeft;
            btnReportes.Location = new Point(0, 495);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(325, 55);
            btnReportes.TabIndex = 9;
            btnReportes.Text = "Reportes";
            btnReportes.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReportes.UseVisualStyleBackColor = true;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnFacturacion
            // 
            btnFacturacion.Dock = DockStyle.Top;
            btnFacturacion.FlatAppearance.BorderSize = 0;
            btnFacturacion.FlatStyle = FlatStyle.Flat;
            btnFacturacion.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFacturacion.IconChar = FontAwesome.Sharp.IconChar.Receipt;
            btnFacturacion.IconColor = Color.Black;
            btnFacturacion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnFacturacion.ImageAlign = ContentAlignment.MiddleLeft;
            btnFacturacion.Location = new Point(0, 440);
            btnFacturacion.Name = "btnFacturacion";
            btnFacturacion.Size = new Size(325, 55);
            btnFacturacion.TabIndex = 8;
            btnFacturacion.Text = "Facturación";
            btnFacturacion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnFacturacion.UseVisualStyleBackColor = true;
            btnFacturacion.Click += btnFacturacion_Click;
            // 
            // btnCancelaciones
            // 
            btnCancelaciones.Dock = DockStyle.Top;
            btnCancelaciones.FlatAppearance.BorderSize = 0;
            btnCancelaciones.FlatStyle = FlatStyle.Flat;
            btnCancelaciones.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancelaciones.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelaciones.IconColor = Color.Black;
            btnCancelaciones.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelaciones.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelaciones.Location = new Point(0, 385);
            btnCancelaciones.Name = "btnCancelaciones";
            btnCancelaciones.Size = new Size(325, 55);
            btnCancelaciones.TabIndex = 7;
            btnCancelaciones.Text = "Cancelaciones";
            btnCancelaciones.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelaciones.UseVisualStyleBackColor = true;
            btnCancelaciones.Click += btnCancelaciones_Click;
            // 
            // btnBoletos
            // 
            btnBoletos.Dock = DockStyle.Top;
            btnBoletos.FlatAppearance.BorderSize = 0;
            btnBoletos.FlatStyle = FlatStyle.Flat;
            btnBoletos.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBoletos.IconChar = FontAwesome.Sharp.IconChar.Ticket;
            btnBoletos.IconColor = Color.Black;
            btnBoletos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnBoletos.ImageAlign = ContentAlignment.MiddleLeft;
            btnBoletos.Location = new Point(0, 330);
            btnBoletos.Name = "btnBoletos";
            btnBoletos.Size = new Size(325, 55);
            btnBoletos.TabIndex = 6;
            btnBoletos.Text = "Boletos";
            btnBoletos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnBoletos.UseVisualStyleBackColor = true;
            btnBoletos.Click += btnBoletos_Click;
            // 
            // btnVentas
            // 
            btnVentas.Dock = DockStyle.Top;
            btnVentas.FlatAppearance.BorderSize = 0;
            btnVentas.FlatStyle = FlatStyle.Flat;
            btnVentas.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVentas.IconChar = FontAwesome.Sharp.IconChar.Tags;
            btnVentas.IconColor = Color.Black;
            btnVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnVentas.ImageAlign = ContentAlignment.MiddleLeft;
            btnVentas.Location = new Point(0, 275);
            btnVentas.Name = "btnVentas";
            btnVentas.Size = new Size(325, 55);
            btnVentas.TabIndex = 5;
            btnVentas.Text = "Ventas";
            btnVentas.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnVentas.UseVisualStyleBackColor = true;
            btnVentas.Click += btnVentas_Click;
            // 
            // btnPartidos
            // 
            btnPartidos.Dock = DockStyle.Top;
            btnPartidos.FlatAppearance.BorderSize = 0;
            btnPartidos.FlatStyle = FlatStyle.Flat;
            btnPartidos.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPartidos.IconChar = FontAwesome.Sharp.IconChar.CalendarDays;
            btnPartidos.IconColor = Color.Black;
            btnPartidos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnPartidos.ImageAlign = ContentAlignment.MiddleLeft;
            btnPartidos.Location = new Point(0, 220);
            btnPartidos.Name = "btnPartidos";
            btnPartidos.Size = new Size(325, 55);
            btnPartidos.TabIndex = 4;
            btnPartidos.Text = "Partidos";
            btnPartidos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnPartidos.UseVisualStyleBackColor = true;
            btnPartidos.Click += btnPartidos_Click;
            // 
            // btnEquipos
            // 
            btnEquipos.Dock = DockStyle.Top;
            btnEquipos.FlatAppearance.BorderSize = 0;
            btnEquipos.FlatStyle = FlatStyle.Flat;
            btnEquipos.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEquipos.IconChar = FontAwesome.Sharp.IconChar.ShieldBlank;
            btnEquipos.IconColor = Color.Black;
            btnEquipos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEquipos.ImageAlign = ContentAlignment.MiddleLeft;
            btnEquipos.Location = new Point(0, 165);
            btnEquipos.Name = "btnEquipos";
            btnEquipos.Size = new Size(325, 55);
            btnEquipos.TabIndex = 3;
            btnEquipos.Text = "Equipos";
            btnEquipos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEquipos.UseVisualStyleBackColor = true;
            btnEquipos.Click += btnEquipos_Click;
            // 
            // btnCatalogo
            // 
            btnCatalogo.Dock = DockStyle.Top;
            btnCatalogo.FlatAppearance.BorderSize = 0;
            btnCatalogo.FlatStyle = FlatStyle.Flat;
            btnCatalogo.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCatalogo.IconChar = FontAwesome.Sharp.IconChar.BookBookmark;
            btnCatalogo.IconColor = Color.Black;
            btnCatalogo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCatalogo.ImageAlign = ContentAlignment.MiddleLeft;
            btnCatalogo.Location = new Point(0, 110);
            btnCatalogo.Name = "btnCatalogo";
            btnCatalogo.Size = new Size(325, 55);
            btnCatalogo.TabIndex = 2;
            btnCatalogo.Text = "Catálogo de estadios";
            btnCatalogo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCatalogo.UseVisualStyleBackColor = true;
            btnCatalogo.Click += btnCatalogo_Click;
            // 
            // btnClientes
            // 
            btnClientes.Dock = DockStyle.Top;
            btnClientes.FlatAppearance.BorderSize = 0;
            btnClientes.FlatStyle = FlatStyle.Flat;
            btnClientes.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClientes.IconChar = FontAwesome.Sharp.IconChar.UsersRectangle;
            btnClientes.IconColor = Color.Black;
            btnClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClientes.ImageAlign = ContentAlignment.MiddleLeft;
            btnClientes.Location = new Point(0, 55);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(325, 55);
            btnClientes.TabIndex = 1;
            btnClientes.Text = "Clientes";
            btnClientes.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnClientes.UseVisualStyleBackColor = true;
            btnClientes.Click += btnClientes_Click;
            // 
            // btnUsuarios
            // 
            btnUsuarios.Dock = DockStyle.Top;
            btnUsuarios.FlatAppearance.BorderSize = 0;
            btnUsuarios.FlatStyle = FlatStyle.Flat;
            btnUsuarios.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUsuarios.IconChar = FontAwesome.Sharp.IconChar.Users;
            btnUsuarios.IconColor = Color.Black;
            btnUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnUsuarios.ImageAlign = ContentAlignment.MiddleLeft;
            btnUsuarios.Location = new Point(0, 0);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Size = new Size(325, 55);
            btnUsuarios.TabIndex = 0;
            btnUsuarios.Text = "Usuarios";
            btnUsuarios.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUsuarios.UseVisualStyleBackColor = true;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.DeepSkyBlue;
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(350, 300);
            panel2.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ActiveCaption;
            panel4.Controls.Add(panel5);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(350, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(1548, 150);
            panel4.TabIndex = 1;
            // 
            // panel5
            // 
            panel5.BackColor = SystemColors.ActiveCaption;
            panel5.Controls.Add(lblNUsuario);
            panel5.Controls.Add(lblUsuario);
            panel5.Controls.Add(iconPictureBox1);
            panel5.Controls.Add(label1);
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(0, 40);
            panel5.Name = "panel5";
            panel5.Size = new Size(1548, 110);
            panel5.TabIndex = 2;
            // 
            // lblNUsuario
            // 
            lblNUsuario.AutoSize = true;
            lblNUsuario.Font = new Font("Segoe UI", 12F);
            lblNUsuario.Location = new Point(1381, 37);
            lblNUsuario.Name = "lblNUsuario";
            lblNUsuario.Size = new Size(120, 32);
            lblNUsuario.TabIndex = 3;
            lblNUsuario.Text = "lblUsuario";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 12F);
            lblUsuario.Location = new Point(1288, 37);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(99, 32);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuario:";
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = SystemColors.ActiveCaption;
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.UserCircle;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 65;
            iconPictureBox1.Location = new Point(1224, 22);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(68, 65);
            iconPictureBox1.TabIndex = 1;
            iconPictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Century Gothic", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(637, 87);
            label1.TabIndex = 0;
            label1.Text = "Gestor de ventas";
            // 
            // panelContenedor
            // 
            panelContenedor.Dock = DockStyle.Fill;
            panelContenedor.Location = new Point(350, 150);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1548, 874);
            panelContenedor.TabIndex = 2;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 1024);
            Controls.Add(panelContenedor);
            Controls.Add(panel4);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmPrincipal";
            WindowState = FormWindowState.Maximized;
            Load += FrmPrincipal_Load;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel3;
        private FontAwesome.Sharp.IconButton btnUsuarios;
        private Panel panel2;
        private FontAwesome.Sharp.IconButton btnCancelaciones;
        private FontAwesome.Sharp.IconButton btnBoletos;
        private FontAwesome.Sharp.IconButton btnVentas;
        private FontAwesome.Sharp.IconButton btnPartidos;
        private FontAwesome.Sharp.IconButton btnEquipos;
        private FontAwesome.Sharp.IconButton btnCatalogo;
        private FontAwesome.Sharp.IconButton btnClientes;
        private FontAwesome.Sharp.IconButton btnSalirPrincipal;
        private FontAwesome.Sharp.IconButton btnReportes;
        private FontAwesome.Sharp.IconButton btnFacturacion;
        private Panel panel4;
        private Panel panel5;
        private Label label1;
        private Panel panelContenedor;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Label lblUsuario;
        private Label lblNUsuario;
    }
}