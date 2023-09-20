using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<Property, PropertyDto>();
            CreateMap<PropertyDto, Property>();
            CreateMap<PropertyUpdateDto, Property>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<Message, MessageDto>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();
            CreateMap<Transaction, SearchTransactionDto>();
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();

        }
    }
}