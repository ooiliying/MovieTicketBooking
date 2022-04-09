using FrontEnd.Models;

namespace FrontEnd.ViewModels {
    public class MovieViewModel {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string PortraitImage { get; set; }
        public string LandscapeImage { get; set; }
        public string Description { get; set; }
        public ReleasedDateTime[] ReleasedDateTimes { get; set; }
        public ReleasedDateTime[] ReleasedDateTimesDay1 { get; set; }
        public ReleasedDateTime[] ReleasedDateTimesDay2 { get; set; }
        public ReleasedDateTime[] ReleasedDateTimesDay3 { get; set; }
        public ReleasedDateTime[] ReleasedDateTimesDay4 { get; set; }
        public ReleasedDateTime[] ReleasedDateTimesDay5 { get; set; }
        public ReleasedDateTime[] ReleasedDateTimesDay6 { get; set; }
        public ReleasedDateTime[] ReleasedDateTimesDay7 { get; set; }
        public string Genre { get; set; }
        public string Duration { get; set; }
        public string Language { get; set; }
        public decimal? Price { get; set; }
        public bool? IsDeleted { get; set; }

        public class ReleasedDateTime {
            public Guid Id { get; set; }
            public string Date { get; set; }
            public TimeSpan Time { get; set; }
        }
    }
}