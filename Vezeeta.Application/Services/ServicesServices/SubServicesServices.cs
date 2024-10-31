using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.ServicesRepositories;
using Vezeeta.Dtos.DTOS.ServicesDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Models;

namespace Vezeeta.Application.Services.ServicesServices
{
    public class SubServicesServices : ISubServicesServices
    {
        private readonly ISubServicesRepository _subServicesRepository;
        private readonly IMapper _mapper;
        private readonly ISubServicesAppointmentRepository _subServicesAppointmentRepository;
        private readonly ISubServicesImagesRepository _subServicesImagesRepository;
        private readonly ISubServicesTimeSlotRepository _subServicesTimeSlotRepository;

        public SubServicesServices(ISubServicesRepository subServicesRepository , IMapper mapper , ISubServicesAppointmentRepository subServicesAppointmentRepository,
            ISubServicesImagesRepository subServicesImagesRepository , ISubServicesTimeSlotRepository subServicesTimeSlotRepository)
        {
            _subServicesRepository = subServicesRepository;
            _mapper = mapper;
            _subServicesAppointmentRepository = subServicesAppointmentRepository;
            _subServicesImagesRepository = subServicesImagesRepository;
            _subServicesTimeSlotRepository = subServicesTimeSlotRepository;
        }
        public async Task<ResultView<SubServicesDto>> Create(SubServicesDto subServicesDto)
        {
            var SubServ = (await _subServicesRepository.GetAllasync())
                .FirstOrDefault(s => s.Name == subServicesDto.Name && s.ServicePlaceAddress == subServicesDto.ServicePlaceAddress 
                && s.ServicePlaceName == subServicesDto.ServicePlaceAddress); 

            if(SubServ is null)
            {
                return new ResultView<SubServicesDto>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The SubServices Added Before"
                };
            }

            var NewSubServ = await _subServicesRepository.Createasync(_mapper.Map<SubServices>(subServicesDto));
            await _subServicesRepository.SaveAsync();

            if(subServicesDto.SubServicesImages != null)
            {
                foreach(var image in subServicesDto.SubServicesImages)
                {
                        var imageModel = _mapper.Map<SubServiceImages>(image);
                        imageModel.SubServiceId = NewSubServ.Id;
                        var NewImage = await _subServicesImagesRepository.Createasync(imageModel);
                }
                await _subServicesImagesRepository.SaveAsync();
            }


            if(subServicesDto.SubServicesAppointments is not null)
            {
                foreach(var App in subServicesDto.SubServicesAppointments)
                {
                    var AppExist = (await _subServicesAppointmentRepository.GetAllasync()).FirstOrDefault(s => s.SubServiceId == NewSubServ.Id && s.Day == App.Day);
                    if(AppExist is null)
                    {
                        var AppModel = _mapper.Map<SubServicesAppointments>(App);
                        AppModel.SubServiceId = NewSubServ.Id;
                        var NewApp = await _subServicesAppointmentRepository.Createasync(AppModel);
                        await _subServicesAppointmentRepository.SaveAsync();
                        if(App.SubServiceTimeSlots is not null)
                        {
                            foreach(var time in App.SubServiceTimeSlots)
                            {
                                var timeModel = _mapper.Map<SubServicesTimeSlot>(time);
                                timeModel.SubServiceAppId = NewApp.Id;
                                var Newtime = await _subServicesTimeSlotRepository.Createasync(timeModel);
                            }
                            await _subServicesTimeSlotRepository.SaveAsync();
                        }
                    }
                }
            }

            return new ResultView<SubServicesDto>
            {
                Entity = _mapper.Map<SubServicesDto>(NewSubServ),
                IsSuccess = true,
                Message = " The Time Created Successfully"
            };
        }

        public async Task<ResultView<SubServicesDto>> Delete(int id)
        {
            var SubService = await _subServicesRepository.GetOneasync(id);
            if (SubService is null)
            {
                return new ResultView<SubServicesDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The SubService NotFound"
                };
            }
            SubService.IsDeleted = true;
            await _subServicesRepository.SaveAsync();

            var images = (await _subServicesImagesRepository.GetAllasync()).Where(s => s.SubServiceId == id);
            foreach (var image in images)
            {
                var Deletedimage = await _subServicesImagesRepository.Deleteasync(image);
            }
            await _subServicesImagesRepository.SaveAsync();


            var App = (await _subServicesAppointmentRepository.GetAllasync()).Where(s => s.SubServiceId == id);
            foreach (var app in App) 
            {
                var times = (await _subServicesTimeSlotRepository.GetAllasync()).Where(s => s.SubServiceAppId == app.Id);
                foreach (var time in times)
                {
                    var Deletedtime = await _subServicesTimeSlotRepository.Deleteasync(time);
                }
                await _subServicesTimeSlotRepository.SaveAsync();
                var DeletedApp = await _subServicesAppointmentRepository.Deleteasync(app);
            }
            await _subServicesAppointmentRepository.SaveAsync();


