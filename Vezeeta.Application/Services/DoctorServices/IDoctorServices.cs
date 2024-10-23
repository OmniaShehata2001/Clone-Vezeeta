using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.DoctorDtos;
using Vezeeta.Dtos.Result;

namespace Vezeeta.Application.Services.DoctorServices
{
    public interface IDoctorServices
    {
        Task<ResultView<DoctorDto>> Create(DoctorDto Doctordto);
        Task<ResultView<DoctorDto>> Update(DoctorDto Doctordto);
        Task<ResultView<DoctorDto>> Delete(int DoctorId);
        Task<ResultView<DoctorDto>> GetOne(int DoctorId);
        Task<ResultDataList<DoctorDto>> GetAll(int PageNumber, int Items);
    }
}
