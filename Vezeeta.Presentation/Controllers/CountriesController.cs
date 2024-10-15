using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Services.Countries_Services;
using Vezeeta.Dtos.DTOS.CountryDtos;

namespace Vezeeta.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesServices _countriesServices;

        public CountriesController(ICountriesServices countriesServices)
        {
            _countriesServices = countriesServices;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrUpdateCountryDto createOrUpdateCountryDto)
        {
            var CreatedCountry = await _countriesServices.CreateAsync(createOrUpdateCountryDto);
            if (CreatedCountry.IsSuccess == true)
            {
                return Ok(CreatedCountry);
            }
            return BadRequest(CreatedCountry.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CreateOrUpdateCountryDto updateOrUpdateCountryDto)
        {
            var UpdatedCountry = await _countriesServices.UpdateAsync(updateOrUpdateCountryDto);
            if(UpdatedCountry.IsSuccess == true)
            {
                return Ok(UpdatedCountry);
            }
            return BadRequest(UpdatedCountry.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var DeletedCountry = await _countriesServices.DeleteAsync(id);
            if(DeletedCountry.IsSuccess == true)
            {
                return Ok(DeletedCountry);
            }
            return BadRequest(DeletedCountry.Message);
        }

        [HttpGet("One")]
        public async Task<IActionResult> GetOne(int id)
        {
            var Country = await _countriesServices.GetOne(id);
            if(Country.IsSuccess == true)
            {
                return Ok(Country);
            }
            return BadRequest(Country.Message);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var Countries = await _countriesServices.GetAll();
            if(Countries.Count == 0)
            {
                return BadRequest();
            }
            return Ok(Countries);
        }
    }
}
