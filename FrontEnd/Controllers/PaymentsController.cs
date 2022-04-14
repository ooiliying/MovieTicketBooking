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
using Newtonsoft.Json;
using static FrontEnd.ViewModels.RoomViewModel;

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

            var r = await _context.ReleasedDateTimes.FirstOrDefaultAsync( m => m.Id == payments.ReleasedDateTimeId );
            payments.ReleasedDateTimeId = await _context.ReleasedDateTimes.Where( m => m.Id == payments.ReleasedDateTimeId ).Select( o => o.Id ).SingleOrDefaultAsync();
            payments.MovieImage = await _context.Movies.Where( m => m.Id == r.MovieId ).Select( o => o.PortraitImage ).SingleOrDefaultAsync();
            payments.MovieId = await _context.Movies.Where( m => m.Id == r.MovieId ).Select( o => o.Id ).SingleOrDefaultAsync();
            payments.Movie = await _context.Movies.Where( m => m.Id == r.MovieId ).Select( o => o.Title ).SingleOrDefaultAsync();
            payments.Date = r.Date;
            payments.Time = r.Time;
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
        public async Task<IActionResult> Create( Guid? releasedDateTimeId, decimal? totalAmount, string? bookedSeat ) {
            Payments payments = new Payments();
            if ( ModelState.IsValid ) {
                var r = await _context.ReleasedDateTimes.FirstOrDefaultAsync( m => m.Id == releasedDateTimeId );

                payments.Id = Guid.NewGuid();
                payments.UserId = Guid.Parse( "29E94F76-822B-4285-92CB-2E7F4D185480" ); //hardcode
                payments.ReleasedDateTimeId = (Guid)releasedDateTimeId;
                payments.MovieId = await _context.Movies.Where( m => m.Id == r.MovieId ).Select( o => o.Id ).SingleOrDefaultAsync();
                payments.BookedSeatStr = bookedSeat;
                payments.RoomId = await _context.ReleasedDateTimes.Where( m => m.Id == releasedDateTimeId ).Select( o => o.RoomId ).SingleOrDefaultAsync();
                payments.TotalAmount = (decimal)totalAmount;
                payments.CreatedDateTime = DateTimeOffset.Now;
                _context.Add( payments );
                await _context.SaveChangesAsync();

                //update seating plan
                var db_seatingPlans = await _context.SeatingPlans.FirstOrDefaultAsync( o => o.ReleasedDateTimeId == releasedDateTimeId );
                if ( db_seatingPlans == null ) {
                    return NotFound();
                }
                var list = JsonConvert.DeserializeObject<PositionPlan[]>( db_seatingPlans.OccupiedPositionJson ).ToList();
                string[] bs = bookedSeat.Split( "," );

                foreach ( var b in bs ) {
                    int i = list.FindIndex( x => x.SeatNo == b );
                    list[i] = new PositionPlan { SeatNo = b, IsOccupied = true };
                }
                db_seatingPlans.BookedStr = bookedSeat;
                db_seatingPlans.OccupiedPositionJson = JsonConvert.SerializeObject( list );
                db_seatingPlans.UpdatedDateTime = DateTimeOffset.Now;
                _context.Update( db_seatingPlans );
                await _context.SaveChangesAsync();
            }
            return View(  );
        }

    }
}
