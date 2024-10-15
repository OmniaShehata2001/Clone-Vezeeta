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
    public class CountriesRepositories : Repository<Countries,int> , ICountriesRepository
    {
        public CountriesRepositories(VezeetaContext vezeetaContext) : base(vezeetaContext) 
        {
            
        }
    }
}
