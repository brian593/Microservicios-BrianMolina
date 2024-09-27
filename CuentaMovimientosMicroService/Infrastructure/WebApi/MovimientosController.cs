using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuentaMovimientosMicroService.Application.DTOs;
using CuentaMovimientosMicroService.Application.Interfaces;
using CuentaMovimientosMicroService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuentaMovimientosMicroService.Infrastructure.WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly ICuentaRepository _cuentaRepository;

        public MovimientosController(IMovimientoRepository movimientoRepository, ICuentaRepository cuentaRepository)
        {
            _movimientoRepository = movimientoRepository;
            _cuentaRepository = cuentaRepository;
        }

        // GET: api/Movimientos
        [HttpGet]
        public async Task<IActionResult> GetMovimientos()
        {
            var movimientos = await _movimientoRepository.GetAllMovimientosAsync();
            return Ok(movimientos);
        }

        // GET: api/Movimientos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovimiento(int id)
        {
            var movimiento = await _movimientoRepository.GetMovimientoByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            return Ok(movimiento);
        }

        // POST: api/Movimientos
        [HttpPost]
        public async Task<IActionResult> PostMovimiento(MovimientoDto movimientoDto)
        {
            // Obtener la cuenta asociada
            var cuenta = await _cuentaRepository.GetCuentaByIdNro(movimientoDto.NumeroCuenta);
            if (cuenta == null)
            {
                return NotFound("Cuenta no encontrada.");
            }

            // Obtener el último saldo de la cuenta (sin modificar el saldo inicial)
            decimal ultimoSaldo = cuenta.SaldoInicial;
            if (await _movimientoRepository.TieneMovimientosAsync(cuenta.Id))
            {
                ultimoSaldo = (decimal)await _movimientoRepository.GetUltimoSaldoByCuentaIdAsync(cuenta.Id);
            }

            // Verificar si el movimiento es un retiro y si hay saldo suficiente
            if (movimientoDto.Valor < 0 && ultimoSaldo + movimientoDto.Valor < 0)
            {
                return BadRequest("Saldo no disponible");
            }

            // No actualizar el saldo inicial de la cuenta, solo usar el último saldo para el movimiento
            decimal nuevoSaldo = ultimoSaldo + movimientoDto.Valor;

            // Registrar el movimiento
            var movimiento = new Movimiento
            {
                Fecha = movimientoDto.Fecha,
                TipoMovimiento = movimientoDto.TipoMovimiento,
                Valor = movimientoDto.Valor,
                Saldo = nuevoSaldo,  // Aquí guardamos el nuevo saldo
                CuentaId = cuenta.Id
            };

            await _movimientoRepository.AddMovimientoAsync(movimiento);

            return CreatedAtAction(nameof(GetMovimiento), new { id = movimiento.Id }, movimiento);
        }


        // PUT: api/Movimientos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimiento(int id, Movimiento movimiento)
        {
            if (id != movimiento.Id)
            {
                return BadRequest();
            }

            await _movimientoRepository.UpdateMovimientoAsync(movimiento);

            return NoContent();
        }

        // DELETE: api/Movimientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var movimiento = await _movimientoRepository.GetMovimientoByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            await _movimientoRepository.DeleteMovimientoAsync(id);

            return NoContent();
        }
    }
}

