using VKBusReservation.Repository;
using VKBusReservation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VKBusReservation.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
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
            AddCustomerDTO Customer = new AddCustomerDTO();
            Customer.RoleIds = customerRepository.GetAllRoles().Select(a => new SelectListItem
            {
                Text = a.RoleName,
                Value = a.RoleId.ToString()
            }).ToList();
            Customer.RoleIds.Insert(0, new SelectListItem { Text = "Select Role", Value = "" });
            return View(Customer);
        }

        [HttpPost]
        public ActionResult Save(AddCustomerDTO customer)
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
            var customerDTO = customerRepository.GetById(id);
            AddCustomerDTO customer = new AddCustomerDTO();
            customer.CustomerName = customerDTO.CustomerName;
            customer.PhoneNumber = customerDTO.PhoneNumber;
            customer.City = customerDTO.City;
            customer.EmailId = customerDTO.EmailId;
            customer.Password = customerDTO.Password;
            customer.RoleId = customerDTO.RoleId;
            customer.Pincode = customerDTO.Pincode;
            customer.CustomerId = customerDTO.CustomerId;
            return View("CreateCustomer", customer);
        }


        public IActionResult AllBuses()
        {
            var busList = busRepository.GetAll();
            return View(busList);
        }
        public IActionResult CreateBus()
        {
            return View();
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

        public IActionResult CustomerDetails(int id)
        {
            CustomerList customers = new CustomerList();
            var customer = customerRepository.GetById(id);
            customers.CustomerName = customer.CustomerName;
            customers.Customers = reservationRepository.ReservationDetailsByCustomerId(id).ToList();
            return View(customers);
        }
        public IActionResult BookTicket()
        {
            AddReservationDTO reservation = new AddReservationDTO();
            reservation.CustomerList = customerRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.CustomerName + "(" + a.CustomerId + ")",
                Value = a.CustomerId.ToString()
            }).ToList();
            reservation.CustomerList.Insert(0, new SelectListItem { Text = "Select Customer", Value = "" });

            reservation.FromList = busRepository.GetAll().DistinctBy(x => x.From).Select(a => new SelectListItem
            {
                Text = a.From,
                Value = a.From.ToString()
            }).ToList();
            reservation.FromList.Insert(0, new SelectListItem { Text = "Select From", Value = "" });

            reservation.ToList = busRepository.GetAll().DistinctBy(x => x.To).Select(a => new SelectListItem
            {
                Text = a.To,
                Value = a.To.ToString()
            }).ToList();
            reservation.ToList.Insert(0, new SelectListItem { Text = "Select Destination", Value = "" });
            return View(reservation);
        }
        public ActionResult GetBus(string from, string to)
        {
            var bus = busRepository.GetByFromTo(from, to);
            return Json(bus);
        }

        [HttpPost]
        public ActionResult GetReservation(int id,DateTime date)
        {
            var bus = reservationRepository.GetAll().Where(x => x.BusId == id).ToList();
            var last = bus.LastOrDefault(x => x.Reservationdate == date);
            return Json(last);
        }
        [HttpPost]
        public ActionResult SaveTicket(AddReservationDTO addReservation)
         {
            if (addReservation.ReservationId > 0)
            {
                return Json(reservationRepository.UpdateTicket(addReservation));
            }
            else
            {
                return Json(reservationRepository.BookTicket(addReservation));
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
            var detail = reservationRepository.ReservationDetailsById(id);
            var customer = customerRepository.GetById(detail.CustomerId);
            var bus = busRepository.GetByBusId(detail.BusId);
            AddReservationDTO reservation = new AddReservationDTO();
            reservation.CustomerId = detail.CustomerId;
            reservation.CustomerName = customer.CustomerName;
            reservation.BusId = detail.BusId;
            reservation.BusNumber = bus.BusNumber;
            reservation.From = bus.From;
            reservation.To = bus.To;
            reservation.ReservationId = detail.ReservationId;
            reservation.AvailableSeats = detail.AvailableSeats;
            reservation.NumberOfSeats = detail.NumberOfSeats;
            reservation.Reservationdate = detail.Reservationdate.ToString("yyyy-MM-d");
            reservation.ReservedSeats = detail.ReservedSeats;
            reservation.CustomerList = customerRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.CustomerName + "(" + a.CustomerId + ")",
                Value = a.CustomerId.ToString()
            }).ToList();
            reservation.CustomerList.Insert(0, new SelectListItem { Text = "Select Customer", Value = "" });

            reservation.FromList = busRepository.GetAll().DistinctBy(x => x.From).Select(a => new SelectListItem
            {
                Text = a.From,
                Value = a.From.ToString()
            }).ToList();
            reservation.FromList.Insert(0, new SelectListItem { Text = "Select From", Value = "" });

            reservation.ToList = busRepository.GetAll().DistinctBy(x => x.To).Select(a => new SelectListItem
            {
                Text = a.To,
                Value = a.To.ToString()
            }).ToList();
            reservation.ToList.Insert(0, new SelectListItem { Text = "Select Destination", Value = "" });

            return View("BookTicket", reservation);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var user = customerRepository.GetLoginDetail(login.EmailId,login.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier , user.CustomerId.ToString()),
                    new Claim(ClaimTypes.Name, user.CustomerName),
                    new Claim(ClaimTypes.Role, user.RoleName)

                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Redirect(login.ReturnUrl == null ? "/Home" : login.ReturnUrl);
            }
            else
                return View(login);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

