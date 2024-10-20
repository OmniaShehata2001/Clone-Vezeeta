using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Dtos.DTOS.AppointmentDtos
{
    public class TimeSlotDto
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public int AppointId { get; set; }

    }
}
