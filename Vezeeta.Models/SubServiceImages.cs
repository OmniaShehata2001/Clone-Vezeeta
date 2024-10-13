using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class SubServiceImages : BaseEntity
    {
        public string ImagePath { get; set; }
        [ForeignKey("SubServices")]
        public int SubServiceId { get; set; }
        public SubServices SubServices { get; set; }
    }
}
