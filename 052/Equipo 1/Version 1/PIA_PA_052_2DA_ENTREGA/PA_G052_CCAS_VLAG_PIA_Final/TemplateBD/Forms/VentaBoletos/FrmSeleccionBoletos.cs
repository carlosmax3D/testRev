using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;
using System.IO;

namespace TemplateCSharp.Forms.VentaBoletos
{
    public partial class FrmSeleccionBoletos : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IZonaService _zonaService;
        private readonly IAsientoService _asientoService;
        private readonly IDetalleBoletoRepository _detalleRepository; //Para validar asientos ocupados
        private readonly IReporteService _reporteService;
        private readonly IEstadioService _estadioService;

        private List<Asiento> _asientosDisponibles = new List<Asiento>();
        private List<DetalleBoleto> _boletosParaComprar = new List<DetalleBoleto>();
        private Zona? _zonaActual = null;

        public FrmSeleccionBoletos(IServiceProvider serviceProvider, IZonaService zonaService, IAsientoService asientoService, IDetalleBoletoRepository detalleRepository, IReporteService reporteService, IEstadioService estadioService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _zonaService = zonaService;
            _asientoService = asientoService;
            _estadioService = estadioService;
            _detalleRepository = detalleRepository;
            _reporteService = reporteService;
        }

