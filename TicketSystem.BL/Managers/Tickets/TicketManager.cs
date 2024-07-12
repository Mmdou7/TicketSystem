﻿using System.Text.RegularExpressions;
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
        bool isUpdated = false;

        foreach (var ticket in query)
        {
            if (ticket.CreationDateTime >= DateTime.Now.AddMinutes(-60) && ticket.Status != "Handled")
            {
                ticket.Status = "Handled";
                isUpdated = true;
            }
        }

        if (isUpdated)
        {
            _ticketRepository.SaveChanges();
        }

        var pagedTickets = query.Skip(model.PageSize * --model.PageNumber).Take(model.PageSize).Select(t => new TicketReadVm
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
            Result = pagedTickets
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
                StatusCode = 001,
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

    public async Task<GeneralResponse<string>> AddTicketAsync(TicketAddVm model)
    {
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
            Ticket ticket = new Ticket()
            {
                PhoneNumber = model.PhoneNumber,
                CreationDateTime = DateTime.Now,
                City = model.City,
                Governorate = model.Governorate,
                District = model.District,
                Status = "UnHandled"
            };
            _ticketRepository.Add(ticket);
            _ticketRepository.SaveChanges();
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
            Ticket? ticket = _ticketRepository.GetById(id);

            if (ticket == null)
            {
                return new GeneralResponse<TicketReadVm>
                {
                    StatusCode = 001,
                    Message = string.Format(ErrorMessages.TicketNotFound, id),
                    Data = null
                };
            }

            ticket.Status = "Handled";
            _ticketRepository.SaveChanges();

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
                    Status = ticket.Status
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
