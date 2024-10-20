using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Services.DoctorServices;
using Vezeeta.Dtos.DTOS.DoctorDtos;

namespace Vezeeta.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorServices _doctorServices;

        public DoctorController(IDoctorServices doctorServices)
        {
            _doctorServices = doctorServices;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]DoctorDto doctorDto )
        {
            List<TimeSpan> times = new List<TimeSpan>();
            foreach(var App in doctorDto.AppointmentDtos)
            {
                foreach(var time in App.TimeSlots)
                {
                   TimeSpan.FromTicks(time.Time);
                }
            }
            var DoctorWithImage = new DoctorWithImageDto
            {
                Name = doctorDto.Name,
                Description = doctorDto.Description,
                PhoneNumber = doctorDto.PhoneNumber,
                AboutDoctor = doctorDto.AboutDoctor,
                //DoctorImage = doctorDto.DoctorImage,
                SSN = doctorDto.SSN,
                Fees = doctorDto.Fees,
                WaitingTime = doctorDto.WaitingTime,
                AppointmentDurationMinutes = doctorDto.AppointmentDurationMinutes,
                CountryId = doctorDto.CountryId,
                SpecId = doctorDto.SpecId,
                AppointmentDtos = doctorDto.AppointmentDtos,
                doctorSubSpecialtyDtos = doctorDto.doctorSubSpecialtyDtos,
                Gender = doctorDto.Gender,
                WorkingPlaceDtos = doctorDto.WorkingPlaceDtos
            };
            var NewDoctor = await _doctorServices.Create(DoctorWithImage);
            if(NewDoctor.Entity is null)
            {
                return BadRequest(NewDoctor.Message);
            }
            return Ok(NewDoctor);
        }
    }
}
