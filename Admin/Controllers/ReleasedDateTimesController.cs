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
    public class ReleasedDateTimesController : Controller
    {
        private readonly MovieTicketBookingContext _context;

        public ReleasedDateTimesController( MovieTicketBookingContext context ) {
            _context = context;
        }

        [HttpPost]
        public async Task<JsonResult> Create( ReleasedDateTimes releasedDateTimes ) {
            releasedDateTimes.Id = Guid.NewGuid();
            if ( ModelState.IsValid ) {
                releasedDateTimes.RoomId = await _context.Rooms.Where( o => o.RoomNo == releasedDateTimes.RoomNo ).Select( o => o.Id ).SingleOrDefaultAsync();
                releasedDateTimes.CreatedDateTime = DateTimeOffset.Now;
                _context.Add( releasedDateTimes );
                await _context.SaveChangesAsync();

                //add to seating plan db
                SeatingPlans seatingPlans = new SeatingPlans();
                seatingPlans.Id = Guid.NewGuid();
                seatingPlans.ReleasedDateTimeId = releasedDateTimes.Id;
                seatingPlans.MovieId = releasedDateTimes.MovieId;
                seatingPlans.RoomId = releasedDateTimes.RoomId;
                seatingPlans.PositionStr = await _context.Rooms.Where( o => o.RoomNo == releasedDateTimes.RoomNo ).Select( o => o.SeatPositionStr ).SingleOrDefaultAsync();
                seatingPlans.CreatedDateTime = DateTimeOffset.Now;
                _context.Add( seatingPlans );
                await _context.SaveChangesAsync();

                return Json( releasedDateTimes );
            } else {
                return Json( null );
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit( ReleasedDateTimes releasedDateTimes ) {
            var db_releasedDateTimes = await _context.ReleasedDateTimes.FindAsync( releasedDateTimes.Id );

            if ( db_releasedDateTimes.Id != releasedDateTimes.Id ) {
                return NotFound();
            }

            if ( ModelState.IsValid ) {
                db_releasedDateTimes.Date = releasedDateTimes.Date;
                db_releasedDateTimes.Time = releasedDateTimes.Time;
                db_releasedDateTimes.RoomNo = releasedDateTimes.RoomNo;
                db_releasedDateTimes.UpdatedDateTime = DateTimeOffset.Now;
                _context.Update( db_releasedDateTimes );
                await _context.SaveChangesAsync();
            }
            return new EmptyResult();
        }

        [HttpPost]
        public async Task<ActionResult> Delete( Guid id ) {
            var db_releasedDateTimes = await _context.ReleasedDateTimes.FindAsync( id );
            _context.Remove( db_releasedDateTimes );
            await _context.SaveChangesAsync();
            return new EmptyResult();
        }

    }
}
