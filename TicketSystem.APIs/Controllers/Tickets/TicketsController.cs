using Microsoft.AspNetCore.Mvc;
using TicketSystem.BL;
using TicketSystem.BL.ViewModels;

namespace TicketSystem.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketManager _ticketManager;
    public TicketsController(ITicketManager ticketManager)
    {
        _ticketManager = ticketManager;
    }

    [HttpPost("filter")]
    public async Task<IActionResult> Filter(TicketFilterInputVm model )
    {
        var result = await _ticketManager.FilterAsync(model);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetTicketById(int id)
    {
        var result = await _ticketManager.GetTicketByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTicket(TicketAddVm ticket)
    {
        var result = await _ticketManager.AddTicketAsync(ticket);
        var obj = new { msg = result };
        return Ok(obj);
    }
}
