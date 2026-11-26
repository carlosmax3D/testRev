using CARR_PA_Proyecto_052.Data;
using CARR_PA_Proyecto_052.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CARR_PA_Proyecto_052
{
    public class HistorialService
    {
        public List<HistorialClienteDto> ObtenerHistorialPorId(int idCliente)
        {
            try
            {
                using (var db = new InventarioContext())
                {
                    return db.HistorialClienteResultados
                        .FromSqlRaw("EXEC sp_HistorialCliente @Busqueda = NULL, @ClienteID = {0}", idCliente)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el historial: " + ex.Message, "Error", MessageBoxButtons.OK);
                return new List<HistorialClienteDto>();
            }
        }
    }
}
