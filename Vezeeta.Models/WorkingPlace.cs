using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class WorkingPlace : BaseEntity
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public ICollection<DoctorWorkingPlace> DoctorWorkingPlaces { get; set; }
        public ICollection<WorkingPlaceImages> WorkingPlaceImages { get; set;}
    }
}
