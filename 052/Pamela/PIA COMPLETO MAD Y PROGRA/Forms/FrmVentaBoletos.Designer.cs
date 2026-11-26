namespace TemplateCSharp.Forms
{
    partial class FrmVentaBoletos
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
            txtVentaBoletos = new Label();
            txtBuscCteVta = new Label();
            elegirOPCte = new ComboBox();
            busc_OP_elegidaCte = new TextBox();
            buscCteVta = new Button();
            nuevoCteVta = new Button();
            txtCteSelec = new Label();
            txtCteVta = new Label();
            cteSelec = new Label();
            txtApellVta = new Label();
            apellSelec = new Label();
            txtCorreoVta = new Label();
            correoSelec = new Label();
            txtCte = new Label();
            txtPart = new Label();
            txtBucsPartVta = new Label();
            elegirOPPart = new ComboBox();
            busc_OP_elegidaPart = new TextBox();
            buscPartVta = new Button();
            dGVInfoPartVta = new DataGridView();
            txtPartDisp = new Label();
            txtInfoSelecPart = new Label();
            TxtInfoEstdVta = new Label();
            estdSelec = new Label();
            txtInfoFechaVta = new Label();
            fechaSelec = new Label();
            txtInfoHoraVta = new Label();
            horaSelec = new Label();
            txtBoletos = new Label();
            ZonaBolVta = new Label();
            opZonaVta = new ComboBox();
            txtCantBoleto = new Label();
            CantBoletoVta = new NumericUpDown();
            botonAgregBoleto = new Button();
            txtBolCompraVTA = new Label();
            dGVBoletosVta = new DataGridView();
            txtTotBol = new Label();
            numBolVta = new Label();
            botonElimBoleto = new Button();
            txtResumen = new Label();
            txtSubtotal = new Label();
            txtIVA = new Label();
            txtTotal = new Label();
            txtFormPago = new Label();
            comboBox1 = new ComboBox();
            cantSubtotal = new Label();
            cantIVA = new Label();
            cantTotal = new Label();
            botonRealizarVta = new Button();
            botonCancelarVta = new Button();
            ((System.ComponentModel.ISupportInitialize)dGVInfoPartVta).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CantBoletoVta).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dGVBoletosVta).BeginInit();
            botonLimpiarVta = new Button();
            botonVerMapaVta = new Button();
            txtMapaVta = new Label();
            pBMapaVta = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pBMapaVta).BeginInit();
            SuspendLayout();
            // 
            // txtVentaBoletos
            // 
            txtVentaBoletos.AutoSize = true;
            txtVentaBoletos.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtVentaBoletos.ForeColor = Color.FromArgb(0, 192, 0);
            txtVentaBoletos.Location = new Point(12, 9);
            txtVentaBoletos.Name = "txtVentaBoletos";
            txtVentaBoletos.Size = new Size(248, 30);
            txtVentaBoletos.TabIndex = 0;
            txtVentaBoletos.Text = "Venta de boletos";
            // 
            // txtBuscCteVta
            // 
            txtBuscCteVta.AutoSize = true;
            txtBuscCteVta.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscCteVta.Location = new Point(12, 83);
            txtBuscCteVta.Name = "txtBuscCteVta";
            txtBuscCteVta.Size = new Size(149, 17);
            txtBuscCteVta.TabIndex = 1;
            txtBuscCteVta.Text = "Buscar cliente Por:";
            // 
            // elegirOPCte
            // 
            elegirOPCte.FormattingEnabled = true;
            elegirOPCte.Location = new Point(167, 80);
            elegirOPCte.Name = "elegirOPCte";
            elegirOPCte.Size = new Size(121, 23);
            elegirOPCte.TabIndex = 2;
            // 
            // busc_OP_elegidaCte
            // 
            busc_OP_elegidaCte.Location = new Point(294, 80);
            busc_OP_elegidaCte.Name = "busc_OP_elegidaCte";
            busc_OP_elegidaCte.PlaceholderText = "Escribe el texto a buscar";
            busc_OP_elegidaCte.Size = new Size(380, 23);
            busc_OP_elegidaCte.TabIndex = 3;
            // 
            // buscCteVta
            // 
            buscCteVta.Location = new Point(684, 79);
            buscCteVta.Name = "buscCteVta";
            buscCteVta.Size = new Size(90, 25);
            buscCteVta.TabIndex = 4;
            buscCteVta.Text = "Buscar";
            buscCteVta.UseVisualStyleBackColor = true;
            // 
            // nuevoCteVta
            // 
            nuevoCteVta.Location = new Point(784, 79);
            nuevoCteVta.Name = "nuevoCteVta";
            nuevoCteVta.Size = new Size(140, 25);
            nuevoCteVta.TabIndex = 5;
            nuevoCteVta.Text = "Nuevo Cliente";
            nuevoCteVta.UseVisualStyleBackColor = true;
            // 
            // txtCteSelec
            // 
            txtCteSelec.AutoSize = true;
            txtCteSelec.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCteSelec.Location = new Point(12, 114);
            txtCteSelec.Name = "txtCteSelec";
            txtCteSelec.Size = new Size(163, 17);
            txtCteSelec.TabIndex = 6;
            txtCteSelec.Text = "Cliente Seleccionado:";
            // 
            // txtCteVta
            // 
            txtCteVta.AutoSize = true;
            txtCteVta.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCteVta.Location = new Point(44, 141);
            txtCteVta.Name = "txtCteVta";
            txtCteVta.Size = new Size(67, 17);
            txtCteVta.TabIndex = 7;
            txtCteVta.Text = "Nombre:";
            txtCteVta.Click += label2_Click;
            // 
            // cteSelec
            // 
            cteSelec.AutoSize = true;
            cteSelec.Location = new Point(117, 143);
            cteSelec.Name = "cteSelec";
            cteSelec.Size = new Size(108, 15);
            cteSelec.TabIndex = 8;
            cteSelec.Text = "Nombre del cliente";
            // 
            // txtApellVta
            // 
            txtApellVta.AutoSize = true;
            txtApellVta.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtApellVta.Location = new Point(29, 158);
            txtApellVta.Name = "txtApellVta";
            txtApellVta.Size = new Size(82, 17);
            txtApellVta.TabIndex = 9;
            txtApellVta.Text = "Apellidos:";
            // 
            // apellSelec
            // 
            apellSelec.AutoSize = true;
            apellSelec.Location = new Point(117, 160);
            apellSelec.Name = "apellSelec";
            apellSelec.Size = new Size(113, 15);
            apellSelec.TabIndex = 10;
            apellSelec.Text = "Apellidos del cliente";
            // 
            // txtCorreoVta
            // 
            txtCorreoVta.AutoSize = true;
            txtCorreoVta.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCorreoVta.Location = new Point(46, 175);
            txtCorreoVta.Name = "txtCorreoVta";
            txtCorreoVta.Size = new Size(65, 17);
            txtCorreoVta.TabIndex = 11;
            txtCorreoVta.Text = "Correo:";
            // 
            // correoSelec
            // 
            correoSelec.AutoSize = true;
            correoSelec.Location = new Point(117, 177);
            correoSelec.Name = "correoSelec";
            correoSelec.Size = new Size(100, 15);
            correoSelec.TabIndex = 12;
            correoSelec.Text = "Correo del cliente";
            // 
            // txtCte
            // 
            txtCte.AutoSize = true;
            txtCte.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtCte.ForeColor = Color.Red;
            txtCte.Location = new Point(12, 48);
            txtCte.Name = "txtCte";
            txtCte.Size = new Size(72, 20);
            txtCte.TabIndex = 13;
            txtCte.Text = "Cliente";
            // 
            // txtPart
            // 
            txtPart.AutoSize = true;
            txtPart.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtPart.ForeColor = Color.Red;
            txtPart.Location = new Point(12, 210);
            txtPart.Name = "txtPart";
            txtPart.Size = new Size(81, 20);
            txtPart.TabIndex = 14;
            txtPart.Text = "Partido";
            // 
            // txtBucsPartVta
            // 
            txtBucsPartVta.AutoSize = true;
            txtBucsPartVta.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBucsPartVta.Location = new Point(12, 243);
            txtBucsPartVta.Name = "txtBucsPartVta";
            txtBucsPartVta.Size = new Size(157, 17);
            txtBucsPartVta.TabIndex = 15;
            txtBucsPartVta.Text = "Buscar partido por:";
            // 
            // elegirOPPart
            // 
            elegirOPPart.FormattingEnabled = true;
            elegirOPPart.Location = new Point(167, 240);
            elegirOPPart.Name = "elegirOPPart";
            elegirOPPart.Size = new Size(121, 23);
            elegirOPPart.TabIndex = 16;
            // 
            // busc_OP_elegidaPart
            // 
            busc_OP_elegidaPart.Location = new Point(294, 240);
            busc_OP_elegidaPart.Name = "busc_OP_elegidaPart";
            busc_OP_elegidaPart.Size = new Size(480, 23);
            busc_OP_elegidaPart.TabIndex = 17;
            // 
            // buscPartVta
            // 
            buscPartVta.Location = new Point(784, 239);
            buscPartVta.Name = "buscPartVta";
            buscPartVta.Size = new Size(140, 25);
            buscPartVta.TabIndex = 18;
            buscPartVta.Text = "Buscar";
            buscPartVta.UseVisualStyleBackColor = true;
            // 
            // dGVInfoPartVta
            // 
            dGVInfoPartVta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGVInfoPartVta.Location = new Point(12, 298);
            dGVInfoPartVta.Name = "dGVInfoPartVta";
            dGVInfoPartVta.Size = new Size(912, 140);
            dGVInfoPartVta.TabIndex = 19;
            // 
            // txtPartDisp
            // 
            txtPartDisp.AutoSize = true;
            txtPartDisp.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPartDisp.Location = new Point(12, 275);
            txtPartDisp.Name = "txtPartDisp";
            txtPartDisp.Size = new Size(167, 17);
            txtPartDisp.TabIndex = 20;
            txtPartDisp.Text = "Partidos disponibles:";
            txtPartDisp.Click += label1_Click;
            // 
            // txtInfoSelecPart
            // 
            txtInfoSelecPart.AutoSize = true;
            txtInfoSelecPart.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtInfoSelecPart.Location = new Point(12, 450);
            txtInfoSelecPart.Name = "txtInfoSelecPart";
            txtInfoSelecPart.Size = new Size(292, 17);
            txtInfoSelecPart.TabIndex = 21;
            txtInfoSelecPart.Text = "Información del partido seleccionado:";
            // 
            // TxtInfoEstdVta
            // 
            TxtInfoEstdVta.AutoSize = true;
            TxtInfoEstdVta.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TxtInfoEstdVta.Location = new Point(43, 478);
            TxtInfoEstdVta.Name = "TxtInfoEstdVta";
            TxtInfoEstdVta.Size = new Size(68, 17);
            TxtInfoEstdVta.TabIndex = 22;
            TxtInfoEstdVta.Text = "Estadio:";
            // 
            // estdSelec
            // 
            estdSelec.AutoSize = true;
            estdSelec.Location = new Point(117, 480);
            estdSelec.Name = "estdSelec";
            estdSelec.Size = new Size(105, 15);
            estdSelec.TabIndex = 23;
            estdSelec.Text = "Estadio del partido";
            // 
            // txtInfoFechaVta
            // 
            txtInfoFechaVta.AutoSize = true;
            txtInfoFechaVta.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtInfoFechaVta.Location = new Point(59, 500);
            txtInfoFechaVta.Name = "txtInfoFechaVta";
            txtInfoFechaVta.Size = new Size(52, 17);
            txtInfoFechaVta.TabIndex = 24;
            txtInfoFechaVta.Text = "Fecha:";
            txtInfoFechaVta.Click += label3_Click;
            // 
            // fechaSelec
            // 
            fechaSelec.AutoSize = true;
            fechaSelec.Location = new Point(117, 502);
            fechaSelec.Name = "fechaSelec";
            fechaSelec.Size = new Size(98, 15);
            fechaSelec.TabIndex = 25;
            fechaSelec.Text = "Fecha del partido";
            // 
            // txtInfoHoraVta
            // 
            txtInfoHoraVta.AutoSize = true;
            txtInfoHoraVta.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtInfoHoraVta.Location = new Point(59, 522);
            txtInfoHoraVta.Name = "txtInfoHoraVta";
            txtInfoHoraVta.Size = new Size(49, 17);
            txtInfoHoraVta.TabIndex = 26;
            txtInfoHoraVta.Text = "Hora:";
            txtInfoHoraVta.Click += txtInfoHoraVta_Click;
            // 
            // horaSelec
            // 
            horaSelec.AutoSize = true;
            horaSelec.Location = new Point(117, 524);
            horaSelec.Name = "horaSelec";
            horaSelec.Size = new Size(93, 15);
            horaSelec.TabIndex = 27;
            horaSelec.Text = "Hora del partido";
            // 
            // txtBoletos
            // 
            txtBoletos.AutoSize = true;
            txtBoletos.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtBoletos.ForeColor = Color.Red;
            txtBoletos.Location = new Point(12, 640);
            txtBoletos.Name = "txtBoletos";
            txtBoletos.Size = new Size(156, 20);
            txtBoletos.TabIndex = 28;
            txtBoletos.Text = "Agregar boletos";
            // 
            // ZonaBolVta
            // 
            ZonaBolVta.AutoSize = true;
            ZonaBolVta.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ZonaBolVta.Location = new Point(43, 673);
            ZonaBolVta.Name = "ZonaBolVta";
            ZonaBolVta.Size = new Size(47, 17);
            ZonaBolVta.TabIndex = 29;
            ZonaBolVta.Text = "Zona:";
            // 
            // opZonaVta
            // 
            opZonaVta.FormattingEnabled = true;
            opZonaVta.Location = new Point(92, 670);
            opZonaVta.Name = "opZonaVta";
            opZonaVta.Size = new Size(330, 23);
            opZonaVta.TabIndex = 30;
            // 
            // txtCantBoleto
            // 
            txtCantBoleto.AutoSize = true;
            txtCantBoleto.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCantBoleto.Location = new Point(440, 673);
            txtCantBoleto.Name = "txtCantBoleto";
            txtCantBoleto.Size = new Size(79, 17);
            txtCantBoleto.TabIndex = 31;
            txtCantBoleto.Text = "Cantidad:";
            txtCantBoleto.Click += label1_Click_1;
            // 
            // CantBoletoVta
            // 
            CantBoletoVta.Location = new Point(530, 670);
            CantBoletoVta.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            CantBoletoVta.Name = "CantBoletoVta";
            CantBoletoVta.Size = new Size(60, 23);
            CantBoletoVta.TabIndex = 32;
            // 
            // botonAgregBoleto
            // 
            botonAgregBoleto.Location = new Point(604, 664);
            botonAgregBoleto.Name = "botonAgregBoleto";
            botonAgregBoleto.Size = new Size(120, 34);
            botonAgregBoleto.TabIndex = 33;
            botonAgregBoleto.Text = "Agregar";
            botonAgregBoleto.UseVisualStyleBackColor = true;
            // 
            // txtBolCompraVTA
            // 
            txtBolCompraVTA.AutoSize = true;
            txtBolCompraVTA.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBolCompraVTA.Location = new Point(12, 710);
            txtBolCompraVTA.Name = "txtBolCompraVTA";
            txtBolCompraVTA.Size = new Size(183, 17);
            txtBolCompraVTA.TabIndex = 34;
            txtBolCompraVTA.Text = "Boletos de esta compra:";
            // 
            // dGVBoletosVta
            // 
            dGVBoletosVta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGVBoletosVta.Location = new Point(12, 732);
            dGVBoletosVta.Name = "dGVBoletosVta";
            dGVBoletosVta.Size = new Size(590, 110);
            dGVBoletosVta.TabIndex = 35;
            dGVBoletosVta.CellContentClick += dGVBoletosVta_CellContentClick;
            // 
            // txtTotBol
            // 
            txtTotBol.AutoSize = true;
            txtTotBol.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTotBol.Location = new Point(12, 852);
            txtTotBol.Name = "txtTotBol";
            txtTotBol.Size = new Size(137, 17);
            txtTotBol.TabIndex = 36;
            txtTotBol.Text = "Total de boletos:";
            // 
            // numBolVta
            // 
            numBolVta.AutoSize = true;
            numBolVta.Location = new Point(150, 854);
            numBolVta.Name = "numBolVta";
            numBolVta.Size = new Size(109, 15);
            numBolVta.TabIndex = 37;
            numBolVta.Text = "Número de boletos";
            // 
            // botonElimBoleto
            // 
            botonElimBoleto.Location = new Point(452, 848);
            botonElimBoleto.Name = "botonElimBoleto";
            botonElimBoleto.Size = new Size(150, 32);
            botonElimBoleto.TabIndex = 38;
            botonElimBoleto.Text = "Eliminar Seleccionado";
            botonElimBoleto.UseVisualStyleBackColor = true;
            // 
            // txtResumen
            // 
            txtResumen.AutoSize = true;
            txtResumen.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtResumen.ForeColor = Color.Red;
            txtResumen.Location = new Point(630, 706);
            txtResumen.Name = "txtResumen";
            txtResumen.Size = new Size(84, 20);
            txtResumen.TabIndex = 39;
            txtResumen.Text = "Resumen";
            // 
            // txtSubtotal
            // 
            txtSubtotal.AutoSize = true;
            txtSubtotal.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSubtotal.Location = new Point(650, 736);
            txtSubtotal.Name = "txtSubtotal";
            txtSubtotal.Size = new Size(81, 17);
            txtSubtotal.TabIndex = 40;
            txtSubtotal.Text = "Subtotal:";
            // 
            // txtIVA
            // 
            txtIVA.AutoSize = true;
            txtIVA.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIVA.Location = new Point(650, 758);
            txtIVA.Name = "txtIVA";
            txtIVA.Size = new Size(33, 17);
            txtIVA.TabIndex = 41;
            txtIVA.Text = "IVA:";
            // 
            // txtTotal
            // 
            txtTotal.AutoSize = true;
            txtTotal.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTotal.Location = new Point(650, 780);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(55, 17);
            txtTotal.TabIndex = 42;
            txtTotal.Text = "Total:";
            // 
            // txtFormPago
            // 
            txtFormPago.AutoSize = true;
            txtFormPago.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFormPago.Location = new Point(630, 812);
            txtFormPago.Name = "txtFormPago";
            txtFormPago.Size = new Size(116, 17);
            txtFormPago.TabIndex = 43;
            txtFormPago.Text = "Forma de pago:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(754, 809);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(170, 23);
            comboBox1.TabIndex = 44;
            // 
            // cantSubtotal
            // 
            cantSubtotal.AutoSize = true;
            cantSubtotal.Location = new Point(760, 738);
            cantSubtotal.Name = "cantSubtotal";
            cantSubtotal.Size = new Size(121, 15);
            cantSubtotal.TabIndex = 45;
            cantSubtotal.Text = "Cantidad del Subtotal";
            // 
            // cantIVA
            // 
            cantIVA.AutoSize = true;
            cantIVA.Location = new Point(760, 760);
            cantIVA.Name = "cantIVA";
            cantIVA.Size = new Size(94, 15);
            cantIVA.TabIndex = 46;
            cantIVA.Text = "Cantidad del IVA";
            // 
            // cantTotal
            // 
            cantTotal.AutoSize = true;
            cantTotal.Location = new Point(760, 782);
            cantTotal.Name = "cantTotal";
            cantTotal.Size = new Size(83, 15);
            cantTotal.TabIndex = 47;
            cantTotal.Text = "Cantidad Total";
            // 
            // botonRealizarVta
            // 
            botonRealizarVta.Location = new Point(630, 846);
            botonRealizarVta.Name = "botonRealizarVta";
            botonRealizarVta.Size = new Size(110, 36);
            botonRealizarVta.TabIndex = 48;
            botonRealizarVta.Text = "Realizar Venta";
            botonRealizarVta.UseVisualStyleBackColor = true;
            // 
            // botonCancelarVta
            // 
            botonCancelarVta.Location = new Point(837, 846);
            botonCancelarVta.Name = "botonCancelarVta";
            botonCancelarVta.Size = new Size(87, 36);
            botonCancelarVta.TabIndex = 49;
            botonCancelarVta.Text = "Cancelar";
            botonCancelarVta.UseVisualStyleBackColor = true;
            // 
            // botonLimpiarVta
            // 
            botonLimpiarVta.Location = new Point(746, 846);
            botonLimpiarVta.Name = "botonLimpiarVta";
            botonLimpiarVta.Size = new Size(85, 36);
            botonLimpiarVta.TabIndex = 50;
            botonLimpiarVta.Text = "Limpiar";
            botonLimpiarVta.UseVisualStyleBackColor = true;
            botonLimpiarVta.Click += botonLimpiarVta_Click;
            // 
            // botonVerMapaVta
            // 
            botonVerMapaVta.Location = new Point(117, 552);
            botonVerMapaVta.Name = "botonVerMapaVta";
            botonVerMapaVta.Size = new Size(170, 30);
            botonVerMapaVta.TabIndex = 50;
            botonVerMapaVta.Text = "Ver mapa en grande";
            botonVerMapaVta.UseVisualStyleBackColor = true;
            botonVerMapaVta.Click += botonVerMapaVta_Click;
            // 
            // txtMapaVta
            // 
            txtMapaVta.AutoSize = true;
            txtMapaVta.Location = new Point(600, 450);
            txtMapaVta.Name = "txtMapaVta";
            txtMapaVta.Size = new Size(100, 15);
            txtMapaVta.TabIndex = 52;
            txtMapaVta.Text = "Mapa del estadio:";
            // 
            // pBMapaVta
            // 
            pBMapaVta.BackColor = Color.White;
            pBMapaVta.BorderStyle = BorderStyle.FixedSingle;
            pBMapaVta.Location = new Point(600, 470);
            pBMapaVta.Name = "pBMapaVta";
            pBMapaVta.Size = new Size(324, 160);
            pBMapaVta.SizeMode = PictureBoxSizeMode.Zoom;
            pBMapaVta.TabIndex = 53;
            pBMapaVta.TabStop = false;
            // 
            // FrmVentaBoletos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(940, 896);
            Controls.Add(pBMapaVta);
            Controls.Add(txtMapaVta);
            Controls.Add(botonVerMapaVta);
            Controls.Add(botonLimpiarVta);
            Controls.Add(botonCancelarVta);
            Controls.Add(botonRealizarVta);
            Controls.Add(cantTotal);
            Controls.Add(cantIVA);
            Controls.Add(cantSubtotal);
            Controls.Add(comboBox1);
            Controls.Add(txtFormPago);
            Controls.Add(txtTotal);
            Controls.Add(txtIVA);
            Controls.Add(txtSubtotal);
            Controls.Add(txtResumen);
            Controls.Add(botonElimBoleto);
            Controls.Add(numBolVta);
            Controls.Add(txtTotBol);
            Controls.Add(dGVBoletosVta);
            Controls.Add(txtBolCompraVTA);
            Controls.Add(botonAgregBoleto);
            Controls.Add(CantBoletoVta);
            Controls.Add(txtCantBoleto);
            Controls.Add(opZonaVta);
            Controls.Add(ZonaBolVta);
            Controls.Add(txtBoletos);
            Controls.Add(horaSelec);
            Controls.Add(txtInfoHoraVta);
            Controls.Add(fechaSelec);
            Controls.Add(txtInfoFechaVta);
            Controls.Add(estdSelec);
            Controls.Add(TxtInfoEstdVta);
            Controls.Add(txtInfoSelecPart);
            Controls.Add(txtPartDisp);
            Controls.Add(dGVInfoPartVta);
            Controls.Add(buscPartVta);
            Controls.Add(busc_OP_elegidaPart);
            Controls.Add(elegirOPPart);
            Controls.Add(txtBucsPartVta);
            Controls.Add(txtPart);
            Controls.Add(txtCte);
            Controls.Add(correoSelec);
            Controls.Add(txtCorreoVta);
            Controls.Add(apellSelec);
            Controls.Add(txtApellVta);
            Controls.Add(cteSelec);
            Controls.Add(txtCteVta);
            Controls.Add(txtCteSelec);
            Controls.Add(nuevoCteVta);
            Controls.Add(buscCteVta);
            Controls.Add(busc_OP_elegidaCte);
            Controls.Add(elegirOPCte);
            Controls.Add(txtBuscCteVta);
            Controls.Add(txtVentaBoletos);
            Name = "FrmVentaBoletos";
            Text = "Venta de boletos";
            Load += FrmVentaBoletos_Load;
            ((System.ComponentModel.ISupportInitialize)dGVInfoPartVta).EndInit();
            ((System.ComponentModel.ISupportInitialize)CantBoletoVta).EndInit();
            ((System.ComponentModel.ISupportInitialize)dGVBoletosVta).EndInit();
            ((System.ComponentModel.ISupportInitialize)pBMapaVta).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pBMapaVta;

        private Label txtMapaVta;

        private Button botonVerMapaVta;

        private Button botonLimpiarVta;

        private Label txtVentaBoletos;
        private Label txtBuscCteVta;
        private ComboBox elegirOPCte;
        private TextBox busc_OP_elegidaCte;
        private Button buscCteVta;
        private Button nuevoCteVta;
        private Label txtCteSelec;
        private Label txtCteVta;
        private Label cteSelec;
        private Label txtApellVta;
        private Label apellSelec;
        private Label txtCorreoVta;
        private Label correoSelec;
        private Label txtCte;
        private Label txtPart;
        private Label txtBucsPartVta;
        private ComboBox elegirOPPart;
        private TextBox busc_OP_elegidaPart;
        private Button buscPartVta;
        private DataGridView dGVInfoPartVta;
        private Label txtPartDisp;
        private Label txtInfoSelecPart;
        private Label TxtInfoEstdVta;
        private Label estdSelec;
        private Label txtInfoFechaVta;
        private Label fechaSelec;
        private Label txtInfoHoraVta;
        private Label horaSelec;
        private Label txtBoletos;
        private Label ZonaBolVta;
        private ComboBox opZonaVta;
        private Label txtCantBoleto;
        private NumericUpDown CantBoletoVta;
        private Button botonAgregBoleto;
        private Label txtBolCompraVTA;
        private DataGridView dGVBoletosVta;
        private Label txtTotBol;
        private Label numBolVta;
        private Button botonElimBoleto;
        private Label txtResumen;
        private Label txtSubtotal;
        private Label txtIVA;
        private Label txtTotal;
        private Label txtFormPago;
        private ComboBox comboBox1;
        private Label cantSubtotal;
        private Label cantIVA;
        private Label cantTotal;
        private Button botonRealizarVta;
        private Button botonCancelarVta;
    }
}