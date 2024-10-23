using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Services.ReviewsServices;
using Vezeeta.Dtos.DTOS.ReviewsDtos;

namespace Vezeeta.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorReviewsController : ControllerBase
    {
        private readonly IDoctorReviewesServices _doctorReviewesServices;

        public DoctorReviewsController(IDoctorReviewesServices doctorReviewesServices)
        {
            _doctorReviewesServices = doctorReviewesServices;
        }
        [HttpPost]
        public async Task<IActionResult> Create (DoctorReviewDto doctorReviewDto)
        {
            var NewReview = await _doctorReviewesServices.Create(doctorReviewDto);
            if(NewReview.Entity is null)
            {
                return BadRequest(NewReview.Message);
            }
            return Ok(NewReview);
        }
        [HttpPut]
        public async Task<IActionResult> Update (DoctorReviewDto doctorReviewDto)
        {
            var UpdatedReview = await _doctorReviewesServices.Update(doctorReviewDto);
            if(UpdatedReview.Entity is null)
            {
                return BadRequest(UpdatedReview.Message);
            }
            return Ok(UpdatedReview);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete (int Id)
        {
            var DeletedReview = await _doctorReviewesServices.Delete(Id);
            if(DeletedReview.Entity is null)
            {
                return BadRequest(DeletedReview.Message);
            }
            return Ok(DeletedReview);
        }
        [HttpGet("One")]
        public async Task<IActionResult> GetOne (int Id)
        {
            var Review = await _doctorReviewesServices.GetOne(Id);
            if(Review.Entity is null)
            {
                return NotFound(Review.Message);
            }
            return Ok(Review);
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll(int PageNumber , int Items)
        {
            var Reviews = await _doctorReviewesServices.GetAll(PageNumber ,Items);
            if(Reviews.Count == 0)
            {
                return BadRequest();
            }
            return Ok(Reviews);
        }
        [HttpGet("ByDoctorId")]
        public async Task<IActionResult> GetByDoctorId(int DoctorId , int PageNumber , int Items)
        {
            var Reviews = await _doctorReviewesServices.GetByDoctorId (DoctorId , PageNumber , Items);
            if(Reviews.Count == 0)
            {
                return NotFound("There is No Reviews For Doctor");
            }
            return Ok(Reviews);
        }
    }
}
