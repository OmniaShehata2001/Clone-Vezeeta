using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.AppointmentRepositories;
using Vezeeta.Application.Contract.BookingRepositories;
using Vezeeta.Application.Contract.ServicesRepositories;
using Vezeeta.Dtos.DTOS.BookingDtos;
using Vezeeta.Dtos.DTOS.ServicesDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Models;

namespace Vezeeta.Application.Services.ServicesServices
{
    public class SubServicesBookingServices : ISubServicesBookingServices
    {
        private readonly ISubServicesBookingRepository _subServicesBookingRepository;
        private readonly IMapper _mapper;
        private readonly ISubServicesTimeSlotRepository _subServicesTimeSlotRepository;

        public SubServicesBookingServices(ISubServicesBookingRepository subServicesBookingRepository, IMapper mapper , ISubServicesTimeSlotRepository subServicesTimeSlotRepository)
        {
            _subServicesBookingRepository = subServicesBookingRepository;
            _mapper = mapper;
            _subServicesTimeSlotRepository = subServicesTimeSlotRepository;
        }

        public async Task<ResultView<SubServicesBookingDto>> Create(SubServicesBookingDto subServicesBookingDto)
        {
            var Booking = (await _subServicesTimeSlotRepository.GetAllasync()).FirstOrDefault(s => s.IsDeleted == false && s.IsBooked == false && s.Id == subServicesBookingDto.SubServiceTimeSlotId);
            if (Booking == null)
            {
                return new ResultView<SubServicesBookingDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The Appointment Not Found"
                };
            }
            var NewBooking = await _subServicesBookingRepository.Createasync(_mapper.Map<SubServicesBooking>(subServicesBookingDto));
            await _subServicesBookingRepository.SaveAsync();

            Booking.IsBooked = true;
            await _subServicesTimeSlotRepository.SaveAsync();

            return new ResultView<SubServicesBookingDto>
            {
                Entity = _mapper.Map<SubServicesBookingDto>(NewBooking),
                IsSuccess = true,
                Message = " The Booking Created Successfully"
            };
        }

        public async Task<ResultView<SubServicesBookingDto>> Delete(int Id)
        {
            var Booking = await _subServicesBookingRepository.GetOneasync(Id);
            if (Booking is null)
            {
                return new ResultView<SubServicesBookingDto>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The Booking Not Found"
                };
            }

            var time = await _subServicesTimeSlotRepository.GetOneasync(Booking.SubServicesId);
            time.IsBooked = false;
            await _subServicesTimeSlotRepository.SaveAsync();

            var DeletedBooking = await _subServicesBookingRepository.Deleteasync(Booking);
            await _subServicesBookingRepository.SaveAsync();

            return new ResultView<SubServicesBookingDto>()
            {
                Entity = _mapper.Map<SubServicesBookingDto>(DeletedBooking),
                IsSuccess = true,
                Message = " The Booking Deleted Successfully"
            };
        }

        public async Task<ResultDataList<SubServicesBookingDto>> GetAll(int pagenumber, int items)
        {
            var SubServBooks = (await _subServicesBookingRepository.GetAllasync()).Where(s => s.IsDeleted == false);
            if (SubServBooks is null)
            {
                return new ResultDataList<SubServicesBookingDto>
                {
                    Entities = null,
                    Count = 0
                };
            }
            var SubServBooksDto = SubServBooks.Skip(items * (pagenumber - 1)).Take(items).Select(s => new SubServicesBookingDto
            {
                Id = s.Id,
                SubServicesId = s.SubServicesId,
                UserId = s.UserId,
                SubServiceTimeSlotId = s.SubServiceTimeSlotId,
                Status = s.Status
            });
            return new ResultDataList<SubServicesBookingDto>
            {
                Entities = SubServBooksDto.ToList(),
                Count = SubServBooks.Count()
            };
        }

        public async Task<ResultDataList<SubServicesBookingDto>> GetByUserId(string userId)
        {
            var Booking = (await _subServicesBookingRepository.GetAllasync()).Where(s => s.UserId == userId);
            if(Booking is null)
            {
                return new ResultDataList<SubServicesBookingDto>
                {
                    Entities = null,
                    Count = 0
                };
            }
            return new ResultDataList<SubServicesBookingDto>
            {
                Entities = _mapper.Map<List<SubServicesBookingDto>>(Booking),
                Count = Booking.Count()
            };
        }

        public async Task<ResultView<SubServicesBookingDto>> GetOne(int Id)
        {
            var Booking = await _subServicesBookingRepository.GetOneasync(Id);
            if (Booking is null)
            {
                return new ResultView<SubServicesBookingDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The Booking not found"
                };
            }
            return new ResultView<SubServicesBookingDto>
            {
                Entity = _mapper.Map<SubServicesBookingDto>(Booking),
                IsSuccess = true,
                Message = "The Booking Found "
            };
        }
    }
}
