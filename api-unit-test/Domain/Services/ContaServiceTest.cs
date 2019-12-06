using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using api.Controllers;
using api.Domain.Models;
using api.Domain.Services;
using api.Domain.Repositories;
using api.Domain.Exceptions;
using api.Domain.DTOs;
using api.Data;
using Xunit;
using Moq;

namespace api.UnitTests.Domain.Services
{
    public class ContaServiceTest
    {
        [Fact]
        public async Task CriaContaCorreto()
        {
            var repoMock = new Mock<IContaRepository>();  
            repoMock.Setup(x => x.FindByNumAsync(1)).Returns(Task.FromResult((IConta) null));

            var service = new ContaService(repoMock.Object);  
            IConta conta = new Conta( 1, "joao", 10m);
            await service.SaveAsync(conta);
        }

        [Fact]
        public void NumeroDeContaExistenteAoCriar()
        {
            var repoMock = new Mock<IContaRepository>();    
            IConta conta = new Conta( 1, "joao", 10m);
            repoMock.Setup(x => x.FindByNumAsync(1)).Returns(Task.FromResult(conta));

            var service = new ContaService(repoMock.Object);

            Assert.ThrowsAsync<NumeroDeContaExistenteException>(async () => await service.SaveAsync(conta));
        }

        [Theory]
        [InlineData(55,60,5,1)]
        [InlineData(14,110,96,92)]
        [InlineData(19,1000,981,977)]
        public async Task SaqueCorreto (decimal valor, decimal saldoAnterior, decimal saldoPosterior, decimal valorContaFinal)
        {
            var repoMock = new Mock<IContaRepository>();    
            IConta conta = new Conta( 1, "joao",saldoAnterior);
            repoMock.Setup(x => x.FindByNumAsync(1)).Returns(Task.FromResult(conta));

            var service = new ContaService(repoMock.Object);
            var transrequest = new TransacaoRequest{NumConta = 1, Valor = valor};
            var trans = await service.SacarAsync(transrequest);

            Assert.Equal(saldoAnterior, trans.SaldoAnterior);
            Assert.Equal(valor, trans.Valor);
            Assert.Equal(saldoPosterior, trans.SaldoPosterior);
            Assert.Equal(valorContaFinal, trans.Conta.Saldo);
        }

        [Fact]
        public void SaqueNaoAchaContaException ()
        {
            var repoMock = new Mock<IContaRepository>();    
            repoMock.Setup(x => x.FindByNumAsync(1)).Returns(Task.FromResult((IConta) null));

            var service = new ContaService(repoMock.Object);
            var transrequest = new TransacaoRequest{NumConta = 1, Valor = 10};

            Assert.ThrowsAsync<ContaNaoEncontradaException>(async () => await service.SacarAsync(transrequest));
        }

        [Fact]
        public void SaqueSaldoInsuficienteException ()
        {
            var repoMock = new Mock<IContaRepository>();    
            IConta conta = new Conta( 1, "joao", 13);
            repoMock.Setup(x => x.FindByNumAsync(1)).Returns(Task.FromResult(conta));

            var service = new ContaService(repoMock.Object);
            var transrequest = new TransacaoRequest{NumConta = 1, Valor = 10};

            Assert.ThrowsAsync<SaldoInsuficienteException>(async () => await service.SacarAsync(transrequest));
        }

        [Theory]
        [InlineData(55,60,115,114.45)]
        [InlineData(10,110,120,119.9)]
        [InlineData(2,3,5,4.98)]
        public async Task DepCorreto (decimal valor, decimal saldoAnterior, decimal saldoPosterior, decimal valorContaFinal)
        {
            var repoMock = new Mock<IContaRepository>();    
            IConta conta = new Conta( 1, "joao",saldoAnterior);
            repoMock.Setup(x => x.FindByNumAsync(1)).Returns(Task.FromResult(conta));

            var service = new ContaService(repoMock.Object);
            var transrequest = new TransacaoRequest{NumConta = 1, Valor = valor};
            var trans = await service.DepositarAsync(transrequest);

            Assert.Equal(saldoAnterior, trans.SaldoAnterior);
            Assert.Equal(valor, trans.Valor);
            Assert.Equal(saldoPosterior, trans.SaldoPosterior);
            Assert.Equal(valorContaFinal, trans.Conta.Saldo);
        }

        [Fact]
        public void DepNaoAchaContaException ()
        {
            var repoMock = new Mock<IContaRepository>();    
            repoMock.Setup(x => x.FindByNumAsync(1)).Returns(Task.FromResult((IConta) null));

            var service = new ContaService(repoMock.Object);
            var transrequest = new TransacaoRequest{NumConta = 1, Valor = 10};

            Assert.ThrowsAsync<ContaNaoEncontradaException>(async () => await service.DepositarAsync(transrequest));
        }
    }
}
