using AutoMapper;
using FrontEnd.Models;
using FrontEnd.ViewModels;

namespace FrontEnd {
    public class AutoMapperProfile : Profile{
        public AutoMapperProfile() {
            CreateMap<Rooms, RoomViewModel>()
                .ForMember( x => x.Seats, opt => opt.MapFrom( o => getRooms( o.SeatPositionStr ) ) )
                .ReverseMap();

            CreateMap<PaymentViewModel, Payments>().ReverseMap();
        }

        private List<string> getRooms( string rooms ) {

            if ( string.IsNullOrEmpty( rooms ) ) {
                return null;
            }

            return rooms.Split( ',' ).ToList();

        }
    }
}
