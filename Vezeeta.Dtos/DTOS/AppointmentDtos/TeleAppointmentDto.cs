using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Dtos.DTOS.AppointmentDtos
{
    public class TeleAppointmentDto
    {
        public int Id { get; set; }
        public Models.DayOfWeek Day { get; set; }
        public int DoctorId { get; set; }
        public ICollection<TeleTimeSlotDto> TimeSlots { get; set; }
    }
}
