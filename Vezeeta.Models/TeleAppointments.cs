using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class TeleAppointments : BaseEntity
    {
        public DayOfWeek Day { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<TeleTimeSlot> TeleTimeSlots { get; set; }
    }
}
