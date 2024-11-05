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
            new(){ DogId = 1, Name = "Rex", Kind = "German Shepherd", DateOfBirth = new DateTime(2015, 6, 1) },
            new(){ DogId = 2, Name = "Bella", Kind = "Labrador", DateOfBirth = new DateTime(2018, 3, 12) },
            new(){ DogId = 3, Name = "Charlie", Kind = "Beagle", DateOfBirth = new DateTime(2020, 11, 5) },
            new(){ DogId = 4, Name = "Daisy", Kind = "Poodle", DateOfBirth = new DateTime(2017, 4, 22) },
            new(){ DogId = 5, Name = "Max", Kind = "Bulldog", DateOfBirth = new DateTime(2019, 8, 30) },
            new(){ DogId = 6, Name = "Mollie", Kind = "Corgi", DateOfBirth = new DateTime(2016, 5, 15) },
            new(){ DogId = 7, Name = "Buddy", Kind = "Rottweiler", DateOfBirth = new DateTime(2014, 12, 1) },
            new(){ DogId = 8, Name = "Lucy", Kind = "Schnauzer", DateOfBirth = new DateTime(2021, 9, 17) },
            new(){ DogId = 9, Name = "Titi", Kind = "Dachshund", DateOfBirth = new DateTime(2013, 2, 8) },
            new(){ DogId = 10, Name = "Rocky", Kind = "Chihuahua", DateOfBirth = new DateTime(2022, 7, 20) },
            new(){ DogId = 11, Name = "Oscar", Kind = "Bulldog", DateOfBirth = new DateTime(2014, 1, 15) },
            new(){ DogId = 12, Name = "Lola", Kind = "Labrador", DateOfBirth = new DateTime(2016, 3, 10) },
            new(){ DogId = 13, Name = "Zoe", Kind = "Beagle", DateOfBirth = new DateTime(2019, 5, 25) },
            new(){ DogId = 14, Name = "Jack", Kind = "Golden Retriever", DateOfBirth = new DateTime(2018, 2, 14) },
            new(){ DogId = 15, Name = "Maggie", Kind = "Husky", DateOfBirth = new DateTime(2017, 11, 20) },
            new(){ DogId = 16, Name = "Cooper", Kind = "Boxer", DateOfBirth = new DateTime(2019, 4, 30) },
            new(){ DogId = 17, Name = "Sadie", Kind = "Yorkshire Terrier", DateOfBirth = new DateTime(2020, 1, 5) },
            new(){ DogId = 18, Name = "Toby", Kind = "Shih Tzu", DateOfBirth = new DateTime(2016, 8, 18) },
            new(){ DogId = 19, Name = "Chloe", Kind = "Cocker Spaniel", DateOfBirth = new DateTime(2021, 3, 22) },
            new(){ DogId = 20, Name = "Duke", Kind = "Great Dane", DateOfBirth = new DateTime(2015, 12, 10) },
            new(){ DogId = 21, Name = "Sophie", Kind = "Maltese", DateOfBirth = new DateTime(2019, 6, 25) },
            new(){ DogId = 22, Name = "Oliver", Kind = "Pug", DateOfBirth = new DateTime(2020, 9, 9) },
            new(){ DogId = 23, Name = "Luna", Kind = "Boston Terrier", DateOfBirth = new DateTime(2018, 5, 15) },
            new(){ DogId = 24, Name = "Bailey", Kind = "Bichon Frise", DateOfBirth = new DateTime(2017, 10, 30) },
            new(){ DogId = 25, Name = "Riley", Kind = "Australian Shepherd", DateOfBirth = new DateTime(2022, 4, 2) },
            new(){ DogId = 26, Name = "Ziggy", Kind = "Pekingese", DateOfBirth = new DateTime(2016, 7, 8) },
            new(){ DogId = 27, Name = "Nala", Kind = "Chow Chow", DateOfBirth = new DateTime(2019, 8, 12) },
            new(){ DogId = 28, Name = "Finn", Kind = "Dalmatian", DateOfBirth = new DateTime(2021, 11, 19) },
            new(){ DogId = 29, Name = "Ruby", Kind = "Pit Bull", DateOfBirth = new DateTime(2015, 3, 3) },
            new(){ DogId = 30, Name = "Leo", Kind = "Vizsla", DateOfBirth = new DateTime(2020, 10, 21) },
            new(){ DogId = 31, Name = "Ella", Kind = "Basset Hound", DateOfBirth = new DateTime(2018, 6, 27) },
            new(){ DogId = 32, Name = "Jasper", Kind = "Saint Bernard", DateOfBirth = new DateTime(2017, 2, 14) },
            new(){ DogId = 33, Name = "Milo", Kind = "Newfoundland", DateOfBirth = new DateTime(2019, 1, 1) },
            new(){ DogId = 34, Name = "Ginger", Kind = "Akita", DateOfBirth = new DateTime(2021, 5, 30) },
            new(){ DogId = 35, Name = "Winston", Kind = "Cavalier King Charles Spaniel", DateOfBirth = new DateTime(2016, 9, 18) },
            new(){ DogId = 36, Name = "Penny", Kind = "Irish Setter", DateOfBirth = new DateTime(2018, 12, 5) },
            new(){ DogId = 37, Name = "Murphy", Kind = "Shetland Sheepdog", DateOfBirth = new DateTime(2020, 4, 4) },
            new(){ DogId = 38, Name = "Zara", Kind = "Rottweiler", DateOfBirth = new DateTime(2017, 8, 22) },
            new(){ DogId = 39, Name = "Coco", Kind = "Papillon", DateOfBirth = new DateTime(2022, 7, 10) },
            new(){ DogId = 40, Name = "Rusty", Kind = "Weimaraner", DateOfBirth = new DateTime(2015, 10, 15) },
            new(){ DogId = 41, Name = "Misty", Kind = "Whippet", DateOfBirth = new DateTime(2019, 3, 8) },
            new(){ DogId = 42, Name = "Simba", Kind = "American Bulldog", DateOfBirth = new DateTime(2020, 11, 11) },
            new(){ DogId = 43, Name = "Tina", Kind = "Border Collie", DateOfBirth = new DateTime(2016, 6, 25) },
            new(){ DogId = 44, Name = "Cody", Kind = "Bloodhound", DateOfBirth = new DateTime(2018, 1, 22) },
            new(){ DogId = 45, Name = "Lola", Kind = "French Bulldog", DateOfBirth = new DateTime(2021, 9, 29) },
            new(){ DogId = 46, Name = "Charlie", Kind = "Old English Sheepdog", DateOfBirth = new DateTime(2017, 12, 12) },
            new(){ DogId = 47, Name = "Bella", Kind = "Basenji", DateOfBirth = new DateTime(2020, 5, 1) },
            new(){ DogId = 48, Name = "Rocky", Kind = "Irish Wolfhound", DateOfBirth = new DateTime(2016, 4, 20) },
            new(){ DogId = 49, Name = "Shadow", Kind = "Cairn Terrier", DateOfBirth = new DateTime(2019, 8, 16) },
            new(){ DogId = 50, Name = "Nina", Kind = "Tibetan Mastiff", DateOfBirth = new DateTime(2022, 3, 5) }
        };
        modelBuilder.Entity<Domain.Dog>()
            .HasData(seedDogs);
    }
}