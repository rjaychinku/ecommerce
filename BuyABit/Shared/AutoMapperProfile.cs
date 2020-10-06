using BuyABit.Models;
using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RegisterRequestModelDTO, ApplicationUser>().ReverseMap();
    }
}
