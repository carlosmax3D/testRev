using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using TemplateCSharp.Data.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmVentas : Form
{
    private readonly VentaRepository _ventaRepo;
    private readonly PartidoRepository _partidoRepo;
    private readonly ClienteRepository _clienteRepo;
    private readonly SesionUsuario _sesion;

    public FrmVentas(VentaRepository ventaRepo, PartidoRepository partidoRepo, ClienteRepository clienteRepo, SesionUsuario sesion)
    {
        InitializeComponent();
        _ventaRepo = ventaRepo;
        _partidoRepo = partidoRepo;
        _clienteRepo = clienteRepo;
        _sesion = sesion;
    }

    private void FrmVentas_Load(object sender, EventArgs e)
    {
        lblVendedor.Text = $"Vendedor: {_sesion.UsuarioActual?.NombreCompleto ?? "Sin sesión"}";
        CargarClientes();
        CargarMetodosPago();
        CargarPartidos();
        CargarBoletos();
    }

    private void CargarClientes()
    {
        try
        {
            var clientes = _clienteRepo.ObtenerTodos();
            cmbClientes.DataSource = clientes;
            cmbClientes.DisplayMember = "NombreCom";
            cmbClientes.ValueMember = "IdCliente";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CargarMetodosPago()
    {
        cmbMetodoPago.Items.Clear();
        cmbMetodoPago.Items.AddRange(new object[] { "Efectivo", "Tarjeta de Crédito", "Tarjeta de Débito", "Transferencia" });
        cmbMetodoPago.SelectedIndex = 0;
    }

    private void CargarPartidos()
    {
        try
        {
            var partidos = _partidoRepo.ObtenerTodos();
            cmbPartidos.DataSource = partidos;
            cmbPartidos.DisplayMember = "EquipoLocal";
            cmbPartidos.ValueMember = "IdPartido";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar lista de partidos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void cmbPartidos_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbPartidos.SelectedValue is int idPartido)
        {
            CargarZonas(idPartido);
        }
    }

    private void CargarZonas(int idPartido)
    {
        try
        {
            var zonas = _ventaRepo.ObtenerZonasPorPartido(idPartido);
            if (zonas.Count == 0)
            {
                // Respaldo por si el estadio aún no tiene zonas dadas de alta en BD
                zonas = new List<ZonaOpcion>
                {
                    new() { IdZona = 1, Nombre = "Normal", PrecioBase = 1200m, Disponibles = 100 },
                    new() { IdZona = 2, Nombre = "Preferente", PrecioBase = 1350m, Disponibles = 50 },
                    new() { IdZona = 3, Nombre = "VIP", PrecioBase = 1500m, Disponibles = 30 }
                };
            }

            cmbZonas.DataSource = zonas;
            cmbZonas.DisplayMember = "Descripcion";
            cmbZonas.ValueMember = "IdZona";
            ActualizarTotales();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar zonas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void cmbZonas_SelectedIndexChanged(object sender, EventArgs e) => ActualizarTotales();
    private void nudCantidad_ValueChanged(object sender, EventArgs e) => ActualizarTotales();

    private void ActualizarTotales()
    {
        if (cmbZonas.SelectedItem is ZonaOpcion zona)
        {
            int cantidad = (int)nudCantidad.Value;
            decimal subtotal = zona.PrecioBase * cantidad;
            decimal iva = Math.Round(subtotal * 0.16m, 2);
            decimal total = subtotal + iva;

            lblResumenPrecio.Text = $"Precio c/u: ${zona.PrecioBase:N2}  |  Subtotal: ${subtotal:N2}  |  IVA (16%): ${iva:N2}  |  Total: ${total:N2}";
        }
    }

    private void CargarBoletos()
    {
        try
        {
            dgvBoletos.DataSource = _ventaRepo.ObtenerBoletos();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al consultar boletos vendidos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static bool YaPasoLaFechaHora(object? partido)
    {
        if (partido == null) return false;

        Type tipo = partido.GetType();
        PropertyInfo? propFechaHora = tipo.GetProperty("FechaHoraPartido") ?? tipo.GetProperty("fecha_hora_partido");
        if (propFechaHora?.GetValue(partido) is DateTime fechaHora)
            return fechaHora < DateTime.Now;

        PropertyInfo? propFecha = tipo.GetProperty("FechaPartido") ?? tipo.GetProperty("fecha_partido");
        PropertyInfo? propHora = tipo.GetProperty("HoraPartido") ?? tipo.GetProperty("hora_partido");

        if (propFecha?.GetValue(partido) is DateTime fecha)
        {
            DateTime combinada = fecha.Date;
            object? horaVal = propHora?.GetValue(partido);
            if (horaVal is TimeSpan ts)
                combinada = combinada.Add(ts);
            else if (horaVal is DateTime horaDt)
                combinada = combinada.Add(horaDt.TimeOfDay);

            return combinada < DateTime.Now;
        }

        return false;
    }

    private void btnVender_Click(object sender, EventArgs e)
    {
        try
        {
            if (_sesion.UsuarioActual == null)
            {
                MessageBox.Show("No hay un usuario logueado en la sesión.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbClientes.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbPartidos.SelectedValue == null || cmbZonas.SelectedItem is not ZonaOpcion zonaSeleccionada)
            {
                MessageBox.Show("Seleccione un partido y una zona.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (YaPasoLaFechaHora(cmbPartidos.SelectedItem))
            {
                MessageBox.Show("No se pueden vender boletos porque la fecha y hora del partido ya pasaron.", "Partido vencido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtFila.Text))
            {
                MessageBox.Show("Ingrese la fila.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidad = (int)nudCantidad.Value;
            int asientoInicial = (int)nudAsientoInicio.Value;
            int idCliente = (int)cmbClientes.SelectedValue;
            int idUsuario = _sesion.UsuarioActual.IdUsuario;
            string metodoPago = cmbMetodoPago.SelectedItem?.ToString() ?? "Efectivo";
            string folioVenta = Guid.NewGuid().ToString("N")[..10].ToUpper();

            for (int i = 0; i < cantidad; i++)
            {
                Boleto boleto = new()
                {
                    FolioVenta = folioVenta,
                    IdPartido = (int)cmbPartidos.SelectedValue,
                    IdZona = zonaSeleccionada.IdZona,
                    Fila = txtFila.Text.Trim().ToUpper(),
                    NumAsiento = (asientoInicial + i).ToString(),
                    PrecioUnitario = zonaSeleccionada.PrecioBase
                };

                _ventaRepo.RegistrarVentaBoleto(boleto, idCliente, idUsuario, metodoPago);
            }

            decimal totalPagado = Math.Round(zonaSeleccionada.PrecioBase * cantidad * 1.16m, 2);
            MessageBox.Show(
                $"Venta registrada con éxito.\nFolio: {folioVenta}\nBoletos: {cantidad} ({zonaSeleccionada.Nombre})\nTotal (c/IVA): ${totalPagado:N2}",
                "Venta Exitosa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            btnLimpiar_Click(sender, e);
            CargarBoletos();
            if (cmbPartidos.SelectedValue is int idPart) CargarZonas(idPart);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al procesar venta de boleto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
        txtFila.Clear();
        nudAsientoInicio.Value = 1;
        nudCantidad.Value = 1;
        txtFila.Focus();
    }
}