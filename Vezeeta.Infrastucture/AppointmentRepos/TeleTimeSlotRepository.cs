using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.AppointmentRepositories;
using Vezeeta.Context;
using Vezeeta.Infrastucture.Repository;
using Vezeeta.Models;

namespace Vezeeta.Infrastucture.AppointmentRepos
{
    public class TeleTimeSlotRepository : Repository<TeleTimeSlot,int> , ITeleTimeSlotRepository
    {
        public TeleTimeSlotRepository(VezeetaContext vezeetaContext) : base(vezeetaContext) 
        {
            
        }
    }
}
