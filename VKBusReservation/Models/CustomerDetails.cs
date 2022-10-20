namespace VKBusReservation.Models
{
    public class CustomerDetails
    {
        public string BusName { get; set; }
        public string BusNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime Reservationdate { get; set; }
        public int ReservedSeats { get; set; }
        public int ReservedTicketPrice { get; set; }
        public string PickupPoint { get; set; }
        public string DropPoint { get; set; }
    }
}
