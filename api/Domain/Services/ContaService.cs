using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Domain.Models;
using api.Domain.Repositories;

namespace api.Domain.Services
{
    public class ContaService: IContaService
    {
        private readonly IContaRepository _repository;
        public ContaService(IContaRepository repository) => _repository = repository;

        public async Task<IEnumerable<Conta>> ListAsync()
        {
            return await _repository.ListAsync();
        }

        public async Task<IConta> FindByNumAsync(int Num)
        {
            var conta = await _repository.FindByNumAsync(Num);
            if (conta == null) 
                throw new System.Exception("Conta  não encontrada");

            return conta;
        }

        public async Task SaveAsync(IConta conta)
        {
            await _repository.SaveAsync(conta);
        }

        public async Task<IDeposito> DepositarAsync(int Num, decimal valor)
        {
            var conta = await FindByNumAsync(Num);
            var saldoAnterior = conta.Saldo;

            conta.Creditar(valor);

            var transacao = new Deposito(conta,
                DateTime.Now, 
                saldoAnterior, 
                conta.Saldo,
                valor);
            transacao.calculaTaxa();

            await this._repository.SaveAsync(transacao);

            return transacao;
        }

        public async Task<ISaque> SacarAsync(int Num, decimal valor)
        {
            var conta = await FindByNumAsync(Num);
            var saldoAnterior = conta.Saldo;

            conta.Debitar(valor);

            var transacao = new Saque(conta,
                DateTime.Now, 
                saldoAnterior, 
                conta.Saldo,
                valor);
            transacao.calculaTaxa();

            await this._repository.SaveAsync(transacao);

            return transacao;
        }

    }
}