using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Dtos.DTOS.BookingDtos
{
    public class DoctorBookingDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string UserId { get; set; }
        public int TimeSlotId { get; set; }
        public Status Status { get; set; }
    }
}
