using TicketSystem.BL.ViewModels;

namespace TicketSystem.BL;

public interface ITicketManager
{
    Task<PagedResultVm<TicketReadVm>> FilterAsync(TicketFilterInputVm model);
    Task<GeneralResponse<TicketReadVm>> GetTicketByIdAsync(int id);
    Task<GeneralResponse<string>> AddTicketAsync(TicketAddVm model);

}
