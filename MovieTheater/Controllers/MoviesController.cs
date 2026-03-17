using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data;
using MovieTheater.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace MovieTheater.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.ActorMovies)
                    .ThenInclude(am => am.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Actors = _context.Actors.ToList();
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseYear,Genre,DurationMinutes,Rating")] Movie movie, int[] selectedActors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync(); 

                if (selectedActors != null)
                {
                    foreach (var actorId in selectedActors)
                    {
                        _context.ActorMovies.Add(new ActorMovie { MovieId = movie.Id, ActorId = actorId });
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Actors = _context.Actors.ToList();
            ViewBag.SelectedActorIds = selectedActors?.ToList() ?? new List<int>();
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sessionData = HttpContext.Session.GetString($"EditMovie_{id}");
            Movie movie;
            List<int> selectedActorIds;

            if (!string.IsNullOrEmpty(sessionData))
            {
                movie = JsonSerializer.Deserialize<Movie>(sessionData);

                selectedActorIds = movie.ActorMovies?.Select(am => am.ActorId).ToList() ?? new List<int>();
            }
            else
            {
                movie = await _context.Movies
                    .Include(m => m.ActorMovies)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (movie == null) return NotFound();

                var movieForSession = new Movie
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ReleaseYear = movie.ReleaseYear,
                    Genre = movie.Genre,
                    DurationMinutes = movie.DurationMinutes,
                    Rating = movie.Rating,
                    ActorMovies = movie.ActorMovies.Select(am => new ActorMovie
                    {
                        ActorId = am.ActorId,
                        MovieId = am.MovieId
                    }).ToList()
                };

                HttpContext.Session.SetString($"EditMovie_{id}", JsonSerializer.Serialize(movieForSession));
                selectedActorIds = movie.ActorMovies.Select(am => am.ActorId).ToList();
            }

            ViewBag.Actors = _context.Actors.ToList();
            ViewBag.SelectedActorIds = selectedActorIds;

            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseYear,Genre,DurationMinutes,Rating")] Movie movie, int[] selectedActors)
        {
            if (id != movie.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);

                    var existingLinks = _context.ActorMovies.Where(am => am.MovieId == id);
                    _context.ActorMovies.RemoveRange(existingLinks);
                    if (selectedActors != null)
                    {
                        foreach (var actorId in selectedActors)
                        {
                            _context.ActorMovies.Add(new ActorMovie { MovieId = id, ActorId = actorId });
                        }
                    }
                    await _context.SaveChangesAsync();

                    HttpContext.Session.Remove($"EditMovie_{id}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            movie.ActorMovies = selectedActors?.Select(aId => new ActorMovie { ActorId = aId, MovieId = id }).ToList() ?? new List<ActorMovie>();
            HttpContext.Session.SetString($"EditMovie_{id}", JsonSerializer.Serialize(movie));

            ViewBag.Actors = _context.Actors.ToList();
            ViewBag.SelectedActorIds = selectedActors?.ToList() ?? new List<int>();

            return View(movie);
        }

        // GET: Movies/CancelEdit/5
        [Authorize]
        public IActionResult CancelEdit(int id)
        {
            HttpContext.Session.Remove($"EditMovie_{id}");
            return RedirectToAction(nameof(Index));
        }

        // GET: Movies/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
