using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.BookingDtos;
using Vezeeta.Dtos.Result;

namespace Vezeeta.Application.Services.BookingServices
{
    public interface ITeleBookingServices
    {
        Task<ResultView<TeleBookingDto>> Create(TeleBookingDto teleBookingDto);
        Task<ResultView<TeleBookingDto>> Delete(int Id);
        Task<ResultView<TeleBookingDto>> GetOne(int Id);
        Task<ResultDataList<TeleBookingDto>> GetAll(int PageNumber, int Items);
    }
}
