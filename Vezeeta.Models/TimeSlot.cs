using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class TimeSlot : BaseEntity
    {
        public TimeSpan Time { get; set; }
        [ForeignKey("Appointment")]
        public int AppointId { get; set; }
        public Appointment Appointment { get; set; }
        public ICollection<DoctorBooking> DoctorBooking { get; set; }

    }
}
