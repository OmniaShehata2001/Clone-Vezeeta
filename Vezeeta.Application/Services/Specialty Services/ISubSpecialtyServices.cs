using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.SubSpecialtyDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Models;

namespace Vezeeta.Application.Services.Specialty_Services
{
    public interface ISubSpecialtyServices
    {
        Task<ResultView<SubSpecialtyDto>> Create(SubSpecialtyDto subSpecialtyDto);
        Task<ResultView<SubSpecialtyDto>> Update(SubSpecialtyDto subSpecialtyDto);
        Task<ResultView<SubSpecialtyDto>> Delete(int SubSpecialtyId);
        Task<ResultView<SubSpecialtyDto>> GetOne(int SubSpecialtyId);
        Task<ResultDataList<SubSpecialtyDto>> GetAll();
        Task<ResultDataList<SubSpecialtyDto>> GetSubSpecialtyBySpecId(int specId);
    }
}
