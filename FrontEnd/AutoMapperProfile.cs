using AutoMapper;
using FrontEnd.Models;
using FrontEnd.ViewModels;

namespace FrontEnd {
    public class AutoMapperProfile : Profile{
        public AutoMapperProfile() {
            CreateMap<MovieViewModel, Movies>().ReverseMap();
        }
    }
}
