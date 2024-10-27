using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Dtos.DTOS.ServicesDtos
{
    public class SubServicesBookingDto
    {
        public int Id { get; set; }
        public int SubServicesId { get; set; }
        public string UserId { get; set; }
        public int SubServiceTimeSlotId { get; set; }
        public Status Status { get; set; }
    }
}
