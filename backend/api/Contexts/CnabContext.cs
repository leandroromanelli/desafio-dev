using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Contexts
{
    public class CnabContext : DbContext
    {
        public CnabContext()
        {

        }

        public CnabContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<CnabEntry> CnabEntries { get; set; }
        public DbSet<CnabFile> CnabFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

    }
}
