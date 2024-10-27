using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.ServicesRepositories;
using Vezeeta.Context;
using Vezeeta.Infrastucture.Repository;
using Vezeeta.Models;

namespace Vezeeta.Infrastucture.ServicesRepos
{
    public class SubServicesTimesSlotRepository : Repository<SubServicesTimeSlot , int> , ISubServicesTimeSlotRepository
    {
        public SubServicesTimesSlotRepository(VezeetaContext vezeetaContext) : base(vezeetaContext) 
        {
            
        }
    }
}
