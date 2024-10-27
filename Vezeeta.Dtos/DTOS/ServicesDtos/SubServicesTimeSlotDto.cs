using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Dtos.DTOS.ServicesDtos
{
    public class SubServicesTimeSlotDto
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public int SubServiceAppId { get; set; }

    }
}
