using AutoMapper;
using FrontEnd.Models;
using FrontEnd.ViewModels;

namespace FrontEnd {
    public class AutoMapperProfile : Profile{
        public AutoMapperProfile() {
            //CreateMap<SeatViewModel, Seats>().ReverseMap();

            CreateMap<Seats, SeatViewModel>()
                .ForMember( x => x.Seats, opt => opt.MapFrom( o => getSeats( o.SeatStr ) ) )
                .ReverseMap();
        }

        private List<string> getSeats( string seats ) {

            if ( string.IsNullOrEmpty( seats ) ) {
                return null;
            }

            return seats.Split( ',' ).ToList();

        }
    }
}
