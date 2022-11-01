using VKBusReservation.Models;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VKBusReservation.Models.DTO;

namespace VKBusReservation.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly VKBusReservationDbContext db;
        public CustomerRepository(VKBusReservationDbContext db)
        {
            this.db = db;
        }


        public Messages CreateCustomer(AddCustomerDTO customerDTO)
        {
            Customer customer = new Customer();
            customer.CustomerName=customerDTO.CustomerName;
            customer.PhoneNumber=customerDTO.PhoneNumber;
            customer.City=customerDTO.City;
            customer.EmailId=customerDTO.EmailId;
            customer.Password = customerDTO.Password;
            customer.RoleId=customerDTO.RoleId;
            customer.Pincode=customerDTO.Pincode;
            customer.CustomerId=customerDTO.CustomerId;
            Messages messages = new Messages();
            messages.Success = false;
            var customerExist = GetByPhoneNumber(customer.PhoneNumber);
            var emailIdExist = GetByEmailId(customer.EmailId);
            if (customerExist != null)
            {
                messages.Message = "PhoneNumber already Registered.";
                return messages;
            }
            if (emailIdExist != null)
            {
                messages.Message = "EmailId already Registered.";
                return messages;
            }
            else
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                messages.Success = true;
                messages.Message = "Customer succssfully added";
                return messages;
            }

        }


        public Messages DeleteCustomer(int customerId)
        {
            Messages messages = new Messages();
            messages.Success = false;
            var customer = GetById(customerId);
            if (customer == null)
            {
                messages.Message = "Customer Id is not found";
                return messages;
            }
            var idReserved = db.Reservations.Where(x => x.CustomerId == customer.CustomerId);
            if (idReserved.Count() > 0)
            {
                messages.Message = "Customer already Reserved ,So Customer cannot delete at this moment";
                return messages;
            }
            else
            {
                db.Customers.Remove(customer);
                db.SaveChanges();               
                messages.Success = true;
                messages.Message = "Customer succssfully deleted";
                return messages;
            }

        }
        public List<Role> GetAllRoles()
        {
            return db.Roles.ToList();
        }

        public List<Customer> GetAll()
        {
            return db.Customers.ToList();
        }


        public Customer GetById(int id)
        {
            var customerExist = db.Customers.FirstOrDefault(x => x.CustomerId == id);
            return customerExist;
        }


        public Customer GetByPhoneNumber(string phoneNumber)
        {
            var phoneNumberExist = db.Customers.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            return phoneNumberExist;
        }

        public Customer GetByEmailId(string emailId)
        {
            var emailIdExist = db.Customers.FirstOrDefault(x => x.EmailId == emailId);
            return emailIdExist;
        }


        public Messages Update(AddCustomerDTO customerDTO) 
        {
            Customer customer = new Customer();
            customer.CustomerName = customerDTO.CustomerName;
            customer.PhoneNumber = customerDTO.PhoneNumber;
            customer.City = customerDTO.City;
            customer.EmailId = customerDTO.EmailId;
            customer.Password = customerDTO.Password;
            customer.RoleId = customerDTO.RoleId;
            customer.Pincode = customerDTO.Pincode;
            customer.CustomerId = customerDTO.CustomerId;
            Messages messages = new Messages();
            messages.Success = false;
            var customerExist = GetById(customer.CustomerId);
            var phoneExist = GetByPhoneNumber(customer.PhoneNumber);
            var emailIdExist = GetByEmailId(customer.EmailId);

            if (phoneExist != null && phoneExist.CustomerId != customerExist.CustomerId)
            {
                messages.Message = "Phone Number already registered";
                return messages;
            }
            if (emailIdExist != null && emailIdExist.CustomerId != customerExist.CustomerId)
            {
                messages.Message = "EmailId already registered";
                return messages;
            }
            if (customerExist == null)
            {
                messages.Message = "Customer Id is not found";
                return messages;
            }
            else
            {
                customerExist.CustomerName = customer.CustomerName;
                customerExist.PhoneNumber = customer.PhoneNumber;
                customerExist.EmailId = customer.EmailId;
                customerExist.Pincode = customer.Pincode;
                customerExist.City = customer.City;
                db.SaveChanges();
                messages.Success = true;
                messages.Message = "Customer succssfully updated";
                return messages;
            }

        }
        public LoginResultDTO GetLoginDetail(string emailId, string password)
        {
            var customers = (from customer in db.Customers
                             join role in db.Roles on customer.RoleId equals role.RoleId
                             where customer.EmailId == emailId && customer.Password== password
                            select new LoginResultDTO()
                            {
                                CustomerId = customer.CustomerId,
                                CustomerName = customer.CustomerName,
                                RoleName = role.RoleName,
                                EmailId = customer.EmailId
                            }).FirstOrDefault();
            
            return customers;
        }
    }
}
