using Microsoft.EntityFrameworkCore;

namespace CRUD_Movies
{
    public class ApplicationDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=CRUD_Movies;Trusted_Connection=True;Encrypt=false");
        }
    }
}
