using Admin.Models;
using Admin.ViewModels;
using AutoMapper;

namespace Admin {
    public class AutoMapperProfile : Profile{
        public AutoMapperProfile() {
            CreateMap<MovieViewModels, Movies>().ReverseMap();
            CreateMap<MovieViewModels.ReleasedDateTime, ReleasedDateTimes>().ReverseMap();
        }
    }
}
