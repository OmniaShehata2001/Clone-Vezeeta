﻿using System;
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
    public class SevicesRepository : Repository<Services,int> , IServicesRepository
    {
        public SevicesRepository(VezeetaContext vezeetaContext) : base(vezeetaContext) 
        {
            
        }
    }
}