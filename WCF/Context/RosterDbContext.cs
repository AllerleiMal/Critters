using System.Data.Entity;
using WCF.Models;


namespace WCF.Context
{
    public class RosterDbContext : DbContext
    {
        public RosterDbContext() : base() { }
        public RosterDbContext(string constring) : base(constring) 
        {
            Database.CreateIfNotExists();
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

       public DbSet<Roster> Rosters { set; get; }
       public DbSet<Temp> Temps { set; get; }
    }
}
