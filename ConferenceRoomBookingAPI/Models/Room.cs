using System.Collections.Generic;

namespace ConferenceRoomBookingAPI.Models
{
    // Клас для представлення конференц-залу
    public class Room
    {
        public int Id { get; set; } // Унікальний ідентифікатор залу
        public string Name { get; set; } // Назва залу
        public int Capacity { get; set; } // Місткість залу
        public decimal BasePricePerHour { get; set; } // Базова вартість оренди за годину

        // Список послуг, що надаються в цьому залі
        public List<Service> Services { get; set; } = new List<Service>();
    }

    // Клас для представлення послуг (наприклад, проектор, Wi-Fi)
    public class Service
    {
        public int Id { get; set; } // Унікальний ідентифікатор послуги
        public string Name { get; set; } // Назва послуги
        public decimal Price { get; set; } // Вартість послуги
    }
}


