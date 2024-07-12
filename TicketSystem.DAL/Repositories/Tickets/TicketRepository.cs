using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.DAL.Repositories.Tickets
{
    public class TicketRepository : ITicketRepository
    {
        private readonly SystemContext _context;
        public TicketRepository(SystemContext context) 
        { 
            _context = context;
        }
        public IEnumerable<Ticket> GetAll()
        {
            return _context.Set<Ticket>();
        }

        public Ticket? GetById(int id)
        {
            return _context.Set<Ticket>().Find(id);
        }
        public void AddTicket(Ticket ticket)
        {
            _context.Set<Ticket>().Add(ticket);
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
