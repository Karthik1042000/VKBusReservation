namespace VKBusReservation.Models
{
    public class BusDetails
    {
        public string BusName { get; set; }
        public string BusNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int TotalSeats { get; set; }
        public int ReservedSeats { get; set; }
        public int ReservedTicketPrice { get; set; }
        public DateTime Reservationdate { get; set; }

    }
}
