using CRUD_Movies.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Movies.Models
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=CRUD_Movies;Trusted_Connection=True;Encrypt=false");
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
