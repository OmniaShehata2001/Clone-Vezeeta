using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public class CountriesImages : BaseEntity
    {
        public string ImgPath { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Countries Country { get; set; }
    }
}
