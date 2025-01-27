using Microsoft.EntityFrameworkCore;
using pruebaTecnicaImagineAppTareas.Models;

namespace pruebaTecnicaImagineAppTareas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
