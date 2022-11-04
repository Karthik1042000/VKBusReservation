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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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


        [Authorize(Roles = "Admin,Customer")]
        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "Customer")]
        public IActionResult CustomerIndex()
        {
            return View();
        }


        [Authorize]
        public IActionResult Profile(int id)
        {
            Customer customer = customerRepository.GetById(id);
            Role role = customerRepository.RoleById(customer.RoleId);
            AddCustomerDTO customerDTO = new AddCustomerDTO();
            customerDTO.CustomerName = customer.CustomerName;
            customerDTO.PhoneNumber = customer.PhoneNumber;
            customerDTO.City = customer.City;
            customerDTO.EmailId = customer.EmailId;
            customerDTO.Password = customer.Password;
            customerDTO.RoleName = role.RoleName;
            customerDTO.Pincode = customer.Pincode;
            customerDTO.CustomerId = customer.CustomerId;
            return View(customerDTO);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CustomerList()
        {
            var customers = customerRepository.GetAll();
            return View(customers);
        }


        public IActionResult CreateCustomer()
        {
            AddCustomerDTO customer = new AddCustomerDTO();
            if (User.Identity.IsAuthenticated)
            {
                var role = User.Identity.GetClaimRole();
                if (role == "Admin")
                {
                    customer.RoleIds = customerRepository.GetAllRoles().Select(a => new SelectListItem
                    {
                        Text = a.RoleName,
                        Value = a.RoleId.ToString()
                    }).ToList();
                    customer.RoleIds.Insert(0, new SelectListItem { Text = "Select Role", Value = "" });
                    customer.Role = true;
                }
            }
            else
            {
                customer.RoleIds = customerRepository.GetAllRoles().Where(x => x.RoleName == "Customer").Select(a => new SelectListItem
                {
                    Text = a.RoleName,
                    Value = a.RoleId.ToString()
                }).ToList();
                customer.Role = false;
            }

            return View(customer);
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

                                                     
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var customer = customerRepository.DeleteCustomer(id);
            return Json(customer);
        }


        [Authorize(Roles = "Admin,Customer")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var role = User.Identity.GetClaimRole();
            Customer customerDTO = customerRepository.GetById(id);
            AddCustomerDTO customer = new AddCustomerDTO();
            customer.CustomerName = customerDTO.CustomerName;
            customer.PhoneNumber = customerDTO.PhoneNumber;
            customer.City = customerDTO.City;
            customer.EmailId = customerDTO.EmailId;
            customer.Password = customerDTO.Password;
            customer.RoleId = customerDTO.RoleId;
            customer.Pincode = customerDTO.Pincode;
            customer.CustomerId = customerDTO.CustomerId;

            if (role == "Admin")
            {
                customer.RoleIds = customerRepository.GetAllRoles().Select(a => new SelectListItem
                {
                    Text = a.RoleName,
                    Value = a.RoleId.ToString()
                }).ToList();
                customer.RoleIds.Insert(0, new SelectListItem { Text = "Select Role", Value = "" });
                customer.Role = true;
            }
            else
            {
                customer.RoleIds = customerRepository.GetAllRoles().Where(x => x.RoleName == "Customer").Select(a => new SelectListItem
                {
                    Text = a.RoleName,
                    Value = a.RoleId.ToString()
                }).ToList();
                customer.Role = false;
            }
            return View("CreateCustomer", customer);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult AllBuses()
        {
            var busList = busRepository.GetAll();
            return View(busList);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult CreateBus()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
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


        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBus(int id)
        {
            var bus = busRepository.DeleteBus(id);
            return Json(bus);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditBus(int id)
        {
            var bus = busRepository.GetByBusId(id);
            return View("CreateBus", bus);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult ReservationList()
        {
            var reservations = reservationRepository.ReserveList();
            return View(reservations);
        }


        [Authorize(Roles = "Admin,Customer")]
        public IActionResult CustomerDetails(int id)
        {
            CustomerList customers = new CustomerList();
            var customer = customerRepository.GetById(id);
            customers.CustomerName = customer.CustomerName;
            customers.Customers = reservationRepository.ReservationDetailsByCustomerId(id).ToList();
            return View(customers);
        }


        [Authorize(Roles = "Admin,Customer")]
        public IActionResult BookTicket()
        {
            var email = User.Identity.GetClaimEmail();
            var role = User.Identity.GetClaimRole();
            AddReservationDTO reservation = new AddReservationDTO();
            if (role == "Admin")
            {
                reservation.CustomerList = customerRepository.GetAll().Select(a => new SelectListItem
                {
                    Text = a.CustomerName + "(" + a.CustomerId + ")",
                    Value = a.CustomerId.ToString()
                }).ToList();
                reservation.CustomerList.Insert(0, new SelectListItem { Text = "Select Customer", Value = "" });
                reservation.Role = true;
            }
            else
            {
                reservation.CustomerList = customerRepository.GetAll().Where(x => x.EmailId == email).Select(a => new SelectListItem
                {
                    Text = a.CustomerName + "(" + a.CustomerId + ")",
                    Value = a.CustomerId.ToString()
                }).ToList();
                reservation.Role = false;
            }

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


        [Authorize(Roles = "Admin,Customer")]
        public ActionResult GetBus(string from, string to)
        {
            var bus = busRepository.GetByFromTo(from, to);
            return Json(bus);
        }


        [Authorize(Roles = "Admin,Customer")]
        [HttpPost]
        public ActionResult GetReservation(int id, DateTime date)
        {
            var bus = reservationRepository.GetAll().Where(x => x.BusId == id).ToList();
            var last = bus.LastOrDefault(x => x.Reservationdate == date);
            return Json(last);
        }


        [Authorize(Roles = "Admin,Customer")]
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


        [Authorize(Roles = "Admin,Customer")]
        public IActionResult CancelTicket(int id)
        {
            var ticket = reservationRepository.CancelTicket(id);
            return Json(ticket);
        }



        [Authorize(Roles = "Admin,Customer")]
        [HttpGet]
        public ActionResult EditReservation(int id)
        {
            var detail = reservationRepository.ReservationDetailsById(id);
            var customer = customerRepository.GetById(detail.CustomerId);
            var bus = busRepository.GetByBusId(detail.BusId);
            AddReservationDTO reservation = new AddReservationDTO();
            var role = User.Identity.GetClaimRole();
            if (role == "Admin")
            {
                reservation.Role = true;
            }
            else
            {
                reservation.Role = false;
            }
            reservation.CustomerId = detail.CustomerId;
            reservation.CustomerName = customer.CustomerName;
            reservation.BusId = detail.BusId;
            reservation.BusNumber = bus.BusNumber;
            reservation.From = bus.From;
            reservation.To = bus.To;
            reservation.ReservationId = detail.ReservationId;
            reservation.AvailableSeats = detail.AvailableSeats;
            reservation.NumberOfSeats = detail.NumberOfSeats;
            reservation.Reservationdate = detail.Reservationdate.ToString("yyyy-MM-dd");
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


        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var user = customerRepository.GetLoginDetail(login.EmailId, login.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier , user.CustomerId.ToString()),
                    new Claim(ClaimTypes.Name, user.CustomerName),
                    new Claim(ClaimTypes.Role, user.RoleName),
                    new Claim(ClaimTypes.Email,user.EmailId)

                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Redirect(login.ReturnUrl == null ? "/Home/Index" : login.ReturnUrl);
            }
            else
                ViewBag.Message = "Invalid Credential";
            return View(login);
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }

    }
}

