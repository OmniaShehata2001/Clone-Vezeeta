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
        private readonly VezeetaContext _vezeetaContext;

        public WorkingPlaceRepository(VezeetaContext vezeetaContext) : base(vezeetaContext)
        {
            _vezeetaContext = vezeetaContext;
        }

        public Task<IQueryable<WorkingPlace>> GetWorkingPlaceByDoctorId(int DoctorId)
        {
            return Task.FromResult(from drworkingplace in _vezeetaContext.DoctorWorkingPlaces
                                   join workingplace in _vezeetaContext.WorkingPlaces on drworkingplace.WorkingPlaceId equals workingplace.Id
                                   where drworkingplace.DoctorId == DoctorId
                                   select workingplace);
        }
    }
}
