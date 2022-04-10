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
    public class PaymentsController : Controller
    {
        private readonly MovieTicketBookingContext _context;
        private readonly IMapper _mapper;

        public PaymentsController( MovieTicketBookingContext context, IMapper mapper ) {
            _context = context;
            _mapper = mapper;
        }

        // GET: Payments
        public async Task<IActionResult> Index( PaymentViewModel payments )
        {
            payments.MovieImage = await _context.Movies.Where( o => o.Id == payments.MovieId ).Select( o => o.PortraitImage ).SingleOrDefaultAsync();
            payments.Seats = payments.BookedSeat.Split(",").ToList();
            payments.TotalAmount = payments.Seats.Count() * payments.Price;
            return View( payments );
        }

        // GET: Payments/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Guid? movieId, decimal? totalAmount, string? bookedSeat, Guid? roomId ) {
            Payments payments = new Payments();
            if ( ModelState.IsValid ) {
                payments.Id = Guid.NewGuid();
                payments.UserId = Guid.Parse( "29E94F76-822B-4285-92CB-2E7F4D185480" ); //hardcode
                payments.MovieId = (Guid)movieId;
                payments.BookedSeatStr = bookedSeat;
                payments.RoomId = (Guid)roomId;
                payments.TotalAmount = (decimal)totalAmount;
                payments.CreatedDateTime = DateTimeOffset.Now;
                _context.Add( payments );
                await _context.SaveChangesAsync();
            }
            return View(  );
        }

    }
}
