using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.DAL.Repositories.Common
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task<int> SaveChanges();
        
    }
}
