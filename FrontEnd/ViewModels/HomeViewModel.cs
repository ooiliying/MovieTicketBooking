using FrontEnd.Models;

namespace FrontEnd.ViewModels {
    public class HomeViewModel {
        public List<Movies> LatestMovies { get; set; }
        public List<Movies> PopularMovies { get; set; }
    }
}