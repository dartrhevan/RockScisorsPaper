using Microsoft.EntityFrameworkCore;
using RockScissorsPaper.Model;


namespace RockScissorsPaper.Services
{
    public class AccountDBContext : DbContext
    {
        public AccountDBContext()
        {
            //Database.EnsureCreated();
        }

        public const string ConnectionString =
            "User ID=postgres;Password=android;Host=localhost;Port=5432;Database=game_db;Pooling=true;SSL Mode=Prefer;Trust Server Certificate=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(ConnectionString);
        }
        public AccountDBContext(DbContextOptions<AccountDBContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
    }
}