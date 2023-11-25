using ApiEcoMapa.Models;
using AutoMapper;

namespace ApiEcoMapa.DTOs.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PontoSustentavel, PontoSustentavelDTO>().ReverseMap();
        }
    }
}