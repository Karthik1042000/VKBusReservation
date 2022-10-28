using VKBusReservation.Models;
using VKBusReservation.Models.DTO;

namespace VKBusReservation.Repository
{
    public interface ICustomerRepository
    {
        public Messages CreateCustomer(AddCustomerDTO customerDTO);

        public Messages Update(AddCustomerDTO customerDTO);

        public List<Customer> GetAll();

        public Customer GetById(int id);

        public Customer GetByPhoneNumber(string phoneNumber);

        public Customer GetByEmailId(string emailId);

        public Messages DeleteCustomer(int customerId);
        public LoginResultDTO GetLoginDetail(string emailId, string password);
        public List<Role> GetAllRoles();
    }
}
