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
    public class InvoicesController : Controller
    {
        private readonly MovieTicketBookingContext _context;
        private readonly IMapper _mapper;

        public InvoicesController( MovieTicketBookingContext context, IMapper mapper ) {
            _context = context;
            _mapper = mapper;
        }

        // GET: Invoices
        public async Task<IActionResult> Index( InvoiceViewModel invoices )
        {
            invoices.MovieImage = await _context.Movies.Where( o => o.Id == invoices.MovieId ).Select( o => o.PortraitImage ).SingleOrDefaultAsync();
            invoices.Seats = invoices.BookedSeat.Split(",").ToList();
            invoices.TotalAmount = invoices.Seats.Count() * invoices.Price;
            return View( invoices );
        }

        // GET: Invoices/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Guid? movieId, decimal? totalAmount, string? bookedSeat, Guid? roomId ) {
            Invoices invoices = new Invoices();
            if ( ModelState.IsValid ) {
                invoices.Id = Guid.NewGuid();
                invoices.UserId = Guid.Parse( "29E94F76-822B-4285-92CB-2E7F4D185480" ); //hardcode
                invoices.MovieId = (Guid)movieId;
                invoices.BookedSeatStr = bookedSeat;
                invoices.RoomId = (Guid)roomId;
                invoices.TotalAmount = (decimal)totalAmount;
                invoices.CreatedDateTime = DateTimeOffset.Now;
                _context.Add( invoices );
                await _context.SaveChangesAsync();
            }
            return View(  );
        }

    }
}
