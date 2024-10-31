using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Application.Contract.UserRepository
{
    public interface IUserRepository : IRepository<ApplicationUser, string>
    {
        Task<ApplicationUser> GetByEmailAsync(string UserEmail);
    }
}
