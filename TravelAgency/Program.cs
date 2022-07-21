using TravelAgency;
using TravelAgency.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory());
builder.AddJsonFile("appsettings.json");
var config = builder.Build();
string connectionString = config.GetConnectionString("DefaultConnection");

var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
var options = optionsBuilder.UseSqlServer(connectionString).Options;


using (ApplicationContext db = new ApplicationContext(options))
{

    Passport passport1 = new Passport { PassportNumber = "12345678", PassportSeria = "AB", Liquid =false};
    Passport passport2 = new Passport { PassportNumber = "12345671", PassportSeria = "AB", Liquid = false };
    db.Passports.AddRange(passport1, passport2);

    Client client1 = new Client { FirstName = "Tom", LastName = "Tomer", Insurance = true, Passport = passport1};
    Client client2 = new Client { FirstName = "Evgeniusz", LastName = "Slavik", Insurance = true, Passport = passport2};
    db.Clients.AddRange(client1, client2);

    Aircraft aircraft = new Aircraft { Model = "AK-55" };
    db.Aircrafts.Add(aircraft);

    City city1 = new City { Name = "Brest" };
    City city2 = new City { Name = "Minsk" };
    db.Cities.AddRange(city1, city2);

    Airport airport1 =new Airport { Name = "CoolAirport", City = city1 };
    Airport airport2 = new Airport { Name = "CoolAirport", City = city2 };
    db.Airports.AddRange(airport1, airport2);

    db.SaveChanges();
}

using (ApplicationContext db = new ApplicationContext(options))
{ 
    var clients = db.Clients.ToList();
    Console.WriteLine("Client list:");
    foreach (Client u in clients)
    {
        Console.WriteLine($"{u.Id}.{u.FirstName} - {u.LastName}");
    }
}
