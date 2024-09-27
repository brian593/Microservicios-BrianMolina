using System;
using CuentaMovimientosMicroService.Domain.Entities;

namespace CuentaMovimientosMicroService.Application.Interfaces
{
    public interface IMovimientoRepository
    {
        Task<Movimiento> GetMovimientoByIdAsync(int id);
        Task<IEnumerable<Movimiento>> GetAllMovimientosAsync();
        Task AddMovimientoAsync(Movimiento movimiento);
        Task UpdateMovimientoAsync(Movimiento movimiento);
        Task DeleteMovimientoAsync(int id);
    }
}

