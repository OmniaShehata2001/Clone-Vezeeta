using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Services.ServicesServices;
using Vezeeta.Dtos.DTOS.ServicesDtos;

namespace Vezeeta.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubServiceController : ControllerBase
    {
        private readonly ISubServicesServices _subServicesServices;

        public SubServiceController(ISubServicesServices subServicesServices)
        {
            _subServicesServices = subServicesServices;
        }
        [HttpPost]
        public async Task<IActionResult> Create (SubServicesDto subServicesDto)
        {
            var NewSubServ = await _subServicesServices.Create(subServicesDto);
            if(NewSubServ.Entity == null)
            {
                return BadRequest(NewSubServ.Message);
            }
            return Ok(NewSubServ);
        }
        [HttpPut]
        public async Task<IActionResult> Update (SubServicesDto subServicesDto)
        {
            var UpdatedSubServ = await _subServicesServices.Update(subServicesDto);
            if(UpdatedSubServ.Entity == null)
            {
                return BadRequest(UpdatedSubServ.Message);
            }
            return Ok(UpdatedSubServ);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete (int id)
        {
            var Deleted = await _subServicesServices.Delete(id);
            if(Deleted.Entity == null)
            {
                return BadRequest(Deleted.Message);
            }
            return Ok(Deleted);
        }
        [HttpGet("One")]
        public async Task<IActionResult> GetOne (int id)
        {
            var SubServ = await _subServicesServices.GetOne(id);
            if(SubServ.Entity == null)
            {
                return BadRequest(SubServ.Message);
            }
            return Ok(SubServ);
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll(int PageNumber , int Items)
        {
            var SubServices = await _subServicesServices.GetAll(PageNumber, Items);
            if(SubServices.Count == 0)
            {
                return BadRequest();
            }
            return Ok(SubServices);
        }
    }
}
