using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class Countries : BaseEntity
    {
        public string Name { get; set; }
        public string FlagImage { get; set; }
        public ICollection<CountriesImages> CountriesImages { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
