using System;
using CuentaMovimientosMicroService.Application.Interfaces;
using CuentaMovimientosMicroService.Domain.Entities;
using CuentaMovimientosMicroService.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CuentaMovimientosMicroService.Infrastructure.Persistence
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly AppDbContext _context;

        public CuentaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cuenta>> GetCuentasConMovimientosPorClienteAsync(int clienteId, DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Cuentas
                .Where(c => c.ClienteId == clienteId)
                .Include(c => c.Movimientos.Where(m => m.Fecha >= fechaInicio && m.Fecha <= fechaFin))
                .ToListAsync();
        }

        public async Task<Cuenta> GetCuentaByIdAsync(int id)
        {
            return await _context.Cuentas.FindAsync(id);
        }

        public async Task<Cuenta> GetCuentaByIdNro(string numeroCuenta)
        {
            return await _context.Cuentas?
                         .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);
        }

        public async Task<IEnumerable<Cuenta>> GetAllCuentasAsync()
        {
            return await _context.Cuentas.ToListAsync();
        }

        public async Task AddCuentaAsync(Cuenta cuenta)
        {
            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCuentaAsync(Cuenta cuenta)
        {
            _context.Cuentas.Update(cuenta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCuentaAsync(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta != null)
            {
                _context.Cuentas.Remove(cuenta);
                await _context.SaveChangesAsync();
            }
        }
    }
}

