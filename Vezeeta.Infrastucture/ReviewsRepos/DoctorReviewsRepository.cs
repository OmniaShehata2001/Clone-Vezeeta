using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.ReviewsRepositories;
using Vezeeta.Context;
using Vezeeta.Infrastucture.Repository;
using Vezeeta.Models;

namespace Vezeeta.Infrastucture.ReviewsRepos
{
    public class DoctorReviewsRepository : Repository<DoctorReviews,int> , IDoctorReviewsRepository
    {
        public DoctorReviewsRepository(VezeetaContext vezeetaContext) : base(vezeetaContext)
        {

        }
    }
}
