using System;
using System.Text.Json.Serialization;

namespace CuentaMovimientosMicroService.Domain.Entities
{
    public class Movimiento
    {
        public int Id { get; set; }  // Clave primaria
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }

        public int CuentaId { get; set; }  // Clave foránea

        [JsonIgnore]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public Cuenta? Cuenta { get; set; }
    }
}

