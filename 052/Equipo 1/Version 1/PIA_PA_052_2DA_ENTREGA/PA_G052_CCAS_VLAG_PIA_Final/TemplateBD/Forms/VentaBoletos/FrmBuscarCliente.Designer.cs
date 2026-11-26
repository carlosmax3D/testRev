namespace TemplateCSharp.Forms.VentaBoletos
{
    partial class FrmBuscarCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBuscarCliente));
            label1 = new Label();
            label2 = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            label3 = new Label();
            lblClienteSeleccionado = new Label();
            btnRegresar = new Button();
            btnNuevoCliente = new Button();
            labell4 = new Label();
            lblCorreo = new Label();
            btnConfirmar = new Button();
            dgvClientes = new DataGridView();
            Nombre = new DataGridViewTextBoxColumn();
            ApellidoPa = new DataGridViewTextBoxColumn();
            Correo = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // txtBuscar
            // 
            resources.ApplyResources(txtBuscar, "txtBuscar");
            txtBuscar.Name = "txtBuscar";
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // btnBuscar
            // 
            resources.ApplyResources(btnBuscar, "btnBuscar");
            btnBuscar.Name = "btnBuscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // lblClienteSeleccionado
            // 
            resources.ApplyResources(lblClienteSeleccionado, "lblClienteSeleccionado");
            lblClienteSeleccionado.Name = "lblClienteSeleccionado";
            lblClienteSeleccionado.Click += lblClienteSeleccionado_Click;
            // 
            // btnRegresar
            // 
            resources.ApplyResources(btnRegresar, "btnRegresar");
            btnRegresar.Name = "btnRegresar";
            btnRegresar.UseVisualStyleBackColor = true;
            btnRegresar.Click += btnRegresar_Click;
            // 
            // btnNuevoCliente
            // 
            resources.ApplyResources(btnNuevoCliente, "btnNuevoCliente");
            btnNuevoCliente.Name = "btnNuevoCliente";
            btnNuevoCliente.UseVisualStyleBackColor = true;
            btnNuevoCliente.Click += btnNuevoCliente_Click;
            // 
            // labell4
            // 
            resources.ApplyResources(labell4, "labell4");
            labell4.Name = "labell4";
            // 
            // lblCorreo
            // 
            resources.ApplyResources(lblCorreo, "lblCorreo");
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Click += lblCorreo_Click;
            // 
            // btnConfirmar
            // 
            resources.ApplyResources(btnConfirmar, "btnConfirmar");
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click_1;
            // 
            // dgvClientes
            // 
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Columns.AddRange(new DataGridViewColumn[] { Nombre, ApellidoPa, Correo });
            resources.ApplyResources(dgvClientes, "dgvClientes");
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.CellClick += dgvClientes_CellClick;
            // 
            // Nombre
            // 
            resources.ApplyResources(Nombre, "Nombre");
            Nombre.Name = "Nombre";
            Nombre.ReadOnly = true;
            // 
            // ApellidoPa
            // 
            resources.ApplyResources(ApellidoPa, "ApellidoPa");
            ApellidoPa.Name = "ApellidoPa";
            ApellidoPa.ReadOnly = true;
            // 
            // Correo
            // 
            resources.ApplyResources(Correo, "Correo");
            Correo.Name = "Correo";
            Correo.ReadOnly = true;
            // 
            // FrmBuscarCliente
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            Controls.Add(dgvClientes);
            Controls.Add(btnConfirmar);
            Controls.Add(lblCorreo);
            Controls.Add(labell4);
            Controls.Add(btnNuevoCliente);
            Controls.Add(btnRegresar);
            Controls.Add(lblClienteSeleccionado);
            Controls.Add(label3);
            Controls.Add(btnBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(label2);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmBuscarCliente";
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Label label3;
        private Label lblClienteSeleccionado;
        private Button btnRegresar;
        private Button btnNuevoCliente;
        private Label labell4;
        private Label lblCorreo;
        private Button btnConfirmar;
        private DataGridView dgvClientes;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn ApellidoPa;
        private DataGridViewTextBoxColumn Correo;
    }
}