            return new ResultView<SubServicesDto>
            {
                Entity = _mapper.Map<SubServicesDto>(SubService)
                ,
                IsSuccess = true,
                Message = "The SubService , images and Appointments Deleted Successfully"
            };
        }

        public async Task<ResultDataList<SubServicesDto>> GetAll(int PageNumber, int Items)
        {
            var SubServices = (await _subServicesRepository.GetAllasync()).Where(s => s.IsDeleted == false);
            if (SubServices is null)
            {
                return new ResultDataList<SubServicesDto>
                {
                    Entities = null,
                    Count = 0
                };
            }
            var SubServiceDto = SubServices.Skip(Items * (PageNumber - 1)).Take(Items).Select(s => new SubServicesDto
            {
                City = s.City,
                Description = s.Description,
                ServicePlaceAddress = s.ServicePlaceAddress,
                ServicePlaceImage = s.ServicePlaceImage,
                ServicePlaceName = s.ServicePlaceName,
                ServicePrice = s.ServicePrice,
                ServicesId = s.ServicesId,
                Name = s.Name,
                Id = s.Id,
                DiscountValue = s.DiscountValue
            }).ToList();

            foreach (var subServ in SubServiceDto)
            {
                var Apps = (await _subServicesAppointmentRepository.GetAllasync()).Where(s => s.IsDeleted == false && s.SubServiceId == subServ.Id);
                if(Apps is not null)
                {
                    subServ.SubServicesAppointments = _mapper.Map<List<SubServicesAppointmentDto>>(Apps);
                    foreach (var App in subServ.SubServicesAppointments)
                    {
                        var times = (await _subServicesTimeSlotRepository.GetAllasync()).Where(s => s.IsDeleted == false && s.SubServiceAppId == App.Id);
                        App.SubServiceTimeSlots = _mapper.Map<List<SubServicesTimeSlotDto>>(times);
                    }
                }

                var images = (await _subServicesImagesRepository.GetAllasync()).Where(s => s.IsDeleted == false && s.SubServiceId == subServ.Id);
                subServ.SubServicesImages = _mapper.Map<List<SubServicesImagesDto>>(images);
            }


            return new ResultDataList<SubServicesDto>
            {
                Entities = SubServiceDto,
                Count = SubServices.Count()
            };
            
        }

        public async Task<ResultView<SubServicesDto>> GetOne(int id)
        {
            var subserv = await _subServicesRepository.GetOneasync(id);
            if(subserv is null)
            {
                return new ResultView<SubServicesDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The SubService Not Found"
                };
            }

            var subservDto = _mapper.Map<SubServicesDto>(subserv);

            var images = (await _subServicesImagesRepository.GetAllasync()).Where(s => s.IsDeleted == false && s.SubServiceId == id);
            subservDto.SubServicesImages = _mapper.Map<IList<SubServicesImagesDto>>(images);

            var Apps = (await _subServicesAppointmentRepository.GetAllasync()).Where(s => s.IsDeleted == false && s.SubServiceId == id);
            var appDto = _mapper.Map<List<SubServicesAppointmentDto>>(Apps);
            foreach(var app in appDto)
            {
                var times = (await _subServicesTimeSlotRepository.GetAllasync()).Where(s => s.IsDeleted == false && s.SubServiceAppId == app.Id);
                app.SubServiceTimeSlots = _mapper.Map<List<SubServicesTimeSlotDto>>(times);
            }

            return new ResultView<SubServicesDto>
            {
                Entity = subservDto,
                IsSuccess = true,
                Message = "The subService Exist"
            };

        }

        public async Task<ResultView<SubServicesDto>> Update(SubServicesDto subServicesDto)
        {
            var UpdatedSubServ = await _subServicesRepository.Updateasync(_mapper.Map<SubServices>(subServicesDto));
            await _subServicesRepository.SaveAsync();

            if(subServicesDto.SubServicesImages != null)
            {
                var images = (await _subServicesImagesRepository.GetAllasync()).Where(s => s.SubServiceId == UpdatedSubServ.Id);
                foreach(var image in images)
                {
                    var Deletedimages = await _subServicesImagesRepository.Deleteasync(image);
                }
                await _subServicesImagesRepository.SaveAsync();

                foreach (var image in subServicesDto.SubServicesImages)
                {
                    var imageModel = _mapper.Map<SubServiceImages>(image);
                    imageModel.SubServiceId = UpdatedSubServ.Id;
                    var NewImage = await _subServicesImagesRepository.Createasync(imageModel);
                }
                await _subServicesImagesRepository.SaveAsync();
            }

            if(subServicesDto.SubServicesAppointments != null)
            {
                var Apps = (await _subServicesAppointmentRepository.GetAllasync()).Where(s => s.SubServiceId == UpdatedSubServ.Id);
                foreach( var appointment in Apps)
                {
                    var times = (await _subServicesTimeSlotRepository.GetAllasync()).Where(s => s.SubServiceAppId == appointment.Id);
                    foreach( var timeslot in times)
                    {
                        var Deletedtime = await _subServicesTimeSlotRepository.Deleteasync(timeslot);
                    }
                    await _subServicesTimeSlotRepository.SaveAsync();
                    var DeletedApp = await _subServicesAppointmentRepository.Deleteasync(appointment);
                }
                await _subServicesAppointmentRepository.SaveAsync();


                foreach (var App in subServicesDto.SubServicesAppointments)
                {
                    var AppExist = (await _subServicesAppointmentRepository.GetAllasync()).FirstOrDefault(s => s.SubServiceId == UpdatedSubServ.Id && s.Day == App.Day);
                    if (AppExist is null)
                    {
                        var AppModel = _mapper.Map<SubServicesAppointments>(App);
                        AppModel.SubServiceId = UpdatedSubServ.Id;
                        var NewApp = await _subServicesAppointmentRepository.Createasync(AppModel);
                        await _subServicesAppointmentRepository.SaveAsync();
                        if (App.SubServiceTimeSlots is not null)
                        {
                            foreach (var time in App.SubServiceTimeSlots)
                            {
                                var timeModel = _mapper.Map<SubServicesTimeSlot>(time);
                                timeModel.SubServiceAppId = NewApp.Id;
                                var Newtime = await _subServicesTimeSlotRepository.Createasync(timeModel);
                            }
                            await _subServicesTimeSlotRepository.SaveAsync();
                        }
                    }
                }
            }

            return new ResultView<SubServicesDto>
            {
                Entity = subServicesDto,
                IsSuccess = true,
                Message = "The SubService Updated Successfully"
            };
        }
    }
}
