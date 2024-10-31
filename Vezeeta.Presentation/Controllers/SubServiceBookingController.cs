using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Contract.ServicesRepositories;
using Vezeeta.Application.Services.ServicesServices;
using Vezeeta.Dtos.DTOS.ServicesDtos;

namespace Vezeeta.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubServiceBookingController : ControllerBase
    {
        private readonly ISubServicesBookingServices _subServicesBookingServices;

        public SubServiceBookingController(ISubServicesBookingServices subServicesBookingServices)
        {
            _subServicesBookingServices = subServicesBookingServices;
        }
        [HttpPost]
        public async Task<IActionResult> Create (SubServicesBookingDto subServicesBookingDto)
        {
            var NewBooking = await _subServicesBookingServices.Create(subServicesBookingDto);
            if(NewBooking.Entity == null)
            {
                return BadRequest(NewBooking.Message);
            }
            return Ok(NewBooking);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete (int id)
        {
            var DeletedBooking = await _subServicesBookingServices.Delete(id);
            if(DeletedBooking.Entity == null)
            {
                return BadRequest(DeletedBooking.Message);
            }
            return Ok(DeletedBooking);
        }
        [HttpGet("One")]
        public async Task<IActionResult> GetOne (int id)
        {
            var Booking = await _subServicesBookingServices.GetOne(id);
            if(Booking.Entity == null)
            {
                return NotFound(Booking.Message);
            }
            return Ok(Booking);
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll (int PageNumber , int items)
        {
            var Bookings = await _subServicesBookingServices.GetAll(PageNumber , items);
            if(Bookings.Count == 0)
            {
                return NotFound();
            }
            return Ok(Bookings);
        }
        [HttpGet("ByUserId")]
        public async Task<IActionResult> GetByUserId (string userId)
        {
            var Booking = await _subServicesBookingServices.GetByUserId(userId);
            if(Booking == null)
            {
                return NotFound("The User Not Have Appointments");
            }
            return Ok(Booking);
        }
    }
}
