using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

public partial class FrmEquipos : Form
{
    private readonly IEquipoService _equipos;
    // Equipo cargado en el campo con un clic en la tabla
    private Equipo? _seleccionado;

    public FrmEquipos(IEquipoService equipos)
    {
        InitializeComponent();
        _equipos = equipos;
        dgvEquipos.AutoGenerateColumns = false;
    }

    private void Cargar()
    {
        try
        {
            dgvEquipos.DataSource = _equipos.Consultar(txtBuscar.Text);
            dgvEquipos.ClearSelection();
            _seleccionado = null;
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Guardar(object? sender, EventArgs e)
    {
        try
        {
            if (!Mensajes.Confirmar("¿Registrar este equipo?"))
            {
                return;
            }

            _equipos.Registrar(txtNombre.Text);
            Mensajes.Informacion("Equipo registrado correctamente.");
            txtNombre.Clear();
            txtBuscar.Clear();
            Cargar();
            txtNombre.Focus();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Modificar(object? sender, EventArgs e)
    {
        if (_seleccionado is null)
        {
            Mensajes.Aviso("Haga clic en un equipo de la lista para modificarlo.");
            return;
        }

        try
        {
            if (!Mensajes.Confirmar("¿Guardar el nuevo nombre del equipo? Los partidos registrados mostrarán el nombre nuevo."))
            {
                return;
            }

            _equipos.Actualizar(_seleccionado, txtNombre.Text);
            Mensajes.Informacion("Equipo modificado correctamente.");
            Limpiar();
            Cargar();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Eliminar(object? sender, EventArgs e)
    {
        if (_seleccionado is null)
        {
            Mensajes.Aviso("Haga clic en un equipo de la lista para eliminarlo.");
            return;
        }

        try
        {
            if (!Mensajes.Confirmar($"¿Eliminar el equipo {_seleccionado.Nombre}?\n\nQuedará inactivo: ya no se podrá elegir al programar partidos, " +
                "pero los partidos donde ya jugó lo seguirán mostrando. No se puede eliminar si tiene partidos programados."))
            {
                return;
            }

            _equipos.Eliminar(_seleccionado);
            Mensajes.Informacion("Equipo eliminado (inactivo).");
            Limpiar();
            Cargar();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Reactivar(object? sender, EventArgs e)
    {
        if (_seleccionado is null)
        {
            Mensajes.Aviso("Haga clic en un equipo inactivo de la lista para reactivarlo.");
            return;
        }

        try
        {
            if (!Mensajes.Confirmar($"¿Reactivar el equipo {_seleccionado.Nombre}?"))
            {
                return;
            }

            _equipos.Reactivar(_seleccionado);
            Mensajes.Informacion("Equipo reactivado correctamente.");
            Limpiar();
            Cargar();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void dgvEquipos_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvEquipos.Rows[e.RowIndex].DataBoundItem is not Equipo equipo)
        {
            return;
        }

        _seleccionado = equipo;
        txtNombre.Text = equipo.Nombre;
    }

    private void Limpiar()
    {
        _seleccionado = null;
        txtNombre.Clear();
        dgvEquipos.ClearSelection();
        txtNombre.Focus();
    }

    private void btnBuscar_Click(object? sender, EventArgs e)
    {
        Cargar();
    }

    private void btnListarTodos_Click(object? sender, EventArgs e)
    {
        txtBuscar.Clear();
        Cargar();
    }

    private void btnLimpiar_Click(object? sender, EventArgs e)
    {
        Limpiar();
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void FrmEquipos_Load(object? sender, EventArgs e)
    {
        Cargar();
    }
}
