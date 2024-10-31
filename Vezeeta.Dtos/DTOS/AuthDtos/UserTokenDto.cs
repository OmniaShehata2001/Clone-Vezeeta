using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Dtos.DTOs.AuthDtos
{
    public class UserTokenDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string token { get; set; }
    }
}
