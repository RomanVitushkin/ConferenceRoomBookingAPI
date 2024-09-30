using ConferenceRoomBookingAPI.Data;
using ConferenceRoomBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Метод для створення нового бронювання
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
        {
            // Перевіряємо, чи всі необхідні дані присутні
            if (booking == null)
                return BadRequest();

            // Шукаємо зал за його ID
            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room == null)
                return NotFound(new { message = "Конференц-залу не знайдено." });

            // Перевіряємо, чи зал доступний у зазначений час
            var isAvailable = !await _context.Bookings
                .AnyAsync(b => b.RoomId == booking.RoomId &&
                               ((booking.StartTime >= b.StartTime && booking.StartTime < b.EndTime) ||
                               (booking.EndTime > b.StartTime && booking.EndTime <= b.EndTime)));

            if (!isAvailable)
                return BadRequest(new { message = "Конференц-зал недоступний у зазначений час." });

            // Розраховуємо загальну вартість бронювання
            decimal totalCost = CalculateBookingCost(room, booking);

            // Встановлюємо загальну вартість бронювання
            booking.TotalPrice = totalCost;

            // Додаємо бронювання до бази даних
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Повертаємо підтвердження успішного бронювання
            return Ok(new { booking.Id, booking.TotalPrice, message = "Бронювання успішно створено." });
        }

        // Метод для отримання бронювання за ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound(new { message = "Бронювання не знайдено." });

            return Ok(booking);
        }

        // Допоміжний метод для розрахунку вартості бронювання
        private decimal CalculateBookingCost(Room room, Booking booking)
        {
            decimal baseCost = room.BasePricePerHour;
            TimeSpan duration = booking.EndTime - booking.StartTime;
            decimal totalCost = baseCost * (decimal)duration.TotalHours;

            // Додаємо вартість послуг
            foreach (var service in booking.SelectedServices)
            {
                totalCost += service.Price;
            }

            return totalCost;
        }
    }
}
