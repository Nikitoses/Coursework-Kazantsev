using System;
using System.Collections.Generic;
using System.IO;

class Program
{>
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string filePath = "hotel_data.txt";
        List<Room> rooms = new List<Room>
        {
            new Room(1, "Одноместный", 50m),
            new Room(2, "Двухместный", 80m),
            new Room(3, "Люкс", 120m)
        };

        Hotel hotel;
        if (File.Exists(filePath))
        {
            hotel = new Hotel("Готель");
            hotel.LoadFromFile(filePath);
        }
        else
        {
            hotel = new Hotel("Готель");
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Система управления гостиницей");
            Console.WriteLine("1. Забронировать номер");
            Console.WriteLine("2. Просмотр броней");
            Console.WriteLine("3. Отменить бронь");
            Console.WriteLine("4. Управление номерами");
            Console.WriteLine("5. Сохранить и выйти");
            Console.Write("Выберите опцию: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BookRoom(hotel, rooms);
                    break;
                case "2":
                    ViewBookings(hotel);
                    break;
                case "3":
                    CancelBooking(hotel);
                    break;
                case "4":
                    ManageRooms(rooms);
                    break;
                case "5":
                    hotel.SaveToFile(filePath);
                    return;
                default:
                    Console.WriteLine("Недействительный вариант. Пожалуйста, попробуйте еще раз.");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }
    }

    static void BookRoom(Hotel hotel, List<Room> rooms)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        try
        {
            Console.Clear();
            Console.WriteLine("Забронировать номер");

            Console.Write("Имя клиента: ");
            string clientName = Console.ReadLine();

            Console.Write("Дата заезда (YYYY-MM-DD): ");
            DateTime checkInDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Дата выезда (YYYY-MM-DD): ");
            DateTime checkOutDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Выберите номер:");
            for (int i = 0; i < rooms.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {rooms[i].Name} - {rooms[i].Price} USD за ночь");
            }
            int roomChoice = int.Parse(Console.ReadLine());
            Room room = rooms[roomChoice - 1];

            int bookingID = hotel.Bookings.Count + 1;
            Booking booking = new Booking(bookingID, clientName, checkInDate, checkOutDate, room);
            hotel.AddBooking(booking);

            Console.WriteLine("Бронирование успешно выполнено");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void ViewBookings(Hotel hotel)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        Console.WriteLine("Бронирования:");
        foreach (var booking in hotel.Bookings)
        {
            Console.WriteLine(booking);
        }
    }

    static void CancelBooking(Hotel hotel)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        Console.WriteLine("Отменить бронирование");

        Console.Write("ID бронирования: ");
        int bookingID = int.Parse(Console.ReadLine());

        var booking = hotel.Bookings.Find(b => b.BookingID == bookingID);
        if (booking != null)
        {
            hotel.Bookings.Remove(booking);
            Console.WriteLine("Бронирование успешно отменено");
        }
        else
        {
            Console.WriteLine("Бронирование не найдено.");
        }
    }

    static void ManageRooms(List<Room> rooms)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Управление номерами");
            Console.WriteLine("1. Добавить номер");
            Console.WriteLine("2. Удалить номер");
            Console.WriteLine("3. Редактировать номер");
            Console.WriteLine("4. Назад в главное меню");
            Console.Write("Выберите опцию: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddRoom(rooms);
                    break;
                case "2":
                    DeleteRoom(rooms);
                    break;
                case "3":
                    EditRoom(rooms);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Недействительный вариант. Пожалуйста, попробуйте еще раз.");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }
    }

    static void AddRoom(List<Room> rooms)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        Console.WriteLine("Добавить номер");

        Console.Write("ID номера: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Название: ");
        string name = Console.ReadLine();

        Console.Write("Цена за ночь: ");
        decimal price = decimal.Parse(Console.ReadLine());

        rooms.Add(new Room(id, name, price));
        Console.WriteLine("Номер успешно добавлен!");
    }

    static void DeleteRoom(List<Room> rooms)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        Console.WriteLine("Удалить номер");

        Console.Write("ID номера: ");
        int id = int.Parse(Console.ReadLine());

        var room = rooms.Find(r => r.RoomID == id);
        if (room != null)
        {
            rooms.Remove(room);
            Console.WriteLine("Номер успешно удален!");
        }
        else
        {
            Console.WriteLine("Номер не найден.");
        }
    }

    static void EditRoom(List<Room> rooms)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        Console.WriteLine("Редактировать номер");

        Console.Write("ID номера: ");
        int id = int.Parse(Console.ReadLine());

        var room = rooms.Find(r => r.RoomID == id);
        if (room != null)
        {
            Console.Write("Новое название: ");
            string name = Console.ReadLine();

            Console.Write("Новая цена за ночь: ");
            decimal price = decimal.Parse(Console.ReadLine());

            room.Name = name;
            room.Price = price;
            Console.WriteLine("Номер успешно отредактирован!");
        }
        else
        {
            Console.WriteLine("Номер не найден.");
        }
    }
}
