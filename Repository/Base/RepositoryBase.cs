using Contracts.Repositories;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DataContext DataContext;
        public RepositoryBase(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            trackChanges ?
                DataContext.Set<T>() :
                DataContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            trackChanges ?
                DataContext.Set<T>().Where(expression) :
                DataContext.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) => DataContext.Set<T>().Add(entity);

        public void Delete(T entity) => DataContext.Set<T>().Remove(entity);

        public void Update(T entity) => DataContext.Set<T>().Update(entity);
    }
}
