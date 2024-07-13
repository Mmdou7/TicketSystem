using System.Text.RegularExpressions;
using TicketSystem.BL.ViewModels;
using TicketSystem.DAL;
using TicketSystem.DAL.Repositories.Tickets;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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
        var query = _ticketRepository.GetAll();

        #region OldHandlingForUpdateIsHandle        
        //bool isUpdated = false;
        //foreach (var ticket in query)
        //{
        //    if (ticket.CreationDateTime >= DateTime.Now.AddMinutes(-60) && !ticket.isHandled)
        //    {
        //        ticket.isHandled = true;
        //        isUpdated = true;
        //    }
        //}
        //if (isUpdated)
        //{
        //    await _ticketRepository.SaveChanges();
        //}
        #endregion

        var pagedTickets = query.Skip(model.PageSize * --model.PageNumber).Take(model.PageSize).Select(t => new TicketReadVm
        {
            Id = t.Id,
            PhoneNumber = t.PhoneNumber,
            CreationDateTime = t.CreationDateTime,
            City = t.City,
            District = t.District,
            Governorate = t.Governorate,
            IsHandled = t.isHandled
        }).ToList();

        var result = new PagedResultVm<TicketReadVm>
        {
            TotalCount = query.Count(),
            Result = pagedTickets
        };

        return result;
    }


    public async Task<GeneralResponse<TicketReadVm>> GetTicketByIdAsync(int id)
    {
        Ticket? ticket = await _ticketRepository.GetById(id);

        if (ticket == null)
        {
            return new GeneralResponse<TicketReadVm>
            {
                StatusCode = 001,
                Message = string.Format(ErrorMessages.TicketNotFound, id),
                Data = null
            };
        }
        #region OldHandlingForIsHandle
        //bool isUpdated = false;
        //if (ticket.CreationDateTime >= DateTime.Now.AddMinutes(-60) && !ticket.isHandled)
        //{
        //    ticket.isHandled = true;
        //    isUpdated = true;
        //}
        //if (isUpdated)
        //{
        //    await _ticketRepository.SaveChanges();
        //}
        #endregion

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
                IsHandled = ticket.isHandled
            }
        };
    }

    public async Task<GeneralResponse<string>> AddTicketAsync(TicketAddVm model)
    {
        if (model.CreationDateTime > DateTime.Now)
        {
            return new GeneralResponse<string>
            {
                StatusCode = 009,
                Message = ErrorMessages.InvalidModel,
                Data = null
            };
        }
        if (model == null || string.IsNullOrEmpty(model.PhoneNumber) ||
            string.IsNullOrEmpty(model.City) ||
            string.IsNullOrEmpty(model.Governorate) ||
            string.IsNullOrEmpty(model.District))
        {
            return new GeneralResponse<string>
            {
                StatusCode = 002,
                Message = ErrorMessages.InvalidModel,
                Data = null
            };
        }
        if (!Regex.IsMatch(model.PhoneNumber, @"^\d+$"))
        {
            return new GeneralResponse<string>
            {
                StatusCode = 003,
                Message = ErrorMessages.InvalidPhoneNumber,
                Data = null
            };
        }
        try
        {
            bool isTicketHandled = (DateTime.Now - model.CreationDateTime).TotalMinutes <= 60 ? true : false;
            Ticket ticket = new Ticket()
            {
                PhoneNumber = model.PhoneNumber,
                //CreationDateTime = DateTime.Now,
                CreationDateTime = model.CreationDateTime,
                City = model.City,
                Governorate = model.Governorate,
                District = model.District,
                isHandled = isTicketHandled
            };
            await _ticketRepository.Add(ticket);
            await _ticketRepository.SaveChanges();
            return new GeneralResponse<string>
            {
                StatusCode = 003,
                Message = Messages.AddedSuccessfully,
                Data = null
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ErrorMessages.TicketNotAdded, ex);
        }
    }

    public async Task<GeneralResponse<TicketReadVm>> ChangeStatusAsync(int id)
    {
        try
        {
            Ticket? ticket = await _ticketRepository.GetById(id);

            if (ticket == null)
            {
                return new GeneralResponse<TicketReadVm>
                {
                    StatusCode = 001,
                    Message = string.Format(ErrorMessages.TicketNotFound, id),
                    Data = null
                };
            }

            ticket.isHandled = true;
            await _ticketRepository.SaveChanges();

            return new GeneralResponse<TicketReadVm>
            {
                StatusCode = 004,
                Message = Messages.StatusUpdatedSuccessfully,
                Data = new TicketReadVm
                {
                    Id = ticket.Id,
                    PhoneNumber = ticket.PhoneNumber,
                    CreationDateTime = ticket.CreationDateTime,
                    City = ticket.City,
                    District = ticket.District,
                    Governorate = ticket.Governorate,
                    IsHandled = ticket.isHandled
                }
            };
        }
        catch (Exception ex)
        {
            return new GeneralResponse<TicketReadVm>
            {
                StatusCode = 007,
                Message = ErrorMessages.ErrorHappened,
                Data = null
            };
        }
    }

}
