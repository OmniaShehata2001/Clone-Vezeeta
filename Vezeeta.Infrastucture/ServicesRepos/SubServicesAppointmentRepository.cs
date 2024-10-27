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
    public class SubServicesAppointmentRepository : Repository<SubServicesAppointments , int> , ISubServicesAppointmentRepository
    {
        public SubServicesAppointmentRepository(VezeetaContext vezeetaContext) : base(vezeetaContext) 
        {
            
        }
    }
}
