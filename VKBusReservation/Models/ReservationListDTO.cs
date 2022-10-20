namespace VKBusReservation.Models
{
    public class ReservationListDTO
    {
        public int ReservationId { get; set; }
        public int NumberOfSeats { get; set; }
        public int TicketPrice { get; set; }
        public string PickupPoint { get; set; }
        public string DropPoint { get; set; }
        public DateTime ReservationTime { get; set; }
        public DateTime Reservationdate { get; set; }
        public string CustomerName { get; set; }
        public string BusNumber { get; set; }
    }
}
