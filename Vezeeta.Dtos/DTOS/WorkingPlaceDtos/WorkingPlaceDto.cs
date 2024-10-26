using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Dtos.DTOS.WorkingPlaceDtos
{
    public class WorkingPlaceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public ICollection<WorkingPlaceImagesDto> Images { get; set; }
    }
}
