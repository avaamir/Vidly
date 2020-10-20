using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return User.IsInRole(Roles.CanManageMovies) ? View("AdminIndex", movies) : View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }


        [Authorize(Roles = Roles.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MoviesFormViewModel
            {
                Genres = genres
            };
            return View("MoviesForm", viewModel);
        }

        [Authorize(Roles = Roles.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            var genres = _context.Genres.ToList();
            var viewModel = new MoviesFormViewModel(movie, genres);
            return View("MoviesForm", viewModel);
        }

        [Authorize(Roles = Roles.CanManageMovies)]
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MoviesFormViewModel(movie, _context.Genres.ToList());
                return View("MoviesForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.AddedDate = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.First(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.Number = movie.Number;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _context.SaveChanges();


            return RedirectToAction("Index", "Movies");
        }
    }
}