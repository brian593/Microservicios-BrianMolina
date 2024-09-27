using System;
using CuentaMovimientosMicroService.Domain.Entities;
using CuentaMovimientosMicroService.Infrastructure.DataBase;
using CuentaMovimientosMicroService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests
{
    [TestFixture]
    public class CuentaTest
	{
        private AppDbContext _context;
        private CuentaRepository _cuentaRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlServer("Server=(local);Database=NeorisDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;").Options;

            _context = new AppDbContext(options);
            _cuentaRepository = new CuentaRepository(_context);
        }

        [Test]
        public async Task AddCuenta_ShouldAddCuentaToDatabase()
        {
            var cuenta = new Cuenta
            {
                NumeroCuenta = "123456",
                TipoCuenta = "Ahorro",
                SaldoInicial = 1000m,
                Estado = true,
                ClienteId = 1  
            };

            await _cuentaRepository.AddCuentaAsync(cuenta);
            var result = await _cuentaRepository.GetCuentaByIdAsync(cuenta.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(cuenta.NumeroCuenta, result.NumeroCuenta);
            Assert.AreEqual(cuenta.SaldoInicial, result.SaldoInicial);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}

