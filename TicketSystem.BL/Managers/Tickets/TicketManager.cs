using TicketSystem.BL.ViewModels;
using TicketSystem.DAL;
using TicketSystem.DAL.Repositories.Tickets;
using static TicketSystem.BL.Common.Constants.Constant;

namespace TicketSystem.BL;

public class TicketManager : ITicketManager 
{
    private readonly ITicketRepository _ticketRepository;
    public TicketManager(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    public async Task<PagedResultVm<TicketReadVm>> FilterAsync(TicketFilterInputVm model)
    {
        IEnumerable<Ticket> query = _ticketRepository.GetAll();

        var tickets = query.Skip(model.PageSize * --model.PageNumber).Take(model.PageSize).Select(t => new TicketReadVm
        {
            Id = t.Id,
            PhoneNumber = t.PhoneNumber,
            CreationDateTime = t.CreationDateTime,
            City = t.City,
            District = t.District,
            Governorate = t.Governorate,
            Status = t.Status
        }).ToList();
           
        var result = new PagedResultVm<TicketReadVm>
        {
            TotalCount = query.Count(),
            Result = tickets
        };

        return result;
    }

    public async Task<GeneralResponse<TicketReadVm>> GetTicketByIdAsync(int id)
    {
        Ticket? ticket = _ticketRepository.GetById(id);

        if (ticket == null)
        {
            return new GeneralResponse<TicketReadVm>
            {
                StatusCode = 200,
                Message = string.Format(ErrorMessages.TicketNotFound, id),
                Data = null
            };
        }
        return new GeneralResponse<TicketReadVm>
        {
            StatusCode = 200,
            Message = "Ticket found successfully.",
            Data = new TicketReadVm
            {
                Id = ticket.Id,
                PhoneNumber = ticket.PhoneNumber,
                CreationDateTime = ticket.CreationDateTime,
                City = ticket.City,
                District = ticket.District,
                Governorate = ticket.Governorate,
                Status = ticket.Status
            }
        };
    }

    public async Task<string> AddTicketAsync(TicketAddVm model)
    {
        Ticket ticket = new Ticket()
        {
            PhoneNumber = model.PhoneNumber,
            CreationDateTime = DateTime.Now,
            City = model.City,
            Governorate = model.Governorate,
            District = model.District,
            Status = "Unhandled"
        };

        _ticketRepository.AddTicket(ticket);

        _ticketRepository.SaveChanges();
        var msg = Messages.AddedSuccessfully;

        return msg;
    }

   
}
