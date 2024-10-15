using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Application.Contract.CountriesRepositries
{
    public interface ICountryImagesRepository : IRepository<CountriesImages, int>
    {
        Task<IQueryable<CountriesImages>> GetCountriesImages(int CountryId);
    }
}
