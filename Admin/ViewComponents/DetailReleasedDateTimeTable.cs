using Admin.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Admin.ViewModels.MovieViewModel;

namespace FrontEnd.ViewComponents {
    [ViewComponent( Name = "DetailReleasedDateTimeTable" )]
    public class DetailReleasedDateTimeTable : ViewComponent{
        private readonly MovieTicketBookingContext _context;
        private readonly IMapper _mapper;

        public DetailReleasedDateTimeTable( MovieTicketBookingContext context, IMapper mapper ) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id) {
            Guid movieId = id;
            var db_releasedDateTime = await _context.ReleasedDateTimes.Where( o => o.MovieId == movieId ).OrderBy( o => o.Date ).ThenBy( o => o.Time ).ToListAsync();
            if ( db_releasedDateTime == null ) {
                db_releasedDateTime = new List<ReleasedDateTimes>();
            }
            List<ReleasedDateTime> releasedDateTime = _mapper.Map<List<ReleasedDateTime>>( db_releasedDateTime );
            releasedDateTime.Insert( 0, new ReleasedDateTime() { MovieId = movieId } );//Add a Dummy Row.
            return await Task.FromResult( (IViewComponentResult)View( "Index", releasedDateTime ) );
        }
    }
}
