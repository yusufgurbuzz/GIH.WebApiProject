using GIH.Entities;
using Microsoft.EntityFrameworkCore;

namespace GIH.Repositories;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions context) : base(context)
    {
    }
    public DbSet<Person> Persons { get; set; } 
    public DbSet<Role> Roles { get; set; }
    
}