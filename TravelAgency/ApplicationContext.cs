using Microsoft.EntityFrameworkCore;
using TravelAgency.Models;

namespace TravelAgency
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } 
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Entertainment> Entertainments { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketFlight> TicketFlightss { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Tour> Tours { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
                : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passport>().HasKey(u => new { u.PassportSeria, u.PassportNumber });
            modelBuilder.Entity<TicketFlight>().HasKey(u => new { u.TickerNumer, u.FlightId });
            modelBuilder.Entity<Aircraft>().HasKey(u => u.AircraftCode);
            modelBuilder.Entity<City>().HasKey(u => u.Id);
            modelBuilder.Entity<Contract>().HasKey(u => u.Id);
            modelBuilder.Entity<Entertainment>().HasKey(u => u.Id);
            modelBuilder.Entity<Flight>().HasKey(u => u.Id);
            modelBuilder.Entity<Food>().HasKey(u => u.Id);
            modelBuilder.Entity<Hotel>().HasKey(u => u.Id);
            modelBuilder.Entity<Position>().HasKey(u => u.Id);
            modelBuilder.Entity<Ticket>().HasKey(u => u.TickerNumer);
            modelBuilder.Entity<Tour>().HasKey(u => u.Id);

            modelBuilder
            .Entity<Passport>()
            .HasOne(u => u.Client)
            .WithOne(p => p.Passport)
            .HasForeignKey<Client>(p => p.PassportId);

            modelBuilder
            .Entity<City>()
            .HasOne(u => u.Tour)
            .WithOne(p => p.City)
            .HasForeignKey<Tour>(p => p.IdCity);
        }
    }
}

