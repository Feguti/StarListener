using Microsoft.EntityFrameworkCore;
using STARListener.Models;

namespace STARListener.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Cada DbSet representa uma tabela no banco de dados
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PontuacaoPrioridade> PontuacoesPrioridade { get; set; }
        public DbSet<PontuacaoCriticidade> PontuacoesCriticidade { get; set; }
        public DbSet<PontuacaoData> PontuacoesData { get; set; } 
    }
}