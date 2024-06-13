using Microsoft.EntityFrameworkCore;
using P6.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace P6.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly CodLuckContext _context;

        public BaseRepository(CodLuckContext codLuckContext)
        {
            _context = codLuckContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            if (expression is null)
            {
                return await _context.Set<T>().ToListAsync();  // Set là trỏ vào table T trong db, sau đó chuyển table sang list
            }
            return await _context.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression = null)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(expression);
        }

        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Entry(entity).State = EntityState.Deleted;
            await Task.CompletedTask;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task MultipleCreate(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public async Task MultipleUpdate(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            await Task.CompletedTask;
        }

        public async Task MultipleDelete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Deleted;
            }
            await Task.CompletedTask;
        }
    }
}
