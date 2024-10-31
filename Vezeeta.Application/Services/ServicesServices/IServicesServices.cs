using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.ServicesDtos;
using Vezeeta.Dtos.Result;

namespace Vezeeta.Application.Services.ServicesServices
{
    public interface IServicesServices
    {
        Task<ResultView<ServicesDto>> Create(ServicesDto servocesDto);
        Task<ResultView<ServicesDto>> Update(ServicesDto servocesDto);
        Task<ResultView<ServicesDto>> Delete(int id);
        Task<ResultView<ServicesDto>> GetOne(int id);
        Task<ResultDataList<ServicesDto>> GetAll(int pageNumber , int Items);
    }
}
