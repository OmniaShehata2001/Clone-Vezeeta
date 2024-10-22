using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Services.Specialty_Services;
using Vezeeta.Dtos.DTOS.SubSpecialtyDtos;

namespace Vezeeta.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubSpecialtyController : ControllerBase
    {
        private readonly ISubSpecialtyServices _subSpecialtyServices;

        public SubSpecialtyController(ISubSpecialtyServices subSpecialtyServices)
        {
            _subSpecialtyServices = subSpecialtyServices;
        }
        [HttpPost]
        public async Task<IActionResult> Create(SubSpecialtyDto subSpecialtyDto)
        {
            var NewSubSpec = await _subSpecialtyServices.Create(subSpecialtyDto);
            if(NewSubSpec.Entity is null)
            {
                return BadRequest(NewSubSpec.Message);
            }
            return Ok(NewSubSpec);
        }
        [HttpPut]
        public async Task<IActionResult> Update(SubSpecialtyDto subSpecialtyDto)
        {
            var UpdatedSubSpec = await _subSpecialtyServices.Update(subSpecialtyDto);
            if(UpdatedSubSpec.Entity is null)
            {
                return BadRequest(UpdatedSubSpec.Message);
            }
            return Ok(UpdatedSubSpec);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var DeletedSubSpec = await _subSpecialtyServices.Delete(id);
            if(DeletedSubSpec.Entity is null)
            {
                return BadRequest(DeletedSubSpec.Message);
            }
            return Ok(DeletedSubSpec);
        }
        [HttpGet("One")]
        public async Task<IActionResult> GetOne(int id)
        {
            var SubSpec = await _subSpecialtyServices.GetOne(id);
            if(SubSpec.Entity is null)
            {
                return BadRequest(SubSpec.Message);
            }
            return Ok(SubSpec);
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var SubSpecalties = await _subSpecialtyServices.GetAll();
            if(SubSpecalties.Count == 0)
            {
                return NotFound();
            }
            return Ok(SubSpecalties);
        }
        [HttpGet("BySpecId")]
        public async Task<IActionResult> GetBySpecId(int Specid)
        {
            var SubSpecs = await _subSpecialtyServices.GetSubSpecialtyBySpecId(Specid);
            if(SubSpecs.Count == 0)
            {
                return NotFound();
            }
            return Ok(SubSpecs);
        }
    }
}
