namespace TicketSystem.BL.Common.Constants;

public class Constant
{
    public static class Messages
    {
        public const string AddedSuccessfully = "تمت الإضافة بنجاح";      
    }
    public static class ErrorMessages
    {
        public const string TicketNotFound = "Ticket with Id {0} was not found.";
        public const string InvalidModel = "The ticket model is invalid.";
        public const string InvalidPhoneNumber = "The phone number must contain only numbers.";
        public const string TicketNotAdded = "An error occurred while adding the ticket.";
        public const string ErrorHappened = "لقد حدث خطأ";

    }
}
