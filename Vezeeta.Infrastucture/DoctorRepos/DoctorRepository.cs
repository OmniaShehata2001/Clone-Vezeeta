using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.DoctorRepositories;
using Vezeeta.Context;
using Vezeeta.Infrastucture.Repository;
using Vezeeta.Models;

namespace Vezeeta.Infrastucture.DoctorRepos
{
    public class DoctorRepository : Repository<Doctor , int> , IDoctorRepository
    {
        public DoctorRepository(VezeetaContext vezeetaContext): base(vezeetaContext) 
        {
            
        }
    }
}
