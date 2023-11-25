using ApiEcoMapa.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiEcoMapa.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<PontoSustentavel> PontoSustentavel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PontoSustentavel>().Property(ps => ps.Latitude).IsRequired();

            modelBuilder.Entity<PontoSustentavel>().Property(ps => ps.Longitude).IsRequired();
        }
    }
}