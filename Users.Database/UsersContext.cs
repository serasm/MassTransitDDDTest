using System;
using Microsoft.EntityFrameworkCore;
using Users.Domain;

namespace Users.Database;

public class UsersContext : DbContext
{
    public DbSet<User> Users { get; set; }

    private UsersContext()
    {
        
    }
    
    public UsersContext(DbContextOptions<UsersContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine).EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersContext).Assembly);
    }
}