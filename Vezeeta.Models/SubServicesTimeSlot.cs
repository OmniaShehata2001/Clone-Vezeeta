using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class SubServicesTimeSlot : BaseEntity
    {
        public TimeSpan Time { get; set; }
        [ForeignKey("SubServicesAppointments")]
        public int SubServiceAppId { get; set; }
        public SubServicesAppointments SubServicesAppointments { get; set; }
        public ICollection<SubServicesBooking> SubServicesBookings { get; set; }
    }
}
