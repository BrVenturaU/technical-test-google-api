using Contracts.Repositories;
using Contracts.Repositories.UserRepository;
using Data;
using Repository.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Base
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _dataContext;
        private IUserRepository _userRepository;

        public RepositoryManager(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dataContext);
                return _userRepository;
            }
        }
        public async Task Save() => await _dataContext.SaveChangesAsync();
    }
}
