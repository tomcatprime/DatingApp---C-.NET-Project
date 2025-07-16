using API.Entities;
using Microsoft.EntityFrameworkCore;
namespace API.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options) //derive from DbContext, 
{
    // This class will be used to interact with the database.
    // It will contain DbSet properties for each entity type.
    // For example:
    // public DbSet<User> Users { get; set; }

    // You can also configure the database connection and other settings here.

    // Example constructor:
    // public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AppUser> Users { get; set; } // Represents a collection of AppUser entities in the database. Users represents table name in the database.


}

// Note: Ensure you have the necessary using directives for your entities and configurations.