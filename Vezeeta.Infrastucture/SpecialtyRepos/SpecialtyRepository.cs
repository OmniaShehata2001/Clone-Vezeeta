using System;
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
    public class SpecialtyRepository : Repository<Specialty , int> , ISpecialtyRepository
    {
        public SpecialtyRepository(VezeetaContext vezeetaContext): base(vezeetaContext) 
        {
            
        }
    }
}
