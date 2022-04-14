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
using static FrontEnd.ViewModels.RoomViewModel;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class RoomsController : Controller
    {
        private readonly MovieTicketBookingContext _context;
        private readonly IMapper _mapper;

        public RoomsController( MovieTicketBookingContext context, IMapper mapper ) {
            _context = context;
            _mapper = mapper;
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details( Guid? releasedDateTimeId, decimal? price ) {
            if ( releasedDateTimeId == null ) {
                return NotFound();
            }

            var db_releasedDateTimes = await _context.ReleasedDateTimes
                .FirstOrDefaultAsync( m => m.Id == releasedDateTimeId );

            var db_rooms = await _context.Rooms
                .FirstOrDefaultAsync( m => m.Id == db_releasedDateTimes.RoomId );

            var db_seatingPlans = await _context.SeatingPlans
               .FirstOrDefaultAsync( m => m.ReleasedDateTimeId == releasedDateTimeId );

            var seats = _mapper.Map<RoomViewModel>( db_rooms );
            seats.ReleasedDateTimeId = await _context.ReleasedDateTimes.Where( m => m.Id == releasedDateTimeId ).Select( o => o.Id ).SingleOrDefaultAsync();
            seats.MovieId = await _context.Movies.Where( m => m.Id == db_releasedDateTimes.MovieId ).Select( o => o.Id ).SingleOrDefaultAsync();
            seats.Movie = await _context.Movies.Where( m => m.Id == db_releasedDateTimes.MovieId ).Select( o => o.Title ).SingleOrDefaultAsync();
            seats.Date = db_releasedDateTimes.Date;
            seats.Time = db_releasedDateTimes.Time;
            seats.Price = price;
            seats.Seats = JsonConvert.DeserializeObject<PositionPlan[]>( db_seatingPlans.OccupiedPositionJson );

            if ( seats == null ) {
                return NotFound();
            }

            return View( seats );
        }
    }
}
