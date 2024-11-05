using AutoMapper;
using Dog.App.Extensions;
using Dog.Web.Models;

namespace Dog.Web.Profiles;

public class DogProfile : Profile
{
    public DogProfile()
    {
        CreateMap<Domain.Dog, DogDto>()
            .ForMember(x => x.Age, opt => opt.MapFrom(src => src.DateOfBirth.GetAge()));
        CreateMap<Domain.Dog, DogCreateDto>().ReverseMap();
        CreateMap<Domain.Dog, DogUpdateDto>().ReverseMap();
        CreateMap<DogCreateDto, DogDto>().ReverseMap();
    }
}