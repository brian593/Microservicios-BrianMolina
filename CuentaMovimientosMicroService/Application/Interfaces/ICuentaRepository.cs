using System;
using CuentaMovimientosMicroService.Domain.Entities;

namespace CuentaMovimientosMicroService.Application.Interfaces
{
	public interface ICuentaRepository
	{
        Task<Cuenta> GetCuentaByIdAsync(int id);
        Task<Cuenta> GetCuentaByIdNro(string numeroCuenta);
        Task<IEnumerable<Cuenta>> GetAllCuentasAsync();
        Task AddCuentaAsync(Cuenta cuenta);
        Task UpdateCuentaAsync(Cuenta cuenta);
        Task DeleteCuentaAsync(int id);
        Task<List<Cuenta>> GetCuentasConMovimientosPorClienteAsync(int clienteId, DateTime fechaInicio, DateTime fechaFin);
    }
}

