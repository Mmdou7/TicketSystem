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
        public string PhoneNumber { get; set; } = "";
        public string Governorate { get; set; } = "";
        public string City { get; set; } = "";
        public string District { get; set; } = "";
        public string Status { get; set; } = "";
    }
}
