using System;
using CuentaMovimientosMicroService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CuentaMovimientosMicroService.Infrastructure.DataBase
{
	public class AppDbContext:DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>()
                .HasMany(c => c.Movimientos)
                .WithOne(m => m.Cuenta)
                .HasForeignKey(m => m.CuentaId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}

