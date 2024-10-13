using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class DoctorSubSpecialties : BaseEntity
    {
        [ForeignKey("SubSpecialty")]
        public int SubSpecId { get; set; }
        public SubSpecialty SubSpecialty { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
