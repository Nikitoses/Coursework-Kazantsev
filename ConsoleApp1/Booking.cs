using System;

public class Booking
{
    public int BookingID { get; set; }
    public string ClientName { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public Room Room { get; set; }

    public Booking(int bookingID, string clientName, DateTime checkInDate, DateTime checkOutDate, Room room)
    {
        if (room == null)
            throw new ArgumentNullException(nameof(room));
        if (checkInDate >= checkOutDate)
            throw new ArgumentException("Дата заезда должна быть раньше даты выезда.");

        BookingID = bookingID;
        ClientName = clientName;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        Room = room;
    }

    public override string ToString()
    {
        return $"{ClientName}: {Room.Name} ({Room.Price} USD за ночь) с {CheckInDate.ToShortDateString()} по {CheckOutDate.ToShortDateString()}";
    }
}