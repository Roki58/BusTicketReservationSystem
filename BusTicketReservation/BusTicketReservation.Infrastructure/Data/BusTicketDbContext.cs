using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using BusTicketReservation.Domain.Entities;

namespace BusTicketReservation.Infrastructure.Data
{
    public class BusTicketDbContext : DbContext
    {
        public BusTicketDbContext(DbContextOptions<BusTicketDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<BusSchedule> BusSchedules { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Force all DateTime to be stored as UTC
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(
                            new ValueConverter<DateTime, DateTime>(
                                v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                                v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                        );
                    }
                }
            }

            base.OnModelCreating(modelBuilder);
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var busId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var busId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var routeId1 = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var routeId2 = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var scheduleId1 = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var scheduleId2 = Guid.Parse("66666666-6666-6666-6666-666666666666");

            // ✅ UTC DateTime instead of local
            var tomorrow = DateTime.UtcNow.Date.AddDays(1);

            // Buses
            modelBuilder.Entity<Bus>().HasData(
                new Bus
                {
                    Id = busId1,
                    CompanyName = "Green Line Paribahan",
                    BusName = "Green Line Express",
                    TotalSeats = 40,
                    BasePrice = 800
                },
                new Bus
                {
                    Id = busId2,
                    CompanyName = "Shyamoli Paribahan",
                    BusName = "Shyamoli Deluxe",
                    TotalSeats = 36,
                    BasePrice = 900
                }
            );

            // Routes
            modelBuilder.Entity<Route>().HasData(
                new Route
                {
                    Id = routeId1,
                    FromCity = "Dhaka",
                    ToCity = "Chittagong",
                    DurationMinutes = 360
                },
                new Route
                {
                    Id = routeId2,
                    FromCity = "Dhaka",
                    ToCity = "Sylhet",
                    DurationMinutes = 300
                }
            );

            // BusSchedules
            modelBuilder.Entity<BusSchedule>().HasData(
                new BusSchedule
                {
                    Id = scheduleId1,
                    BusId = busId1,
                    RouteId = routeId1,
                    JourneyDate = tomorrow,
                    DepartureTime = new TimeSpan(8, 0, 0),
                    ArrivalTime = new TimeSpan(14, 0, 0)
                },
                new BusSchedule
                {
                    Id = scheduleId2,
                    BusId = busId2,
                    RouteId = routeId2,
                    JourneyDate = tomorrow,
                    DepartureTime = new TimeSpan(9, 30, 0),
                    ArrivalTime = new TimeSpan(14, 30, 0)
                }
            );
        }
    }
}
