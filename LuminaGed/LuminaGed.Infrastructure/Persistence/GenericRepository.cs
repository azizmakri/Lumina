using LuminaGed.Application.Interfaces;
using LuminaGed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Infrastructure.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LuminaGedContext _context;

        public GenericRepository(LuminaGedContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(object studentId)
        {
            throw new NotImplementedException();
        }

        public Task GetByIdAsync(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<object>> GetAsync(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
