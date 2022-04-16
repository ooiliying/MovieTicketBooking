using Admin.Models;
using Admin.ViewModels;
using AutoMapper;

namespace Admin {
    public class AutoMapperProfile : Profile{
        public AutoMapperProfile() {
            CreateMap<MovieViewModel, Movies>().ReverseMap();
            CreateMap<MovieViewModel.ReleasedDateTime, ReleasedDateTimes>().ReverseMap();
            CreateMap<SaleViewModel, Payments>().ReverseMap();
        }
    }
}
