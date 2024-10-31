using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.UserRepository;
using Vezeeta.Context;
using Vezeeta.Infrastucture.Repository;
using Vezeeta.Models;

namespace Vezeeta.Infrastucture.AccountRepositories
{
    public class AccountRepository : Repository<ApplicationUser,string> , IUserRepository
    {
        private readonly VezeetaContext _vezeetaContext;

        public AccountRepository(VezeetaContext vezeetaContext):base(vezeetaContext)
        {
            _vezeetaContext = vezeetaContext;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string UserEmail)
        {
            return await _vezeetaContext.Users.FirstOrDefaultAsync(u => u.Email == UserEmail);
        }
    }
}
