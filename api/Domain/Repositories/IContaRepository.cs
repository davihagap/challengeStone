using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain.Models;

namespace api.Domain.Repositories
{
    public interface IContaRepository
    {
        Task<IEnumerable<Conta>> ListAsync();
        Task<IConta> FindByNumAsync(int num);
        Task<IEnumerable<Transacao>> ListTransacoesAsync(int NumConta);        
        Task SaveAsync(IConta conta);
        Task SaveTransacaoAsync(ITransacao trans);
    }
}