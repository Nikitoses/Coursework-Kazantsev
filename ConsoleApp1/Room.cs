public class Room
{
    public int RoomID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Room(int roomID, string name, decimal price)
    {
        RoomID = roomID;
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price} USD за ночь";
    }
}



public class Hotel
{
    public string Name { get; set; }
    public List<Booking> Bookings { get; set; }

    public Hotel(string name)
    {
        Name = name;
        Bookings = new List<Booking>();
    }

    public void AddBooking(Booking booking)
    {
        Bookings.Add(booking);
    }

    public void SaveToFile(string filePath)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(Name);
            writer.WriteLine(Bookings.Count);
            foreach (var booking in Bookings)
            {
                writer.WriteLine(booking.BookingID);
                writer.WriteLine(booking.ClientName);
                writer.WriteLine(booking.CheckInDate);
                writer.WriteLine(booking.CheckOutDate);
                writer.WriteLine(booking.Room.RoomID);
                writer.WriteLine(booking.Room.Name);
                writer.WriteLine(booking.Room.Price);
            }
        }
    }

    public void LoadFromFile(string filePath)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Файл не найден");

        using (StreamReader reader = new StreamReader(filePath))
        {
            Name = reader.ReadLine();
            int bookingCount = int.Parse(reader.ReadLine());

            Bookings.Clear();
            for (int i = 0; i < bookingCount; i++)
            {
                int bookingID = int.Parse(reader.ReadLine());
                string clientName = reader.ReadLine();
                DateTime checkInDate = DateTime.Parse(reader.ReadLine());
                DateTime checkOutDate = DateTime.Parse(reader.ReadLine());
                int roomID = int.Parse(reader.ReadLine());
                string roomName = reader.ReadLine();
                decimal roomPrice = decimal.Parse(reader.ReadLine());

                Room room = new Room(roomID, roomName, roomPrice);
                Booking booking = new Booking(bookingID, clientName, checkInDate, checkOutDate, room);
                Bookings.Add(booking);
            }
        }
    }
}
