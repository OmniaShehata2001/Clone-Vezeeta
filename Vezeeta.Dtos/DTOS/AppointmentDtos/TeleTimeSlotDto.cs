using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Dtos.DTOS.AppointmentDtos
{
    public class TeleTimeSlotDto
    {
        public int Id { get; set; }
        public bool IsBooked { get; set; }
        public TimeSpan Time { get; set; }
        public int TeleAppointId { get; set; }
    }
}
