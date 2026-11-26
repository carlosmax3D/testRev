
namespace TemplateCSharp.Forms.Menu
{
    partial class FrmMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenu));
            accesUsuarios = new Button();
            accesClient = new Button();
            accesEstadio = new Button();
            accespartido = new Button();
            acceszona = new Button();
            accesventa = new Button();
            lblBienvenida = new Label();
            accescancel = new Button();
            label2 = new Label();
            reportventa = new Button();
            reportocupacion = new Button();
            reporthistorial = new Button();
            accesAsientos = new Button();
            accesEquipos = new Button();
            btnRegresar = new Button();
            SuspendLayout();
            // 
            // accesUsuarios
            // 
            accesUsuarios.Font = new Font("Calisto MT", 12F);
            accesUsuarios.Location = new Point(66, 64);
            accesUsuarios.Name = "accesUsuarios";
            accesUsuarios.Size = new Size(141, 49);
            accesUsuarios.TabIndex = 0;
            accesUsuarios.Text = "Usuarios";
            accesUsuarios.UseVisualStyleBackColor = true;
            accesUsuarios.Click += accesUsuarios_Click;
            // 
            // accesClient
            // 
            accesClient.Font = new Font("Calisto MT", 12F);
            accesClient.Location = new Point(335, 64);
            accesClient.Name = "accesClient";
            accesClient.Size = new Size(149, 49);
            accesClient.TabIndex = 1;
            accesClient.Text = "Clientes";
            accesClient.UseVisualStyleBackColor = true;
            accesClient.Click += accesClient_Click;
            // 
            // accesEstadio
            // 
            accesEstadio.Font = new Font("Calisto MT", 12F);
            accesEstadio.Location = new Point(615, 64);
            accesEstadio.Name = "accesEstadio";
            accesEstadio.Size = new Size(127, 49);
            accesEstadio.TabIndex = 2;
            accesEstadio.Text = "Estadios";
            accesEstadio.UseVisualStyleBackColor = true;
            accesEstadio.Click += accesEstadio_Click;
            // 
            // accespartido
            // 
            accespartido.Font = new Font("Calisto MT", 12F);
            accespartido.Location = new Point(66, 137);
            accespartido.Name = "accespartido";
            accespartido.Size = new Size(141, 49);
            accespartido.TabIndex = 3;
            accespartido.Text = "Partidos";
            accespartido.UseVisualStyleBackColor = true;
            accespartido.Click += accespartido_Click;
            // 
            // acceszona
            // 
            acceszona.Font = new Font("Calisto MT", 12F);
            acceszona.Location = new Point(335, 137);
            acceszona.Name = "acceszona";
            acceszona.Size = new Size(149, 49);
            acceszona.TabIndex = 4;
            acceszona.Text = "Zonas";
            acceszona.UseVisualStyleBackColor = true;
            acceszona.Click += acceszona_Click;
            // 
            // accesventa
            // 
            accesventa.Font = new Font("Calisto MT", 12F);
            accesventa.Location = new Point(66, 224);
            accesventa.Name = "accesventa";
            accesventa.Size = new Size(200, 49);
            accesventa.TabIndex = 5;
            accesventa.Text = "Venta de Boletos";
            accesventa.UseVisualStyleBackColor = true;
            accesventa.Click += accesventa_Click;
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Calisto MT", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBienvenida.Location = new Point(12, 9);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(255, 21);
            lblBienvenida.TabIndex = 6;
            lblBienvenida.Text = "Bienvenido de regreso, Usuario.";
            lblBienvenida.Click += lblBienvenida_Click;
            // 
            // accescancel
            // 
            accescancel.Font = new Font("Calisto MT", 12F);
            accescancel.Location = new Point(335, 224);
            accescancel.Name = "accescancel";
            accescancel.Size = new Size(149, 49);
            accescancel.TabIndex = 8;
            accescancel.Text = "Cancelaciones";
            accescancel.UseVisualStyleBackColor = true;
            accescancel.Click += accescancel_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(153, 331);
            label2.Name = "label2";
            label2.Size = new Size(509, 28);
            label2.TabIndex = 9;
            label2.Text = " _________________________Reportes _________________________";
            // 
            // reportventa
            // 
            reportventa.Font = new Font("Calisto MT", 12F);
            reportventa.Location = new Point(66, 416);
            reportventa.Name = "reportventa";
            reportventa.Size = new Size(141, 49);
            reportventa.TabIndex = 10;
            reportventa.Text = "Ventas";
            reportventa.UseVisualStyleBackColor = true;
            reportventa.Click += reportventa_Click;
            // 
            // reportocupacion
            // 
            reportocupacion.Font = new Font("Calisto MT", 12F);
            reportocupacion.Location = new Point(335, 416);
            reportocupacion.Name = "reportocupacion";
            reportocupacion.Size = new Size(149, 49);
            reportocupacion.TabIndex = 11;
            reportocupacion.Text = "Ocupacion";
            reportocupacion.UseVisualStyleBackColor = true;
            reportocupacion.Click += reportocupacion_Click;
            // 
            // reporthistorial
            // 
            reporthistorial.Font = new Font("Calisto MT", 12F);
            reporthistorial.Location = new Point(593, 416);
            reporthistorial.Name = "reporthistorial";
            reporthistorial.Size = new Size(149, 49);
            reporthistorial.TabIndex = 12;
            reporthistorial.Text = "Historial";
            reporthistorial.UseVisualStyleBackColor = true;
            reporthistorial.Click += reporthistorial_Click;
            // 
            // accesAsientos
            // 
            accesAsientos.Font = new Font("Calisto MT", 12F);
            accesAsientos.Location = new Point(615, 137);
            accesAsientos.Name = "accesAsientos";
            accesAsientos.Size = new Size(127, 49);
            accesAsientos.TabIndex = 13;
            accesAsientos.Text = "Asientos";
            accesAsientos.UseVisualStyleBackColor = true;
            accesAsientos.Click += accesAsientos_Click;
            // 
            // accesEquipos
            // 
            accesEquipos.Font = new Font("Calisto MT", 12F);
            accesEquipos.Location = new Point(615, 228);
            accesEquipos.Name = "accesEquipos";
            accesEquipos.Size = new Size(127, 45);
            accesEquipos.TabIndex = 14;
            accesEquipos.Text = "Equipos";
            accesEquipos.UseVisualStyleBackColor = true;
            accesEquipos.Click += accesEquipos_Click;
            // 
            // btnRegresar
            // 
            btnRegresar.Font = new Font("Calisto MT", 12F);
            btnRegresar.Location = new Point(335, 538);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(149, 49);
            btnRegresar.TabIndex = 15;
            btnRegresar.Text = "Regresar";
            btnRegresar.UseVisualStyleBackColor = true;
            btnRegresar.Click += btnRegresar_Click;
            // 
            // FrmMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(806, 599);
            Controls.Add(btnRegresar);
            Controls.Add(accesEquipos);
            Controls.Add(accesAsientos);
            Controls.Add(reporthistorial);
            Controls.Add(reportocupacion);
            Controls.Add(reportventa);
            Controls.Add(label2);
            Controls.Add(accescancel);
            Controls.Add(lblBienvenida);
            Controls.Add(accesventa);
            Controls.Add(acceszona);
            Controls.Add(accespartido);
            Controls.Add(accesEstadio);
            Controls.Add(accesClient);
            Controls.Add(accesUsuarios);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmMenu";
            Text = "Sistema de Venta de Boletos";
            FormClosing += FrmMenu_FormClosing;
            Load += FrmMenu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void accescancel_Click_1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Button accesUsuarios;
        private Button accesClient;
        private Button accesEstadio;
        private Button accespartido;
        private Button acceszona;
        private Button accesventa;
        private Label lblBienvenida;
        private Button accescancel;
        private Label label2;
        private Button reportventa;
        private Button reportocupacion;
        private Button reporthistorial;
        private Button accesAsientos;
        private Button accesEquipos;
        private Button btnRegresar;
    }
}
