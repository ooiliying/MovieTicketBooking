using AutoMapper;
using FrontEnd.Models;
using FrontEnd.ViewModels;
using Newtonsoft.Json;
using static FrontEnd.ViewModels.RoomViewModel;

namespace FrontEnd {
    public class AutoMapperProfile : Profile{
        //public AutoMapperProfile() {
        //    CreateMap<Rooms, RoomViewModel>()
        //        .ForMember( x => x.Seats, opt => opt.MapFrom( o => getRooms( o.SeatPositionStr ) ) )
        //        .ReverseMap();

        //    CreateMap<PaymentViewModel, Payments>().ReverseMap();
        //}

        //private List<string> getRooms( string rooms ) {

        //    if ( string.IsNullOrEmpty( rooms ) ) {
        //        return null;
        //    }

        //    return rooms.Split( ',' ).ToList();

        //}

        public AutoMapperProfile() {
            CreateMap<Rooms, RoomViewModel>()
                .ForMember( x => x.Seats, opt => opt.MapFrom( o => JsonConvert.DeserializeObject<PositionPlan[]>( o.SeatPositionJson ) ) )
                .ReverseMap();

            CreateMap<PaymentViewModel, Payments>().ReverseMap();
        }
    }
}
