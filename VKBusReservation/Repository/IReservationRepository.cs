using VKBusReservation.Models;
using VKBusReservation.Models.DTO;

namespace VKBusReservation.Repository
{
    public interface IReservationRepository
    {
        public Messages BookTicket(AddReservationDTO addReservation);

        public Messages UpdateTicket(AddReservationDTO addReservation);
        public Reservation ReservationDetailsById(int id);
        public Bus GetBus(int id);
        public Customer GetCustomer(int id);
        public List<Reservation> GetAll();
        public Messages CancelTicket(int reservationId);
        public IEnumerable<CustomerDetails> ReservationDetailsByCustomerId(int customerId);
        public IEnumerable<AddReservationDTO> ReserveList();
    }
}
