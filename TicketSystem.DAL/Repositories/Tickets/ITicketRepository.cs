using TicketSystem.DAL;
using TicketSystem.DAL.Repositories.Common;

namespace TicketSystem.DAL.Repositories.Tickets
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        #region OldRepo
        //IEnumerable<Ticket> GetAll();
        //Ticket? GetById(int id);
        //void AddTicket(Ticket ticket);
        //int SaveChanges();
        #endregion 

    }
}
