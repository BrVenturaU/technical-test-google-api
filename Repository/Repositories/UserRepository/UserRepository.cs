using Contracts.Repositories.UserRepository;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.UserRepository
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext context): base(context)
        {

        }

        public async Task<User> GetProfile(string id, bool trackChanges) =>
            await FindByCondition(u => u.Id == id, trackChanges).SingleOrDefaultAsync();

        public async Task<(bool existsUserName, bool isActualUserName)> ExistsUserName(string id, string userName, bool trackChanges) {
            var user = await FindByCondition(u => u.UserName == userName, trackChanges).FirstOrDefaultAsync();
            return (user != null, user?.Id == id);
        }

        public void UpdateUser(User user) => Update(user);
    }
}
