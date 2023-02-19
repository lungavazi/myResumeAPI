using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myResumeAPI.Contracts;
using myResumeAPI.Models;
using System.Linq.Expressions;

namespace myResumeAPI.Repositories
{
    public class GenericRepository<T>
        : IRepository<T> where T : class
    {
        protected ResumeAPIContext context;

        public GenericRepository(ResumeAPIContext context)
        {
            this.context = context;
        }

        public T Add(T entity)
        {
            return context.Add(entity).Entity;
        }

        public T Delete(T entity)
        {
            return context.Set<T>().Remove(entity).Entity;  
        }

        public async Task<T> FindAsync(long id)
        {
            var results =  await context.FindAsync<T>(id);
            return results;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .ToListAsync();
        }

        public T Update(T entity)
        {
            return context.Update(entity).Entity;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();  
        }

    }
        
}
