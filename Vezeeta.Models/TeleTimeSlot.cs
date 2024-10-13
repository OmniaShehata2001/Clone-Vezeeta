using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class TeleTimeSlot : BaseEntity
    {
        public TimeSpan Time { get; set; }
        [ForeignKey("TeleAppointments")]
        public int AppointId { get; set; }
        public TeleAppointments TeleAppointments { get; set; }
        public ICollection<TeleBooking> TeleBooking { get; set; }
    }
}
