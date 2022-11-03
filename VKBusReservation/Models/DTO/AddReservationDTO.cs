using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VKBusReservation.Models.DTO
{
    public class AddReservationDTO
    {
        public int ReservationId { get; set; }
        public int NumberOfSeats { get; set; }
        public int TicketPrice { get; set; }
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public DateTime ReservationTime { get; set; } = DateTime.Now;
        public string Reservationdate { get; set; }


        public int? ReservedSeats { get; set; }

        public int? AvailableSeats { get; set; } 
        public int CustomerId { get; set; }
        public int BusId { get; set; }
        public List<SelectListItem> BusList { get; set; }
        public List<SelectListItem> FromList { get; set; }
        public List<SelectListItem> ToList { get; set; }
        public List<SelectListItem> CustomerList { get; set; }
        public string CustomerName { get; set; } 
        public string BusNumber { get; set; }
        public bool Role { get; set; }
    }

}
