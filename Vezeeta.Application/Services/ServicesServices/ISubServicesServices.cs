using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.ServicesDtos;
using Vezeeta.Dtos.Result;

namespace Vezeeta.Application.Services.ServicesServices
{
    public interface ISubServicesServices
    {
        Task<ResultView<SubServicesDto>> Create (SubServicesDto subServicesDto);
        Task<ResultView<SubServicesDto>> Update (SubServicesDto subServicesDto);
        Task<ResultView<SubServicesDto>> Delete (int id);
        Task<ResultView<SubServicesDto>> GetOne (int id);
        Task<ResultDataList<SubServicesDto>> GetAll (int PageNumber , int Items);
    }
}
