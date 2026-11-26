namespace TemplateCSharp.Forms.Cancelaciones
{
    partial class FrmCancelacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCancelacion));
            busquedacancel = new Label();
            label2 = new Label();
            txtFolio = new TextBox();
            btnBuscar = new Button();
            SuspendLayout();
            // 
            // busquedacancel
            // 
            busquedacancel.AutoSize = true;
            busquedacancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            busquedacancel.Location = new Point(22, 21);
            busquedacancel.Name = "busquedacancel";
            busquedacancel.Size = new Size(214, 28);
            busquedacancel.TabIndex = 0;
            busquedacancel.Text = "Cancelacion de Venta";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(34, 76);
            label2.Name = "label2";
            label2.Size = new Size(163, 21);
            label2.TabIndex = 1;
            label2.Text = "Código de compra: ";
            // 
            // txtFolio
            // 
            txtFolio.Location = new Point(203, 75);
            txtFolio.Name = "txtFolio";
            txtFolio.Size = new Size(277, 27);
            txtFolio.TabIndex = 2;
            txtFolio.TextChanged += txtFolio_TextChanged;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(498, 73);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 3;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // FrmCancelacion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(682, 152);
            Controls.Add(btnBuscar);
            Controls.Add(txtFolio);
            Controls.Add(label2);
            Controls.Add(busquedacancel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmCancelacion";
            Text = "Cancelacion Venta";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label busquedacancel;
        private Label label2;
        private TextBox txtFolio;
        private Button btnBuscar;
    }
}
