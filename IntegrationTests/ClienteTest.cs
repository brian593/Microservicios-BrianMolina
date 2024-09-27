using ClientePersonaMicroService.Domain.Entities;

using System;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]

    public class ClienteTest
	{
        [Test]
        public void Cliente_Creation_ShouldSetPropertiesCorrectly_ForJoseLema()
        {
            // Arrange
            var cliente = new Cliente
            {
                Id = 1,
                Nombre = "Jose Lema",
                Direccion = "Otavalo sn y principal",
                Telefono = "098254785",
                Contrasena = "1234",
                Estado = true
            };

            // Assert
            Assert.AreEqual(1, cliente.Id);
            Assert.AreEqual("Jose Lema", cliente.Nombre);
            Assert.AreEqual("Otavalo sn y principal", cliente.Direccion);
            Assert.AreEqual("098254785", cliente.Telefono);
            Assert.AreEqual("1234", cliente.Contrasena);
            Assert.IsTrue(cliente.Estado);
        }

        [Test]
        public void Cliente_Creation_ShouldSetPropertiesCorrectly_ForMarianelaMontalvo()
        {
            // Arrange
            var cliente = new Cliente
            {
                Id = 2,
                Nombre = "Marianela Montalvo",
                Direccion = "Amazonas y NNUU",
                Telefono = "097548965",
                Contrasena = "5678",
                Estado = true
            };

            // Assert
            Assert.AreEqual(2, cliente.Id);
            Assert.AreEqual("Marianela Montalvo", cliente.Nombre);
            Assert.AreEqual("Amazonas y NNUU", cliente.Direccion);
            Assert.AreEqual("097548965", cliente.Telefono);
            Assert.AreEqual("5678", cliente.Contrasena);
            Assert.IsTrue(cliente.Estado);
        }

        [Test]
        public void Cliente_Creation_ShouldSetPropertiesCorrectly_ForJuanOsorio()
        {
            // Arrange
            var cliente = new Cliente
            {
                Id = 3,
                Nombre = "Juan Osorio",
                Direccion = "13 junio y Equinoccial",
                Telefono = "098874587",
                Contrasena = "1245",
                Estado = true
            };

            // Assert
            Assert.AreEqual(3, cliente.Id);
            Assert.AreEqual("Juan Osorio", cliente.Nombre);
            Assert.AreEqual("13 junio y Equinoccial", cliente.Direccion);
            Assert.AreEqual("098874587", cliente.Telefono);
            Assert.AreEqual("1245", cliente.Contrasena);
            Assert.IsTrue(cliente.Estado);
        }

    
    }
}

