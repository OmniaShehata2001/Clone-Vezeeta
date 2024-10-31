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
    public class ServicesServices : IServicesServices
    {
        private readonly IServicesRepository _servicesRepository;
        private readonly IMapper _mapper;

        public ServicesServices(IServicesRepository servicesRepository  , IMapper mapper)
        {
            _servicesRepository = servicesRepository;
            _mapper = mapper;
        }
        public async Task<ResultView<ServicesDto>> Create(ServicesDto servocesDto)
        {
            var serviceExist = (await _servicesRepository.GetAllasync()).FirstOrDefault(s => s.ServiceName == servocesDto.ServiceName);
            if (serviceExist is null)
            {
                return new ResultView<ServicesDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The Services Already Exist"
                };
            }
            var newService = await _servicesRepository.Createasync(_mapper.Map<Models.Services>(servocesDto));
            await _servicesRepository.SaveAsync();
            return new ResultView<ServicesDto>
            {
                Entity = _mapper.Map<ServicesDto>(newService),
                IsSuccess = true,
                Message = "The Services Created Successfully"
            };
        }

        public async Task<ResultView<ServicesDto>> Delete(int id)
        {
            var Service = await _servicesRepository.GetOneasync(id);
            if (Service is null)
            {
                return new ResultView<ServicesDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The Services Not Found"
                };
            }
            Service.IsDeleted  = true;
            await _servicesRepository.SaveAsync();
            return new ResultView<ServicesDto>
            {
                Entity = _mapper.Map<ServicesDto>(Service),
                IsSuccess = true,
                Message = " The Service Deleted Succesfully"
            };
        }

        public async Task<ResultDataList<ServicesDto>> GetAll(int pageNumber, int Items)
        {
            var services = (await _servicesRepository.GetAllasync()).Where(s => s.IsDeleted == false);
            var sevicesDto = services.Skip(Items * (pageNumber - 1)).Take(Items).Select(s => new ServicesDto
            {
                Id = s.Id,
                ServiceImage = s.ServiceImage,
                ServiceName = s.ServiceName
            });
            if (services is null)
            {
                return new ResultDataList<ServicesDto>
                {
                    Entities = null,
                    Count = 0
                };
            }
            return new ResultDataList<ServicesDto>
            {
                Entities = sevicesDto.ToList(),
                Count = services.Count()
            };
        }

        public async Task<ResultView<ServicesDto>> GetOne(int id)
        {
            var service = await _servicesRepository.GetOneasync(id);
            if(service is null)
            {
                return new ResultView<ServicesDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The Service Not Found"
                };
            }
            return new ResultView<ServicesDto>
            {
                Entity = _mapper.Map<ServicesDto>(service),
                IsSuccess = true,
                Message = " The Service Exist"
            };
        }

        public async Task<ResultView<ServicesDto>> Update(ServicesDto servocesDto)
        {
            var UpdatedService = await _servicesRepository.Updateasync(_mapper.Map<Models.Services>(servocesDto));
            await _servicesRepository.SaveAsync();
            return new ResultView<ServicesDto>
            {
                Entity = _mapper.Map<ServicesDto>(UpdatedService),
                IsSuccess = true,
                Message = " The Service Updated Successfully"
            };
        }
    }
}
