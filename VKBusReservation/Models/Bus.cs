using System;
using System.ComponentModel;
//using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;

namespace VKBusReservation.Models
{
    public class Bus
    {

        [Key]
        public int BusId { get; set; }


        [Required(ErrorMessage = "Please Enter the Bus Number !!!")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Bus Number length must be 10 characters")]
        public string BusNumber { get; set; }


        [Required(ErrorMessage = "Please Enter the BusName !!!")]
        [StringLength(30, ErrorMessage = "Bus Name length must be below 30 characters")]
        public string BusName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please Enter the Total Seats in Bus !!!")]
        [Range(1, 50, ErrorMessage = "The Total Seats must be in the range of 1-50 and it is fixed")]
        public int TotalSeats { get; set; } 


        [Required(ErrorMessage = "Please Enter From which Place Bus starts!!!")]
        [StringLength(20, ErrorMessage = " length must be below 20 characters")]
        public string From { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please Enter To which Place is Destiny !!!")]
        [StringLength(20, ErrorMessage = " length must be below 20 characters")]
        public string To { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Enter the Trip Start Time !!!")]
        [DataType(DataType.Time)]
        public DateTime TripStartTime { get; set; }

        [Required(ErrorMessage = "Please Enter the Trip End Time !!!")]
        [DataType(DataType.Time)]
        public DateTime TripEndTime { get; set; }


        [Required(ErrorMessage = "Please Enter the  TicketPrice in Bus !!!")]
        [Range(100, 50000, ErrorMessage = "The TicketPrice  must be in the range of 100-50000 and it is fixed")]
        public int TicketPrice { get; set; }


    }
}
