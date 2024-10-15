using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.CountryDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Infrastucture.CountriesRepos;

namespace Vezeeta.Application.Services.Countries_Services
{
    public interface ICountriesServices
    {
        Task<ResultView<CreateOrUpdateCountryDto>> CreateAsync (CreateOrUpdateCountryDto Countrydto);
        Task<ResultView<CountriesImagesDTos>> UpdateAsync (CreateOrUpdateCountryDto Countrydto);
        Task<ResultView<CreateOrUpdateCountryDto>> DeleteAsync (int CountryId);
        Task<ResultView<CountriesImagesDTos>> GetOne (int CountryId);
        Task<ResultDataList<CountriesImagesDTos>> GetAll();

    }
}
