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
    }
}
