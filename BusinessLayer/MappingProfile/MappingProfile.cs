using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Dtos.wareHousDto;
using DomainLayer.Entities;


namespace BusinessLayer.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Warehouse, WareHouseInputDto>()
             .ForMember(dest => dest.WareHouseItemsDto, opt => opt.MapFrom(src => src.Items))
             .ReverseMap()
             .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.WareHouseItemsDto));
            CreateMap<Warehouse, WareHouseOutputDto>().ReverseMap();
            CreateMap<WarehouseItems, WareHouseItemsDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, GetAllUsersOutput>().ReverseMap();
            CreateMap<Logs, LogsDto>().ReverseMap();
        }
    }
}
