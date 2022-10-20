using VKBusReservation.Models;
using VKBusReservation.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusReservationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {

        private readonly IReservationRepository reservationRepository;
        public ReservationsController(IReservationRepository _reservationRepository)
        {
            reservationRepository = _reservationRepository;
        }

        [HttpGet("GetReservationList")]
        public IActionResult GetReservationList()
        {
            var list = reservationRepository.GetAll();
            if (list.Count() == 0)
            {
                throw new Exception("The Reservation list is empty");
            }
            return Ok(list);
        }


        [HttpGet("ReservationDetails/{id}")]

        public ActionResult ReservationDetails(int id)
        {
            var reservation = reservationRepository.ReservationDetailsById(id);
            if (reservation == null)
            {
                throw new Exception("The Reservation Id is not found");
            }
            return Ok(reservation);
        }


        [HttpPost("BookTicket")]

        public IActionResult BookTicket(Reservation reservation)
        {
            return Ok(reservationRepository.BookTicket(reservation));
        }


        [HttpPut("Update")]
        public ActionResult Update(Reservation reservation)
        {
            return Ok(reservationRepository.UpdateTicket(reservation));
        }


        [HttpDelete("CancelTicket/{id}")]

        public IActionResult CancelTicket(int id)
        {
            return Ok(reservationRepository.CancelTicket(id));
        }


        [HttpGet("CustomerReservationsDetailsById/{id}")]

        public ActionResult CustomerReservationsDetailsById(int id)
        {
            var customerExist = reservationRepository.GetCustomer(id);
            if (customerExist == null)
            {
                throw new Exception("The Customer Id is not registered");
            }
            var customer = reservationRepository.ReservationDetailsByCustomerId(id).ToList();
            if (customer.Count() == 0)
            {
                throw new Exception("The Customer Id is not reserved");
            }
            return Ok(customer);
        }
    }
}
