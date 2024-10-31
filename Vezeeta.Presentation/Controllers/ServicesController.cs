using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Services.ServicesServices;
using Vezeeta.Dtos.DTOS.ServicesDtos;

namespace Vezeeta.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesServices _servicesServices;

        public ServicesController(IServicesServices servicesServices)
        {
            _servicesServices = servicesServices;
        }
        [HttpPost]
        public async Task<IActionResult> Create (ServicesDto servicesDto)
        {
            var NewServices = await _servicesServices.Create(servicesDto);
            if (NewServices.Entity == null)
            {
                return BadRequest(NewServices.Message);
            }
            return Ok(NewServices);
        }
        [HttpPut]
        public async Task<IActionResult> Update (ServicesDto servicesDto)
        {
            var UpdatedService = await _servicesServices.Update(servicesDto);
            if (UpdatedService.Entity == null)
            {
                return BadRequest(UpdatedService.Message);
            }
            return Ok(UpdatedService);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete (int id)
        {
            var DeletedService = await _servicesServices.Delete(id);
            if(DeletedService.Entity == null)
            {
                return NotFound(DeletedService.Message);
            }
            return Ok(DeletedService);
        }
        [HttpGet("One")]
        public async Task<IActionResult> GetOne (int id)
        {
            var service = await _servicesServices.GetOne(id);
            if(service.Entity == null)
            {
                return NotFound(service.Message);
            }
            return Ok(service);
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll (int pageNumber , int items)
        {
            var Services = await  _servicesServices.GetAll(pageNumber, items);
            if(Services.Count == 0)
            {
                return BadRequest();
            }
            return Ok(Services);
        }
    }
}
