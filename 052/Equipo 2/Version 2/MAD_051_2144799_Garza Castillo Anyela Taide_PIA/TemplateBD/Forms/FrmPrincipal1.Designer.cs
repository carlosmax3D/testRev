namespace TemplateCSharp.Forms;

partial class FrmPrincipal1
{
    private System.ComponentModel.IContainer components = null;
    private Button btnProductos;

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
        btnProductos = new Button();

        SuspendLayout();

        // 
        // btnProductos
        // 
        btnProductos.Location = new Point(40, 40);
        btnProductos.Name = "btnProductos";
        btnProductos.Size = new Size(180, 40);
        btnProductos.TabIndex = 0;
        btnProductos.Text = "Productos";
        btnProductos.UseVisualStyleBackColor = true;
        btnProductos.Click += btnProductos_Click;

        // 
        // FrmPrincipal
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(400, 200);
        Controls.Add(btnProductos);
        Name = "FrmPrincipal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Sistema - Menú Principal";

        ResumeLayout(false);
    }
}
