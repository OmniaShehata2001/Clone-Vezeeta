using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.CountryDtos;
using Vezeeta.Dtos.DTOS.SpecialtyDtos;
using Vezeeta.Infrastucture.CountriesRepos;
using Vezeeta.Models;

namespace Vezeeta.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateOrUpdateCountryDto,Countries>().ReverseMap();
            CreateMap<CountriesImages,CountryImagesDto>().ReverseMap();
            CreateMap<CountriesImagesDTos , Countries>().ReverseMap();
            CreateMap<Specialty, SpecialtyDto>().ReverseMap();
        }
    }
}
