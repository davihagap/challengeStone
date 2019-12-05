using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Domain.Models;
using api.Domain.Repositories;
using api.Domain.DTOs;
using api.Domain.Exceptions;

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
                throw new ContaNaoEncontradaException("Conta  não encontrada");

            return conta;
        }

        public async Task<IEnumerable<Transacao>> ListTransacoesAsync(int Num)
        {
            return await _repository.ListTransacoesAsync(Num);
        }

        public async Task SaveAsync(IConta conta)
        {
            if(await _repository.FindByNumAsync(conta.Numero) != null)
                throw new NumeroDeContaExistenteException("Número de conta existente");
            await _repository.SaveAsync(conta);
        }

        public async Task<IDeposito> DepositarAsync(TransacaoRequest trans)
        {
            var conta = await FindByNumAsync(trans.NumConta);
            var saldoAnterior = conta.Saldo;

            conta.Creditar(trans.Valor);

            var transacao = new Deposito(
                conta,
                DateTime.Now, 
                saldoAnterior, 
                conta.Saldo,
                trans.Valor);
            
            await this._repository.SaveTransacaoAsync(transacao);
            await this._repository.SaveTransacaoAsync(transacao.calculaTaxa());

            return transacao;
        }

        public async Task<ISaque> SacarAsync(TransacaoRequest trans)
        {
            var conta = await FindByNumAsync(trans.NumConta);
            var saldoAnterior = conta.Saldo;

            conta.Debitar(trans.Valor);
    
            var transacao = new Saque(
                conta,
                DateTime.Now, 
                saldoAnterior, 
                conta.Saldo,
                trans.Valor);

            var taxa = transacao.calculaTaxa();
            
            await this._repository.SaveTransacaoAsync(transacao);
            await this._repository.SaveTransacaoAsync(taxa);

            return transacao;
        }

    }
}