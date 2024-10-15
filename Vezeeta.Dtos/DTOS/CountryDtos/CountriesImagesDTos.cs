using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Dtos.DTOS.CountryDtos;

namespace Vezeeta.Infrastucture.CountriesRepos
{
    public class CountriesImagesDTos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string flagImage { get; set; }
        public List<CountryImagesDto> Images { get; set; }
    }
}
