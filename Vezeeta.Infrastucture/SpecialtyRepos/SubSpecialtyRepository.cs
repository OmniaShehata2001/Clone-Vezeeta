﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.SpecialtyRepositories;
using Vezeeta.Context;
using Vezeeta.Infrastucture.Repository;
using Vezeeta.Models;

namespace Vezeeta.Infrastucture.SpecialtyRepos
{
    public class SubSpecialtyRepository : Repository<SubSpecialty , int> , ISubSpecialtyRepository
    {
        private readonly VezeetaContext _vezeetaContext;

        public SubSpecialtyRepository(VezeetaContext vezeetaContext) : base(vezeetaContext) 
        {
            _vezeetaContext = vezeetaContext;
        }

        public Task<IQueryable<SubSpecialty>> GetSubSpecialtyByDoctorId(int doctorId)
        {
            return Task.FromResult(from doctorSubSpecialty in _vezeetaContext.DoctorSubSpecialties
                                   join subspecialty in _vezeetaContext.SubSpecialty on doctorSubSpecialty.SubSpecId equals subspecialty.Id
                                   where doctorSubSpecialty.DoctorId == doctorId
                                   select subspecialty);
        }

        public Task<IQueryable<SubSpecialty>> GetSubSpecialtyBySpecId (int specId)
        {
            return Task.FromResult(_vezeetaContext.SubSpecialty.Where(s => s.SpecId ==  specId));
        }
    }
}
