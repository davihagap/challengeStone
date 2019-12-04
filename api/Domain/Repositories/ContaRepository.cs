using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Domain.Models;

namespace api.Domain.Repositories
{
    public class ContaRepository : IContaRepository
    {
        protected readonly DataContext context;

        public ContaRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Conta>> ListAsync()
        {
            return await this.context.Contas
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IConta> FindByNumAsync(int Num)
        {
            return await this.context.Contas.FindAsync(Num);
        }

        public async Task<IEnumerable<Transacao>> ListTransacoesAsync(IConta conta)
        {
            return await this.context.Transacoes
                .Where(t => t.Conta.Numero == conta.Numero)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveAsync(IConta conta)
        {
            this.context.Contas.Add((Conta) conta);
            await this.context.SaveChangesAsync();
        }


        public bool ContaExiste(IConta conta)
        {
            return this.context.Contas.Any(c => c.Numero == conta.Numero);
        }
    }
}