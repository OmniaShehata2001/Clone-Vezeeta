using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.AppointmentRepositories;
using Vezeeta.Application.Contract.DoctorRepositories;
using Vezeeta.Application.Contract.SpecialtyRepositories;
using Vezeeta.Application.Contract.WorkingPlaceRepositories;
using Vezeeta.Dtos.DTOS.AppointmentDtos;
using Vezeeta.Dtos.DTOS.DoctorDtos;
using Vezeeta.Dtos.DTOS.SubSpecialtyDtos;
using Vezeeta.Dtos.DTOS.WorkingPlaceDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Models;

namespace Vezeeta.Application.Services.DoctorServices
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly IDoctorSubSpecialtyRepository _doctorSubSpecialtyRepository;
        private readonly IWorkingPlaceRepository _workingPlaceRepository;
        private readonly IDoctorWorkingPlaceRepository _doctorWorkingPlaceRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly IWorkingPlaceImagesRepository _workingPlaceImagesRepository;
        private readonly ISubSpecialtyRepository _subSpecialtyRepository;

        public DoctorServices(IDoctorRepository doctorRepository, IMapper mapper , IDoctorSubSpecialtyRepository doctorSubSpecialtyRepository,
            IWorkingPlaceRepository workingPlaceRepository , IDoctorWorkingPlaceRepository doctorWorkingPlaceRepository
            ,IAppointmentRepository appointmentRepository , ITimeSlotRepository timeSlotRepository , IWorkingPlaceImagesRepository workingPlaceImagesRepository
            ,ISubSpecialtyRepository subSpecialtyRepository) 
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _doctorSubSpecialtyRepository = doctorSubSpecialtyRepository;
            _workingPlaceRepository = workingPlaceRepository;
            _doctorWorkingPlaceRepository = doctorWorkingPlaceRepository;
            _appointmentRepository = appointmentRepository;
            _timeSlotRepository = timeSlotRepository;
            _workingPlaceImagesRepository = workingPlaceImagesRepository;
            _subSpecialtyRepository = subSpecialtyRepository;
        }
        public async Task<ResultView<DoctorDto>> Create(DoctorDto Doctordto)
        {
            var DoctorExist = (await _doctorRepository.GetAllasync())
                .FirstOrDefault(s=>s.Name == Doctordto.Name && s.SSN == Doctordto.SSN && s.IsDeleted == false); 
            if(DoctorExist is not null)
            {
                return new ResultView<DoctorDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The Doctor Already Exist"
                };
            }

            var DoctorModel = _mapper.Map<Doctor>(Doctordto);
            var NewDoctor = await _doctorRepository.Createasync(_mapper.Map<Doctor>(Doctordto));
            await _doctorRepository.SaveAsync();




            if(Doctordto.doctorSubSpecialtyDtos is not null)
            {
                foreach(var DrSubSpec in Doctordto.doctorSubSpecialtyDtos)
                {
                    var drSubSpecDto = new DoctorSubSpecialtyDto { DoctorId = NewDoctor.Id, SubSpecId = DrSubSpec };
                    var NewDrSubSpec = await _doctorSubSpecialtyRepository.Createasync(_mapper.Map<DoctorSubSpecialties>(drSubSpecDto));
                }
                await _doctorSubSpecialtyRepository.SaveAsync();
            }




            if(Doctordto.WorkingPlaceDtos is not null)
            {
                foreach(var workingplace in Doctordto.WorkingPlaceDtos)
                {
                    var WorkingPlaceExist = (await _workingPlaceRepository.GetAllasync())
                                           .FirstOrDefault(s => s.Area == workingplace.Area && s.City == workingplace.City && s.Name == workingplace.Name);
                    if (WorkingPlaceExist is null)
                    {
                        var NewWorkingPlace = await _workingPlaceRepository.Createasync(_mapper.Map<WorkingPlace>(workingplace));
                        await _workingPlaceRepository.SaveAsync();
                        var DrWorkingPlaceDto = new DoctorWorkingPlaceDto { DoctorId = NewDoctor.Id, WorkingPlaceId = NewWorkingPlace.Id };
                        var NewDrWorkingPlace = await _doctorWorkingPlaceRepository.Createasync(_mapper.Map<DoctorWorkingPlace>(DrWorkingPlaceDto));
                        if (Doctordto.WorkingPlaceImages is not null)
                        {
                            foreach(var image in Doctordto.WorkingPlaceImages)
                            {
                                var NewImageDto = new WorkingPlaceImagesDto { ImgPath = image, WorkingPlaceId = NewWorkingPlace.Id };
                                var NewImage = await _workingPlaceImagesRepository.Createasync(_mapper.Map<WorkingPlaceImages>(NewImageDto));
                            }
                            await _workingPlaceImagesRepository.SaveAsync();
                        }
                    }
                    else
                    {
                        var DrWorkingPlaceDto = new DoctorWorkingPlaceDto { DoctorId = NewDoctor.Id, WorkingPlaceId = WorkingPlaceExist.Id };
                        var NewDrWorkingPlace = await _doctorWorkingPlaceRepository.Createasync(_mapper.Map<DoctorWorkingPlace>(DrWorkingPlaceDto));
                    }
                }
                await _doctorWorkingPlaceRepository.SaveAsync();
            }





            if(Doctordto.AppointmentDtos is not null)
            {
                foreach(var appointment in  Doctordto.AppointmentDtos)
                {
                    var AppointmentExist = (await _appointmentRepository.GetAllasync())
                                        .FirstOrDefault(s => s.DoctorId == appointment.DoctorId && ((int)s.Day) == ((int)appointment.Day));
                    if (AppointmentExist is null)
                    {
                        appointment.DoctorId = NewDoctor.Id;
                        var NewAppointment = await _appointmentRepository.Createasync(_mapper.Map<Appointment>(appointment));
                        await _appointmentRepository.SaveAsync();
                        if (appointment.TimeSlots is not null)
                        {
                            foreach (var time in appointment.TimeSlots)
                            {
                                time.AppointId = NewAppointment.Id;
                                var NewTime = await _timeSlotRepository.Createasync(_mapper.Map<TimeSlot>(time));
                            }
                        }
                        await _timeSlotRepository.SaveAsync();
                    }
                }
            }






            return new ResultView<DoctorDto>
            {
                Entity = _mapper.Map<DoctorDto>(NewDoctor),
                IsSuccess = true,
                Message = " The Doctor Created Successfully"
            };
        }

        public async Task<ResultView<DoctorDto>> Delete(int DoctorId)
        {
            var Doctor = await _doctorRepository.GetOneasync(DoctorId);
            if(Doctor == null)
            {
                return new ResultView<DoctorDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The Doctor Not Found"
                };
            }
            Doctor.IsDeleted = true;
            await _doctorRepository.SaveAsync();



            var SupSpec = (await _doctorSubSpecialtyRepository.GetAllasync()).Where(s => s.DoctorId == DoctorId);
            foreach(var supspec in SupSpec)
            {
                supspec.IsDeleted = true;
            }
            await _doctorSubSpecialtyRepository.SaveAsync();



            var Drworkingplace = (await _doctorWorkingPlaceRepository.GetAllasync()).Where(s => s.DoctorId == DoctorId);
            foreach(var drworkingplace in Drworkingplace)
            {
                drworkingplace.IsDeleted = true;
            }
            await _doctorWorkingPlaceRepository.SaveAsync();



            var Appointments = (await _appointmentRepository.GetAllasync()).Where(s => s.DoctorId == DoctorId);
            foreach(var appointment in Appointments)
            {
                appointment.IsDeleted = true;
            }
            await _appointmentRepository.SaveAsync();


            return new ResultView<DoctorDto>
            {
                IsSuccess = true,
                Entity = _mapper.Map<DoctorDto>(Doctor),
                Message = "The Doctor Deleted Successfully"
            };

        }

        public async Task<ResultDataList<DoctorWithDetails>> GetAll(int PageNumber, int Items)
        {
            var doctors = (await _doctorRepository.GetAllasync()).Where(s => s.IsDeleted == false);
            var doctorsdto = doctors.Skip(Items * (PageNumber - 1)).Take(Items).Select(s => new DoctorWithDetails
            {
                Name = s.Name,
                Description = s.Description,
                AboutDoctor = s.AboutDoctor,
                PhoneNumber = s.PhoneNumber,
                Gender = s.Gender,
                SSN = s.SSN,
                DoctorImage = s.DoctorImage,
                Fees = s.Fees,
                WaitingTime = s.WaitingTime,
                CountryId = s.CountryId,
                SpecId = s.SpecId,
                AppointmentDurationMinutes = s.AppointmentDurationMinutes
            }).ToList();

            foreach (var doctor in doctorsdto)
            {
                var DrSubSup = await _subSpecialtyRepository.GetSubSpecialtyByDoctorId(doctor.Id);
                doctor.SubSpecialtyDtos = _mapper.Map<List<SubSpecialtyDto>>(DrSubSup);
            }


            foreach(var doctor in doctorsdto)
            {
                var DrWorkingPlaces = await _workingPlaceRepository.GetWorkingPlaceByDoctorId(doctor.Id);
                doctor.WorkingPlaceDtos = _mapper.Map<List<WorkingPlaceDto>>(DrWorkingPlaces);
                foreach(var workingplace in DrWorkingPlaces)
                {
                    var Images = (await _workingPlaceImagesRepository.GetAllasync())
                        .Where(s => s.IsDeleted == false && s.WorkingPlaceId == workingplace.Id).ToList();
                    doctor.WorkingPlaceImages = _mapper.Map<List<WorkingPlaceImagesDto>>(Images);
                }
            }


            foreach(var doctor in doctorsdto)
            {
                var App = await _appointmentRepository.GetAppByDoctorId(doctor.Id);
                doctor.AppointmentDtos = _mapper.Map<List<AppointmentDto>>(App);
                foreach(var Appoint in App)
                {
                    var Timeslot = (await _timeSlotRepository.GetAllasync()).Where(s => s.AppointId == Appoint.Id && s.IsDeleted == false);
                    doctor.TimeSlotDtos = _mapper.Map<List<TimeSlotDto>>(Timeslot);
                }
            }


            return new ResultDataList<DoctorWithDetails>
            {
                Entities = doctorsdto,
                Count = doctors.Count()
            };


        }

        public Task<ResultView<DoctorDto>> GetOne(int DoctorId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultView<DoctorDto>> Update(DoctorDto Doctordto)
        {
            throw new NotImplementedException();
        }
    }
}
