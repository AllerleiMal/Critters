using Critters.Models;
using Microsoft.EntityFrameworkCore;

namespace Critters.Context
{
    public class RosterDbContext : DbContext
    {
        public RosterDbContext(DbContextOptions<RosterDbContext> options) : base(options) { }


        public DbSet<Roster> Rosters { set; get; }

    }
}
