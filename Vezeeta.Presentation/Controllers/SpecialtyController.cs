using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using Vezeeta.Application.Services.Specialty_Services;
using Vezeeta.Dtos.DTOS.SpecialtyDtos;

namespace Vezeeta.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        private readonly ISpecialtyServices _specialtyServices;

        public SpecialtyController(ISpecialtyServices specialtyServices) 
        {
            _specialtyServices = specialtyServices;
        }
        [HttpPost]
        public async Task<IActionResult> Create(SpecialtyDto specialtyDto)
        {
            var NewSpecialty = await _specialtyServices.CreateAsync(specialtyDto);
            if(NewSpecialty.Entity == null)
            {
                return BadRequest(NewSpecialty.Message);
            }
            return Ok(NewSpecialty);
        }
        [HttpPut]
        public async Task<IActionResult> Update (SpecialtyDto specialtyDto)
        {
            var UpdatedSpecialty = await _specialtyServices.UpdateAsync(specialtyDto);
            if(UpdatedSpecialty.Entity == null)
            {
                return BadRequest(UpdatedSpecialty.Message);
            }
            return Ok(UpdatedSpecialty);
        }
        [HttpGet("One")]
        public async Task<IActionResult> GetById(int id)
        {
            var Specialty = await _specialtyServices.GetOne(id);
            if(Specialty.Entity == null)
            {
                return BadRequest(Specialty.Message);
            }
            return Ok(Specialty);
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll(int PageNumber , int Items)
        {
            var Specialties = await _specialtyServices.GetAll(PageNumber, Items);
            if(Specialties.Count == 0)
            {
                return BadRequest();
            }
            return Ok(Specialties);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var DeletedSpecialty = await _specialtyServices.DeleteAsync(id);
            if(DeletedSpecialty.Entity == null)
            {
                return BadRequest(DeletedSpecialty.Message);
            }
            return Ok(DeletedSpecialty);
        }
    }
}
