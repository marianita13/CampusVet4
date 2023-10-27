using System.Reflection;
using Domain.entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class CampusVet4Context : DbContext
{
    public CampusVet4Context(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Appointment> Appointments {get; set;}
    public DbSet<Breed> Breeds {get; set;}
    public DbSet<City> Citys {get; set;}
    public DbSet<Client> Clients {get; set;}
    public DbSet<ClientAddress> ClientAddresses {get; set;}
    public DbSet<ClientPhone> ClientPhones {get; set;}
    public DbSet<Country> Countrys {get; set;}
    public DbSet<Pet> Pets {get; set;}
    public DbSet<Service> Services {get; set;}
    public DbSet<State> States {get; set;}
    public DbSet<Rol> Roles {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
