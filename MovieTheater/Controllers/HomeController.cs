using Microsoft.AspNetCore.Mvc;
using MovieTheater.Models;
using MovieTheater.Data; 
using System.Diagnostics;
using System.Linq;

namespace MovieTheater.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalMovies = _context.Movies.Count();
            ViewBag.TotalActors = _context.Actors.Count();
            ViewBag.TopRating = _context.Movies.Any() ? _context.Movies.Max(m => m.Rating) : 0;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}