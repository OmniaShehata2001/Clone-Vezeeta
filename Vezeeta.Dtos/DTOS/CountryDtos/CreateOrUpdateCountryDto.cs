using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vezeeta.Dtos.DTOS.CountryDtos
{
    public class CreateOrUpdateCountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile FlagImage { get; set; }
        public List<IFormFile> CountryImages { get; set; }
    }
}
