using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class SubServicesAppointments : BaseEntity
    {
        public DayOfWeek Day { get; set; }
        [ForeignKey("SubServices")]
        public int SubServiceId { get; set; }
        public SubServices SubServices { get; set; }
        public ICollection<SubServicesTimeSlot> TimeSlots { get; set; }
    }
}
