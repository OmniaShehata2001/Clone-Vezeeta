using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.ServicesDtos;
using Vezeeta.Dtos.Result;

namespace Vezeeta.Application.Services.ServicesServices
{
    public interface ISubServicesBookingServices
    {
        Task<ResultView<SubServicesBookingDto>> Create (SubServicesBookingDto subServicesBookingDto);
        Task<ResultView<SubServicesBookingDto>> Delete (int Id);
        Task<ResultView<SubServicesBookingDto>> GetOne(int Id);
        Task<ResultDataList<SubServicesBookingDto>> GetAll(int pagenumber , int items);
        Task<ResultDataList<SubServicesBookingDto>> GetByUserId(string userId);
    }
}
