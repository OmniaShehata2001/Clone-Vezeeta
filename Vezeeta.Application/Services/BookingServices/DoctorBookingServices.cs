using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.AppointmentRepositories;
using Vezeeta.Application.Contract.BookingRepositories;
using Vezeeta.Dtos.DTOS.BookingDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Models;

namespace Vezeeta.Application.Services.BookingServices
{
    public class DoctorBookingServices : IDoctorBookingServices
    {
        private readonly IDoctorBookingRepository _doctorBookingRepository;
        private readonly IMapper _mapper;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public DoctorBookingServices(IDoctorBookingRepository doctorBookingRepository, IMapper mapper , ITimeSlotRepository timeSlotRepository)
        {
            _doctorBookingRepository = doctorBookingRepository;
            _mapper = mapper;
            _timeSlotRepository = timeSlotRepository;
        }
        public async Task<ResultView<DoctorBookingDto>> Create(DoctorBookingDto doctorBookingDto)
        {
            var time = (await _timeSlotRepository.GetAllasync()).FirstOrDefault(s => s.Id == doctorBookingDto.TimeSlotId && s.IsBooked == false);
            if(time is null)
            {
                return new ResultView<DoctorBookingDto>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The time is booked"
                };
            }


            var Booking = await _doctorBookingRepository.Createasync(_mapper.Map<DoctorBooking>(doctorBookingDto));
            await _doctorBookingRepository.SaveAsync();


            time.IsBooked = true;
            await _timeSlotRepository.SaveAsync();


            return new ResultView<DoctorBookingDto>()
            {
                Entity = _mapper.Map<DoctorBookingDto>(Booking),
                IsSuccess = true,
                Message = "The Booking Created Successfully"
            };
        }

        public async Task<ResultView<DoctorBookingDto>> Delete(int Id)
        {
            var Booking = await _doctorBookingRepository.GetOneasync(Id);
            if(Booking is null)
            {
                return new ResultView<DoctorBookingDto>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The Booking Not Found"
                };
            }

            var time = await _timeSlotRepository.GetOneasync(Booking.TimeSlotId);
            time.IsBooked = false;
            await _timeSlotRepository.SaveAsync();

            var DeletedBooking = await _doctorBookingRepository.Deleteasync(Booking);
            await _doctorBookingRepository.SaveAsync();

            return new ResultView<DoctorBookingDto>()
            {
                Entity = _mapper.Map<DoctorBookingDto>(DeletedBooking),
                IsSuccess = true,
                Message = " The Booking Deleted Successfully"
            };
        }

        public async Task<ResultDataList<DoctorBookingDto>> GetAll(int pageNumber , int Items)
        {
            var DrBooks = (await _doctorBookingRepository.GetAllasync()).Where(s => s.IsDeleted == false);
            if(DrBooks is null)
            {
                return new ResultDataList<DoctorBookingDto>
                {
                    Entities = null,
                    Count = 0
                };
            }
            var DrBookDto = DrBooks.Skip(Items * (pageNumber - 1)).Take(Items).Select(s => new DoctorBookingDto
            {
                Id = s.Id,
                DoctorId = s.DoctorId,
                UserId = s.UserId,
                TimeSlotId = s.TimeSlotId,
                Status = s.Status
            });
            return new ResultDataList<DoctorBookingDto>
            {
                Entities = DrBookDto.ToList(),
                Count = DrBooks.Count()
            };
        }

        public async Task<ResultView<DoctorBookingDto>> GetOne(int Id)
        {
            var Booking = await _doctorBookingRepository.GetOneasync(Id);
            if(Booking  is null)
            {
                return new ResultView<DoctorBookingDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The Booking not found"
                };
            }
            return new ResultView<DoctorBookingDto>
            {
                Entity = _mapper.Map<DoctorBookingDto>(Booking),
                IsSuccess = true,
                Message = "The Booking Found "
            };
        }

    }
}
