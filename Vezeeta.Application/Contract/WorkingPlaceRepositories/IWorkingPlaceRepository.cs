﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Application.Contract.WorkingPlaceRepositories
{
    public interface IWorkingPlaceRepository : IRepository<WorkingPlace , int>
    {
        Task<IQueryable<WorkingPlace>> GetWorkingPlaceByDoctorId(int DoctorId);
    }
}
