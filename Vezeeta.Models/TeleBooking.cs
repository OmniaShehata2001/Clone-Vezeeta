using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class TeleBooking : BaseEntity
    {
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("TeleTimeSlot")]
        public int TeleTimeSlotId { get; set; }
        public TeleTimeSlot TeleTimeSlot { get; set; }
        public Status Status { get; set; }
    }
}
