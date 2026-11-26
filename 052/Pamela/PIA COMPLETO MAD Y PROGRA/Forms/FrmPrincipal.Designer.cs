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
            txtMenuPrin = new Label();
            botonClientes = new Button();
            botonEstadios = new Button();
            botonPartidos = new Button();
            botonVenta = new Button();
            botonFacturacion = new Button();
            botonCancelacion = new Button();
            botonCerrarSesion = new Button();
            botonUsuarios = new Button();
            botonReportVent = new Button();
            botonROD = new Button();
            SuspendLayout();
            // 
            // txtMenuPrin
            // 
            txtMenuPrin.AutoSize = true;
            txtMenuPrin.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMenuPrin.ForeColor = Color.FromArgb(192, 0, 0);
            txtMenuPrin.Location = new Point(71, 9);
            txtMenuPrin.Name = "txtMenuPrin";
            txtMenuPrin.Size = new Size(219, 30);
            txtMenuPrin.TabIndex = 0;
            txtMenuPrin.Text = "Menú principal";
            // 
            // botonClientes
            // 
            botonClientes.Location = new Point(71, 93);
            botonClientes.Name = "botonClientes";
            botonClientes.Size = new Size(219, 33);
            botonClientes.TabIndex = 1;
            botonClientes.Text = "Gestión de Clientes";
            botonClientes.UseVisualStyleBackColor = true;
            botonClientes.Click += botonClientes_Click;
            // 
            // botonEstadios
            // 
            botonEstadios.Location = new Point(71, 144);
            botonEstadios.Name = "botonEstadios";
            botonEstadios.Size = new Size(219, 33);
            botonEstadios.TabIndex = 2;
            botonEstadios.Text = "Gestión de Estadios";
            botonEstadios.UseVisualStyleBackColor = true;
            botonEstadios.Click += botonEstadios_Click;
            // 
            // botonPartidos
            // 
            botonPartidos.Location = new Point(71, 200);
            botonPartidos.Name = "botonPartidos";
            botonPartidos.Size = new Size(219, 33);
            botonPartidos.TabIndex = 3;
            botonPartidos.Text = "Gestión de Partidos";
            botonPartidos.UseVisualStyleBackColor = true;
            botonPartidos.Click += botonPartidos_Click;
            // 
            // botonVenta
            // 
            botonVenta.Location = new Point(71, 254);
            botonVenta.Name = "botonVenta";
            botonVenta.Size = new Size(219, 33);
            botonVenta.TabIndex = 4;
            botonVenta.Text = "Venta de Boletos";
            botonVenta.UseVisualStyleBackColor = true;
            botonVenta.Click += botonVenta_Click;
            // 
            // botonFacturacion
            // 
            botonFacturacion.Location = new Point(71, 303);
            botonFacturacion.Name = "botonFacturacion";
            botonFacturacion.Size = new Size(219, 33);
            botonFacturacion.TabIndex = 5;
            botonFacturacion.Text = "Facturación";
            botonFacturacion.UseVisualStyleBackColor = true;
            botonFacturacion.Click += botonFacturacion_Click;
            // 
            // botonCancelacion
            // 
            botonCancelacion.Location = new Point(71, 352);
            botonCancelacion.Name = "botonCancelacion";
            botonCancelacion.Size = new Size(219, 33);
            botonCancelacion.TabIndex = 6;
            botonCancelacion.Text = "Cancelaciones";
            botonCancelacion.UseVisualStyleBackColor = true;
            botonCancelacion.Click += botonCancelacion_Click;
            // 
            // botonCerrarSesion
            // 
            botonCerrarSesion.Location = new Point(71, 500);
            botonCerrarSesion.Name = "botonCerrarSesion";
            botonCerrarSesion.Size = new Size(219, 33);
            botonCerrarSesion.TabIndex = 7;
            botonCerrarSesion.Text = "Cerrar Sesión";
            botonCerrarSesion.UseVisualStyleBackColor = true;
            botonCerrarSesion.Click += botonCerrarSesion_Click;
            // 
            // botonUsuarios
            // 
            botonUsuarios.Location = new Point(71, 54);
            botonUsuarios.Name = "botonUsuarios";
            botonUsuarios.Size = new Size(219, 33);
            botonUsuarios.TabIndex = 8;
            botonUsuarios.Text = "Gestión de Usuarios";
            botonUsuarios.UseVisualStyleBackColor = true;
            botonUsuarios.Click += botonUsuarios_Click;
            // 
            // botonReportVent
            // 
            botonReportVent.Location = new Point(71, 400);
            botonReportVent.Name = "botonReportVent";
            botonReportVent.Size = new Size(219, 33);
            botonReportVent.TabIndex = 9;
            botonReportVent.Text = "Reporte de Ventas";
            botonReportVent.UseVisualStyleBackColor = true;
            botonReportVent.Click += botonReportVent_Click;
            // 
            // botonROD
            // 
            botonROD.Location = new Point(71, 448);
            botonROD.Name = "botonROD";
            botonROD.Size = new Size(219, 33);
            botonROD.TabIndex = 10;
            botonROD.Text = "Reporte de ocupación y disponibilidad";
            botonROD.UseVisualStyleBackColor = true;
            botonROD.Click += botonROD_Click;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(372, 545);
            Controls.Add(botonROD);
            Controls.Add(botonReportVent);
            Controls.Add(botonUsuarios);
            Controls.Add(botonCerrarSesion);
            Controls.Add(botonCancelacion);
            Controls.Add(botonFacturacion);
            Controls.Add(botonVenta);
            Controls.Add(botonPartidos);
            Controls.Add(botonEstadios);
            Controls.Add(botonClientes);
            Controls.Add(txtMenuPrin);
            Name = "FrmPrincipal";
            Text = "Menú principal";
            Load += FrmPrincipal_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtMenuPrin;
        private Button botonClientes;
        private Button botonEstadios;
        private Button botonPartidos;
        private Button botonVenta;
        private Button botonFacturacion;
        private Button botonCancelacion;
        private Button botonCerrarSesion;
        private Button botonUsuarios;
        private Button botonReportVent;
        private Button botonROD;
    }
}