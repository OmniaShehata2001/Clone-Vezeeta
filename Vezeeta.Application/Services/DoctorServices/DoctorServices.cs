using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.AppointmentRepositories;
using Vezeeta.Application.Contract.CountriesRepositries;
using Vezeeta.Application.Contract.DoctorRepositories;
using Vezeeta.Application.Contract.SpecialtyRepositories;
using Vezeeta.Application.Contract.WorkingPlaceRepositories;
using Vezeeta.Dtos.DTOS.AppointmentDtos;
using Vezeeta.Dtos.DTOS.CountryDtos;
using Vezeeta.Dtos.DTOS.DoctorDtos;
using Vezeeta.Dtos.DTOS.SpecialtyDtos;
using Vezeeta.Dtos.DTOS.SubSpecialtyDtos;
using Vezeeta.Dtos.DTOS.WorkingPlaceDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Infrastucture.CountriesRepos;
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
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly ICountriesRepository _countriesRepository;
        private readonly ITeleAppointmentRepository _teleAppointmentRepository;
        private readonly ITeleTimeSlotRepository _teleTimeSlotRepository;

        public DoctorServices(IDoctorRepository doctorRepository, IMapper mapper , IDoctorSubSpecialtyRepository doctorSubSpecialtyRepository,
            IWorkingPlaceRepository workingPlaceRepository , IDoctorWorkingPlaceRepository doctorWorkingPlaceRepository
            ,IAppointmentRepository appointmentRepository , ITimeSlotRepository timeSlotRepository , IWorkingPlaceImagesRepository workingPlaceImagesRepository
            ,ISubSpecialtyRepository subSpecialtyRepository, ISpecialtyRepository specialtyRepository, ICountriesRepository countriesRepository,
            ITeleAppointmentRepository teleAppointmentRepository , ITeleTimeSlotRepository teleTimeSlotRepository) 
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
            _specialtyRepository = specialtyRepository;
            _countriesRepository = countriesRepository;
            _teleAppointmentRepository = teleAppointmentRepository;
            _teleTimeSlotRepository = teleTimeSlotRepository;
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
                                time.IsBooked = false;
                                var NewTime = await _timeSlotRepository.Createasync(_mapper.Map<TimeSlot>(time));
                            }
                            await _timeSlotRepository.SaveAsync();
                        }

                    }
                }
            }



            if (Doctordto.TeleAppointmentDtos is not null)
            {
                foreach (var appointment in Doctordto.TeleAppointmentDtos)
                {
                    var AppointmentExist = (await _teleAppointmentRepository.GetAllasync())
                                        .FirstOrDefault(s => s.DoctorId == appointment.DoctorId && ((int)s.Day) == ((int)appointment.Day));
                    if (AppointmentExist is null)
                    {
                        appointment.DoctorId = NewDoctor.Id;
                        var NewAppointment = await _teleAppointmentRepository.Createasync(_mapper.Map<TeleAppointments>(appointment));
                        await _teleAppointmentRepository.SaveAsync();
                        if (appointment.TimeSlots is not null)
                        {
                            foreach (var time in appointment.TimeSlots)
                            {
                                time.TeleAppointId = NewAppointment.Id;
                                time.IsBooked = false;
                                var NewTime = await _teleTimeSlotRepository.Createasync(_mapper.Map<TeleTimeSlot>(time));
                            }
                            await _teleTimeSlotRepository.SaveAsync();
                        }

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



            var Appointments = (await _appointmentRepository.GetAllasync()).Where(s => s.DoctorId == DoctorId).ToList();
            foreach(var appointment in Appointments)
            {
                appointment.IsDeleted = true;
            }
            await _appointmentRepository.SaveAsync();

            foreach(var appointment in Appointments)
            {
                var times = (await _timeSlotRepository.GetAllasync()).Where(s => s.AppointId == appointment.Id).ToList();
                foreach (var timeslot in times)
                {
                    timeslot.IsDeleted = true;
                }
            }
            await _timeSlotRepository.SaveAsync();







            var teleAppointments = (await _teleAppointmentRepository.GetAllasync()).Where(s => s.DoctorId == DoctorId).ToList();
            foreach (var appointment in teleAppointments)
            {
                appointment.IsDeleted = true;
            }
            await _teleAppointmentRepository.SaveAsync();

            foreach (var appointment in teleAppointments)
            {
                var times = (await _teleTimeSlotRepository.GetAllasync()).Where(s => s.AppointId == appointment.Id).ToList();
                foreach (var timeslot in times)
                {
                    timeslot.IsDeleted = true;
                }
            }
            await _teleTimeSlotRepository.SaveAsync();


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
                Id = s.Id,
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

            foreach (var doctor in doctorsdto) 
            {
                var Spec = await _specialtyRepository.GetOneasync(doctor.SpecId);
                doctor.SpecialtyDto = _mapper.Map<SpecialtyDto>(Spec);
            }


            foreach(var doctor in doctorsdto)
            {
                var Country = await _countriesRepository.GetOneasync(doctor.CountryId);
                doctor.CountryDtos = _mapper.Map<CountriesImagesDTos>(Country);
            }


            foreach(var doctor in doctorsdto)
            {
                var DrWorkingPlaces = await _workingPlaceRepository.GetWorkingPlaceByDoctorId(doctor.Id);
                doctor.WorkingPlaceDtos = _mapper.Map<List<WorkingPlaceDto>>(DrWorkingPlaces);
                foreach(var workingplace in doctor.WorkingPlaceDtos)
                {
                    var Images = (await _workingPlaceImagesRepository.GetAllasync())
                        .Where(s => s.IsDeleted == false && s.WorkingPlaceId == workingplace.Id).ToList();
                    workingplace.Images = _mapper.Map<List<WorkingPlaceImagesDto>>(Images);
                }
            }


            foreach(var doctor in doctorsdto)
            {
                var App = await _appointmentRepository.GetAppByDoctorId(doctor.Id);
                doctor.AppointmentDtos = _mapper.Map<List<AppointmentDto>>(App);
                foreach(var Appoint in doctor.AppointmentDtos)
                {
                    var Timeslot = (await _timeSlotRepository.GetAllasync()).Where(s => s.AppointId == Appoint.Id && s.IsDeleted == false);
                    Appoint.TimeSlots = _mapper.Map<List<TimeSlotDto>>(Timeslot);
                }
            }




            foreach (var doctor in doctorsdto)
            {
                var teleApp = (await _teleAppointmentRepository.GetAllasync()).Where(s => s.DoctorId == doctor.Id);
                doctor.TeleAppointmentDtos = _mapper.Map<List<TeleAppointmentDto>>(teleApp);
                foreach (var Appoint in doctor.TeleAppointmentDtos)
                {
                    var Timeslot = (await _teleTimeSlotRepository.GetAllasync()).Where(s => s.AppointId == Appoint.Id && s.IsDeleted == false);
                    Appoint.TimeSlots = _mapper.Map<List<TeleTimeSlotDto>>(Timeslot);
                }
            }


            return new ResultDataList<DoctorWithDetails>
            {
                Entities = doctorsdto,
                Count = doctors.Count()
            };


        }

        public async Task<ResultView<DoctorWithDetails>> GetOne(int DoctorId)
        {
            var Dr = await _doctorRepository.GetOneasync(DoctorId);
            if(Dr is null)
            {
                return new ResultView<DoctorWithDetails>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The Doctor Not Found"
                };
            }

            var DrDto = _mapper.Map<DoctorWithDetails>(Dr);

            var SupSpec = await _subSpecialtyRepository.GetSubSpecialtyByDoctorId(DoctorId);
            DrDto.SubSpecialtyDtos = _mapper.Map<List<SubSpecialtyDto>>(SupSpec);


            var DrSpec = await _specialtyRepository.GetOneasync(Dr.SpecId);
            DrDto.SpecialtyDto = _mapper.Map<SpecialtyDto>(DrSpec);

            var DrCountry = await _countriesRepository.GetOneasync(Dr.CountryId);
            DrDto.CountryDtos = _mapper.Map<CountriesImagesDTos>(DrCountry);

            var DrWorkingPlace = await _workingPlaceRepository.GetWorkingPlaceByDoctorId(DoctorId);
            DrDto.WorkingPlaceDtos = _mapper.Map<List<WorkingPlaceDto>>(DrWorkingPlace);


            foreach(var workingplace in DrDto.WorkingPlaceDtos)
            {
                var Images = (await _workingPlaceImagesRepository.GetAllasync())
                        .Where(s => s.IsDeleted == false && s.WorkingPlaceId == workingplace.Id).ToList();
                workingplace.Images = _mapper.Map<List<WorkingPlaceImagesDto>>(Images);
            }


            var Apps = await _appointmentRepository.GetAppByDoctorId(DoctorId);
            DrDto.AppointmentDtos = _mapper.Map<List<AppointmentDto>>(Apps);

            foreach(var App in DrDto.AppointmentDtos)
            {
                var Times = (await _timeSlotRepository.GetAllasync()).Where(s => s.AppointId == App.Id);
                App.TimeSlots = _mapper.Map<List<TimeSlotDto>>(Times);
            }



            var teleApps = (await _teleAppointmentRepository.GetAllasync()).Where(s => s.DoctorId == DoctorId);
            DrDto.TeleAppointmentDtos = _mapper.Map<List<TeleAppointmentDto>>(teleApps);

            foreach (var App in DrDto.TeleAppointmentDtos)
            {
                var Times = (await _teleTimeSlotRepository.GetAllasync()).Where(s => s.AppointId == App.Id);
                App.TimeSlots = _mapper.Map<List<TeleTimeSlotDto>>(Times);
            }


            return new ResultView<DoctorWithDetails>
            {
                Entity = DrDto,
                IsSuccess = true,
                Message = " The Doctor Found"
            };

        }

        public async Task<ResultView<DoctorDto>> Update(DoctorDto Doctordto)
        {
            //var Dr = (await _doctorRepository.GetAllasync()).FirstOrDefault(s => s.Id == Doctordto.Id);
            //if(Dr is null)
            //{
            //    return new ResultView<DoctorDto>
            //    {
            //        Entity = null,
            //        IsSuccess = false,
            //        Message = " The Doctor Not Found"
            //    };
            //}

            var UpdatedDoctor = await _doctorRepository.Updateasync(_mapper.Map<Doctor>(Doctordto));
            await _doctorRepository.SaveAsync();



            if (Doctordto.doctorSubSpecialtyDtos is not null)
            {
                var DrSubSpec = (await _doctorSubSpecialtyRepository.GetAllasync()).Where(s => s.DoctorId == UpdatedDoctor.Id && s.IsDeleted == false);
                foreach (var SubSpec in DrSubSpec)
                {
                    var DeletedSubSpec = await _doctorSubSpecialtyRepository.Deleteasync(SubSpec);
                }
                await _doctorSubSpecialtyRepository.SaveAsync();





                foreach (var DSubSpec in Doctordto.doctorSubSpecialtyDtos)
                {
                    var drSubSpecDto = new DoctorSubSpecialtyDto { DoctorId = UpdatedDoctor.Id, SubSpecId = DSubSpec };
                    var NewDrSubSpec = await _doctorSubSpecialtyRepository.Createasync(_mapper.Map<DoctorSubSpecialties>(drSubSpecDto));
                }
                await _doctorSubSpecialtyRepository.SaveAsync();
            }






            if (Doctordto.WorkingPlaceDtos is not null)
            {
                var DrWorkingPlace = (await _doctorWorkingPlaceRepository.GetAllasync()).Where(s => s.DoctorId == UpdatedDoctor.Id && s.IsDeleted == false);
                foreach (var drworkingplace in DrWorkingPlace)
                {
                    var Images = (await _workingPlaceImagesRepository.GetAllasync()).Where(s => s.WorkingPlaceId == drworkingplace.WorkingPlaceId);
                    foreach (var image in Images)
                    {
                        var DeletedImages = await _workingPlaceImagesRepository.Deleteasync(image);
                    }
                    await _workingPlaceImagesRepository.SaveAsync();
                    var deletedWorkingplace = await _doctorWorkingPlaceRepository.Deleteasync(drworkingplace);
                }
                await _doctorWorkingPlaceRepository.SaveAsync();








                foreach (var workingplace in Doctordto.WorkingPlaceDtos)
                {
                    var WorkingPlaceExist = (await _workingPlaceRepository.GetAllasync())
                                           .FirstOrDefault(s => s.Area == workingplace.Area && s.City == workingplace.City && s.Name == workingplace.Name);
                    if (WorkingPlaceExist is null)
                    {
                        var NewWorkingPlace = await _workingPlaceRepository.Createasync(_mapper.Map<WorkingPlace>(workingplace));
                        await _workingPlaceRepository.SaveAsync();
                        var DrWorkingPlaceDto = new DoctorWorkingPlaceDto { DoctorId = UpdatedDoctor.Id, WorkingPlaceId = NewWorkingPlace.Id };
                        var NewDrWorkingPlace = await _doctorWorkingPlaceRepository.Createasync(_mapper.Map<DoctorWorkingPlace>(DrWorkingPlaceDto));
                        if (Doctordto.WorkingPlaceImages is not null)
                        {
                            foreach (var image in Doctordto.WorkingPlaceImages)
                            {
                                var NewImageDto = new WorkingPlaceImagesDto { ImgPath = image, WorkingPlaceId = NewWorkingPlace.Id };
                                var NewImage = await _workingPlaceImagesRepository.Createasync(_mapper.Map<WorkingPlaceImages>(NewImageDto));
                            }
                            await _workingPlaceImagesRepository.SaveAsync();
                        }
                    }
                    else
                    {
                        var DrWorkingPlaceDto = new DoctorWorkingPlaceDto { DoctorId = UpdatedDoctor.Id, WorkingPlaceId = WorkingPlaceExist.Id };
                        var NewDrWorkingPlace = await _doctorWorkingPlaceRepository.Createasync(_mapper.Map<DoctorWorkingPlace>(DrWorkingPlaceDto));
                    }
                }
                await _doctorWorkingPlaceRepository.SaveAsync();
            }






            if (Doctordto.AppointmentDtos is not null)
            {
                var Apps = await _appointmentRepository.GetAppByDoctorId(UpdatedDoctor.Id);
                foreach (var appointment in Apps)
                {
                    var timeslot = (await _timeSlotRepository.GetAllasync()).Where(s => s.AppointId == appointment.Id);
                    foreach (var time in timeslot)
                    {
                        var DeletedTime = await _timeSlotRepository.Deleteasync(time);
                    }
                    await _timeSlotRepository.SaveAsync();
                    var DeletedApp = await _appointmentRepository.Deleteasync(appointment);
                }
                await _appointmentRepository.SaveAsync();








                foreach (var appointment in Doctordto.AppointmentDtos)
                {
                    var AppointmentExist = (await _appointmentRepository.GetAllasync())
                                        .FirstOrDefault(s => s.DoctorId == appointment.DoctorId && ((int)s.Day) == ((int)appointment.Day));
                    if (AppointmentExist is null)
                    {
                        appointment.DoctorId = UpdatedDoctor.Id;
                        var NewAppointment = await _appointmentRepository.Createasync(_mapper.Map<Appointment>(appointment));
                        await _appointmentRepository.SaveAsync();
                        if (appointment.TimeSlots is not null)
                        {
                            foreach (var time in appointment.TimeSlots)
                            {
                                time.AppointId = NewAppointment.Id;
                                time.IsBooked = false;
                                var NewTime = await _timeSlotRepository.Createasync(_mapper.Map<TimeSlot>(time));
                            }
                            await _timeSlotRepository.SaveAsync();
                        }

                    }
                }
            }







            if (Doctordto.TeleAppointmentDtos is not null)
            {
                var Apps = (await _teleAppointmentRepository.GetAllasync()).Where(s => s.DoctorId == UpdatedDoctor.Id);
                foreach (var appointment in Apps)
                {
                    var timeslot = (await _teleTimeSlotRepository.GetAllasync()).Where(s => s.AppointId == appointment.Id);
                    foreach (var time in timeslot)
                    {
                        var DeletedTime = await _teleTimeSlotRepository.Deleteasync(time); 
                    }
                    await _teleTimeSlotRepository.SaveAsync();
                    var DeletedApp = await _teleAppointmentRepository.Deleteasync(appointment);
                }
                await _teleAppointmentRepository.SaveAsync();








                foreach (var appointment in Doctordto.TeleAppointmentDtos)
                {
                    var AppointmentExist = (await _teleAppointmentRepository.GetAllasync())
                                        .FirstOrDefault(s => s.DoctorId == appointment.DoctorId && ((int)s.Day) == ((int)appointment.Day));
                    if (AppointmentExist is null)
                    {
                        appointment.DoctorId = UpdatedDoctor.Id;
                        var NewAppointment = await _teleAppointmentRepository.Createasync(_mapper.Map<TeleAppointments>(appointment));
                        await _teleAppointmentRepository.SaveAsync();
                        if (appointment.TimeSlots is not null)
                        {
                            foreach (var time in appointment.TimeSlots)
                            {
                                time.TeleAppointId = NewAppointment.Id;
                                time.IsBooked = false;
                                var NewTime = await _teleTimeSlotRepository.Createasync(_mapper.Map<TeleTimeSlot>(time));
                            }
                            await _teleTimeSlotRepository.SaveAsync();
                        }

                    }
                }
            }


            return new ResultView<DoctorDto>
            {
                Entity = _mapper.Map<DoctorDto>(UpdatedDoctor),
                IsSuccess = true,
                Message = " The Doctor Updated Successfully"
            };
        }
    }
}
