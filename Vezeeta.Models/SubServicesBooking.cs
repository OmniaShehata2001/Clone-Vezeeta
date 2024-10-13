using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class SubServicesBooking : BaseEntity
    {
        [ForeignKey("SubServices")]
        public int SubServicesId { get; set; }
        public SubServices SubServices { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("SubServiceTimeSlot")]
        public int SubServiceTimeSlotId { get; set; }
        public SubServicesTimeSlot SubServiceTimeSlot { get; set; }
        public Status Status { get; set; }
    }
}
