using Microsoft.EntityFrameworkCore;

namespace TicketSystem.DAL
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options)
        {

        }
        public DbSet<Ticket> Tickets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region SeedingData
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    CreationDateTime = new DateTime(2024, 7, 11),
                    PhoneNumber = "1234567890",
                    Governorate = "Governorate1",
                    City = "City1",
                    District = "District1",
                    Status = "Handled"
                },
                new Ticket
                {
                    Id= 2,
                    CreationDateTime = new DateTime(2024, 7, 11),
                    PhoneNumber = "0987654321",
                    Governorate = "Governorate2",
                    City = "City2",
                    District = "District2",
                    Status = "Handled"
                },
                new Ticket
                {
                    Id= 3,
                    CreationDateTime = new DateTime(2024, 7, 9),
                    PhoneNumber = "0987784321",
                    Governorate = "Governorate3",
                    City = "City3",
                    District = "District3",
                    Status = "Handled"
                }
            );
            #endregion
        }
    }
}