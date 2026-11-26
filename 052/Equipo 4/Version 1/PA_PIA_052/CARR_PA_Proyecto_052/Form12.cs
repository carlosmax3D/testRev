using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CARR_PA_Proyecto_052.Forms
{
    public partial class Form12 : Form
    {
        private int idUsuario;
        private readonly string nombreUsuario;
        private readonly string rolUsuario;
        private bool usuarioEsAdministrador;

        public Form12() : this(string.Empty, string.Empty)
        {
        }

        public Form12(string nombreUsuario, string rolUsuario)
        {
            this.nombreUsuario = nombreUsuario;
            this.rolUsuario = rolUsuario;
            InitializeComponent();
        }

        private void Form12_Load(object? sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            try
            {
                if (rolUsuario != "Administrativo")
                {
                    MessageBox.Show("Solo un usuario administrador puede realizar cancelaciones.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                using var db = new InventarioContext();
                var usuario = db.Usuarios.FirstOrDefault(u => u.Nombre == nombreUsuario && u.Rol == "Administrativo");

                if (usuario == null)
                {
                    MessageBox.Show("No se encontró el usuario administrador en la base de datos.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                idUsuario = usuario.IDUsr;
                usuarioEsAdministrador = true;
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo validar el usuario administrador:\n" + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void CargarDatos()
        {
            try
            {
                using var db = new InventarioContext();
                var ventas = db.Ventas
                    .AsNoTracking()
                    .Where(venta => !db.Cancelaciones.Any(cancelacion => cancelacion.GUID == venta.GUID)
                        && db.DetalleVentas.Any(detalle => detalle.GUID == venta.GUID))
                    .Select(venta => new VentaCancelacionRow
                    {
                        Folio = venta.GUID,
                        Fecha = venta.FechaHoraVent,
                        Cliente = (from realizada in db.VentasRealizadas
                                   join cliente in db.Clientes on realizada.IDCts equals cliente.IDCts
                                   where realizada.GUID == venta.GUID
                                   select cliente.Nombre).FirstOrDefault() ?? "Sin cliente asociado",
                        Boletos = db.DetalleVentas.Count(detalle => detalle.GUID == venta.GUID),
                        Total = venta.Total
                    })
                    .OrderByDescending(venta => venta.Fecha)
                    .ToList();

                var cancelaciones = (from cancelacion in db.Cancelaciones.AsNoTracking()
                                     join usuario in db.Usuarios.AsNoTracking() on cancelacion.IDUsr equals usuario.IDUsr into usuarios
                                     from usuario in usuarios.DefaultIfEmpty()
                                     orderby cancelacion.FechaHora descending
                                     select new CancelacionRow
                                     {
                                         Folio = cancelacion.GUID,
                                         Motivo = cancelacion.Motivo,
                                         Fecha = cancelacion.FechaHora,
                                         Usuario = usuario == null ? "Usuario sin registro" : usuario.Nombre,
                                         Factura = db.Facturas.Any(factura => factura.GUID == cancelacion.GUID) ? "Anulada" : "No generada"
                                     }).ToList();

                dgvVentas.DataSource = ventas;
                dgvVentas.ClearSelection();
                dgvCancelaciones.DataSource = cancelaciones;
                ActualizarEstadoBoton();
            }
            catch (Exception ex)
            {
                dgvVentas.DataSource = null;
                dgvCancelaciones.DataSource = null;
                MessageBox.Show("No se pudieron cargar las ventas y cancelaciones:\n" + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarEstadoBoton()
        {
            btnCancelarBoleto.Enabled = usuarioEsAdministrador
                && Guid.TryParse(txtCodigoCompra.Text.Trim(), out _)
                && !string.IsNullOrWhiteSpace(txtMotivo.Text);
        }

        private void txtCodigoCompra_TextChanged(object? sender, EventArgs e)
        {
            ActualizarEstadoBoton();
        }

        private void txtMotivo_TextChanged(object? sender, EventArgs e)
        {
            ActualizarEstadoBoton();
        }

        private void btnCancelarBoleto_Click(object? sender, EventArgs e)
        {
            if (!Guid.TryParse(txtCodigoCompra.Text.Trim(), out Guid folioCompra))
            {
                MessageBox.Show("Ingresa un código de compra válido.", "Código inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigoCompra.Focus();
                return;
            }

            string motivo = txtMotivo.Text.Trim();
            if (string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show("Escribe el motivo de la cancelación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMotivo.Focus();
                return;
            }

            var confirmacion = MessageBox.Show(
                $"¿Confirmas la cancelación de la compra {folioCompra}? Se liberarán sus boletos y se cancelará la factura asociada.",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using var db = new InventarioContext();
                using var transaction = db.Database.BeginTransaction();

                bool administradorAutorizado = db.Usuarios.Any(usuario =>
                    usuario.IDUsr == idUsuario && usuario.Rol == "Administrativo");
                if (!administradorAutorizado)
                {
                    transaction.Rollback();
                    usuarioEsAdministrador = false;
                    ActualizarEstadoBoton();
                    MessageBox.Show("Solo un usuario administrador puede realizar cancelaciones.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool ventaActiva = db.Ventas.Any(venta => venta.GUID == folioCompra)
                    && db.DetalleVentas.Any(detalle => detalle.GUID == folioCompra)
                    && !db.Cancelaciones.Any(cancelacion => cancelacion.GUID == folioCompra);
                if (!ventaActiva)
                {
                    transaction.Rollback();
                    MessageBox.Show("El código no corresponde a una compra activa con boletos.", "Compra no disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    return;
                }

                bool facturaGenerada = db.Facturas.Any(factura => factura.GUID == folioCompra);
                db.Cancelaciones.Add(new Cancelaciones
                {
                    GUID = folioCompra,
                    Motivo = motivo,
                    FechaHora = DateTime.Now,
                    IDUsr = idUsuario
                });
                db.SaveChanges();
                transaction.Commit();

                string mensajeFactura = facturaGenerada
                    ? " La factura asociada quedó cancelada; se conserva su registro histórico."
                    : " Esta compra no tenía una factura asociada.";
                MessageBox.Show("La compra se canceló correctamente y sus boletos quedaron liberados." + mensajeFactura,
                    "Cancelación registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigoCompra.Clear();
                txtMotivo.Clear();
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo registrar la cancelación:\n" + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private sealed class VentaCancelacionRow
        {
            public Guid Folio { get; set; }
            public DateTime? Fecha { get; set; }
            public string Cliente { get; set; } = string.Empty;
            public int Boletos { get; set; }
            public decimal Total { get; set; }
        }

        private sealed class CancelacionRow
        {
            public Guid Folio { get; set; }
            public string Motivo { get; set; } = string.Empty;
            public DateTime Fecha { get; set; }
            public string Usuario { get; set; } = string.Empty;
            public string Factura { get; set; } = string.Empty;
        }
    }
}
