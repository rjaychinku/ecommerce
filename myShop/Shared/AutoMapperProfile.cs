using myShop.Models;
using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RegisterRequestModelDTO, ApplicationUser>().ReverseMap();
    }
}
