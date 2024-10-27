
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Dtos.DTOS.ServicesDtos
{
    public class SubServicesAppointmentDto
    {
        public int Id { get; set; }
        public Models.DayOfWeek Day { get; set; }
        public int SubServiceId { get; set; }
        public ICollection<SubServicesTimeSlotDto> SubServiceTimeSlots { get; set; }

    }
}
