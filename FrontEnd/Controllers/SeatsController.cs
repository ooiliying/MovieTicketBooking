#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Models;
using FrontEnd.ViewModels;
using AutoMapper;

namespace FrontEnd.Controllers
{
    public class SeatsController : Controller
    {
        private readonly MovieTicketBookingContext _context;
        private readonly IMapper _mapper;

        public SeatsController( MovieTicketBookingContext context, IMapper mapper ) {
            _context = context;
            _mapper = mapper;
        }

        // GET: Seats
        public async Task<IActionResult> Index()
        {
            return View(await _context.Seats.ToListAsync());
        }

        // GET: Seats/Details/5
        public async Task<IActionResult> Details( Guid? id ) {
            if ( id == null ) {
                return NotFound();
            }

            var s = await _context.Seats
                .FirstOrDefaultAsync( m => m.Id == id );

            var seats = _mapper.Map<SeatViewModel>( s );

            if ( seats == null ) {
                return NotFound();
            }

            return View( seats );
        }

        // GET: Seats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SeatStr")] Seats seats)
        {
            if (ModelState.IsValid)
            {
                seats.Id = Guid.NewGuid();
                _context.Add(seats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seats);
        }

        // GET: Seats/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seats = await _context.Seats.FindAsync(id);
            if (seats == null)
            {
                return NotFound();
            }
            return View(seats);
        }

        // POST: Seats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,SeatStr")] Seats seats)
        {
            if (id != seats.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seats);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeatsExists(seats.Id))
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
            return View(seats);
        }

        // GET: Seats/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seats = await _context.Seats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seats == null)
            {
                return NotFound();
            }

            return View(seats);
        }

        // POST: Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var seats = await _context.Seats.FindAsync(id);
            _context.Seats.Remove(seats);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeatsExists(Guid id)
        {
            return _context.Seats.Any(e => e.Id == id);
        }
    }
}
