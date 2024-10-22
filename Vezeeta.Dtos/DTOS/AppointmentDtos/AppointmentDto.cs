using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Dtos.DTOS.AppointmentDtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public Models.DayOfWeek Day { get; set; }
        public int DoctorId { get; set; }
        public ICollection<TimeSlotDto> TimeSlots { get; set; }
    }
}
