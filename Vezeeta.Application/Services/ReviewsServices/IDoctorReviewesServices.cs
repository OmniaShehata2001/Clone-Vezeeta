using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.ReviewsDtos;
using Vezeeta.Dtos.DTOS.SubSpecialtyDtos;
using Vezeeta.Dtos.Result;

namespace Vezeeta.Application.Services.ReviewsServices
{
    public interface IDoctorReviewesServices
    {
        Task<ResultView<DoctorReviewDto>> Create(DoctorReviewDto doctorReviewDto);
        Task<ResultView<DoctorReviewDto>> Update(DoctorReviewDto doctorReviewDto);
        Task<ResultView<DoctorReviewDto>> Delete(int doctorReviewId);
        Task<ResultView<DoctorReviewDto>> GetOne(int doctorReviewId);
        Task<ResultDataList<DoctorReviewDto>> GetAll(int pageNumber, int Items);
    }
}
