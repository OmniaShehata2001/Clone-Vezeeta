using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Dtos.DTOs.AuthDtos
{
    public class UserDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
