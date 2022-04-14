using AutoMapper;
using FrontEnd.Models;
using FrontEnd.ViewModels;
using Newtonsoft.Json;
using static FrontEnd.ViewModels.RoomViewModel;

namespace FrontEnd {
    public class AutoMapperProfile : Profile{
        public AutoMapperProfile() {
            CreateMap<Rooms, RoomViewModel>()
                .ForMember( x => x.Seats, opt => opt.MapFrom( o => JsonConvert.DeserializeObject<PositionPlan[]>( o.SeatPositionJson ) ) )
                .ReverseMap();

            CreateMap<PaymentViewModel, Payments>().ReverseMap();
        }
    }
}
