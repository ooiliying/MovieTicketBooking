using AutoMapper;
using FrontEnd.Models;
using FrontEnd.ViewModels;
using Newtonsoft.Json;
using static FrontEnd.ViewModels.RoomViewModel;

namespace FrontEnd {
    public class AutoMapperProfile : Profile{
        public AutoMapperProfile() {
            CreateMap<Rooms, RoomViewModel>().ReverseMap();
            CreateMap<PaymentViewModel, Payments>().ReverseMap();
        }
    }
}
