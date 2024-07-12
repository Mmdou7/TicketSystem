using System.Collections.Generic;

namespace TicketSystem.BL;

public class PagedResultVm<T>
{
    public PagedResultVm()
    {
        Result = new List<T>();
    }
    public List<T> Result { get; set; }
    public int TotalCount { get; set; }
}
