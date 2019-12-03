using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain.Models;

namespace api.Domain.Services
{
    public interface IContaService
    {
        Task<IEnumerable<Conta>> ListAsync();
        Task<IConta> FindByNumAsync(int Num);
        Task SaveAsync(IConta conta);
        Task<IDeposito> DepositarAsync(int Num, decimal valor);
        Task<ISaque> SacarAsync(int Num, decimal valor);
    }
}