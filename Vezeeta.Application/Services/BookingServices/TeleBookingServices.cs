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
    public class TeleBookingServices : ITeleBookingServices
    {
        private readonly ITeleBookingRepository _teleBookingRepository;
        private readonly IMapper _mapper;
        private readonly ITeleTimeSlotRepository _teleTimeSlotRepository;

        public TeleBookingServices(ITeleBookingRepository teleBookingRepository , IMapper mapper , ITeleTimeSlotRepository teleTimeSlotRepository)
        {
            _teleBookingRepository = teleBookingRepository;
            _mapper = mapper;
            _teleTimeSlotRepository = teleTimeSlotRepository;
        }

        public async Task<ResultView<TeleBookingDto>> Create(TeleBookingDto teleBookingDto)
        {
            var time = (await _teleTimeSlotRepository.GetAllasync()).FirstOrDefault(s => s.IsBooked == false && s.Id == teleBookingDto.TeleTimeSlotId);
            if(time is null)
            {
                return new ResultView<TeleBookingDto>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The Time is Booked"
                };
            }

            time.IsBooked = true;
            await _teleTimeSlotRepository.SaveAsync();

            var NewBooking = await _teleBookingRepository.Createasync(_mapper.Map<TeleBooking>(teleBookingDto));
            await _teleBookingRepository.SaveAsync();

            return new ResultView<TeleBookingDto>()
            {
                Entity = _mapper.Map<TeleBookingDto>(NewBooking),
                IsSuccess = true,
                Message = " The Booking Done"
            };
        }

        public async Task<ResultView<TeleBookingDto>> Delete(int Id)
        {
            var Booking = await _teleBookingRepository.GetOneasync(Id);
            if (Booking is null)
            {
                return new ResultView<TeleBookingDto>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The Booking Not Found"
                };
            }

            var time = await _teleTimeSlotRepository.GetOneasync(Booking.TeleTimeSlotId);
            time.IsBooked = false;
            await _teleTimeSlotRepository.SaveAsync();

            var DeletedBooking = await _teleBookingRepository.Deleteasync(Booking);
            await _teleBookingRepository.SaveAsync();

            return new ResultView<TeleBookingDto>()
            {
                Entity = _mapper.Map<TeleBookingDto>(DeletedBooking),
                IsSuccess = true,
                Message = " The Booking Deleted Successfully"
            };
        }

        public async Task<ResultDataList<TeleBookingDto>> GetAll(int PageNumber , int Items)
        {
            var DrBooks = (await _teleBookingRepository.GetAllasync()).Where(s => s.IsDeleted == false);
            if (DrBooks is null)
            {
                return new ResultDataList<TeleBookingDto>
                {
                    Entities = null,
                    Count = 0
                };
            }
            var DrBookDto = DrBooks.Skip(Items * (PageNumber - 1)).Take(Items).Select(s => new TeleBookingDto
            {
                Id = s.Id,
                DoctorId = s.DoctorId,
                UserId = s.UserId,
                TeleTimeSlotId = s.TeleTimeSlotId,
                Status = s.Status
            });
            return new ResultDataList<TeleBookingDto>
            {
                Entities = DrBookDto.ToList(),
                Count = DrBooks.Count()
            };
        }

        public async Task<ResultView<TeleBookingDto>> GetOne(int Id)
        {
            var Booking = await _teleBookingRepository.GetOneasync(Id);
            if (Booking is null)
            {
                return new ResultView<TeleBookingDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The Booking not found"
                };
            }
            return new ResultView<TeleBookingDto>
            {
                Entity = _mapper.Map<TeleBookingDto>(Booking),
                IsSuccess = true,
                Message = "The Booking Found "
            };
        }
    }
}
