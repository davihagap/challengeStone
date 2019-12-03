using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain.Models;

namespace api.Domain.Repositories
{
    public interface IContaRepository
    {
        Task<IEnumerable<Conta>> ListAsync();
        Task<IConta> FindByNumAsync(int num);
        Task AddAsync(IConta conta);
        bool ContaExiste(IConta conta);
    }
}