using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Dtos.DTOS.ServicesDtos
{
    public class SubServicesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal ServicePrice { get; set; }
        public int? DiscountValue { get; set; }
        public decimal? DiscountedPrice => ServicePrice - (ServicePrice * DiscountValue / 100);
        public string ServicePlaceName { get; set; }
        public string City { get; set; }
        public string ServicePlaceAddress { get; set; }
        public string ServicePlaceImage { get; set; }
        public int ServicesId { get; set; }
        public ICollection<SubServicesImagesDto> SubServiceImages { get; set; }
        public ICollection<SubServicesAppointmentDto> SubServicesAppointments { get; set; }
    }
}
