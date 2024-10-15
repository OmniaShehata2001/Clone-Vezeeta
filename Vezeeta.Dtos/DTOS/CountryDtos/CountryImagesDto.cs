using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Dtos.DTOS.CountryDtos
{
    public class CountryImagesDto
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public int CountryId { get; set; }
    }
}
