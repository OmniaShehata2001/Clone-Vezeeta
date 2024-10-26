using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.AppointmentDtos;
using Vezeeta.Dtos.DTOS.CountryDtos;
using Vezeeta.Dtos.DTOS.SpecialtyDtos;
using Vezeeta.Dtos.DTOS.SubSpecialtyDtos;
using Vezeeta.Dtos.DTOS.WorkingPlaceDtos;
using Vezeeta.Infrastucture.CountriesRepos;
using Vezeeta.Models;

namespace Vezeeta.Dtos.DTOS.DoctorDtos
{
    public class DoctorWithDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AboutDoctor { get; set; }
        public decimal Fees { get; set; }
        public int WaitingTime { get; set; }
        public string PhoneNumber { get; set; }
        public string SSN { get; set; }
        public int? AppointmentDurationMinutes { get; set; }
        public int CountryId { get; set; }
        public CountriesImagesDTos CountryDtos { get; set; }
        public int SpecId { get; set; }
        public SpecialtyDto SpecialtyDto { get; set; }
        public string? DoctorImage { get; set; }
        public Gender Gender { get; set; }
        public ICollection<AppointmentDto> AppointmentDtos { get; set; }
        public ICollection<WorkingPlaceDto> WorkingPlaceDtos { get; set; }
        public ICollection<SubSpecialtyDto>? SubSpecialtyDtos { get; set; }
        public ICollection<TeleAppointmentDto>? TeleAppointmentDtos { get; set; }
        //public ICollection<WorkingPlaceImagesDto>? WorkingPlaceImages { get; set; }
        //public ICollection<TimeSlotDto> TimeSlotDtos { get; set;}
    }
}
