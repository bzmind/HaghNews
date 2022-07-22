using System.Data.Entity;
using DataLayer.Models;

namespace DataLayer.Context
{
    public class CmsContext : DbContext
    {
        public DbSet<Page> Pages { get; set; }

        public DbSet<PageGroup> PageGroups { get; set; }

        public DbSet<PageComment> PageComments { get; set; }

        public DbSet<AdminLogin> AdminLogins { get; set; }
    }
}