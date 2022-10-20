using System;
using System.ComponentModel;
//using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKBusReservation.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }


        [Required(ErrorMessage = "Please Enter the Seat Number !!!")]
        [Range(1, 50, ErrorMessage = "The Seat Number must be in the range of 1-50")]
        public int NumberOfSeats { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReservationTime { get; set; }=DateTime.Now;

        [Required(ErrorMessage = "Please Enter the Reservation date!!!")]
        [DataType(DataType.Date)]
        public DateTime Reservationdate { get; set; } 


        public int? ReservedSeats { get; set; } = 0;

        public int? AvailableSeats { get; set; } = 0;



        [ForeignKey("Customer")]
        [Required(ErrorMessage = "Please Enter the CustomerId !!!")]
        public int CustomerId { get; set; }



        [ForeignKey("Bus")]
        [Required(ErrorMessage = "Please Enter the BusId !!!")]
        public int BusId { get; set; }

    }
}
