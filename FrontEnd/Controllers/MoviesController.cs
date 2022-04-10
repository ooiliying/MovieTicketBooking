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
using Dapper;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieTicketBookingContext _context;

        public MoviesController( MovieTicketBookingContext context ) {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index( string searchString )
        {
            var search = "";
            if ( !String.IsNullOrEmpty( searchString ) ) {

                search = @"where m.Title like '%" + searchString  + @"%' ";
            }

            var q = @"select q = JSON_QUERY((
	                    select * from Movies m 
	                    cross apply(
		                    select ReleasedDateTimes = JSON_QUERY((
			                    select Id, Time from ReleasedDateTimes rdt
			                    where m.Id = rdt.MovieId 
                                and rdt.Date =  convert(date, getdate()) -- get today date
                                order by rdt.Time asc
			                    for json path
		                    ))
	                    )rdt "
                        + search +
                    @"for json path
                ))";

            var dataJson = _context.Database.GetDbConnection().QueryFirst<string>( q );
            var resultList = JsonConvert.DeserializeObject<MovieViewModel[]>( dataJson ?? "[]" );
            await Task.CompletedTask;
            return View( resultList );
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details( Guid? id ) {
            if ( id == null ) {
                return NotFound();
            }

            var q = @"select q = JSON_QUERY((
					select * from Movies m 
					cross apply(
						select ReleasedDateTimesDay1 = JSON_QUERY((
						select rdt.Id, rdt.Date, rdt.Time, s.Id as RoomId, rdt.RoomNo from ReleasedDateTimes rdt
						inner join Seats s
						on rdt.RoomNo = s.RoomNo
						where m.Id = rdt.MovieId
						and rdt.Date =  convert(date, getdate()) -- get today date
						order by rdt.Date, rdt.Time asc
						for json path
						))
					)d1
					cross apply(
						select ReleasedDateTimesDay2 = JSON_QUERY((
						select rdt.Id, rdt.Date, rdt.Time, s.Id as RoomId, rdt.RoomNo from ReleasedDateTimes rdt
						inner join Seats s
						on rdt.RoomNo = s.RoomNo
						where m.Id = rdt.MovieId
						and rdt.Date =  convert(date, getdate()+1)
						order by rdt.Date, rdt.Time asc
						for json path
						))
					)d2
					cross apply(
						select ReleasedDateTimesDay3 = JSON_QUERY((
						select rdt.Id, rdt.Date, rdt.Time, s.Id as RoomId, rdt.RoomNo from ReleasedDateTimes rdt
						inner join Seats s
						on rdt.RoomNo = s.RoomNo
						where m.Id = rdt.MovieId 
						and rdt.Date =  convert(date, getdate()+2)
						order by rdt.Date, rdt.Time asc
						for json path
						))
					)d3
					cross apply(
						select ReleasedDateTimesDay4 = JSON_QUERY((
						select rdt.Id, rdt.Date, rdt.Time, s.Id as RoomId, rdt.RoomNo from ReleasedDateTimes rdt
						inner join Seats s
						on rdt.RoomNo = s.RoomNo
						where m.Id = rdt.MovieId
						and rdt.Date =  convert(date, getdate()+3)
						order by rdt.Date, rdt.Time asc
						for json path
						))
					)d4
					cross apply(
						select ReleasedDateTimesDay5 = JSON_QUERY((
						select rdt.Id, rdt.Date, rdt.Time, s.Id as RoomId, rdt.RoomNo from ReleasedDateTimes rdt
						inner join Seats s
						on rdt.RoomNo = s.RoomNo
						where m.Id = rdt.MovieId
						and rdt.Date =  convert(date, getdate()+4)
						order by rdt.Date, rdt.Time asc
						for json path
						))
					)d5
					cross apply(
						select ReleasedDateTimesDay6 = JSON_QUERY((
						select rdt.Id, rdt.Date, rdt.Time, s.Id as RoomId, rdt.RoomNo from ReleasedDateTimes rdt
						inner join Seats s
						on rdt.RoomNo = s.RoomNo
						where m.Id = rdt.MovieId
						and rdt.Date =  convert(date, getdate()+5)
						order by rdt.Date, rdt.Time asc
						for json path
						))
					)d6
					cross apply(
						select ReleasedDateTimesDay7 = JSON_QUERY((
						select rdt.Id, rdt.Date, rdt.Time, s.Id as RoomId, rdt.RoomNo from ReleasedDateTimes rdt
						inner join Seats s
						on rdt.RoomNo = s.RoomNo
						where m.Id = rdt.MovieId
						and rdt.Date =  convert(date, getdate()+6)
						order by rdt.Date, rdt.Time asc
						for json path
						))
					)d7
					where m.id = '" + id + @"'
					for json path, without_array_wrapper
					))";

            var dataJson = _context.Database.GetDbConnection().QueryFirst<string>( q );
            var resultList = JsonConvert.DeserializeObject<MovieViewModel>( dataJson ?? "[]" );

            if ( resultList == null ) {
                return NotFound();
            }
            await Task.CompletedTask;
            return View( resultList );
        }
    }
}
