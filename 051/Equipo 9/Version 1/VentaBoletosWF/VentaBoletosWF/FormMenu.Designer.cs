namespace VentaBoletosWF
{
    partial class FormMenu
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnZonas = new System.Windows.Forms.Button();
            this.btnPartidos = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnEstadios = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnCancelaciones = new System.Windows.Forms.Button();
            this.btnVentas = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.btnDisponibilidad = new System.Windows.Forms.Button();
            this.btnFacturacion = new System.Windows.Forms.Button();
            this.btnHistorialClientes = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Controls.Add(this.btnZonas);
            this.panel1.Controls.Add(this.btnPartidos);
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Controls.Add(this.btnEstadios);
            this.panel1.Controls.Add(this.btnReportes);
            this.panel1.Controls.Add(this.btnCancelaciones);
            this.panel1.Controls.Add(this.btnVentas);
            this.panel1.Controls.Add(this.btnClientes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 450);
            this.panel1.TabIndex = 0;
            // 
            // btnZonas
            // 
            this.btnZonas.Location = new System.Drawing.Point(54, 308);
            this.btnZonas.Name = "btnZonas";
            this.btnZonas.Size = new System.Drawing.Size(75, 23);
            this.btnZonas.TabIndex = 3;
            this.btnZonas.Text = "Zona";
            this.btnZonas.UseVisualStyleBackColor = true;
            this.btnZonas.Click += new System.EventHandler(this.btnZona_Click);
            // 
            // btnPartidos
            // 
            this.btnPartidos.Location = new System.Drawing.Point(54, 268);
            this.btnPartidos.Name = "btnPartidos";
            this.btnPartidos.Size = new System.Drawing.Size(75, 23);
            this.btnPartidos.TabIndex = 2;
            this.btnPartidos.Text = "Partidos";
            this.btnPartidos.UseVisualStyleBackColor = true;
            this.btnPartidos.Click += new System.EventHandler(this.btnPartidos_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(54, 191);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnEstadios
            // 
            this.btnEstadios.Location = new System.Drawing.Point(54, 230);
            this.btnEstadios.Name = "btnEstadios";
            this.btnEstadios.Size = new System.Drawing.Size(75, 23);
            this.btnEstadios.TabIndex = 1;
            this.btnEstadios.Text = "Estadios";
            this.btnEstadios.UseVisualStyleBackColor = true;
            this.btnEstadios.Click += new System.EventHandler(this.btnEstadios_Click);
            // 
            // btnReportes
            // 
            this.btnReportes.Location = new System.Drawing.Point(54, 148);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(75, 23);
            this.btnReportes.TabIndex = 1;
            this.btnReportes.Text = "Reportes";
            this.btnReportes.UseVisualStyleBackColor = true;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // btnCancelaciones
            // 
            this.btnCancelaciones.Location = new System.Drawing.Point(54, 107);
            this.btnCancelaciones.Name = "btnCancelaciones";
            this.btnCancelaciones.Size = new System.Drawing.Size(75, 23);
            this.btnCancelaciones.TabIndex = 1;
            this.btnCancelaciones.Text = "Cancelaciones";
            this.btnCancelaciones.UseVisualStyleBackColor = true;
            this.btnCancelaciones.Click += new System.EventHandler(this.btnCancelaciones_Click);
            // 
            // btnVentas
            // 
            this.btnVentas.Location = new System.Drawing.Point(54, 65);
            this.btnVentas.Name = "btnVentas";
            this.btnVentas.Size = new System.Drawing.Size(75, 23);
            this.btnVentas.TabIndex = 1;
            this.btnVentas.Text = "Ventas";
            this.btnVentas.UseVisualStyleBackColor = true;
            this.btnVentas.Click += new System.EventHandler(this.btnVentas_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.Location = new System.Drawing.Point(54, 22);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(75, 23);
            this.btnClientes.TabIndex = 1;
            this.btnClientes.Text = "Clientes";
            this.btnClientes.UseVisualStyleBackColor = true;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // btnDisponibilidad
            // 
            this.btnDisponibilidad.Location = new System.Drawing.Point(249, 45);
            this.btnDisponibilidad.Name = "btnDisponibilidad";
            this.btnDisponibilidad.Size = new System.Drawing.Size(75, 23);
            this.btnDisponibilidad.TabIndex = 1;
            this.btnDisponibilidad.Text = "Disponibilidad";
            this.btnDisponibilidad.UseVisualStyleBackColor = true;
            this.btnDisponibilidad.Click += new System.EventHandler(this.btnDisponibilidad_Click);
            // 
            // btnFacturacion
            // 
            this.btnFacturacion.Location = new System.Drawing.Point(249, 90);
            this.btnFacturacion.Name = "btnFacturacion";
            this.btnFacturacion.Size = new System.Drawing.Size(75, 23);
            this.btnFacturacion.TabIndex = 2;
            this.btnFacturacion.Text = "Facturacion";
            this.btnFacturacion.UseVisualStyleBackColor = true;
            this.btnFacturacion.Click += new System.EventHandler(this.btnFacturacion_Click);
            // 
            // btnHistorialClientes
            // 
            this.btnHistorialClientes.Location = new System.Drawing.Point(249, 135);
            this.btnHistorialClientes.Name = "btnHistorialClientes";
            this.btnHistorialClientes.Size = new System.Drawing.Size(75, 23);
            this.btnHistorialClientes.TabIndex = 3;
            this.btnHistorialClientes.Text = "Historial Clientes";
            this.btnHistorialClientes.UseVisualStyleBackColor = true;
            this.btnHistorialClientes.Click += new System.EventHandler(this.btnHistorialClientes_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(249, 180);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(75, 23);
            this.btnDashboard.TabIndex = 4;
            this.btnDashboard.Text = "Dashboard Administrativo";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDashboard);
            this.Controls.Add(this.btnHistorialClientes);
            this.Controls.Add(this.btnFacturacion);
            this.Controls.Add(this.btnDisponibilidad);
            this.Controls.Add(this.panel1);
            this.Name = "FormMenu";
            this.Text = "Menu principal";
            this.Load += new System.EventHandler(this.FormMenu_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelaciones;
        private System.Windows.Forms.Button btnVentas;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnPartidos;
        private System.Windows.Forms.Button btnEstadios;
        private System.Windows.Forms.Button btnZonas;
        private System.Windows.Forms.Button btnDisponibilidad;
        private System.Windows.Forms.Button btnFacturacion;
        private System.Windows.Forms.Button btnHistorialClientes;
        private System.Windows.Forms.Button btnDashboard;
    }
}