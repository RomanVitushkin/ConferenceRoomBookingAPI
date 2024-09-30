using ConferenceRoomBookingAPI.Data;
using ConferenceRoomBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Метод для додавання нового конференц-залу
        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] Room room)
        {
            // Перевіряємо, чи всі необхідні дані присутні
            if (room == null)
                return BadRequest();

            // Додаємо зал до контексту бази даних і зберігаємо зміни
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            // Повертаємо підтвердження з ID залу
            return Ok(new { room.Id, message = "Конференц-зал успішно додано." });
        }

        // Метод для оновлення інформації про конференц-зал
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            // Перевіряємо, чи є зал в базі даних
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return NotFound();

            // Оновлюємо дані залу
            room.Name = updatedRoom.Name;
            room.Capacity = updatedRoom.Capacity;
            room.BasePricePerHour = updatedRoom.BasePricePerHour;
            room.Services = updatedRoom.Services;

            // Зберігаємо зміни в базі даних
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();

            // Повертаємо підтвердження успішного оновлення
            return Ok(new { message = "Конференц-зал успішно оновлено." });
        }

        // Метод для видалення конференц-залу
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            // Шукаємо зал за ID
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return NotFound();

            // Видаляємо зал з бази даних
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            // Повертаємо підтвердження видалення
            return Ok(new { message = "Конференц-зал успішно видалено." });
        }

        // Метод для пошуку доступних залів за параметрами
        [HttpGet("search")]
        public async Task<IActionResult> SearchRooms(DateTime date, int capacity)
        {
            // Шукаємо зали, які відповідають місткості
            var availableRooms = await _context.Rooms
                .Where(r => r.Capacity >= capacity)
                .ToListAsync();

            // TODO: Додати логіку для пошуку залів за наявністю у певний час

            // Повертаємо список доступних залів
            return Ok(availableRooms);
        }

        // Метод для отримання залу за його унікальним ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            // Шукаємо зал в базі даних за його ID
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return NotFound();

            // Повертаємо знайдений зал
            return Ok(room);
        }
    }
}
