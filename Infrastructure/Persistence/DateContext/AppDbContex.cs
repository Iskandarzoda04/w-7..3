
using Domain.Entites;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DataContext;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set;}
    public DbSet<Post> Posts { get; set;}
    public DbSet<Comment> Comments { get; set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
      
    }

}
