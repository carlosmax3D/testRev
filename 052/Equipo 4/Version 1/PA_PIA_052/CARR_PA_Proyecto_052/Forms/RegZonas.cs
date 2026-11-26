using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using CARR_PA_Proyecto_052.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CARR_PA_Proyecto_052.Forms
{
    public partial class RegZonas : Form
    {
        private void ValidarCamposRegistro()
        {
            bool camposCompletos = !string.IsNullOrWhiteSpace(cajatxtNomZn.Text) &&
                    !string.IsNullOrWhiteSpace(cajatxtPrBase.Text) &&
                    !string.IsNullOrWhiteSpace(cajatxtCupo.Text) &&
                    !string.IsNullOrWhiteSpace(cajatxtfilas.Text) &&
                    !string.IsNullOrWhiteSpace(cajatxtAsintFilas.Text);

            bool esCupoValido = int.TryParse(cajatxtCupo.Text, out int capacidad) && capacidad > 0;

            bool esPrecioValido = decimal.TryParse(cajatxtPrBase.Text, out decimal precio) && precio > 0.00m;

            bool estadioSeleccionado = dgvEstadios.SelectedRows.Count > 0;

            btnConfirmar.Enabled = camposCompletos && esCupoValido && esPrecioValido && estadioSeleccionado;
        }

        public RegZonas()
        {
            InitializeComponent();
        }

        private void CargarDatosEstadios()
        {
            using (var db = new InventarioContext())
            {
                dgvEstadios.DataSource = db.Estadios.ToList();
            }
        }

        private void RegZonas_Load(object sender, EventArgs e)
        {
            CargarDatosEstadios();

            btnConfirmar.Enabled = false;

            cajatxtNomZn.TextChanged += (s, ev) => ValidarCamposRegistro();
            cajatxtPrBase.TextChanged += (s, ev) => ValidarCamposRegistro();
            cajatxtCupo.TextChanged += (s, ev) => ValidarCamposRegistro();
            cajatxtfilas.TextChanged += (s, ev) => ValidarCamposRegistro();
            cajatxtAsintFilas.TextChanged += (s, ev) => ValidarCamposRegistro();
            dgvEstadios.SelectionChanged += (s, ev) => ValidarCamposRegistro();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int IDEstadio;

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (dgvEstadios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un estadio de la lista.", "Estadio no seleccionado", MessageBoxButtons.OK);
                return;
            }

            DataGridViewRow fila = dgvEstadios.SelectedRows[0];
            IDEstadio = Convert.ToInt32(fila.Cells["IDEst"].Value);

            if (!int.TryParse(cajatxtCupo.Text, out int numeroCupo))
            {
                MessageBox.Show("Solo puede ingresar numeros enteros", "Numero de Cupo Invalido");
                return;
            }

            if (numeroCupo <= 0)
            {
                MessageBox.Show("Solo puede ingresar valores mayores a 0", "Numero de Cupo Invalido");
                return;
            }

            if (!int.TryParse(cajatxtfilas.Text, out int numeroFilas))
            {
                MessageBox.Show("Solo puede ingresar numeros enteros", "Numero de Filas Invalido");
                return;
            }

            if (numeroFilas <= 0)
            {
                MessageBox.Show("Solo puede ingresar valores mayores a 0", "Numero de Filas Invalido");
                return;
            }

            if (!int.TryParse(cajatxtAsintFilas.Text, out int numeroAsFilas))
            {
                MessageBox.Show("Solo puede ingresar numeros enteros", "Numero de Asientos Invalido");
                return;
            }

            if (numeroAsFilas <= 0)
            {
                MessageBox.Show("Solo puede ingresar valores mayores a 0", "Numero de Asientos Invalido");
                return;
            }

            if (numeroFilas * numeroAsFilas > numeroCupo)
            {
                MessageBox.Show("El numero de filas por el numero de asientos por filas no puede ser mas grande que el cupo de la zona", "Invalido");
                return;
            }

            if (!decimal.TryParse(cajatxtPrBase.Text, out decimal numeroPrBase))
            {
                MessageBox.Show("Solo puede ingresar numeros decimales (ej. 150.50)", "Numero de Precio Base Invalido");
                return;
            }

            if (numeroPrBase <= 0.00m)
            {
                MessageBox.Show("Solo puede ingresar valores mayores a 0.00", "Numero de Precio Base Invalido");
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Están los datos del estadio correctos?", "Confirmación", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    using (var db = new InventarioContext())
                    {
                        var nuevaZona = new Zonas
                        {
                            NomZona = cajatxtNomZn.Text,
                            PrecioBase = numeroPrBase,
                            Cupo = numeroCupo,
                            IDEst = IDEstadio
                        };
                        db.Zonas.Add(nuevaZona);
                        db.SaveChanges();

                        int idZonaGenerada = nuevaZona.IDZona;

                        for (int f = 1; f <= numeroFilas; f++)
                        {
                            var nuevaFilaObj = new Filas
                            {
                                NumFila = f,
                                IDZona = idZonaGenerada
                            };
                            db.Filas.Add(nuevaFilaObj);
                            db.SaveChanges();


                            for (int a = 1; a <= numeroAsFilas; a++)
                            {
                                var nuevoAsientoObj = new Asientos
                                {
                                    NumAsiento = a,
                                    IDFila = nuevaFilaObj.IDFila
                                };
                                db.Asientos.Add(nuevoAsientoObj);
                            }
                        }
                        db.SaveChanges();
                    }

                    MessageBox.Show("Zona, filas y asientos registrados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al registrar la zona:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