        private void FrmSeleccionBoletos_Load(object sender, EventArgs e)
        {
            if (VentaContext.PartidoSeleccionado == null)
            {
                MessageBox.Show("No hay un partido seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            CargarResumenPartido();
            CargarZonas();
            ActualizarTotales();
            CargarMapaEstadio();


        }

        private void CargarMapaEstadio()
        {
            try
            {
                var partido = VentaContext.PartidoSeleccionado;
                if (partido == null) return;

                var estadio = _estadioService.ObtenerEstadio(partido.IdEstadio);
                if (estadio == null || string.IsNullOrWhiteSpace(estadio.MapaEstadio)) return;

                string rutaImagen = Path.Combine(Application.StartupPath, "Mapas", estadio.MapaEstadio);
                if (File.Exists(rutaImagen))
                {
                    picMapaEstadio.Image = Image.FromFile(rutaImagen);
                    picMapaEstadio.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch { } //Si falla la imagen, no pasa nada, el usuario puede seguir comprando
        }

        private void CargarResumenPartido()
        {
            try
            {
                var partido = VentaContext.PartidoSeleccionado;
                if (partido == null) return;

                var resumen = _reporteService.ObtenerResumenPartido(partido.IdPartido);
                if (resumen != null)
                {
                    lblResumenPartido.Text =
                        $"Vendido: {resumen.TotalVendido:C2}   |   " +
                        $"Disponibles: {resumen.AsientosDisponiblesTotal}   |   " +
                        $"Ocupación: {resumen.PorcentajeOcupacionPromedio:N2}%";
                }
            }
            catch
            {
                lblResumenPartido.Text = "";
            }
        }

        private void CargarZonas()
        {
            try
            {
                var partido = VentaContext.PartidoSeleccionado;
                if (partido == null) return;

                var zonas = _zonaService.ObtenerZonasPorEstadio(partido.IdEstadio);
                
                //desconectamos el evento temporalmente para que no se dispare al asignar la lista
                cmbZona.SelectedIndexChanged -= cmbZona_SelectedIndexChanged;

                cmbZona.DataSource = zonas;
                cmbZona.DisplayMember = "NombreZona";
                cmbZona.ValueMember = "IdZona";
                cmbZona.SelectedIndex = -1; // Lo dejamos en blanco

                // Volvemos a conectar el evento
                cmbZona.SelectedIndexChanged += cmbZona_SelectedIndexChanged;

                //limpiamos por si acaso
                _zonaActual = null;
                flowLayoutPanel1.Controls.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar zonas: " + ex.Message);
            }
        }

        private void cmbZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si el usuario deselecciona o no hay nada, limpiamos todo
            if (cmbZona.SelectedIndex == -1 || cmbZona.SelectedItem == null)
            {
                _zonaActual = null;
                flowLayoutPanel1.Controls.Clear();
                return;
            }

            _zonaActual = (Zona)cmbZona.SelectedItem;
            label4.Text = _zonaActual.PrecioBase.ToString("C2");

            try
            {
                _asientosDisponibles = _asientoService.ObtenerAsientosPorZona(_zonaActual.IdZona);
                DibujarAsientos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar asientos: " + ex.Message);
            }
        }

        private void DibujarAsientos()
        {
            var partido = VentaContext.PartidoSeleccionado;
            if (partido == null) return;

            flowLayoutPanel1.Controls.Clear();
            if (_zonaActual == null) return;

            foreach (var asiento in _asientosDisponibles)
            {
                Button btnAsiento = new Button
                {
                    Text = $"{asiento.Fila}-{asiento.NumeroAsiento}",
                    Size = new Size(80, 40),
                    Tag = asiento
                };

                // VALIDACIÓN CLAVE: Checar si el asiento ya está vendido en la BD para este partido
                bool ocupado = _detalleRepository.IsAsientoOcupado(asiento.IdAsiento, partido.IdPartido);

                if (ocupado)
                {
                    btnAsiento.BackColor = Color.LightCoral; // Rojo clarito
                    btnAsiento.Enabled = false; // No se puede hacer clic muejejeje
                    btnAsiento.Text = $"{asiento.Fila}-{asiento.NumeroAsiento} (X)";
                }
                else
                {
                    //revisar si este asiento ya esta en nuestra lista de compra
                    bool yaSeleccionado = _boletosParaComprar.Any(d => d.IdAsiento == asiento.IdAsiento);

                    if (yaSeleccionado)
                        btnAsiento.BackColor = Color.LightGreen; //Si ya lo teniamos, se queda verde
                    else
                        btnAsiento.BackColor = SystemColors.Control; //Si no, se queda gris

                    btnAsiento.Click += BtnAsiento_Click;
                }

                flowLayoutPanel1.Controls.Add(btnAsiento);
            }
        }

        private void BtnAsiento_Click(object? sender, EventArgs e)
        {
            Button btn = (Button)sender!;
            Asiento asiento = (Asiento)btn.Tag!;

            var partido = VentaContext.PartidoSeleccionado;
            if (partido == null) return;

            // Si ya estaba seleccionado (Verde), lo quitamos
            if (btn.BackColor == Color.LightGreen)
            {
                btn.BackColor = SystemColors.Control;

                var detalle = _boletosParaComprar.FirstOrDefault(d => d.IdAsiento == asiento.IdAsiento);
                if (detalle != null) _boletosParaComprar.Remove(detalle);
            }
            else
            {
                // Si intenta poner más de 4
                if (_boletosParaComprar.Count >= 4)
                {
                    MessageBox.Show("Máximo 4 boletos por transacción.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btn.BackColor = Color.LightGreen;

                _boletosParaComprar.Add(new DetalleBoleto
                {
                    IdPartido = partido.IdPartido,
                    IdAsiento = asiento.IdAsiento,
                    PrecioUnitario = _zonaActual!.PrecioBase,
                    // Datos para visualización en la tabla
                    NombreZona = _zonaActual.NombreZona,
                    Fila = asiento.Fila,
                    NumeroAsiento = asiento.NumeroAsiento
                });
            }

            ActualizarGridYContador();
        }

        private void ActualizarGridYContador()
        {
            lblVistaAsiento.Text = $"{_boletosParaComprar.Count} / 4";

            dgvDetalleBoleto.AutoGenerateColumns = false;
            dgvDetalleBoleto.DataSource = null;
            dgvDetalleBoleto.DataSource = _boletosParaComprar.ToList();

            
            if (dgvDetalleBoleto.Columns.Count >= 4)
            {
                dgvDetalleBoleto.Columns[0].DataPropertyName = "NombreZona";
                dgvDetalleBoleto.Columns[1].DataPropertyName = "Fila";
                dgvDetalleBoleto.Columns[2].DataPropertyName = "NumeroAsiento";
                dgvDetalleBoleto.Columns[3].DataPropertyName = "PrecioUnitario";
                dgvDetalleBoleto.Columns[3].DefaultCellStyle.Format = "C2";
            }

            ActualizarTotales();
        }

        private void ActualizarTotales()
        {
            decimal subtotal = _boletosParaComprar.Sum(d => d.PrecioUnitario);
            decimal iva = subtotal * 0.16m; // 16%
            decimal total = subtotal + iva;

            
            lblSubtotal.Text = $"Subtotal: {subtotal:C2}";
            lblIva.Text = $"IVA (16%): {iva:C2}";
            lblTotal.Text = $"Total: {total:C2}";
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            var partido = VentaContext.PartidoSeleccionado;
            if (partido == null) return;

            if (_boletosParaComprar.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un asiento.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Guardamos los boletos en el contexto global (el carrito o mochila)
            VentaContext.BoletosSeleccionados = _boletosParaComprar;

            this.Hide();
            using var form = _serviceProvider.GetRequiredService<FrmSeleccionPago>();
            form.ShowDialog();

            if (form.DialogResult == DialogResult.Cancel)
            {
                DibujarAsientos();
                this.Show();
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            VentaContext.BoletosSeleccionados = new List<DetalleBoleto>();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}