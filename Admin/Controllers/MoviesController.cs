#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin.Models;

namespace Admin.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieTicketBookingContext _context;

        public MoviesController(MovieTicketBookingContext context)
        {
            _context = context;
        }

        public string DataImage { get; set; }

        // GET: Movies
        public async Task<IActionResult> Index( string searchString ) {
            var movie = _context.Movies
                .Where( o => o.IsDeleted != true )
                .OrderByDescending( o => o.CreatedDateTime )
                .ToList();

            if ( !String.IsNullOrEmpty( searchString ) ) {
                movie = movie.Where( o => o.Title.Contains( searchString ) ).ToList();
            }
            return View( movie );
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Image,Description,ReleasedDateTime,Genre,Price")] Movies movies)
        {
            if (ModelState.IsValid)
            {
                movies.Id = Guid.NewGuid();
                movies.CreatedDateTime = DateTimeOffset.Now;
                _context.Add(movies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }
            return View(movies);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Image,Description,ReleasedDateTime,Genre,Price,CreatedDateTime")] Movies movies )
        {
            if (id != movies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    movies.UpdatedDateTime = DateTimeOffset.Now;
                    _context.Update(movies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        //POST: Movies/Delete/5
        [HttpPost, ActionName( "Delete" )]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed( Guid id ) {
            var movies = await _context.Movies.FindAsync( id );
            movies.IsDeleted = true;
            _context.Update( movies );
            await _context.SaveChangesAsync();
            return RedirectToAction( nameof( Index ) );
        }

        private bool MoviesExists(Guid id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

    }
}
