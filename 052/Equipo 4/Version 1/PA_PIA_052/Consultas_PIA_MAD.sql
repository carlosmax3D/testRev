-- Vistas -------------------------------

Create View Vista_OcupacionDisponibilidad As
	Select 
		p.IDEvnt As ID_Evento,
		z.NomZona As Zona,
		z.PrecioBase As Precio_Base,
		z.Cupo As Asientos_Total,
		ISNULL(Count(DV.IDDetVent), 0) As Asientos_Vendidos,
		(Z.Cupo - ISNULL(Count(DV.IDDetVent), 0)) As Asientos_Disponibles,
		CAST(
			CASE 
				When z.Cupo = 0 Then 0 
				Else (ISNULL(COUNT(DV.IDDetVent), 0) * 100.0) / z.Cupo 
			End As DECIMAL(5,2)
		) As Porcentaje_Ocupacion
		from Partidos As P
		Inner Join Estadios As E
			On p.IDEst = E.IDEst
		Inner Join Zonas As Z
			On E.IDEst = Z.IDEst
		Left Join DetalleVentas As DV
			On p.IDEvnt = DV.IDEvnt and Z.IDZona = DV.IDZona
		Group By p.IDEvnt, E.Nombre, z.IDZona, z.NomZona, z.PrecioBase, z.Cupo;

Create View Vista_InformacionCliente AS
    Select 
        C.IDCts,
		C.Nombre,
		C.Domicilio,
		C.RFC,
		C.Correo,
		C.FechaNac,
		C.IDUsr,
		C.FechaHoraReg,
        TelCel.Numero As Telefono_Celular,
        TelCasa.Numero As Telefono_Casa
    from Clientes As C
    LEFT JOIN TelefonosClientes As TelCel 
        ON TelCel.IDCts = C.IDCts AND TelCel.Tipo = 'Celular'
    LEFT JOIN TelefonosClientes As TelCasa 
        ON TelCasa.IDCts = C.IDCts AND TelCasa.Tipo = 'Casa';

Create View Vista_VentasPorEvento As
	Select 
		e.IDEvnt,
		e.FechaHora,
		COUNT(dv.IDDetVent) As BoletosVendidos,
		SUM(v.Subtotal) As IngresoTotalEvento
	From Partidos As e
	INNER JOIN DetalleVentas As dv ON e.IDEvnt = dv.IDEvnt
	Inner Join Ventas As v On dv.GUID = v.GUID
	Group By e.IDEvnt, e.FechaHora;
GO

Create View Vista_IngresosDiarios As
	Select 
		CAST(v.FechaHoraVent As Date) As FechaDia,
		COUNT(v.GUID) As NumeroDeVentasRealizadas,
		SUM(v.Subtotal) As SubtotalDia,
		SUM(v.IVA) As IVADia,
		SUM(v.Total) As IngresoTotalDia
	From Ventas As v
	Group By CAST(v.FechaHoraVent As Date);
GO

Create View Vista_ReporteFacturacion As
	Select
		f.FolioFac,
		f.FechaEmision,
		f.HoraEmision,
		f.Metodo_de_Pago,
		f.RecNombre As NombreCliente,
		f.RecRFC As RFCCliente,
		v.Subtotal,
		v.IVA,
		v.Total,
		v.GUID As FolioVenta
	From Facturas As f
	INNER JOIN Ventas As v On f.GUID = v.GUID;
GO

Create View Vista_DetalleBoletoVendido As
	Select 
		dv.IDDetVent,
		dv.GUID As FolioVenta,
		e.Nombre As NombreEstadio,
		p.FechaHora As FechaPartido,
		z.NomZona As Zona,
		f.NumFila As Fila,
		a.NumAsiento As Asiento
	From DetalleVentas As dv
	INNER JOIN Partidos As p On dv.IDEvnt = p.IDEvnt
	INNER JOIN Estadios As e On p.IDEst = e.IDEst
	INNER JOIN Zonas As z On dv.IDZona = z.IDZona
	INNER JOIN Filas As f On dv.IDFila = f.IDFila
	INNER JOIN Asientos As a On dv.IDAsiento = a.IDAsiento;
