using Critters.Models;
using Microsoft.EntityFrameworkCore;

namespace Critters.Context
{
    public class RosterDbContext : DbContext
    {
        public RosterDbContext() { }

        public RosterDbContext(DbContextOptions<RosterDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

       protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            builder.UseSqlServer("Data Source=NIZIER\\NIKITADB;Initial Catalog=Critters;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            base.OnConfiguring(builder);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // builder.Entity<Roster>().HasNoKey();
            builder.Entity<Roster>().HasKey(entity => entity.playerid);
        }

        public DbSet<Roster> Rosters { set; get; }
    }

}
