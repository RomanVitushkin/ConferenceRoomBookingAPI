using Microsoft.EntityFrameworkCore;
using ConferenceRoomBookingAPI.Models;
using System.Data;

namespace ConferenceRoomBookingAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; } // Таблиця конференц-залів
        public DbSet<Service> Services { get; set; } // Таблиця послуг
        public DbSet<Booking> Bookings { get; set; } // Таблиця бронювань

    }
}
