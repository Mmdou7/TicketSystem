namespace TicketSystem.BL;

public class GeneralResponse
{
    public string Message { get; set; }
    public GeneralResponse(string msg)
    {
        Message = msg;
    }
}
