using Microsoft.EntityFrameworkCore;
using RockScisorsPaper.Model;


namespace RockScisorsPaper.Services
{
    public class AccountDBContext : DbContext
    {
        public AccountDBContext()
        {
            Database.EnsureCreated();
        }

        public const string ConnectionString = ""; //{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(ConnectionString);
            optionsBuilder.UseNpgsql(ConnectionString);
            //UseNpgsql
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}