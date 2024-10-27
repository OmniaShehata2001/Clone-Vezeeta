using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.BookingRepositories;
using Vezeeta.Context;
using Vezeeta.Infrastucture.Repository;
using Vezeeta.Models;

namespace Vezeeta.Infrastucture.BookingRepos
{
    public class TeleBookingRepository : Repository<TeleBooking,int> , ITeleBookingRepository
    {
        public TeleBookingRepository(VezeetaContext vezeetaContext): base(vezeetaContext) 
        {
            
        }
    }
}
