using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.AppointmentDtos;
using Vezeeta.Dtos.DTOS.CountryDtos;
using Vezeeta.Dtos.DTOS.DoctorDtos;
using Vezeeta.Dtos.DTOS.ReviewsDtos;
using Vezeeta.Dtos.DTOS.SpecialtyDtos;
using Vezeeta.Dtos.DTOS.SubSpecialtyDtos;
using Vezeeta.Dtos.DTOS.WorkingPlaceDtos;
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
            CreateMap<DoctorWithDetails,Doctor>().ReverseMap();
            CreateMap<DoctorDto, Doctor>().ReverseMap();
            CreateMap<DoctorSubSpecialties,DoctorSubSpecialtyDto>().ReverseMap();
            CreateMap<SubSpecialty , SubSpecialtyDto>().ReverseMap();
            CreateMap<Appointment , AppointmentDto>().ReverseMap();
            CreateMap<TimeSlot , TimeSlotDto>().ReverseMap();
            CreateMap<DoctorReviews , DoctorReviewDto>().ReverseMap();
            CreateMap<WorkingPlace,WorkingPlaceDto>().ReverseMap();
            CreateMap<DoctorWorkingPlace,DoctorWorkingPlaceDto>().ReverseMap();
            CreateMap<SubSpecialty, SubSpecialtyDto>().ReverseMap();
            CreateMap<DoctorReviewDto, DoctorReviews>().ReverseMap();
            CreateMap<WorkingPlaceImages,WorkingPlaceImagesDto>().ReverseMap();

        }
    }
}
