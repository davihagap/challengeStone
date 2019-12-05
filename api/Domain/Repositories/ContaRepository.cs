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

        public async Task<IEnumerable<Transacao>> ListTransacoesAsync(int NumConta)
        {
            return await this.context.Transacoes
                .Where(t => t.NumeroConta == NumConta)
                .ToListAsync();
        }

        public async Task SaveAsync(IConta conta)
        {
            this.context.Contas.Add((Conta) conta);
            await this.context.SaveChangesAsync();
        }

        public async Task SaveTransacaoAsync(ITransacao trans)
        {
            this.context.Transacoes.Add((Transacao) trans);
            await this.context.SaveChangesAsync();
        }
    }
}