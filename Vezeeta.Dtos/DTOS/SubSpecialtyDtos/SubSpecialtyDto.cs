using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Dtos.DTOS.SubSpecialtyDtos
{
    public class SubSpecialtyDto
    {
        public int Id { get; set; }
        public string SubSpecName { get; set; }
        public int SpecId { get; set; }
    }
}
