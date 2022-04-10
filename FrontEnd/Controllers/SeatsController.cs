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

        // GET: Seats/Details/5
        public async Task<IActionResult> Details( Guid? id, string? movie, Guid? movieId, string? date, TimeSpan time, string bookSeat, decimal? price ) {
            if ( id == null ) {
                return NotFound();
            }

            var s = await _context.Seats
                .FirstOrDefaultAsync( m => m.Id == id );

            var seats = _mapper.Map<SeatViewModel>( s );
            seats.MovieId = await _context.Movies.Where( m => m.Id == movieId ).Select( o => o.Id ).SingleOrDefaultAsync();
            seats.Movie = await _context.Movies.Where( m => m.Id == movieId ).Select(o => o.Title).SingleOrDefaultAsync();
            seats.Date = date;
            seats.Time = time;
            seats.Price = price;

            if ( seats == null ) {
                return NotFound();
            }

            return View( seats );
        }
    }
}