GO

----------------------------------------------------------------------

-- Procedimeintos Almacenados ----------------------------------------

Create Procedure sp_ReporteVentas
    @FechaInicio DATETIME = NULL,
    @FechaFin DATETIME = NULL,
    @EstadioID INT = NULL,
    @PartidoID INT = NULL
	As
	Begin
		SET NOCOUNT ON;

		Select 
			v.GUID As Folio_Venta,
			v.FechaHoraVent As FechaHora_Venta,
			e.Nombre As Estadio,
			p.IDEvnt As Partido,
			p.FechaHora As FechaHora_Partido,
			z.NomZona As Zona,
			Count(dv.IDAsiento) As Cantidad_BoletosVendidos,
			SUM(v.Subtotal) As Subtotal,
			v.Total As Total,
			F.Metodo_de_Pago As MetodoPago
		From Ventas As v
		INNER JOIN DetalleVentas As dv ON v.GUID = dv.GUID
		INNER JOIN Partidos As p ON dv.IDEvnt = p.IDEvnt
		INNER JOIN Estadios As e ON p.IDEst = e.IDEst
		INNER JOIN Zonas As z ON dv.IDZona = z.IDZona
		INNER JOIN Facturas As F ON v.GUID = F.GUID
		Where 
			(@FechaInicio IS NULL OR v.FechaHoraVent >= @FechaInicio)
			AND (@FechaFin IS NULL OR v.FechaHoraVent <= @FechaFin)
			AND (@EstadioID IS NULL OR e.IDEst = @EstadioID)
			AND (@PartidoID IS NULL OR p.IDEvnt = @PartidoID)
		Group By 
			v.GUID,
			v.FechaHoraVent,
			e.Nombre,
			p.IDEvnt,
			p.FechaHora,
			z.NomZona,
			v.Total,
			F.Metodo_de_Pago;

		Select 
			ISNULL(Count(dv.GUID), 0) As CantidadTotalBoletosVendidos,
			ISNULL(SUM(v.Subtotal), 0) As IngresoTotalVentas
		From Ventas v
		INNER JOIN DetalleVentas As dv On v.GUID = dv.GUID
		INNER JOIN Partidos As p On dv.IDEvnt = p.IDEvnt
		INNER JOIN Estadios As e On p.IDEst = e.IDEst
		Where
			(@FechaInicio IS NULL OR v.FechaHoraVent >= @FechaInicio)
			AND (@FechaFin IS NULL OR v.FechaHoraVent <= @FechaFin)
			AND (@EstadioID IS NULL OR e.IDEst = @EstadioID)
			AND (@PartidoID IS NULL OR p.IDEvnt = @PartidoID);
	End;
GO

Create Procedure sp_HistorialCliente
    @Busqueda VARCHAR(100) = NULL,
    @ClienteID INT = NULL
	As
	Begin
		SET NOCOUNT ON;

		if @Busqueda IS NOT NULL
		Begin
			Select 
				C.IDCts,
				C.Nombre,
				C.Correo,
				Count(TC.Numero)
			From Clientes As C
			Inner Join TelefonosClientes As TC On C.IDCts = TC.IDCts
			Where Nombre Like '%' + @Busqueda + '%'
			Group By C.IDCts, C.Nombre, C.Correo;
		End

		if @ClienteID IS NOT NULL
		Begin
			Select 
				v.GUID As Folio_Compra,
				v.FechaHoraVent As Fecha_Compra,
				p.IDEvnt Partido,
				e.Nombre As Estadio,
				z.NomZona As Zona,
				p.FechaHora As Fecha_Partido,
				COUNT(dv.IDAsiento) As Cantidad_Boletos,
				SUM(v.Subtotal) As Subtotal,
				v.Total As Total,
				F.Metodo_de_Pago As MetodoPago,
				F.FolioFac As Factura 
			From Ventas As v
			INNER JOIN DetalleVentas As dv On v.GUID = dv.GUID
			INNER JOIN Partidos As p On dv.IDEvnt = p.IDEvnt
			INNER JOIN Estadios As e On p.IDEst = e.IDEst
			INNER JOIN Zonas As z On dv.IDZona = z.IDZona
			INNER JOIN Facturas As F On v.GUID = F.GUID
			Inner Join VentasRealizadas As VR On v.GUID = VR.GUID
			Where VR.IDCts = @ClienteID
			Group By 
				v.GUID,
				v.FechaHoraVent,
				p.IDEvnt,
				e.Nombre,
				z.NomZona,
				p.FechaHora,
				v.Total,
				F.Metodo_de_Pago,
				F.FolioFac
			Order By 
				v.FechaHoraVent ASC,
				CASE When p.FechaHora >= GETDATE() Then 0 Else 1 End ASC,
				p.FechaHora ASC;
		End
	End;
