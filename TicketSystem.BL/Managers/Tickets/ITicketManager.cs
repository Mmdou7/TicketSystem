using TicketSystem.BL.ViewModels;

namespace TicketSystem.BL;

public interface ITicketManager
{
    Task<PagedResultVm<TicketReadVm>> FilterAsync(TicketFilterInputVm model);
    Task<TicketReadVm> GetTicketByIdAsync(int id);
    Task<string> AddTicketAsync(TicketAddVm model);

}
