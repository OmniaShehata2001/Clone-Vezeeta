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
            var NewDoctor = await _doctorServices.Create(doctorDto);
            if(NewDoctor.Entity is null)
            {
                return BadRequest(NewDoctor.Message);
            }
            return Ok(NewDoctor);
        }
        [HttpPut]
        public async Task<IActionResult> Update(DoctorDto doctorDto)
        {
            var UpdatedDoctor = await _doctorServices.Update(doctorDto);
            if(UpdatedDoctor.Entity is null)
            {
                return BadRequest(UpdatedDoctor.Message);
            }
            return Ok(UpdatedDoctor);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var DeletedDoctor = await _doctorServices.Delete(id);
            if(DeletedDoctor.Entity is null)
            {
                return BadRequest(DeletedDoctor.Message);
            }
            return Ok(DeletedDoctor);
        }
        [HttpGet("One")]
        public async Task<IActionResult> GetOne(int id)
        {
            var Doctor = await _doctorServices.GetOne(id);
            if(Doctor.Entity is null)
            {
                return BadRequest(Doctor.Message);
            }
            return Ok(Doctor);
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll(int PageNumber , int Items)
        {
            var Doctors = await _doctorServices.GetAll(PageNumber, Items);
            if(Doctors.Count == 0)
            {
                return NotFound();
            }
            return Ok(Doctors);
        }
    }
}
 