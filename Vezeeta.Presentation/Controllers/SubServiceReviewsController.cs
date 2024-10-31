using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Services.ServicesServices;
using Vezeeta.Dtos.DTOS.ServicesDtos;

namespace Vezeeta.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubServiceReviewsController : ControllerBase
    {
        private readonly ISubServicesReviewsServices _subServicesReviewsServices;

        public SubServiceReviewsController(ISubServicesReviewsServices subServicesReviewsServices)
        {
            _subServicesReviewsServices = subServicesReviewsServices;
        }
        [HttpPost]
        public async Task<IActionResult> Create (SubServicesReviewsDto subServicesReviewsDto)
        {
            var Review = await _subServicesReviewsServices.Create(subServicesReviewsDto);
            if(Review.Entity is null)
            {
                return BadRequest(Review.Message);
            }
            return Ok(Review);
        }
        [HttpPut]
        public async Task<IActionResult> Update (SubServicesReviewsDto subServicesReviewsDto)
        {
            var UpdatedReview = await _subServicesReviewsServices.Update(subServicesReviewsDto);
            if(UpdatedReview.Entity is null)
            {
                return BadRequest(UpdatedReview.Message);
            }
            return Ok(UpdatedReview);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete (int id)
        {
            var DeletedReview = await _subServicesReviewsServices.Delete(id);
            if(DeletedReview.Entity is null)
            {
                return BadRequest(DeletedReview.Message);
            }
            return Ok(DeletedReview);
        }
        [HttpGet("One")]
        public async Task<IActionResult> GetOne (int id)
        {
            var Review = await _subServicesReviewsServices.GetOne(id);
            if(Review.Entity is null)
            {
                return NotFound(Review.Message);
            }
            return Ok(Review);
        }
        [HttpGet("bySubServiceId")]
        public async Task<IActionResult> GetBySubServiceId (int SubServiceId , int PageNumber , int Items)
        {
            var Reviews = await _subServicesReviewsServices.GetReviewBysubServiceId(SubServiceId, PageNumber, Items);
            if(Reviews.Count == 0)
            {
                return BadRequest();
            }
            return Ok(Reviews);
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll(int PageNumber , int Items)
        {
            var Reviews = await _subServicesReviewsServices.GetAll(PageNumber, Items);
            if(Reviews.Count == 0)
            {
                return BadRequest();
            }
            return Ok(Reviews);
        }
    }
}
