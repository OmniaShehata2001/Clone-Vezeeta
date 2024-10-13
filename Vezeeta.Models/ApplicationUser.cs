using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime BithDay { get; set; }
        public Gender Gender { get; set; }
        public ICollection<DoctorReviews> DoctorReviews { get; set; }
        public ICollection<SubServiceReview> SubServiceReviews { get; set;}
        public ICollection<Payment> payments { get; set; }
        public ICollection<DoctorBooking> DoctorBooking { get; set; }
        public ICollection<TeleBooking>? TeleBookings { get; set; }
        public ICollection<SubServicesBooking> SubServicesBooking { get; set; }

    }
}
