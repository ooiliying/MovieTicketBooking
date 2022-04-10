#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Models;

namespace FrontEnd.Controllers
{
    public class BookedTicketsController : Controller
    {
        private readonly MovieTicketBookingContext _context;

        public BookedTicketsController(MovieTicketBookingContext context)
        {
            _context = context;
        }

        // GET: BookedTickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookedTickets.ToListAsync());
        }

        // GET: BookedTickets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedTickets = await _context.BookedTickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookedTickets == null)
            {
                return NotFound();
            }

            return View(bookedTickets);
        }

        // GET: BookedTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookedTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,BookedSeatStr,RoomId,TotalAmount,CreatedDateTime,UpdatedDateTime")] BookedTickets bookedTickets)
        {
            if (ModelState.IsValid)
            {
                bookedTickets.Id = Guid.NewGuid();
                _context.Add(bookedTickets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookedTickets);
        }

        // GET: BookedTickets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedTickets = await _context.BookedTickets.FindAsync(id);
            if (bookedTickets == null)
            {
                return NotFound();
            }
            return View(bookedTickets);
        }

        // POST: BookedTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,BookedSeatStr,RoomId,TotalAmount,CreatedDateTime,UpdatedDateTime")] BookedTickets bookedTickets)
        {
            if (id != bookedTickets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookedTickets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookedTicketsExists(bookedTickets.Id))
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
            return View(bookedTickets);
        }

        // GET: BookedTickets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedTickets = await _context.BookedTickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookedTickets == null)
            {
                return NotFound();
            }

            return View(bookedTickets);
        }

        // POST: BookedTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bookedTickets = await _context.BookedTickets.FindAsync(id);
            _context.BookedTickets.Remove(bookedTickets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookedTicketsExists(Guid id)
        {
            return _context.BookedTickets.Any(e => e.Id == id);
        }
    }
}
