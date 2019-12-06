using System;
using Moq;
using Xunit;
using api.Domain.Models;

namespace api.UnitTests.Domain.Models
{
    public class DepositoUnitTest
    {
        [Theory]
        [InlineData(12.30, 0.12)]
        [InlineData(1.06, 0.01)]
        public void CalculaTaxaCorretamente(decimal valor, decimal esperado)
        {
            var contaMock = new Mock<IConta>();

            contaMock.Setup(x => x.Saldo).Returns(0);
            
            var deposito = new Deposito(contaMock.Object, DateTime.Now, 0, valor, valor);

            var taxa = deposito.calculaTaxa();

            Assert.Equal(esperado, taxa.Valor);
        }
    }
}