namespace VKBusReservation.Models
{
    public class CustomerDetails
    {
        public int ReservationId { get; set; }
        public string BusName { get; set; }
        public string BusNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime Reservationdate { get; set; }
        public int ReservedSeats { get; set; }
        public int TicketPrice { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
    public class CustomerList
    {
        public string CustomerName { get; set; }
        public List<CustomerDetails> Customers { get; set; }
    }
}
