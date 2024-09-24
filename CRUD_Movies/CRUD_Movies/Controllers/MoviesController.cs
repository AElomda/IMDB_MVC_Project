using CRUD_Movies.Models;
using CRUD_Movies.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await _Context.Genres.OrderBy(m => m.Name).ToListAsync();
                return View(model);
            }

            var files = Request.Form.Files;

            if (!files.Any())
            {
                model.Genres = await _Context.Genres.OrderBy(m => m.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Please select movie poster!");
                return View(model);
            }

            var poster = files.FirstOrDefault();
            var _allowedExtenstions = new List<string> { ".gpj", ".png" };

            if (!_allowedExtenstions.Contains(Path.GetExtension(poster.FileName).ToLower()))
            {
                model.Genres = await _Context.Genres.OrderBy(m => m.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Only .PNG, .JPG images are allowed!");
                return View(model);
            }

            if (poster.Length > 1048576)
            {
                model.Genres = await _Context.Genres.OrderBy(m => m.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Poster cannot be more than 1 MB!");
                return View(model);
            }

            using var dataStream = new MemoryStream();

            await poster.CopyToAsync(dataStream);

            var movies = new Movie
            {
                Title = model.Title,
                GenreId = model.GenreId,
                Year = model.Year,
                Rate = model.Rate,
                Storeline = model.Storeline,
                Poster = dataStream.ToArray()
            };

            _Context.Movies.Add(movies);
            _Context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

