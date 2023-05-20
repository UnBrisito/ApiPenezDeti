using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace SpravaPenezDeti.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Dite> Deti { get; set; }
        public DbSet<Ucet> Ucty { get; set; }
        public DbSet<Pohyb> Pohyby { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dite>().Property(p => p.CasVytoreni).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Ucet>().Property(p => p.CasVytoreni).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Pohyb>().Property(p => p.CasVytoreni).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Ucet>().HasMany(a => a.Pohyby).WithOne(b => b.Ucet).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Ucet>().HasMany(a => a.Majitele).WithMany(a => a.Ucty).UsingEntity(e=>e.ToTable("DiteUcet"));
        }
    }
}
