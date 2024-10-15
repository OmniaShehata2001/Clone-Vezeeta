using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.CountriesRepositries;
using Vezeeta.Context;
using Vezeeta.Infrastucture.Repository;
using Vezeeta.Models;

namespace Vezeeta.Infrastucture.CountriesRepos
{
    public class CountriesImagesRepository : Repository<CountriesImages , int> , ICountryImagesRepository
    {
        private readonly VezeetaContext _vezeetaContext;

        public CountriesImagesRepository(VezeetaContext vezeetaContext) : base(vezeetaContext) 
        {
            _vezeetaContext = vezeetaContext;
        }

        public Task<IQueryable<CountriesImages>> GetCountriesImages(int CountryId)
        {
            return Task.FromResult(_vezeetaContext.CountriesImages.Select(s => s).Where(s => s.CountryId == CountryId));
        }
    }
}
