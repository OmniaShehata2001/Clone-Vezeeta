using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Application.Contract.ReviewsRepositories
{
    public interface IDoctorReviewsRepository : IRepository<DoctorReviews , int>
    {
        Task<IQueryable<DoctorReviews>> GetReviewsByDrId(int DoctorId);
    }
}
