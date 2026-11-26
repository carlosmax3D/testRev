namespace VentaBoletosApp.Forms
{
    partial class FrmPrincipal
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
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.lblRol = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.lblMarca = new System.Windows.Forms.Label();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClientes = new System.Windows.Forms.Button();
            this.btnVenta = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnEstadios = new System.Windows.Forms.Button();
            this.btnPartidos = new System.Windows.Forms.Button();
            this.btnCancelaciones = new System.Windows.Forms.Button();
            this.btnFacturacion = new System.Windows.Forms.Button();
            this.btnRepVentas = new System.Windows.Forms.Button();
            this.btnRepOcupacion = new System.Windows.Forms.Button();
            this.btnHistorial = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.pnlContenido.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.flpMenu.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlContenido
            //
            this.pnlContenido.Controls.Add(this.lblBienvenida);
            this.pnlContenido.Controls.Add(this.lblRol);
            this.pnlContenido.Controls.Add(this.lblAyuda);
            this.pnlContenido.Location = new System.Drawing.Point(230, 0);
            this.pnlContenido.Size = new System.Drawing.Size(730, 600);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.TabIndex = 0;
            //
            // lblBienvenida
            //
            this.lblBienvenida.Text = "Bienvenido";
            this.lblBienvenida.Location = new System.Drawing.Point(40, 40);
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenida.ForeColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.TabIndex = 1;
            //
            // lblRol
            //
            this.lblRol.Text = "Perfil:";
            this.lblRol.Location = new System.Drawing.Point(40, 85);
            this.lblRol.AutoSize = true;
            this.lblRol.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRol.Name = "lblRol";
            this.lblRol.TabIndex = 2;
            //
            // lblAyuda
            //
            this.lblAyuda.Text = "Seleccione una opción del menú.";
            this.lblAyuda.Location = new System.Drawing.Point(40, 115);
            this.lblAyuda.AutoSize = true;
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.TabIndex = 3;
            //
            // pnlMenu
            //
            this.pnlMenu.Controls.Add(this.lblMarca);
            this.pnlMenu.Controls.Add(this.flpMenu);
            this.pnlMenu.Controls.Add(this.btnCerrarSesion);
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Size = new System.Drawing.Size(230, 600);
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.TabIndex = 4;
            //
            // lblMarca
            //
            this.lblMarca.Text = "LMA7";
            this.lblMarca.Location = new System.Drawing.Point(16, 14);
            this.lblMarca.AutoSize = true;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.TabIndex = 5;
            //
            // flpMenu
            //
            this.flpMenu.Controls.Add(this.btnClientes);
            this.flpMenu.Controls.Add(this.btnVenta);
            this.flpMenu.Controls.Add(this.btnUsuarios);
            this.flpMenu.Controls.Add(this.btnEstadios);
            this.flpMenu.Controls.Add(this.btnPartidos);
            this.flpMenu.Controls.Add(this.btnCancelaciones);
            this.flpMenu.Controls.Add(this.btnFacturacion);
            this.flpMenu.Controls.Add(this.btnRepVentas);
            this.flpMenu.Controls.Add(this.btnRepOcupacion);
            this.flpMenu.Controls.Add(this.btnHistorial);
            this.flpMenu.Location = new System.Drawing.Point(10, 64);
            this.flpMenu.Size = new System.Drawing.Size(210, 470);
            this.flpMenu.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMenu.WrapContents = false;
            this.flpMenu.Name = "flpMenu";
            this.flpMenu.TabIndex = 6;
            //
            // btnClientes
            //
            this.btnClientes.Text = "Clientes";
            this.btnClientes.Location = new System.Drawing.Point(3, 3);
            this.btnClientes.Size = new System.Drawing.Size(200, 38);
            this.btnClientes.BackColor = System.Drawing.Color.FromArgb(52, 84, 125);
            this.btnClientes.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClientes.UseVisualStyleBackColor = false;
            this.btnClientes.FlatAppearance.BorderSize = 0;
            this.btnClientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClientes.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.TabIndex = 7;
            this.btnClientes.Click += new System.EventHandler(this.BtnClientes_Click);
            //
            // btnVenta
            //
            this.btnVenta.Text = "Venta de boletos";
            this.btnVenta.Location = new System.Drawing.Point(3, 3);
            this.btnVenta.Size = new System.Drawing.Size(200, 38);
            this.btnVenta.BackColor = System.Drawing.Color.FromArgb(52, 84, 125);
            this.btnVenta.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVenta.UseVisualStyleBackColor = false;
            this.btnVenta.FlatAppearance.BorderSize = 0;
            this.btnVenta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVenta.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnVenta.Name = "btnVenta";
            this.btnVenta.TabIndex = 8;
            this.btnVenta.Click += new System.EventHandler(this.BtnVenta_Click);
            //
            // btnUsuarios
            //
            this.btnUsuarios.Text = "Usuarios";
            this.btnUsuarios.Location = new System.Drawing.Point(3, 3);
            this.btnUsuarios.Size = new System.Drawing.Size(200, 38);
            this.btnUsuarios.BackColor = System.Drawing.Color.FromArgb(52, 84, 125);
            this.btnUsuarios.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUsuarios.UseVisualStyleBackColor = false;
            this.btnUsuarios.FlatAppearance.BorderSize = 0;
            this.btnUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.TabIndex = 9;
            this.btnUsuarios.Click += new System.EventHandler(this.BtnUsuarios_Click);
            //
            // btnEstadios
            //
            this.btnEstadios.Text = "Estadios y zonas";
            this.btnEstadios.Location = new System.Drawing.Point(3, 3);
            this.btnEstadios.Size = new System.Drawing.Size(200, 38);
            this.btnEstadios.BackColor = System.Drawing.Color.FromArgb(52, 84, 125);
            this.btnEstadios.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnEstadios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstadios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEstadios.UseVisualStyleBackColor = false;
            this.btnEstadios.FlatAppearance.BorderSize = 0;
            this.btnEstadios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstadios.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnEstadios.Name = "btnEstadios";
            this.btnEstadios.TabIndex = 10;
            this.btnEstadios.Click += new System.EventHandler(this.BtnEstadios_Click);
            //
            // btnPartidos
            //
            this.btnPartidos.Text = "Partidos";
            this.btnPartidos.Location = new System.Drawing.Point(3, 3);
            this.btnPartidos.Size = new System.Drawing.Size(200, 38);
            this.btnPartidos.BackColor = System.Drawing.Color.FromArgb(52, 84, 125);
            this.btnPartidos.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnPartidos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPartidos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartidos.UseVisualStyleBackColor = false;
            this.btnPartidos.FlatAppearance.BorderSize = 0;
            this.btnPartidos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPartidos.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnPartidos.Name = "btnPartidos";
            this.btnPartidos.TabIndex = 11;
            this.btnPartidos.Click += new System.EventHandler(this.BtnPartidos_Click);
            //
            // btnCancelaciones
            //
            this.btnCancelaciones.Text = "Cancelaciones";
            this.btnCancelaciones.Location = new System.Drawing.Point(3, 3);
            this.btnCancelaciones.Size = new System.Drawing.Size(200, 38);
            this.btnCancelaciones.BackColor = System.Drawing.Color.FromArgb(52, 84, 125);
            this.btnCancelaciones.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnCancelaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelaciones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelaciones.UseVisualStyleBackColor = false;
            this.btnCancelaciones.FlatAppearance.BorderSize = 0;
            this.btnCancelaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelaciones.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnCancelaciones.Name = "btnCancelaciones";
            this.btnCancelaciones.TabIndex = 12;
            this.btnCancelaciones.Click += new System.EventHandler(this.BtnCancelaciones_Click);
            //
            // btnFacturacion
            //
            this.btnFacturacion.Text = "Facturación";
            this.btnFacturacion.Location = new System.Drawing.Point(3, 3);
            this.btnFacturacion.Size = new System.Drawing.Size(200, 38);
            this.btnFacturacion.BackColor = System.Drawing.Color.FromArgb(52, 84, 125);
            this.btnFacturacion.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnFacturacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFacturacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFacturacion.UseVisualStyleBackColor = false;
            this.btnFacturacion.FlatAppearance.BorderSize = 0;
            this.btnFacturacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFacturacion.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnFacturacion.Name = "btnFacturacion";
            this.btnFacturacion.TabIndex = 13;
            this.btnFacturacion.Click += new System.EventHandler(this.BtnFacturacion_Click);
            //
            // btnRepVentas
            //
            this.btnRepVentas.Text = "Reporte de ventas";
            this.btnRepVentas.Location = new System.Drawing.Point(3, 3);
            this.btnRepVentas.Size = new System.Drawing.Size(200, 38);
            this.btnRepVentas.BackColor = System.Drawing.Color.FromArgb(52, 84, 125);
            this.btnRepVentas.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnRepVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepVentas.UseVisualStyleBackColor = false;
            this.btnRepVentas.FlatAppearance.BorderSize = 0;
            this.btnRepVentas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepVentas.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnRepVentas.Name = "btnRepVentas";
            this.btnRepVentas.TabIndex = 14;
            this.btnRepVentas.Click += new System.EventHandler(this.BtnRepVentas_Click);
            //
            // btnRepOcupacion
            //
            this.btnRepOcupacion.Text = "Reporte de ocupación";
            this.btnRepOcupacion.Location = new System.Drawing.Point(3, 3);
            this.btnRepOcupacion.Size = new System.Drawing.Size(200, 38);
            this.btnRepOcupacion.BackColor = System.Drawing.Color.FromArgb(52, 84, 125);
            this.btnRepOcupacion.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnRepOcupacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepOcupacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepOcupacion.UseVisualStyleBackColor = false;
            this.btnRepOcupacion.FlatAppearance.BorderSize = 0;
            this.btnRepOcupacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepOcupacion.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnRepOcupacion.Name = "btnRepOcupacion";
            this.btnRepOcupacion.TabIndex = 15;
            this.btnRepOcupacion.Click += new System.EventHandler(this.BtnRepOcupacion_Click);
            //
            // btnHistorial
            //
            this.btnHistorial.Text = "Historial de cliente";
            this.btnHistorial.Location = new System.Drawing.Point(3, 3);
            this.btnHistorial.Size = new System.Drawing.Size(200, 38);
            this.btnHistorial.BackColor = System.Drawing.Color.FromArgb(52, 84, 125);
            this.btnHistorial.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnHistorial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorial.UseVisualStyleBackColor = false;
            this.btnHistorial.FlatAppearance.BorderSize = 0;
            this.btnHistorial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistorial.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnHistorial.Name = "btnHistorial";
            this.btnHistorial.TabIndex = 16;
            this.btnHistorial.Click += new System.EventHandler(this.BtnHistorial_Click);
            //
            // btnCerrarSesion
            //
            this.btnCerrarSesion.Text = "Cerrar sesión";
            this.btnCerrarSesion.Location = new System.Drawing.Point(15, 545);
            this.btnCerrarSesion.Size = new System.Drawing.Size(200, 36);
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 0;
            this.btnCerrarSesion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.TabIndex = 17;
            this.btnCerrarSesion.Click += new System.EventHandler(this.BtnCerrarSesion_Click);
            //
            // FrmPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.ClientSize = new System.Drawing.Size(960, 600);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(800, 520);
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Boletera LMA7 - Gestión de Venta de Boletos";
            this.pnlContenido.ResumeLayout(false);
            this.pnlContenido.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            this.flpMenu.ResumeLayout(false);
            this.flpMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnVenta;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnEstadios;
        private System.Windows.Forms.Button btnPartidos;
        private System.Windows.Forms.Button btnCancelaciones;
        private System.Windows.Forms.Button btnFacturacion;
        private System.Windows.Forms.Button btnRepVentas;
        private System.Windows.Forms.Button btnRepOcupacion;
        private System.Windows.Forms.Button btnHistorial;
        private System.Windows.Forms.Button btnCerrarSesion;
    }
}
