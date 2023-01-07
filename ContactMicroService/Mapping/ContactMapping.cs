using AutoMapper;
using ContactMicroService.Entities;
using ContactMicroService.Entities.Dtos;

namespace ContactMicroService.Mapping
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<ContactDto, Contact>().ReverseMap();
        }
        
    }
}
