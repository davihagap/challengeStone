using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain.Models;
using api.Domain.DTOs;

namespace api.Domain.Services
{
    public interface IContaService
    {
        Task<IEnumerable<Conta>> ListAsync();
        Task<IConta> FindByNumAsync(int Num);
        Task<IEnumerable<Transacao>> ListTransacoesAsync(int Num);
        Task SaveAsync(IConta conta);
        Task<IDeposito> DepositarAsync(TransacaoRequest trans);
        Task<ISaque> SacarAsync(TransacaoRequest trans);
    }
}