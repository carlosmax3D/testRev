using System;
using System.Collections.Generic;
using System.Text;
using CARR_PA_Proyecto_052.Models;
using Microsoft.EntityFrameworkCore;

namespace CARR_PA_Proyecto_052.Data
{
    public class InventarioContext : DbContext
    {
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Estadios> Estadios => Set<Estadios>();
        public DbSet<Partidos> Partidos => Set<Partidos>();
        public DbSet<Zonas> Zonas => Set<Zonas>();
        public DbSet<Filas> Filas => Set<Filas>();
        public DbSet<Asientos> Asientos => Set<Asientos>();
        public DbSet<Ventas> Ventas => Set<Ventas>();
        public DbSet<DetalleVentas> DetalleVentas => Set<DetalleVentas>();
        public DbSet<Facturas> Facturas => Set<Facturas>();
        public DbSet<Cancelaciones> Cancelaciones => Set<Cancelaciones>();
        public DbSet<VentasRealizadas> VentasRealizadas => Set<VentasRealizadas>();
        public DbSet<EquiposAJugar> EquiposAJugar => Set<EquiposAJugar>();
        public DbSet<TelefonosClientes> TelefonosClientes => Set<TelefonosClientes>();
        public DbSet<OcupacionDisponibilidad> VistaOcupacionDisponibilidad { get; set; }
        public DbSet<HistorialClienteDto> HistorialClienteResultados { get; set; }
        public DbSet<InformacionCliente>  VistaInformacionCliente { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=localhost\\SQLEXPRESS;" +
                    "Database=PIA_MAD;" +
                    "Trusted_Connection=True;" +
                    "TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .ToTable(tb => tb.UseSqlOutputClause(false));

            modelBuilder.Entity<Estadios>()
                .ToTable(tb => tb.UseSqlOutputClause(false));

            modelBuilder.Entity<Partidos>()
                .ToTable(tb => tb.UseSqlOutputClause(false));

            modelBuilder.Entity<OcupacionDisponibilidad>()
                .HasNoKey()
                .ToView("Vista_OcupacionDisponibilidad");

            modelBuilder.Entity<HistorialClienteDto>()
                .HasNoKey();

            modelBuilder.Entity<InformacionCliente>()
                .HasNoKey()
                .ToView("Vista_InformacionCliente");
        }
    }
}