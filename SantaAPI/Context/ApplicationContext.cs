using Microsoft.EntityFrameworkCore;
using SantaAPI.Models;

namespace SantaAPI.Context
{
    public class ApplicationContext : DbContext
    {
        //public DbSet<GroupBase> GroupBase { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<People> Peoples { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
