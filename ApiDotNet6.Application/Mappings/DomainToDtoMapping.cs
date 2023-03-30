using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Domain.Entities;
using AutoMapper;

namespace ApiDotNet6.Application.Mappings
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Purchase, PurchaseDetailDTO>()
                .ForMember(x => x.Person, opt => opt.Ignore())
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ConstructUsing((model, contex) => 
                {
                    var dto = new PurchaseDetailDTO
                    {
                        Product = model.Product.Name,
                        Id = model.Id,
                        Date = model.Date,
                        Person = model.Person.Name
                    };
                    return dto;
                });
            CreateMap<User, UserDTO>();
        }
    }
}
