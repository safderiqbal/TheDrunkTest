using System.Data.Entity;
using DrunkCheck.Models;

namespace DrunkCheck
{
    public class DrunkCheckerContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Reading> Readings { get; set; }

        public DbSet<Charity> Charities { get; set; }

        public DrunkCheckerContext() : base("DrunkChecker")
        {
            
        }
    }
}