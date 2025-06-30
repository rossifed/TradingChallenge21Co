using Microsoft.EntityFrameworkCore;
namespace QuantLab.Modules.Risk.Infrastructure.Entities
{
    internal class RiskDbContext : DbContext
    {
        public RiskDbContext(DbContextOptions<RiskDbContext> opts)
      : base(opts) { }


        public DbSet<Position> Positions { get; set; } = default!;


        public DbSet<PositionPnL> PositionPnLs { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.UseInMemoryDatabase("Name=TradingDatabase");
    }
}
