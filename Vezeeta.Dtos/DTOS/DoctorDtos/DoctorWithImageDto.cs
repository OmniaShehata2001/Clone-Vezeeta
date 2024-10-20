using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.AppointmentDtos;
using Vezeeta.Dtos.DTOS.SubSpecialtyDtos;
using Vezeeta.Dtos.DTOS.WorkingPlaceDtos;
using Vezeeta.Models;

namespace Vezeeta.Dtos.DTOS.DoctorDtos
{
    public class DoctorWithImageDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AboutDoctor { get; set; }
        public decimal Fees { get; set; }
        public int WaitingTime { get; set; }
        public string PhoneNumber { get; set; }
        public string SSN { get; set; }
        public int? AppointmentDurationMinutes { get; set; }
        public int CountryId { get; set; }
        public int SpecId { get; set; }
        public Gender Gender { get; set; }
        public IFormFile? DoctorImage { get; set; }
        public ICollection<AppointmentDto> AppointmentDtos { get; set; }
        public ICollection<WorkingPlaceDto> WorkingPlaceDtos { get; set; }
        public ICollection<int>? doctorSubSpecialtyDtos { get; set; }
    }
}
