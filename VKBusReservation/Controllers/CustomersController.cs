using VKBusReservation.Models;
using VKBusReservation.Repository;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VKBusReservation.Models.DTO;

namespace BusReservationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        public CustomersController(ICustomerRepository _customerRepository)
        {
            customerRepository = _customerRepository;
        }


        [HttpGet("GetCustomers")]
        public IActionResult GetCustomers()
        {
            var customer = customerRepository.GetAll();
            if (customer.Count() == 0)
            {
                throw new Exception("The Customers list is empty");
            }
            return Ok(customer);
        }


        [HttpGet("GetCustomer/{id}")]

        public ActionResult GetCustomer(int id)
        {
            var customer = customerRepository.GetById(id);
            if (customer == null)
            {
                throw new Exception("The Customer Id is not found");
            }
            return Ok(customer);
        }


        [HttpPost("Add")]

        public IActionResult Add(AddCustomerDTO customer)
        {
            return Ok(customerRepository.CreateCustomer(customer));
        }


        [HttpPut("Update")]
        public ActionResult Update(AddCustomerDTO customer)
        {
            return Ok(customerRepository.Update(customer));
        }


        [HttpDelete("RemoveById/{id}")]

        public IActionResult RemoveById(int id)
        {
            return Ok(customerRepository.DeleteCustomer(id));
        }
    }
}
