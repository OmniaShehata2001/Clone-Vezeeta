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

        public DoctorServices(IDoctorRepository doctorRepository, IMapper mapper , IDoctorSubSpecialtyRepository doctorSubSpecialtyRepository,
            IWorkingPlaceRepository workingPlaceRepository , IDoctorWorkingPlaceRepository doctorWorkingPlaceRepository
            ,IAppointmentRepository appointmentRepository , ITimeSlotRepository timeSlotRepository , IWorkingPlaceImagesRepository workingPlaceImagesRepository) 
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _doctorSubSpecialtyRepository = doctorSubSpecialtyRepository;
            _workingPlaceRepository = workingPlaceRepository;
            _doctorWorkingPlaceRepository = doctorWorkingPlaceRepository;
            _appointmentRepository = appointmentRepository;
            _timeSlotRepository = timeSlotRepository;
            _workingPlaceImagesRepository = workingPlaceImagesRepository;
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

        //public Task<ResultView<DoctorDto>> Delete(int DoctorId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ResultDataList<DoctorDto>> GetAll(int PageNumber, int Items)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ResultView<DoctorDto>> GetOne(int DoctorId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ResultView<DoctorDto>> Update(DoctorDto Doctordto)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
