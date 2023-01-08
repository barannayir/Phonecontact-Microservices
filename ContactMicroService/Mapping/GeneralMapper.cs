using AutoMapper;
using ContactMicroService.Entities;
using ContactMicroService.Entities.Dtos;
using Shared.Dtos;

namespace ContactMicroService.Mappers
{
    public class GeneralMapper : Profile
    {
        public GeneralMapper()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Contact, ContactCreateDto>().ReverseMap();
            CreateMap<Contact, ContactUpdateDto>().ReverseMap();
            CreateMap<Contact, ContactWithCommunicationsDto>().ReverseMap();

            CreateMap<Communication, CommunicationDto>().ReverseMap();
            CreateMap<Communication, CommunicationCreateDto>().ReverseMap();
            CreateMap<Communication, CommunicationUpdateDto>().ReverseMap();
        }
    }
}