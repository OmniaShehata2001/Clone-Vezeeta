using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class SubServiceReview : BaseEntity
    {
        public string Comment { get; set; }
        public int Rating { get; set; }
        [ForeignKey("SubServices")]
        public int SubServiceId { get; set; }
        public SubServices SubServices { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
