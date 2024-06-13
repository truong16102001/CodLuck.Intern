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
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
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

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Entry(entity).State = EntityState.Deleted;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
