using System;
namespace CuentaMovimientosMicroService.Domain.Entities
{
	public class Cuenta
	{
        public int Id { get; set; }  // Clave primaria
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }

        public List<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

        public int ClienteId { get; set; }  // Llave foránea que referencia al cliente
    }
}

