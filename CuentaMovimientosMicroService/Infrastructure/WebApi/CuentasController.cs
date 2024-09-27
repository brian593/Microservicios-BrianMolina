using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuentaMovimientosMicroService.Application.Interfaces;
using CuentaMovimientosMicroService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuentaMovimientosMicroService.Infrastructure.WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly RabbitMQPublisher _rabbitMqPublisher;
        private readonly RabbitMQConsumer _rabbitMqConsumer;

        public CuentasController(ICuentaRepository cuentaRepository, RabbitMQPublisher rabbitMqPublisher, RabbitMQConsumer rabbitMqConsumer)
        {
            _cuentaRepository = cuentaRepository;
            _rabbitMqPublisher = rabbitMqPublisher;
            _rabbitMqConsumer = rabbitMqConsumer;
        }

        // GET: api/Cuentas
        [HttpGet]
        public async Task<IActionResult> GetCuentas()
        {
            var cuentas = await _cuentaRepository.GetAllCuentasAsync();
            return Ok(cuentas);
        }

        // GET: api/Cuentas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCuenta(int id)
        {
            var cuenta = await _cuentaRepository.GetCuentaByIdAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }
            return Ok(cuenta);
        }

        // POST: api/Cuentas
        [HttpPost]
        public async Task<IActionResult> PostCuenta(Cuenta cuenta)
        {
            // Publicar solicitud de validación
            _rabbitMqPublisher.PublishValidarCliente(cuenta.ClienteId);

            // Esperar la respuesta de validación
            var isValid = await _rabbitMqConsumer.WaitForValidationResponseAsync();

            if (!isValid)
            {
                return BadRequest("Cliente no válido");
            }

            // Si el cliente es válido, agregar la cuenta
            await _cuentaRepository.AddCuentaAsync(cuenta);
            return CreatedAtAction(nameof(GetCuenta), new { id = cuenta.Id }, cuenta);
        }

        // PUT: api/Cuentas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuenta(int id, Cuenta cuenta)
        {
            if (id != cuenta.Id)
            {
                return BadRequest();
            }

            await _cuentaRepository.UpdateCuentaAsync(cuenta);

            return NoContent();
        }

        // DELETE: api/Cuentas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(int id)
        {
            var cuenta = await _cuentaRepository.GetCuentaByIdAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            await _cuentaRepository.DeleteCuentaAsync(id);

            return NoContent();
        }
    }
}

