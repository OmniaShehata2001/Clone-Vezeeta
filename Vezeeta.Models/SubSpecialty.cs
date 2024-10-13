using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class SubSpecialty : BaseEntity
    {
        public string SubSpecName { get; set; }
        [ForeignKey("Specialty")]
        public int SpecId { get; set; }
        public Specialty Specialty { get; set; }
        public ICollection<DoctorSubSpecialties> DoctorSubSpecialties { get; set; }
    }
}
