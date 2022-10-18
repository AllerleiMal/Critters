using gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace gRPC.Context
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
            base.OnConfiguring(builder);

        }
       
       public DbSet<Roster> Rosters { set; get; }
       public DbSet<Temp> Temps { set; get; }
    }
}
