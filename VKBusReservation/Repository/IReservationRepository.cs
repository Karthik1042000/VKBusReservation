using VKBusReservation.Models;
using VKBusReservation.Models.DTO;

namespace VKBusReservation.Repository
{
    public interface IReservationRepository
    {
        public Messages BookTicket(Reservation reservation);

        public Messages UpdateTicket(Reservation reservation);
        public Reservation ReservationDetailsById(int id);
        public Bus GetBus(int id);
        public Customer GetCustomer(int id);
        public List<Reservation> GetAll();
        public Messages CancelTicket(int reservationId);
        public IEnumerable<CustomerDetails> ReservationDetailsByCustomerId(int customerId);
        public IEnumerable<AddReservationDTO> ReserveList();
    }
}
