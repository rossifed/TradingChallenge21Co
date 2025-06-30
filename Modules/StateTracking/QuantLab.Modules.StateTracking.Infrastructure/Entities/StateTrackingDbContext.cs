using Microsoft.EntityFrameworkCore;
namespace QuantLab.Modules.StateTracking.Infrastructure.Entities
{
    internal class StateTrackingDbContext : DbContext
    {
        public StateTrackingDbContext(DbContextOptions<StateTrackingDbContext> opts)
      : base(opts) { }

        public DbSet<Trade> Trades { get; set; } = default!;
        public DbSet<Position> Positions { get; set; } = default!;

        public DbSet<BestBidOffer> BestBidOffers { get; set; } = default!;
        public DbSet<PositionPnL> PositionPnLs { get; set; } = default!;

        public DbSet<OrderRejection> OrderRejections { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BestBidOffer>().HasNoKey();
            modelBuilder.Entity<OrderRejection>()
                .HasOne(or => or.Order)
                .WithMany()
                .HasForeignKey(or => or.OrderId);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.UseInMemoryDatabase("Name=TradingDatabase");
    }
}
