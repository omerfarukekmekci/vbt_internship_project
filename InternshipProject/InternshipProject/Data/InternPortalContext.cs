using Microsoft.EntityFrameworkCore;
using InternshipProject.Models;

namespace InternshipProject.Data
{
    public class InternPortalContext : DbContext
    {
        public InternPortalContext(DbContextOptions<InternPortalContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<DataEntry> DataEntries { get; set; }
    }
}
