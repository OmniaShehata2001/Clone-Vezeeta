using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.SpecialtyRepositories;
using Vezeeta.Dtos.DTOS.SpecialtyDtos;
using Vezeeta.Dtos.DTOS.SubSpecialtyDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Models;

namespace Vezeeta.Application.Services.Specialty_Services
{
    public class SubSpecialtyServices : ISubSpecialtyServices
    {
        private readonly ISubSpecialtyRepository _subSpecialtyRepository;
        private readonly IMapper _mapper;

        public SubSpecialtyServices(ISubSpecialtyRepository subSpecialtyRepository, IMapper mapper)
        {
            _subSpecialtyRepository = subSpecialtyRepository;
            _mapper = mapper;
        }

        public async Task<ResultView<SubSpecialtyDto>> Create(SubSpecialtyDto subSpecialtyDto)
        {
            var SubSpecaltyExist = (await _subSpecialtyRepository.GetAllasync())
                                   .FirstOrDefault(s => s.SubSpecName == subSpecialtyDto.SubSpecName && s.IsDeleted == false);
            if (SubSpecaltyExist == null)
            {
                var CreatedSubSpecialty = await _subSpecialtyRepository.Createasync(_mapper.Map<SubSpecialty>(subSpecialtyDto));
                await _subSpecialtyRepository.SaveAsync();
                return new ResultView<SubSpecialtyDto>
                {
                    Entity = _mapper.Map<SubSpecialtyDto>(CreatedSubSpecialty),
                    IsSuccess = true,
                    Message = " The SubSpecialty Created Successfully"
                };
            }
            else
            {
                return new ResultView<SubSpecialtyDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The SubSpecialty Already Exist"
                };
            }
        }

        public async Task<ResultView<SubSpecialtyDto>> Delete(int SubSpecialtyId)
        {
            var subSpecialty = await _subSpecialtyRepository.GetOneasync(SubSpecialtyId);

            if(subSpecialty is null)
            {
                return new ResultView<SubSpecialtyDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The SubSpecialty Not Found"
                };
            }
            else
            {
                subSpecialty.IsDeleted = true;
                await _subSpecialtyRepository.SaveAsync();
                return new ResultView<SubSpecialtyDto>
                {
                    Entity = _mapper.Map<SubSpecialtyDto>(subSpecialty),
                    IsSuccess = true,
                    Message = " The SubSpecialty Deleted Successfully"
                };
            }
        }

        public async Task<ResultDataList<SubSpecialtyDto>> GetAll()
        {
            var SubSpecialties = await _subSpecialtyRepository.GetAllasync();
            if(SubSpecialties is null)
            {
                return new ResultDataList<SubSpecialtyDto>
                {
                    Entities = null,
                    Count = 0
                };
            }
            return new ResultDataList<SubSpecialtyDto>
            {
                Entities = _mapper.Map<List<SubSpecialtyDto>>(SubSpecialties),
                Count = SubSpecialties.Count()
            };
        }

        public async Task<ResultView<SubSpecialtyDto>> GetOne(int SubSpecialtyId)
        {
            var subSpecialty = await _subSpecialtyRepository.GetOneasync(SubSpecialtyId);
            if(subSpecialty is null)
            {
                return new ResultView<SubSpecialtyDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The SubSpecialty Not Found"
                };
            }
            return new ResultView<SubSpecialtyDto>
            {
                Entity = _mapper.Map<SubSpecialtyDto>(subSpecialty),
                IsSuccess = true,
                Message = " The SubSpecialty Found Successfully"
            };
        }

        public async Task<ResultView<SubSpecialtyDto>> Update(SubSpecialtyDto subSpecialtyDto)
        {
            var UpdatedSubSpec = await _subSpecialtyRepository.Updateasync(_mapper.Map<SubSpecialty>(subSpecialtyDto));
            await _subSpecialtyRepository.SaveAsync();
            return new ResultView<SubSpecialtyDto>
            {
                Entity = _mapper.Map<SubSpecialtyDto>(UpdatedSubSpec),
                IsSuccess = true,
                Message = " The SubSpecialty Updated Successfully"
            };
        }
    }
}
