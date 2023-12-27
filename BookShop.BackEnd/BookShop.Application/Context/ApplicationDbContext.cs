using System.Security.Cryptography;
using System.Text;
using BookShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Application.Context;

#nullable disable
public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    
    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        using var hmac = new HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Admin12345678"));
        modelBuilder.Entity<User>(option =>
        {
            option.HasData(new User()
            {
                Id = 1,
                PasswordHash = hash,
                PasswordSalt = salt,
                Login = "Admin"
            });
        });
        
        
    }
}