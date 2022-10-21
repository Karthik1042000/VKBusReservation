using VKBusReservation.Repository;
using VKBusReservation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VKBusReservation.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusReservationManagement.Controllers
{
    public class HomeController : Controller

    {
        private readonly ILogger<HomeController> _logger;

        private readonly IReservationRepository reservationRepository;

        private readonly ICustomerRepository customerRepository;

        private readonly IBusRepository busRepository;

        public HomeController(ILogger<HomeController> logger, IReservationRepository _reservationRepository, ICustomerRepository _customerRepository, IBusRepository _busRepository)
        {
            _logger = logger;
            reservationRepository = _reservationRepository;
            customerRepository = _customerRepository;
            busRepository = _busRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        

        public IActionResult CustomerList()
        {
            var customers = customerRepository.GetAll();
            return View(customers);
        }
        public IActionResult CreateCustomer()
        {
            Customer customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.CustomerId > 0)
            {
                return Json(customerRepository.Update(customer));
            }
            else
            {
                var ok = customerRepository.CreateCustomer(customer);
                return Json(ok);
            }
        }


        public IActionResult Delete(int id)
        {
            var customer = customerRepository.DeleteCustomer(id);
            return Json(customer);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var customer = customerRepository.GetById(id);
            return View("CreateCustomer", customer);
        }


        public IActionResult AllBuses()
        {
            var busList = busRepository.GetAll();
            return View(busList);
        }
        public IActionResult CreateBus()
        {
            Bus bus = new Bus();
            return View(bus);
        }

        [HttpPost]
        public ActionResult SaveBus(Bus bus)
        {
            if (bus.BusId > 0)
            {
                return Json(busRepository.Update(bus));
            }
            else
            {
                return Json(busRepository.CreateBus(bus));
            }
        }

        public IActionResult DeleteBus(int id)
        {
            var bus = busRepository.DeleteBus(id);
            return Json(bus);
        }


        [HttpGet]
        public ActionResult EditBus(int id)
        {
            var bus = busRepository.GetByBusId(id);
            return View("CreateBus", bus);
        }


        public IActionResult ReservationList()
        {
            var reservations = reservationRepository.ReserveList();
            return View(reservations);
        }
        public IActionResult BookTicket()
        {
            AddReservationDTO reservation = new AddReservationDTO();
            reservation.BusList=busRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.BusName +"(" +a.BusNumber+")",
                Value = a.BusId.ToString()
            }).ToList();
            reservation.CustomerList = customerRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.CustomerName + "(" + a.CustomerId + ")",
                Value = a.CustomerId.ToString()
            }).ToList();

            reservation.FromList =busRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.From,
                Value = a.From.ToString()
            }).ToList();
            reservation.ToList = busRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.To,
                Value = a.To.ToString()
            }).ToList();
            return View(reservation);
        }
        [HttpPost]
        public ActionResult GetBus( string from,string to)
        {
            var bus = busRepository.GetByFromTo(from,to);
            return Json(bus);
        }

        [HttpPost]
        public ActionResult SaveTicket(Reservation reservation)
        {
            if (reservation.ReservationId > 0)
            {
                return Json(reservationRepository.UpdateTicket(reservation));
            }
            else
            {
                return Json(reservationRepository.BookTicket(reservation));
            }
        }

        public IActionResult CancelTicket(int id)
        {
            var ticket = reservationRepository.CancelTicket(id);
            return Json(ticket);
        }


        [HttpGet]
        public ActionResult EditReservation(int id)
        {
            var reservation = reservationRepository.ReservationDetailsById(id);
            return View("BookTicket", reservation);
        }

    }
}
