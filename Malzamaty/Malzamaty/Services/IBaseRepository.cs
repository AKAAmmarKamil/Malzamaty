using Malzamaty.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty
{
    public interface IBaseRepository<T> 
    {
        Task<IEnumerable<T>> FindAll(int PageNumber,int count);
        Task<T> FindById(Guid k);
        Task<T> Create(T entity);
        Task<T> Delete(Guid k);

        void SaveChanges();
    }
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly TheContext RepositoryContext;
        private DbSet<T> table = null;
        protected BaseRepository(TheContext context)
        {
            RepositoryContext = context;
            table = RepositoryContext.Set<T>();
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
            var result=table.Find(id);
            if (result == null) return null;
            return result;
        }

   
    }

}
