using api.Domain.Exceptions;
using api.Domain.Models;
using Xunit;

namespace api.UnitTests.Domain.Models
{
    public class ContaUnitTest
    {
        [Fact]
        public void DepositoCorreto()
        {
            var conta = new Conta(1, "joao", 20);

            conta.Creditar(30);
        
            Assert.Equal(50, conta.Saldo);
        }

        [Fact]
        public void SaqueCorreto()
        {
            var conta = new Conta(1, "joao", 20);

            conta.Debitar(10);
        
            Assert.Equal(10, conta.Saldo);
        }

        [Theory]
        [InlineData(0.01, 0.0)]
        [InlineData(5, 4)]
        [InlineData(900, 899.99)]
        public void SaqueSaldoInsuficiente(decimal valor, decimal saldoAnterior)
        {
            var conta = new Conta(1, "joao", saldoAnterior);

            Assert.Throws<SaldoInsuficienteException>(() => conta.Debitar(valor));
        }
    }
}