namespace TemplateCSharp.Forms;

partial class FrmPrincipal
{
    private System.ComponentModel.IContainer components = null;

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
        btnClientes = new Button();
        btnVentas = new Button();
        button1 = new Button();
        btnUsuarios = new Button();
        btnCatalogos = new Button();
        btnCancelaciones = new Button();
        SuspendLayout();
        // 
        // btnClientes
        // 
        btnClientes.Location = new Point(0, 0);
        btnClientes.Name = "btnClientes";
        btnClientes.Size = new Size(94, 29);
        btnClientes.TabIndex = 1;
        btnClientes.Text = "Clientes";
        btnClientes.UseVisualStyleBackColor = true;
        btnClientes.Click += btnClientes_Click_1;
        // 
        // btnVentas
        // 
        btnVentas.Location = new Point(100, 0);
        btnVentas.Name = "btnVentas";
        btnVentas.Size = new Size(152, 29);
        btnVentas.TabIndex = 2;
        btnVentas.Text = "Venta de Boletos";
        btnVentas.UseVisualStyleBackColor = true;
        btnVentas.Click += btnVentas_Click_1;
        // 
        // button1
        // 
        button1.Location = new Point(258, 0);
        button1.Name = "button1";
        button1.Size = new Size(150, 29);
        button1.TabIndex = 3;
        button1.Text = "Reportes y Partidos";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // btnUsuarios
        // 
        btnUsuarios.Location = new Point(414, 0);
        btnUsuarios.Name = "btnUsuarios";
        btnUsuarios.Size = new Size(94, 29);
        btnUsuarios.TabIndex = 4;
        btnUsuarios.Text = "Usuarios";
        btnUsuarios.UseVisualStyleBackColor = true;
        btnUsuarios.Click += btnUsuarios_Click_1;
        // 
        // btnCatalogos
        // 
        btnCatalogos.Location = new Point(514, 0);
        btnCatalogos.Name = "btnCatalogos";
        btnCatalogos.Size = new Size(94, 29);
        btnCatalogos.TabIndex = 5;
        btnCatalogos.Text = "Catalogos";
        btnCatalogos.UseVisualStyleBackColor = true;
        btnCatalogos.Click += btnCatalogos_Click_1;
        // 
        // btnCancelaciones
        // 
        btnCancelaciones.Location = new Point(614, 0);
        btnCancelaciones.Name = "btnCancelaciones";
        btnCancelaciones.Size = new Size(112, 29);
        btnCancelaciones.TabIndex = 6;
        btnCancelaciones.Text = "Cancelaciones";
        btnCancelaciones.UseVisualStyleBackColor = true;
        btnCancelaciones.Click += btnCancelaciones_Click_1;
        // 
        // FrmPrincipal
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 381);
        Controls.Add(btnCancelaciones);
        Controls.Add(btnCatalogos);
        Controls.Add(btnUsuarios);
        Controls.Add(button1);
        Controls.Add(btnVentas);
        Controls.Add(btnClientes);
        Margin = new Padding(3, 4, 3, 4);
        Name = "FrmPrincipal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Reportes y Partidos";
        ResumeLayout(false);
    }

    private Button btnClientes;
    private Button btnVentas;
    private Button button1;
    private Button btnUsuarios;
    private Button btnCatalogos;
    private Button btnCancelaciones;
}
