using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using api.Controllers;
using api.Models;
using api.Data;
using Xunit;

namespace api_test
{
    public class ContaControllerTest
    {
        private readonly ContaController _controller;

        public ContaControllerTest()
        {
            _controller = new ContaController();            
        }

        [Fact]
        public void TestCriaContaValida()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "cria_conta_valida")
                .Options;
            using(var context = new DataContext(options))
            {                
                _controller.PostCriaConta(context, new Conta{ Cliente = "joao", Saldo = 400});
                Assert.Equal(1, context.Contas.Count());
            }
        }

        [Fact]
        public void TestGetConta()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "get_conta")
                .Options;

            using(var context = new DataContext(options))
            {                
                context.Contas.Add(new Conta{ Numero = 3355, Cliente = "joao", Saldo = 100});
                context.SaveChanges();
            }

            using(var context = new DataContext(options))
            {                
                var result = _controller.GetByNum(context, 3355);
                Assert.Equal(1, context.Contas.Count());
            }
        }

        [Fact]
        public void TestGetContas()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "get_contas")
                .Options;

            using(var context = new DataContext(options))
            {                
                context.Contas.Add(new Conta{ Numero = 3355, Cliente = "joao", Saldo = 100});
                context.SaveChanges();
            }

            using(var context = new DataContext(options))
            {                
                var result = _controller.Get(context).Result.Value.Count;
                Assert.Equal(1, result);
            }
        }

        [Fact]
        public void TestValorRealizaSaque()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "realiza_saque")
                .Options;
            
            using(var context = new DataContext(options))
            {                
                context.Contas.Add(new Conta{ Numero = 3355, Cliente = "joao", Saldo = 100});
                context.SaveChanges();
            }

            using(var context = new DataContext(options))
            {                
                _controller.PostSaque(context, new TransacaoViewModel{ Descricao = "saque", Valor = 50}, 3355);
                Assert.Equal(46, context.Contas.Single().Saldo);
            }
        }

        [Fact]
        public void TestValorRealizaDeposito()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "realiza_deposito")
                .Options;
            
            using(var context = new DataContext(options))
            {                
                context.Contas.Add(new Conta{ Numero = 3355, Cliente = "joao", Saldo = 100});
                context.SaveChanges();
            }

            using(var context = new DataContext(options))
            {                
                _controller.PostDeposito(context, new TransacaoViewModel{ Descricao = "deposito", Valor = 50}, 3355);
                Assert.Equal(new decimal(149.5), context.Contas.Single().Saldo);
            }
        }

        [Fact]
        public void TestExtrato()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "extrato")
                .Options;
            
            using(var context = new DataContext(options))
            {                
                context.Contas.Add(new Conta{ Numero = 3355, Cliente = "joao", Saldo = 100});
                context.Transacoes.Add(new Transacao{                     
                    Descricao = "deposito",
                    Tipo="Deposito", 
                    Data = DateTime.Now, 
                    Valor = 10,
                    ContaNum = 3355
                });
                context.Transacoes.Add(new Transacao{ 
                    Descricao = "taxa sobre deposito",
                    Tipo="taxa", 
                    Data = DateTime.Now,
                    Valor = new decimal(0.01),
                    ContaNum = 3355
                });
                context.SaveChanges();
            }

            using(var context = new DataContext(options))
            {                
                var result = _controller.GetExtratos(context, 3355).Result.Value.Count;
                
                Assert.Equal(2, result);
            }
        }
    }
}
