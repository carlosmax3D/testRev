namespace Menu_p_Operativo
{
    partial class dclientes_eligio_registrar
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
            etiqueta_ingresatal = new Label();
            ingresa_rfc = new TextBox();
            primer_nom_client = new TextBox();
            segundo_nom_client = new TextBox();
            apellido_pater_client = new TextBox();
            apellido_mater_client = new TextBox();
            mail_cliente = new TextBox();
            tel_casa_client = new TextBox();
            celular_cliente = new TextBox();
            fecha_naci_client = new TextBox();
            domicilio_calle_client = new TextBox();
            domicilio_colonia_client = new TextBox();
            domicilio_ciudad_client = new TextBox();
            domicilio_numviv_client = new TextBox();
            domicilio_municipio_client = new TextBox();
            etiqueta_datper_client = new Label();
            etiqueta_mdcontacto = new Label();
            etiqueta_domicilio = new Label();
            save_registro_client = new Button();
            cancelar_reg_cliente = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            SuspendLayout();
            // 
            // etiqueta_ingresatal
            // 
            etiqueta_ingresatal.AutoSize = true;
            etiqueta_ingresatal.ForeColor = SystemColors.Window;
            etiqueta_ingresatal.Location = new Point(32, 29);
            etiqueta_ingresatal.Margin = new Padding(4, 0, 4, 0);
            etiqueta_ingresatal.Name = "etiqueta_ingresatal";
            etiqueta_ingresatal.Size = new Size(273, 25);
            etiqueta_ingresatal.TabIndex = 0;
            etiqueta_ingresatal.Text = "Complete los siguientes campos:";
            // 
            // ingresa_rfc
            // 
            ingresa_rfc.BackColor = SystemColors.ActiveCaption;
            ingresa_rfc.ForeColor = SystemColors.ActiveCaptionText;
            ingresa_rfc.Location = new Point(61, 153);
            ingresa_rfc.Margin = new Padding(4);
            ingresa_rfc.MaxLength = 13;
            ingresa_rfc.Name = "ingresa_rfc";
            ingresa_rfc.PlaceholderText = "Ingresa RFC cliente";
            ingresa_rfc.Size = new Size(454, 31);
            ingresa_rfc.TabIndex = 1;
            ingresa_rfc.TextChanged += ingresa_rfc_TextChanged;
            ingresa_rfc.KeyPress += ingresa_rfc_KeyPress;
            // 
            // primer_nom_client
            // 
            primer_nom_client.BackColor = SystemColors.ActiveCaption;
            primer_nom_client.Location = new Point(60, 235);
            primer_nom_client.Margin = new Padding(4);
            primer_nom_client.MaxLength = 50;
            primer_nom_client.Name = "primer_nom_client";
            primer_nom_client.PlaceholderText = "Primer Nombre";
            primer_nom_client.Size = new Size(453, 31);
            primer_nom_client.TabIndex = 2;
            primer_nom_client.KeyPress += primer_nom_client_KeyPress;
            // 
            // segundo_nom_client
            // 
            segundo_nom_client.BackColor = SystemColors.ActiveCaption;
            segundo_nom_client.Location = new Point(61, 319);
            segundo_nom_client.Margin = new Padding(4);
            segundo_nom_client.MaxLength = 50;
            segundo_nom_client.Name = "segundo_nom_client";
            segundo_nom_client.PlaceholderText = "Segundo Nombre";
            segundo_nom_client.Size = new Size(454, 31);
            segundo_nom_client.TabIndex = 3;
            segundo_nom_client.KeyPress += segundo_nom_client_KeyPress;
            // 
            // apellido_pater_client
            // 
            apellido_pater_client.BackColor = SystemColors.ActiveCaption;
            apellido_pater_client.Location = new Point(59, 406);
            apellido_pater_client.Margin = new Padding(4);
            apellido_pater_client.MaxLength = 50;
            apellido_pater_client.Name = "apellido_pater_client";
            apellido_pater_client.PlaceholderText = "Apellido Paterno";
            apellido_pater_client.Size = new Size(454, 31);
            apellido_pater_client.TabIndex = 4;
            apellido_pater_client.KeyPress += apellido_pater_client_KeyPress;
            // 
            // apellido_mater_client
            // 
            apellido_mater_client.BackColor = SystemColors.ActiveCaption;
            apellido_mater_client.Location = new Point(59, 495);
            apellido_mater_client.Margin = new Padding(4);
            apellido_mater_client.MaxLength = 50;
            apellido_mater_client.Name = "apellido_mater_client";
            apellido_mater_client.PlaceholderText = "Apellido Materno";
            apellido_mater_client.Size = new Size(453, 31);
            apellido_mater_client.TabIndex = 5;
            apellido_mater_client.KeyPress += apellido_mater_client_KeyPress;
            // 
            // mail_cliente
            // 
            mail_cliente.BackColor = SystemColors.ActiveCaption;
            mail_cliente.Location = new Point(1021, 153);
            mail_cliente.Margin = new Padding(4);
            mail_cliente.Name = "mail_cliente";
            mail_cliente.PlaceholderText = "correoelectronico@ejemplo.com";
            mail_cliente.Size = new Size(454, 31);
            mail_cliente.TabIndex = 7;
            mail_cliente.TextChanged += mail_cliente_TextChanged;
            // 
            // tel_casa_client
            // 
            tel_casa_client.BackColor = SystemColors.ActiveCaption;
            tel_casa_client.Location = new Point(1021, 235);
            tel_casa_client.Margin = new Padding(4);
            tel_casa_client.MaxLength = 10;
            tel_casa_client.Name = "tel_casa_client";
            tel_casa_client.PlaceholderText = "Telefono de casa";
            tel_casa_client.Size = new Size(454, 31);
            tel_casa_client.TabIndex = 8;
            tel_casa_client.KeyPress += tel_casa_client_KeyPress;
            // 
            // celular_cliente
            // 
            celular_cliente.BackColor = SystemColors.ActiveCaption;
            celular_cliente.Location = new Point(1021, 319);
            celular_cliente.Margin = new Padding(4);
            celular_cliente.MaxLength = 10;
            celular_cliente.Name = "celular_cliente";
            celular_cliente.PlaceholderText = "Numero Celular";
            celular_cliente.Size = new Size(454, 31);
            celular_cliente.TabIndex = 9;
            celular_cliente.KeyPress += celular_cliente_KeyPress;
            // 
            // fecha_naci_client
            // 
            fecha_naci_client.BackColor = SystemColors.ActiveCaption;
            fecha_naci_client.Location = new Point(59, 587);
            fecha_naci_client.Margin = new Padding(4);
            fecha_naci_client.Name = "fecha_naci_client";
            fecha_naci_client.PlaceholderText = "DD/MM/AAAA";
            fecha_naci_client.Size = new Size(323, 31);
            fecha_naci_client.TabIndex = 6;
            fecha_naci_client.TextChanged += fecha_naci_client_TextChanged;
            // 
            // domicilio_calle_client
            // 
            domicilio_calle_client.BackColor = SystemColors.ActiveCaption;
            domicilio_calle_client.Location = new Point(601, 319);
            domicilio_calle_client.Margin = new Padding(4);
            domicilio_calle_client.MaxLength = 50;
            domicilio_calle_client.Name = "domicilio_calle_client";
            domicilio_calle_client.PlaceholderText = "Calle";
            domicilio_calle_client.Size = new Size(323, 31);
            domicilio_calle_client.TabIndex = 12;
            domicilio_calle_client.KeyPress += domicilio_calle_client_KeyPress;
            // 
            // domicilio_colonia_client
            // 
            domicilio_colonia_client.BackColor = SystemColors.ActiveCaption;
            domicilio_colonia_client.Location = new Point(601, 406);
            domicilio_colonia_client.Margin = new Padding(4);
            domicilio_colonia_client.MaxLength = 50;
            domicilio_colonia_client.Name = "domicilio_colonia_client";
            domicilio_colonia_client.PlaceholderText = "Colonia";
            domicilio_colonia_client.Size = new Size(323, 31);
            domicilio_colonia_client.TabIndex = 13;
            domicilio_colonia_client.KeyPress += domicilio_colonia_client_KeyPress;
            // 
            // domicilio_ciudad_client
            // 
            domicilio_ciudad_client.BackColor = SystemColors.ActiveCaption;
            domicilio_ciudad_client.Location = new Point(601, 235);
            domicilio_ciudad_client.Margin = new Padding(4);
            domicilio_ciudad_client.MaxLength = 50;
            domicilio_ciudad_client.Name = "domicilio_ciudad_client";
            domicilio_ciudad_client.PlaceholderText = "Ciudad";
            domicilio_ciudad_client.Size = new Size(323, 31);
            domicilio_ciudad_client.TabIndex = 11;
            domicilio_ciudad_client.KeyPress += domicilio_ciudad_client_KeyPress;
            // 
            // domicilio_numviv_client
            // 
            domicilio_numviv_client.BackColor = SystemColors.ActiveCaption;
            domicilio_numviv_client.Location = new Point(601, 495);
            domicilio_numviv_client.Margin = new Padding(4);
            domicilio_numviv_client.MaxLength = 10;
            domicilio_numviv_client.Name = "domicilio_numviv_client";
            domicilio_numviv_client.PlaceholderText = "Numero de Vivienda";
            domicilio_numviv_client.Size = new Size(323, 31);
            domicilio_numviv_client.TabIndex = 14;
            domicilio_numviv_client.KeyPress += domicilio_numviv_client_KeyPress;
            // 
            // domicilio_municipio_client
            // 
            domicilio_municipio_client.BackColor = SystemColors.ActiveCaption;
            domicilio_municipio_client.Location = new Point(601, 153);
            domicilio_municipio_client.Margin = new Padding(4);
            domicilio_municipio_client.MaxLength = 50;
            domicilio_municipio_client.Name = "domicilio_municipio_client";
            domicilio_municipio_client.PlaceholderText = "Municipio";
            domicilio_municipio_client.Size = new Size(323, 31);
            domicilio_municipio_client.TabIndex = 10;
            domicilio_municipio_client.KeyPress += domicilio_municipio_client_KeyPress;
            // 
            // etiqueta_datper_client
            // 
            etiqueta_datper_client.AutoSize = true;
            etiqueta_datper_client.ForeColor = SystemColors.Window;
            etiqueta_datper_client.Location = new Point(88, 72);
            etiqueta_datper_client.Margin = new Padding(4, 0, 4, 0);
            etiqueta_datper_client.Name = "etiqueta_datper_client";
            etiqueta_datper_client.Size = new Size(149, 25);
            etiqueta_datper_client.TabIndex = 15;
            etiqueta_datper_client.Text = "Datos personales";
            // 
            // etiqueta_mdcontacto
            // 
            etiqueta_mdcontacto.AutoSize = true;
            etiqueta_mdcontacto.ForeColor = SystemColors.Window;
            etiqueta_mdcontacto.Location = new Point(1021, 72);
            etiqueta_mdcontacto.Margin = new Padding(4, 0, 4, 0);
            etiqueta_mdcontacto.Name = "etiqueta_mdcontacto";
            etiqueta_mdcontacto.Size = new Size(170, 25);
            etiqueta_mdcontacto.TabIndex = 16;
            etiqueta_mdcontacto.Text = "Medios de contacto";
            // 
            // etiqueta_domicilio
            // 
            etiqueta_domicilio.AutoSize = true;
            etiqueta_domicilio.ForeColor = SystemColors.Window;
            etiqueta_domicilio.Location = new Point(601, 72);
            etiqueta_domicilio.Margin = new Padding(4, 0, 4, 0);
            etiqueta_domicilio.Name = "etiqueta_domicilio";
            etiqueta_domicilio.Size = new Size(87, 25);
            etiqueta_domicilio.TabIndex = 17;
            etiqueta_domicilio.Text = "Domicilio";
            // 
            // save_registro_client
            // 
            save_registro_client.BackColor = SystemColors.Window;
            save_registro_client.Location = new Point(1294, 558);
            save_registro_client.Margin = new Padding(4);
            save_registro_client.Name = "save_registro_client";
            save_registro_client.Size = new Size(181, 82);
            save_registro_client.TabIndex = 18;
            save_registro_client.Text = "Guardar";
            save_registro_client.UseVisualStyleBackColor = false;
            save_registro_client.Click += save_registro_client_Click;
            // 
            // cancelar_reg_cliente
            // 
            cancelar_reg_cliente.Location = new Point(997, 558);
            cancelar_reg_cliente.Margin = new Padding(4);
            cancelar_reg_cliente.Name = "cancelar_reg_cliente";
            cancelar_reg_cliente.Size = new Size(181, 82);
            cancelar_reg_cliente.TabIndex = 19;
            cancelar_reg_cliente.Text = "Cancelar";
            cancelar_reg_cliente.UseVisualStyleBackColor = true;
            cancelar_reg_cliente.Click += cancelar_reg_cliente_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.Window;
            label1.Location = new Point(61, 124);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(105, 25);
            label1.TabIndex = 20;
            label1.Text = "RFC Cliente:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.Window;
            label2.Location = new Point(61, 206);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(135, 25);
            label2.TabIndex = 21;
            label2.Text = "Primer nombre:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.Window;
            label3.Location = new Point(61, 290);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(156, 25);
            label3.TabIndex = 22;
            label3.Text = "Segundo nombre:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.Window;
            label4.Location = new Point(60, 377);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(149, 25);
            label4.TabIndex = 23;
            label4.Text = "Apellido paterno:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.Window;
            label5.Location = new Point(60, 466);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(154, 25);
            label5.TabIndex = 24;
            label5.Text = "Apellido materno:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.Window;
            label6.Location = new Point(60, 558);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(178, 25);
            label6.TabIndex = 25;
            label6.Text = "Fecha de nacimiento:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = SystemColors.Window;
            label7.Location = new Point(601, 124);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(94, 25);
            label7.TabIndex = 26;
            label7.Text = "Municipio:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = SystemColors.Window;
            label8.Location = new Point(601, 206);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(72, 25);
            label8.TabIndex = 27;
            label8.Text = "Ciudad:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = SystemColors.Window;
            label9.Location = new Point(601, 290);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(53, 25);
            label9.TabIndex = 28;
            label9.Text = "Calle:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = SystemColors.Window;
            label10.Location = new Point(601, 377);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(72, 25);
            label10.TabIndex = 29;
            label10.Text = "Colonia";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = SystemColors.Window;
            label11.Location = new Point(601, 466);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(176, 25);
            label11.TabIndex = 30;
            label11.Text = "Numero de vivienda:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = SystemColors.Window;
            label12.Location = new Point(1021, 124);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(161, 25);
            label12.TabIndex = 31;
            label12.Text = "Correo electronico:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.ForeColor = SystemColors.Window;
            label13.Location = new Point(1021, 206);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(104, 25);
            label13.TabIndex = 32;
            label13.Text = "Tel. de casa:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.ForeColor = SystemColors.Window;
            label14.Location = new Point(1021, 290);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(139, 25);
            label14.TabIndex = 33;
            label14.Text = "Num. de celular:";
            // 
            // dclientes_eligio_registrar
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1514, 693);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cancelar_reg_cliente);
            Controls.Add(save_registro_client);
            Controls.Add(etiqueta_domicilio);
            Controls.Add(etiqueta_mdcontacto);
            Controls.Add(etiqueta_datper_client);
            Controls.Add(domicilio_municipio_client);
            Controls.Add(domicilio_numviv_client);
            Controls.Add(domicilio_ciudad_client);
            Controls.Add(domicilio_colonia_client);
            Controls.Add(domicilio_calle_client);
            Controls.Add(fecha_naci_client);
            Controls.Add(celular_cliente);
            Controls.Add(tel_casa_client);
            Controls.Add(mail_cliente);
            Controls.Add(apellido_mater_client);
            Controls.Add(apellido_pater_client);
            Controls.Add(segundo_nom_client);
            Controls.Add(primer_nom_client);
            Controls.Add(ingresa_rfc);
            Controls.Add(etiqueta_ingresatal);
            ForeColor = SystemColors.HotTrack;
            Margin = new Padding(4);
            Name = "dclientes_eligio_registrar";
            Text = "Registro de Clientes";
            Load += dclientes_eligio_registrar_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_ingresatal;
        private TextBox ingresa_rfc;
        private TextBox primer_nom_client;
        private TextBox segundo_nom_client;
        private TextBox apellido_pater_client;
        private TextBox apellido_mater_client;
        private TextBox mail_cliente;
        private TextBox tel_casa_client;
        private TextBox celular_cliente;
        private TextBox fecha_naci_client;
        private TextBox domicilio_calle_client;
        private TextBox domicilio_colonia_client;
        private TextBox domicilio_ciudad_client;
        private TextBox domicilio_numviv_client;
        private TextBox domicilio_municipio_client;
        private Label etiqueta_datper_client;
        private Label etiqueta_mdcontacto;
        private Label etiqueta_domicilio;
        private Button save_registro_client;
        private Button cancelar_reg_cliente;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
    }
}
