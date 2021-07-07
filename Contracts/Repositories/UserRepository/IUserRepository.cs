using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User> GetProfile(string id, bool trackChanges);


    }
}
