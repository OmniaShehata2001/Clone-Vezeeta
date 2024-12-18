﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Application.Contract.SpecialtyRepositories
{
    public interface ISubSpecialtyRepository : IRepository<SubSpecialty , int >
    {
        Task<IQueryable<SubSpecialty>> GetSubSpecialtyBySpecId(int specId);
        Task<IQueryable<SubSpecialty>> GetSubSpecialtyByDoctorId (int doctorId);
    }
}
