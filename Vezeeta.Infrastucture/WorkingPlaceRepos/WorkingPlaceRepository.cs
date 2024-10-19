using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.WorkingPlaceRepositories;
using Vezeeta.Context;
using Vezeeta.Infrastucture.Repository;
using Vezeeta.Models;

namespace Vezeeta.Infrastucture.WorkingPlaceRepos
{
    public class WorkingPlaceRepository : Repository<WorkingPlace , int> , IWorkingPlaceRepository
    {
        public WorkingPlaceRepository(VezeetaContext vezeetaContext) : base(vezeetaContext)
        {

        }
    }
}
