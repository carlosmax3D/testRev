namespace Pantalla_MotivoCancelacionVB_PIA_PA
{
    partial class pantalla_motivoCancelacionVB
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
            txtbox1_motivoCancelacion = new TextBox();
            label1_motivoCancelacion = new Label();
            boton1_motivoCancelacion = new Button();
            boton2_motivoCancelacion = new Button();
            SuspendLayout();
            // 
            // txtbox1_motivoCancelacion
            // 
            txtbox1_motivoCancelacion.Location = new Point(55, 114);
            txtbox1_motivoCancelacion.Multiline = true;
            txtbox1_motivoCancelacion.Name = "txtbox1_motivoCancelacion";
            txtbox1_motivoCancelacion.PlaceholderText = "Redacta motivo de cancelacion";
            txtbox1_motivoCancelacion.Size = new Size(480, 202);
            txtbox1_motivoCancelacion.TabIndex = 0;
            txtbox1_motivoCancelacion.TextChanged += txtbox1_motivoCancelacion_TextChanged;
            // 
            // label1_motivoCancelacion
            // 
            label1_motivoCancelacion.AutoSize = true;
            label1_motivoCancelacion.Font = new Font("Sitka Subheading", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1_motivoCancelacion.Location = new Point(55, 60);
            label1_motivoCancelacion.Name = "label1_motivoCancelacion";
            label1_motivoCancelacion.Size = new Size(480, 35);
            label1_motivoCancelacion.TabIndex = 1;
            label1_motivoCancelacion.Text = "Motivo de cancelacion de la venta de boleto:";
            label1_motivoCancelacion.Click += label1_motivoCancelacion_Click;
            // 
            // boton1_motivoCancelacion
            // 
            boton1_motivoCancelacion.BackColor = Color.PaleTurquoise;
            boton1_motivoCancelacion.Location = new Point(423, 378);
            boton1_motivoCancelacion.Name = "boton1_motivoCancelacion";
            boton1_motivoCancelacion.Size = new Size(112, 34);
            boton1_motivoCancelacion.TabIndex = 2;
            boton1_motivoCancelacion.Text = "Siguiente";
            boton1_motivoCancelacion.UseVisualStyleBackColor = false;
            boton1_motivoCancelacion.Click += boton1_motivoCancelacion_Click;
            // 
            // boton2_motivoCancelacion
            // 
            boton2_motivoCancelacion.BackColor = Color.LightCoral;
            boton2_motivoCancelacion.Location = new Point(55, 378);
            boton2_motivoCancelacion.Name = "boton2_motivoCancelacion";
            boton2_motivoCancelacion.Size = new Size(112, 34);
            boton2_motivoCancelacion.TabIndex = 3;
            boton2_motivoCancelacion.Text = "Regresar";
            boton2_motivoCancelacion.UseVisualStyleBackColor = false;
            boton2_motivoCancelacion.Click += boton2_motivoCancelacion_Click;
            // 
            // pantalla_motivoCancelacionVB
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(614, 450);
            Controls.Add(boton2_motivoCancelacion);
            Controls.Add(boton1_motivoCancelacion);
            Controls.Add(label1_motivoCancelacion);
            Controls.Add(txtbox1_motivoCancelacion);
            Name = "pantalla_motivoCancelacionVB";
            Text = "Motivo de Cancelacion";
            Load += pantalla_motivoCancelacionVB_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtbox1_motivoCancelacion;
        private Label label1_motivoCancelacion;
        private Button boton1_motivoCancelacion;
        private Button boton2_motivoCancelacion;
    }
}
