using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Dtos.DTOS.BookingDtos
{
    public class TeleBookingDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string UserId { get; set; }
        public int TeleTimeSlotId { get; set; }
        public Status Status { get; set; }

    }
}
