using Contracts.Repositories;
using Data;
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

        public RepositoryManager(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Save() => _dataContext.SaveChanges();
    }
}
