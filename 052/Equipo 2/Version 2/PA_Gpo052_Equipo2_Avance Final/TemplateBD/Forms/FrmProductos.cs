using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmProductos : Form
{
    private readonly IProductoService _productoService;

    public FrmProductos(IProductoService productoService)
    {
        InitializeComponent();

        _productoService = productoService;
    }

    private void FrmProductos_Load(object sender, EventArgs e)
    {
        CargarProductos();
    }

    private void CargarProductos()
    {
        try
        {
            dgvProductos.DataSource =
                _productoService.ObtenerProductos();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"No fue posible cargar los productos.\n\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void btnNuevo_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Producto producto = new()
            {
                Nombre = txtNombre.Text.Trim(),
                Precio = nudPrecio.Value
            };

            _productoService.CrearProducto(producto);

            MessageBox.Show(
                "Producto registrado correctamente.",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarProductos();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void btnActualizar_Click(object sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show(
                    "Seleccione un producto válido.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            Producto producto = new()
            {
                Id = id,
                Nombre = txtNombre.Text.Trim(),
                Precio = nudPrecio.Value
            };

            _productoService.ActualizarProducto(producto);

            MessageBox.Show(
                "Producto actualizado correctamente.",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarProductos();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show(
                    "Seleccione un producto válido.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de eliminar el producto?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
            {
                return;
            }

            _productoService.EliminarProducto(id);

            MessageBox.Show(
                "Producto eliminado correctamente.",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarProductos();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void dgvProductos_CellClick(
        object sender,
        DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        DataGridViewRow fila =
            dgvProductos.Rows[e.RowIndex];

        txtId.Text =
            fila.Cells["Id"].Value?.ToString() ?? string.Empty;

        txtNombre.Text =
            fila.Cells["Nombre"].Value?.ToString() ?? string.Empty;

        if (decimal.TryParse(
            fila.Cells["Precio"].Value?.ToString(),
            out decimal precio))
        {
            nudPrecio.Value = precio;
        }
    }

    private void LimpiarFormulario()
    {
        txtId.Clear();
        txtNombre.Clear();
        nudPrecio.Value = 0;
        txtNombre.Focus();
    }
}
