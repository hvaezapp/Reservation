using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Entities;
using System.Reflection;

namespace Reservation.Infrastructure.Persistence.Context
{
    public class ReservationDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        private const string DefaultSchema = "reservation";
        public const string  DefaultConnectionStringName = "SvcDbContext";

        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(DefaultSchema);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
