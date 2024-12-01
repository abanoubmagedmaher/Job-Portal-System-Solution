﻿using Job_Portal_System.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_System.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(DbContext context) 
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        
        public async Task<IEnumerable<T>> GetAllWithConditionAsync(string include)
        {
            return await _dbSet.Include(include).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdWithConditionAsync(int id, string include = null)
        {
            IQueryable<T> query = _dbSet;

            if (!string.IsNullOrEmpty(include))
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(entity => EF.Property<int>(entity, "Id") == id);
        }

        public async Task AddAsync(T entity)
        {
             await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

       
    }
}
