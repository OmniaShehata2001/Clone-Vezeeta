using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.SpecialtyRepositories;
using Vezeeta.Dtos.DTOS.SpecialtyDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Models;

namespace Vezeeta.Application.Services.Specialty_Services
{
    public class SpecialtyServices : ISpecialtyServices
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IMapper _mapper;

        public SpecialtyServices(ISpecialtyRepository specialtyRepository, IMapper mapper)
        {
            _specialtyRepository = specialtyRepository;
            _mapper = mapper;
        }
        public async Task<ResultView<SpecialtyDto>> CreateAsync(SpecialtyDto specialtyDto)
        {
            var SpecialtyExist = (await _specialtyRepository.GetAllasync())
                                  .FirstOrDefault(s => s.SpecName == specialtyDto.SpecName && s.IsDeleted == false);
            if (SpecialtyExist is null)
            {
                var SpecialtyCreated = await _specialtyRepository.Createasync(_mapper.Map<Specialty>(specialtyDto));
                await _specialtyRepository.SaveAsync();
                return new ResultView<SpecialtyDto>
                {
                    Entity = _mapper.Map<SpecialtyDto>(SpecialtyCreated),
                    IsSuccess = true,
                    Message = "The Specialty Created Successfully"
                };
            }
            else
            {
                return new ResultView<SpecialtyDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The Specialty Already Exist"
                };
            }
        }

        public async Task<ResultView<SpecialtyDto>> DeleteAsync(int SpecialtyId)
        {
            var Spec = await _specialtyRepository.GetOneasync(SpecialtyId);
            Spec.IsDeleted = true;
            await _specialtyRepository.SaveAsync();
            return new ResultView<SpecialtyDto>
            {
                Entity = _mapper.Map<SpecialtyDto>(Spec),
                IsSuccess = true,
                Message = "The Specialty Deleted Successfully"
            };
        }

        public async Task<ResultDataList<SpecialtyDto>> GetAll(int PageNumber , int Items)
        {
            var Specialties = (await _specialtyRepository.GetAllasync()).Where(s => s.IsDeleted == false);
            var SpecialtiesDto = Specialties.Skip(Items * (PageNumber - 1)).Take(Items).Select(s => new SpecialtyDto
                              {
                                  Id = s.Id,
                                  SpecName = s.SpecName
                              });
            return new ResultDataList<SpecialtyDto>
            {
                Entities = SpecialtiesDto.ToList(),
                Count = Specialties.Count()
            };
        }

        public async Task<ResultView<SpecialtyDto>> GetOne(int SpecialtyId)
        {
            var Specialty = await _specialtyRepository.GetOneasync(SpecialtyId);
            if (Specialty is null)
            {
                return new ResultView<SpecialtyDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The Specialty Not Allowed Now"
                };
            }
            else
            {
                return new ResultView<SpecialtyDto>
                {
                    Entity = _mapper.Map<SpecialtyDto>(Specialty),
                    IsSuccess = true,
                    Message = " The Specialty Exist"
                };
            }
        }

        public async Task<ResultView<SpecialtyDto>> UpdateAsync(SpecialtyDto specialtyDto)
        {
            var UpdatedSpecialty = await _specialtyRepository.Updateasync(_mapper.Map<Specialty>(specialtyDto));
            await _specialtyRepository.SaveAsync();
            return new ResultView<SpecialtyDto>
            {
                Entity = _mapper.Map<SpecialtyDto>(UpdatedSpecialty),
                IsSuccess = true,
                Message = " The Specialty Updated Successfully"
            };
        }
    }
}
