using System;
namespace CuentaMovimientosMicroService.Application.DTOs
{
	public class MovimientoDto
	{
        public string NumeroCuenta { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal SaldoDisponible { get; set; }
    }
}

