using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Domain.Entities;
using AutoMapper;

namespace ApiDotNet6.Application.Mappings
{
    public class DtoToDomainMapping: Profile
    {
        public DtoToDomainMapping()
        {
            CreateMap<PersonDTO, Person>();
            CreateMap<ProductDTO, Product>();
            CreateMap<UserDTO, User>();
        }
    }
}
