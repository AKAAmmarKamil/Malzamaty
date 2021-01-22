using Malzamaty.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Malzamaty.Repositories
{
    public interface IBaseRepository<T> where T:class
    {
        Task<IEnumerable<T>> FindAll(int PageNumber,int count);
        Task<T> FindById(Guid k);
        Task<T> Create(T entity);
        Task<T> Delete(Guid k);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext RepositoryContext;

        public BaseRepository(DbContext context)
        {
            RepositoryContext = context;
        }
        public async Task<IEnumerable<T>> FindAll(int PageNumber, int count)
        {
           return await RepositoryContext.Set<T>().Skip((PageNumber -1) * count).Take(count).ToListAsync();
        }

        public async Task<T> Create(T t)
        {
            await RepositoryContext.Set<T>().AddAsync(t);
            await RepositoryContext.SaveChangesAsync();
            return t;
        }
        public async Task<T> Delete(Guid id)
        {
            var result = await FindById(id);
            if (result == null) return null;
            RepositoryContext.Remove(result);
            await RepositoryContext.SaveChangesAsync();
            return result;
        }

        public void SaveChanges()
        {
            RepositoryContext.SaveChanges();
        }

        public async Task<T> FindById(Guid id)
        {
            var result = RepositoryContext.Set<T>().Find(id);
            if (result == null) return null;
            return result;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) =>RepositoryContext.Set<T>().Where(predicate);
           
    }

}
