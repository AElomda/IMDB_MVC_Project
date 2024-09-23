using CRUD_Movies.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace CRUD_Movies.Controllers
{
    public class MoviesController : Controller
    {
         ApplicationDbContext _Context = new ApplicationDbContext();
        public async Task<IActionResult> Index()
        {
            var movies = await _Context.Movies.ToListAsync();
            return View(movies);
        }
        public async Task<IActionResult> Create()
        {
            var viewModel = new MovieFormViewModel
            {
                Genres = await _Context.Genres.OrderBy(m => m.Name).ToListAsync()
            };  
            return View(viewModel);
        }
    }
}
