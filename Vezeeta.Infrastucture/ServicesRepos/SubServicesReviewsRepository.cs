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
    public class SubServicesReviewsRepository : Repository<SubServiceReview, int> , ISubServicesReviewsRepository
    {
        public SubServicesReviewsRepository(VezeetaContext vezeetaContext) : base(vezeetaContext) 
        {
            
        }
    }
}
