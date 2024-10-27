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
    public class SubServicesBookingRepository : Repository<SubServicesBooking , int>, ISubServicesBookingRepository
    {
        public SubServicesBookingRepository(VezeetaContext vezeetaContext) : base(vezeetaContext)
        {
            
        }
    }
}
