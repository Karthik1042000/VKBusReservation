using VKBusReservation.Models;

namespace VKBusReservation.Repository
{
    public interface ICustomerRepository
    {
        public Messages CreateCustomer(Customer customer);

        public Messages Update(Customer customer);

        public List<Customer> GetAll();

        public Customer GetById(int id);

        public Customer GetByPhoneNumber(string phoneNumber);

        public Customer GetByEmailId(string emailId);

        public Messages DeleteCustomer(int customerId);
    }
}
