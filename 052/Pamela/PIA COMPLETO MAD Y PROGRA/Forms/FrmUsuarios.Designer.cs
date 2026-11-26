namespace TemplateCSharp.Forms
{
    partial class FrmUsuarios
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
            txtGestUsrios = new Label();
            txtBuscUsrioGU = new Label();
            cBBuscUsrioGU = new ComboBox();
            tBBuscUsrioGU = new TextBox();
            botonBuscUsrioGU = new Button();
            dGVUsrioGU = new DataGridView();
            botonNuevoUsrioGU = new Button();
            botonEditUsrioGU = new Button();
            botonElimUsrioGU = new Button();
            botonRegresarGU = new Button();
            botonMostrarTodosGU = new Button();
            ((System.ComponentModel.ISupportInitialize)dGVUsrioGU).BeginInit();
            SuspendLayout();
            // 
            // txtGestUsrios
            // 
            txtGestUsrios.AutoSize = true;
            txtGestUsrios.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtGestUsrios.ForeColor = Color.Crimson;
            txtGestUsrios.Location = new Point(12, 9);
            txtGestUsrios.Name = "txtGestUsrios";
            txtGestUsrios.Size = new Size(283, 30);
            txtGestUsrios.TabIndex = 0;
            txtGestUsrios.Text = "Gestión de usuarios";
            // 
            // txtBuscUsrioGU
            // 
            txtBuscUsrioGU.AutoSize = true;
            txtBuscUsrioGU.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscUsrioGU.Location = new Point(12, 59);
            txtBuscUsrioGU.Name = "txtBuscUsrioGU";
            txtBuscUsrioGU.Size = new Size(156, 17);
            txtBuscUsrioGU.TabIndex = 1;
            txtBuscUsrioGU.Text = "Buscar usuario por:";
            // 
            // cBBuscUsrioGU
            // 
            cBBuscUsrioGU.DropDownStyle = ComboBoxStyle.DropDownList;
            cBBuscUsrioGU.FormattingEnabled = true;
            cBBuscUsrioGU.Items.AddRange(new object[] { "Correo", "Nombre", "Numero de Nómina" });
            cBBuscUsrioGU.Location = new Point(174, 58);
            cBBuscUsrioGU.Name = "cBBuscUsrioGU";
            cBBuscUsrioGU.Size = new Size(121, 23);
            cBBuscUsrioGU.TabIndex = 2;
            // 
            // tBBuscUsrioGU
            // 
            tBBuscUsrioGU.Location = new Point(301, 58);
            tBBuscUsrioGU.Name = "tBBuscUsrioGU";
            tBBuscUsrioGU.Size = new Size(231, 23);
            tBBuscUsrioGU.TabIndex = 3;
            // 
            // botonBuscUsrioGU
            // 
            botonBuscUsrioGU.Location = new Point(538, 51);
            botonBuscUsrioGU.Name = "botonBuscUsrioGU";
            botonBuscUsrioGU.Size = new Size(97, 35);
            botonBuscUsrioGU.TabIndex = 4;
            botonBuscUsrioGU.Text = "Buscar";
            botonBuscUsrioGU.UseVisualStyleBackColor = true;
            botonBuscUsrioGU.Click += botonBuscUsrioGU_Click;
            // 
            // dGVUsrioGU
            // 
            dGVUsrioGU.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGVUsrioGU.Location = new Point(12, 96);
            dGVUsrioGU.MultiSelect = false;
            dGVUsrioGU.Name = "dGVUsrioGU";
            dGVUsrioGU.ReadOnly = true;
            dGVUsrioGU.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dGVUsrioGU.Size = new Size(720, 163);
            dGVUsrioGU.TabIndex = 5;
            // 
            // botonNuevoUsrioGU
            // 
            botonNuevoUsrioGU.Location = new Point(12, 282);
            botonNuevoUsrioGU.Name = "botonNuevoUsrioGU";
            botonNuevoUsrioGU.Size = new Size(103, 33);
            botonNuevoUsrioGU.TabIndex = 6;
            botonNuevoUsrioGU.Text = "Nuevo Usuario";
            botonNuevoUsrioGU.UseVisualStyleBackColor = true;
            botonNuevoUsrioGU.Click += botonNuevoUsrioGU_Click;
            // 
            // botonEditUsrioGU
            // 
            botonEditUsrioGU.Location = new Point(133, 282);
            botonEditUsrioGU.Name = "botonEditUsrioGU";
            botonEditUsrioGU.Size = new Size(103, 33);
            botonEditUsrioGU.TabIndex = 7;
            botonEditUsrioGU.Text = "Editar";
            botonEditUsrioGU.UseVisualStyleBackColor = true;
            botonEditUsrioGU.Click += botonEditUsrioGU_Click;
            // 
            // botonElimUsrioGU
            // 
            botonElimUsrioGU.Location = new Point(253, 282);
            botonElimUsrioGU.Name = "botonElimUsrioGU";
            botonElimUsrioGU.Size = new Size(103, 33);
            botonElimUsrioGU.TabIndex = 8;
            botonElimUsrioGU.Text = "Eliminar";
            botonElimUsrioGU.UseVisualStyleBackColor = true;
            botonElimUsrioGU.Click += botonElimUsrioGU_Click;
            // 
            // botonRegresarGU
            // 
            botonRegresarGU.Location = new Point(629, 312);
            botonRegresarGU.Name = "botonRegresarGU";
            botonRegresarGU.Size = new Size(103, 33);
            botonRegresarGU.TabIndex = 9;
            botonRegresarGU.Text = "Regresar";
            botonRegresarGU.UseVisualStyleBackColor = true;
            botonRegresarGU.Click += botonRegresarGU_Click;
            // 
            // botonMostrarTodosGU
            // 
            botonMostrarTodosGU.Location = new Point(641, 51);
            botonMostrarTodosGU.Name = "botonMostrarTodosGU";
            botonMostrarTodosGU.Size = new Size(97, 35);
            botonMostrarTodosGU.TabIndex = 10;
            botonMostrarTodosGU.Text = "Mostrar todos";
            botonMostrarTodosGU.UseVisualStyleBackColor = true;
            botonMostrarTodosGU.Click += botonMostrarTodosGU_Click;
            // 
            // FrmUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(744, 357);
            Controls.Add(botonMostrarTodosGU);
            Controls.Add(botonRegresarGU);
            Controls.Add(botonElimUsrioGU);
            Controls.Add(botonEditUsrioGU);
            Controls.Add(botonNuevoUsrioGU);
            Controls.Add(dGVUsrioGU);
            Controls.Add(botonBuscUsrioGU);
            Controls.Add(tBBuscUsrioGU);
            Controls.Add(cBBuscUsrioGU);
            Controls.Add(txtBuscUsrioGU);
            Controls.Add(txtGestUsrios);
            Name = "FrmUsuarios";
            Text = "Gestión de usuarios";
            Load += FrmUsuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dGVUsrioGU).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtGestUsrios;
        private Label txtBuscUsrioGU;
        private ComboBox cBBuscUsrioGU;
        private TextBox tBBuscUsrioGU;
        private Button botonBuscUsrioGU;
        private DataGridView dGVUsrioGU;
        private Button botonNuevoUsrioGU;
        private Button botonEditUsrioGU;
        private Button botonElimUsrioGU;
        private Button botonRegresarGU;
        private Button botonMostrarTodosGU;
    }
}