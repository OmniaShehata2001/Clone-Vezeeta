using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Application.Services.BookingServices;
using Vezeeta.Dtos.DTOS.BookingDtos;

namespace Vezeeta.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorBookingController : ControllerBase
    {
        private readonly IDoctorBookingServices _doctorBookingServices;

        public DoctorBookingController(IDoctorBookingServices doctorBookingServices)
        {
            _doctorBookingServices = doctorBookingServices;
        }
        [HttpPost]
        public async Task<IActionResult> Create (DoctorBookingDto doctorBookingDto)
        {
            var Booking = await _doctorBookingServices.Create(doctorBookingDto);
            if(Booking.IsSuccess == false)
            {
                return BadRequest(Booking.Message);
            }
            return Ok(Booking);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var DeletedBooking = await _doctorBookingServices.Delete(Id);
            if(DeletedBooking.IsSuccess == false)
            {
                return NotFound(DeletedBooking.Message);
            }
            return Ok(DeletedBooking);
        }
        [HttpGet("One")]
        public async Task<IActionResult> GetOne(int Id)
        {
            var Booking = await _doctorBookingServices.GetOne(Id);
            if(Booking.IsSuccess == false)
            {
                return BadRequest(Booking.Message);
            }
            return Ok(Booking);
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll(int PageNumber , int Items)
        {
            var Bookings = await _doctorBookingServices.GetAll(PageNumber, Items);
            if(Bookings.Count == 0)
            {
                return NotFound();
            }
            return Ok(Bookings);
        }
    }
}
