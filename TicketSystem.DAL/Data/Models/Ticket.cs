using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketSystem.DAL
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Governorate { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public bool isHandled { get; set; } 
    }
}
