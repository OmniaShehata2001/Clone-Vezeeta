using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Dtos.DTOS.AppointmentDtos
{
    public class TimeSlotDto
    {
        public int Id { get; set; }
        public long Time { get; set; }
        public int AppointId { get; set; }

    }
}
