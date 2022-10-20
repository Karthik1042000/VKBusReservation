using VKBusReservation.Models;

namespace VKBusReservation.Repository
{
    public interface IBusRepository
    {
        public Messages CreateBus(Bus bus);

        public Messages Update(Bus bus);

        public List<Bus> GetAll();

        public Bus GetByBusId(int busId);

        public Messages DeleteBus(int busNumber);
        public List<Bus> GetByFromTo(string from, string to);

        public Bus GetByBusNumber(string busNumber);
        public IEnumerable<BusDetails> ReservationDetailsByBusId(int busId);
    }
}
