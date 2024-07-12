using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.DAL.Repositories.Common
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private SystemContext _context;
        private readonly DbSet<TEntity> _entities;
        protected RepositoryBase(SystemContext context)
        {
            _entities = context.Set<TEntity>();
            _context = context;
        }
        public IQueryable<TEntity> GetAll()
        {
            return _entities;
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }
        public async Task Add(TEntity entity)
        {
           await _entities.AddAsync(entity);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
