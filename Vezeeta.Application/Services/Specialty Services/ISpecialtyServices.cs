using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.CountryDtos;
using Vezeeta.Dtos.DTOS.SpecialtyDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Infrastucture.CountriesRepos;

namespace Vezeeta.Application.Services.Specialty_Services
{
    public interface ISpecialtyServices
    {
        Task<ResultView<SpecialtyDto>> CreateAsync(SpecialtyDto specialtyDto);
        Task<ResultView<SpecialtyDto>> UpdateAsync(SpecialtyDto specialtyDto);
        Task<ResultView<SpecialtyDto>> DeleteAsync(int SpecialtyId);
        Task<ResultView<SpecialtyDto>> GetOne(int SpecialtyId);
        Task<ResultDataList<SpecialtyDto>> GetAll(int PageNumber, int Items);
    }
}
