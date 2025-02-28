﻿using NPOI.SS.Formula.Functions;

namespace TicketSystem.BL;

public class GeneralResponse<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
}
