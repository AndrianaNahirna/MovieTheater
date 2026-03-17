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
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actors.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .Include(a => a.ActorMovies)
                    .ThenInclude(am => am.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: Actors/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,BirthDate,Country,Biography")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actors/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sessionData = HttpContext.Session.GetString($"EditActor_{id}");
            Actor actor;

            if (!string.IsNullOrEmpty(sessionData))
            {
                actor = JsonSerializer.Deserialize<Actor>(sessionData);
            }
            else
            {
                actor = await _context.Actors.FindAsync(id);
                if (actor == null) return NotFound();

                var actorForSession = new Actor
                {
                    Id = actor.Id,
                    FirstName = actor.FirstName,
                    LastName = actor.LastName,
                    BirthDate = actor.BirthDate,
                    Country = actor.Country,
                    Biography = actor.Biography
                };
                HttpContext.Session.SetString($"EditActor_{id}", JsonSerializer.Serialize(actorForSession));
            }

            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,BirthDate,Country,Biography")] Actor actor)
        {
            if (id != actor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actor);
                    await _context.SaveChangesAsync();

                    HttpContext.Session.Remove($"EditActor_{id}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            HttpContext.Session.SetString($"EditActor_{id}", JsonSerializer.Serialize(actor));

            return View(actor);
        }

        // GET: Actors/CancelEdit/5
        [Authorize]
        public IActionResult CancelEdit(int id)
        {
            HttpContext.Session.Remove($"EditActor_{id}");
            return RedirectToAction(nameof(Index));
        }

        // GET: Actors/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}
