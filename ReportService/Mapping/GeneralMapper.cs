using AutoMapper;
using ReportService.Entities;
using ReportService.Entities.Dtos;

namespace ReportService.Mapping
{
    public class GeneralMapper : Profile
    {
        public GeneralMapper()
        {
            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<Report, ReportCreateDto>().ReverseMap();
        }
    }
}