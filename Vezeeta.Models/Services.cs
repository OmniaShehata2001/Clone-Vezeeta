using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class Services : BaseEntity
    {
        public string ServiceName { get; set; }
        public string ServiceImage { get; set; }
        public ICollection<SubServices> SubServices { get; set; }
    }
}
