using FrontEnd.Models;
using FrontEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FrontEnd.Controllers {
    public class HomeController : Controller {
        private readonly MovieTicketBookingContext _context;

        public HomeController( MovieTicketBookingContext context ) {
            _context = context;
        }

        public async Task<IActionResult> Index( ) {
            HomeViewModel model = new HomeViewModel();
            model.LatestMovies = await GetLatestMovies();
            model.PopularMovies = await GetPopularMovies();

            var movie = _context.Movies
                .Where( o => o.IsDeleted != true )
                .OrderBy( o => Guid.NewGuid() )
                .Take(8)
                .ToList();
            return View( model );
        }
        private async Task<List<Movies>> GetLatestMovies() {
            var movie = _context.Movies
                .Where( o => o.IsDeleted != true )
                .OrderBy( o => Guid.NewGuid() )
                .Take( 8 )
                .ToList();
            await Task.CompletedTask;
            return movie;
        }

        private async Task<List<Movies>> GetPopularMovies() {
            var movie = _context.Movies
                .Where( o => o.IsDeleted != true )
                .OrderByDescending( o => o.CreatedDateTime )
                .Take( 6 )
                .ToList();
            await Task.CompletedTask;
            return movie;
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
        public IActionResult Error() {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        }
    }
}