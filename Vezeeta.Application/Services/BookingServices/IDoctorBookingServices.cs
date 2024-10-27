using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.BookingDtos;
using Vezeeta.Dtos.Result;

namespace Vezeeta.Application.Services.BookingServices
{
    public interface IDoctorBookingServices
    {
        Task<ResultView<DoctorBookingDto>> Create (DoctorBookingDto doctorBookingDto);
        Task<ResultView<DoctorBookingDto>> Delete (int Id);
        Task<ResultView<DoctorBookingDto>> GetOne(int Id);
        Task<ResultDataList<DoctorBookingDto>> GetAll(int pageNumber, int Items);
    }
}
