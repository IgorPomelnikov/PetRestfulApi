using AutoMapper;
using Dog.App.Extensions;
using Dog.Web.Models;


namespace Dog.Web.Profiles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class DogProfile : Profile
{
    
    public DogProfile()
    {
        CreateMap<Domain.Dog, DogDto>()
            .ForMember(x => x.Age, opt => opt.MapFrom(src => src.DateOfBirth.GetAge()));
        CreateMap<Domain.Dog, DogCreateDto>().ReverseMap();
        CreateMap<Domain.Dog, DogUpdateDto>().ReverseMap();
        CreateMap<Domain.Dog, DogWithToyDto>().ReverseMap(); 
        CreateMap<DogCreateDto, DogDto>().ReverseMap();
        CreateMap<Contracts.PetsToyDto, PetsToyDto>().ReverseMap();

        CreateMap<PetsToyDto, DogWithToyDto>()
            .ForMember(dest => dest.Toy,
                opt => opt.MapFrom(src => src));
        CreateMap<Domain.Dog, DogConcatNameDto>()
            .ForMember(dest => dest.NameAndKind,
                opt => opt.MapFrom(src=>$"{src.Name} породы {src.Kind}"));
        
        CreateMap<DogShepherdCrateDto, Domain.Dog>()
            .ForMember(dest=>dest.Kind, opt=>
                opt.MapFrom(x=>"Shepherd"));
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
