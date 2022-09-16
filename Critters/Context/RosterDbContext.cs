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

          //  builder.UseSqlServer(@"Data Source=NIZIER\NIKITADB;Initial Catalog=Critters;Integrated Security=True");
            base.OnConfiguring(builder);

        }
       
       public DbSet<Roster> Rosters { set; get; }
        public DbSet<Temp> Temps { set; get; }
    }
}
