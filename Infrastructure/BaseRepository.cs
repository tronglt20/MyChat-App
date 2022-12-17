﻿using Domain.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public ApplicationDbContext DbContext { get; set; }
        public DbSet<T> Entities => DbContext.Set<T>();

        public BaseRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> expression)
        {
            return Entities.Where(expression);
        }

        public async Task InsertAsync(T entity, bool saveChanges = true)
        {
            await Entities.AddAsync(entity);

            if (saveChanges)
                await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity, bool saveChanges = true)
        {
            Entities.Remove(entity);

            if(saveChanges)
                await DbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
        {
            if(entities.Any())
                Entities.RemoveRange(entities);

            if (saveChanges)
                await DbContext.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await Entities.AnyAsync(expression);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await GetQuery(expression).FirstOrDefaultAsync();
        }
    }
}
