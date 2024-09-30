using System;

namespace ConferenceRoomBookingAPI.Models
{
    public class Booking
    {
        public int Id { get; set; } // Унікальний ідентифікатор бронювання
        public int RoomId { get; set; } // Ідентифікатор конференц-залу
        public DateTime StartTime { get; set; } // Час початку бронювання
        public DateTime EndTime { get; set; } // Час закінчення бронювання
        public List<Service> SelectedServices { get; set; } // Обрані послуги
        public decimal TotalPrice { get; set; } // Загальна вартість бронювання

        public Booking()
        {
            SelectedServices = new List<Service>(); // Ініціалізація списку послуг
        }
    }
}
