using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class SubServices : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal ServicePrice { get; set; }
        public int? DiscountValue { get; set; }
        public decimal? DiscountedPrice => ServicePrice - (ServicePrice * DiscountValue / 100);
        public string ServicePlaceName { get; set; }
        public string City { get; set; }
        public string ServicePlaceAddress { get; set; }
        public string ServicePlaceImage { get; set; }
        [ForeignKey("Services")]
        public int ServicesId { get; set; }
        public Services Services { get; set; }
        public ICollection<SubServiceImages> SubServiceImages { get; set; }
        public ICollection<SubServiceReview> SubServiceReviews { get; set; }
        public ICollection<SubServicesAppointments> SubServicesAppointments { get; set; }
        public ICollection<SubServicesBooking> SubServicesBooking { get; set; }
    }
}
