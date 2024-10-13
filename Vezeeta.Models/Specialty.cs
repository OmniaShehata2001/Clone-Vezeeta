using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class Specialty : BaseEntity
    {
        public string SpecName { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<SubSpecialty> SubSpecialties { get; set; } 
    }
}
