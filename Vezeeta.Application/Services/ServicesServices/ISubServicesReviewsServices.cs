using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.ServicesDtos;
using Vezeeta.Dtos.Result;

namespace Vezeeta.Application.Services.ServicesServices
{
    public interface ISubServicesReviewsServices
    {
        Task<ResultView<SubServicesReviewsDto>> Create(SubServicesReviewsDto subServicesReviews);
        Task<ResultView<SubServicesReviewsDto>> Update(SubServicesReviewsDto subServicesReviews);
        Task<ResultView<SubServicesReviewsDto>> Delete(int id);
        Task<ResultView<SubServicesReviewsDto>> GetOne(int id);
        Task<ResultDataList<SubServicesReviewsDto>> GetAll(int pagenumber , int Items);
        Task<ResultDataList<SubServicesReviewsDto>> GetReviewBysubServiceId (int subServiceId, int pageNumber, int Items);
    }
}
