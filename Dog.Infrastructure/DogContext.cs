using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Dog.Infrastructure;

public class DogContext : DbContext
{
    public DbSet<Domain.Dog> Dogs { get; set; }
    public DogContext()
    {
        
    }
    public DogContext(DbContextOptions<DogContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Dog.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var seedDogs = new List<Domain.Dog>()
        {
            new() {DogId = 1, DateOfBirth = new DateTime(1980, 09, 09), Name = "Mollie", Kind = "Bulldog"},
            new() {DogId = 2, DateOfBirth = new DateTime(1980, 09, 09), Name = "Sam", Kind = "Labrador"},
            new() {DogId = 3, DateOfBirth = new DateTime(1980, 09, 09), Name = "Titi", Kind = "Corgi"},
            new() {DogId = 4, DateOfBirth = new DateTime(1980, 09, 09), Name = "Rex", Kind = "German shepherd"},
        };
        modelBuilder.Entity<Domain.Dog>()
            .HasData(seedDogs);
    }
}