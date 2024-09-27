using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CuentaMovimientosMicroService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuentaMovimientosMicroService.Infrastructure.WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IMovimientoRepository _movimientoRepository;

        public ReportesController(ICuentaRepository cuentaRepository, IMovimientoRepository movimientoRepository)
        {
            _cuentaRepository = cuentaRepository;
            _movimientoRepository = movimientoRepository;
        }

        // GET: api/Reportes?clienteId={clienteId}&fechaInicio={fechaInicio}&fechaFin={fechaFin}
        [HttpGet]
        public async Task<IActionResult> GetReporteEstadoCuenta(int clienteId, string fechaInicio, string fechaFin)
        {
            if (!DateTime.TryParseExact(fechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaInicioParsed))
            {
                return BadRequest("Formato de fechaInicio no válido. Use dd/MM/yyyy.");
            }

            if (!DateTime.TryParseExact(fechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaFinParsed))
            {
                return BadRequest("Formato de fechaFin no válido. Use dd/MM/yyyy.");
            }

            var cuentasCliente = await _cuentaRepository.GetCuentasConMovimientosPorClienteAsync(clienteId, fechaInicioParsed, fechaFinParsed);

            if (!cuentasCliente.Any())
            {
                return NotFound("No se encontraron cuentas o movimientos para el cliente especificado.");
            }

            return Ok(cuentasCliente);
        }

    }
}

