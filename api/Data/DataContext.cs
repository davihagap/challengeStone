using Microsoft.EntityFrameworkCore;
using api.Domain.Models;

namespace api.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {

        }

        public DbSet<Conta> Contas {get; set; }

        public DbSet<Transacao> Transacoes {get; set; }
    }
}