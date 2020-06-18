using Microsoft.EntityFrameworkCore;
using RockScissorsPaper.Model;


namespace RockScissorsPaper.Services
{
    public class AccountDBContext : DbContext
    {
        public AccountDBContext()
        {
            Database.EnsureCreated();
        }

        public const string ConnectionString =
            "User ID=postgres;Password=android;Host=localhost;Port=5432;Database=db1;Pooling=true;SSL Mode=Prefer;Trust Server Certificate=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}