GO

CREATE PROCEDURE sp_ConsultarDatosCliente_Cte
    @IDCts INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IDCts,
        Nombre,
		Domicilio,
		RFC,
        Correo,
		FechaNac
    FROM Clientes
    WHERE IDCts = @IDCts;
END;
GO

----------------------------------------------------------------------

-- Funciones ----------------------------------------

Create Function fn_CalcularIVA (@Monto Decimal(10,2))
	Returns Decimal(10,2)
	As
	Begin
		Return @Monto * 0.16;
	End;
Go

Create Function fn_CalcularCantidadTotal (@Subtotal Decimal(10,2), @IVA Decimal(10,2))
	Returns Decimal(10,2)
	As
	Begin
		Return @Subtotal + @IVA
	End;
Go

Create Function fn_TotalClientes()
	Returns Int
	As
	Begin
		Declare @Total Int;

		Select @Total = Count(*)
			from Clientes

		Return @Total;
	End;
Go

----------------------------------------------------------------------

-- Procedimeintos Disparadores ----------------------------------------

Create Trigger Trig_FechaHoraUsuarioRegistradorCliente On Clientes 
After Insert 
As
	Begin
		SET NOCOUNT ON;

		Update C
			Set C.FechaHoraReg = GetDate()
			from Clientes As C
			Inner Join inserted As i
				On C.IDCts = i.IDCts
	End;
Go

Create Trigger Trig_FechaHoraUsuarioRegistradorEstadio On Estadios 
After Insert 
As
	Begin
		SET NOCOUNT ON;

		Update E
			Set E.FechaHoraReg = GetDate()
			from Estadios As E
			Inner Join inserted As i
				On E.IDEst = i.IDEst
	End;
Go

Create Trigger Trig_CalcularIVA ON Ventas 
After Insert
As
	Begin
		SET NOCOUNT ON;

		Update V
			SET V.IVA = dbo.fn_CalcularIVA(V.Subtotal)
			From Ventas As V
			INNER JOIN inserted As i ON V.GUID = i.GUID;
	End;
GO

Create Trigger Trig_CalcularTotal On Ventas
After Insert
As
	Begin
		Set NOCount On;

		Update V
			Set V.Total = dbo.fn_CalcularCantidadTotal(V.Subtotal, dbo.fn_CalcularIVA(V.Subtotal))
			from Ventas As V
			Inner Join inserted As i 
				On V.GUID = i.GUID
	End;
Go

CREATE TRIGGER trg_ValidarFechaPartido
ON dbo.Partidos
AFTER INSERT, UPDATE
AS
	BEGIN
		SET NOCOUNT ON; 
		IF UPDATE(FechaHora) -- o en un trigger de INSERT
		BEGIN
			IF EXISTS (SELECT 1 FROM inserted WHERE FechaHora < GETDATE())
			BEGIN
				RAISERROR('Error: No se pueden registrar partidos en fechas pasadas.', 16, 1);
				ROLLBACK TRANSACTION;
				RETURN;
			END
		END
	END;