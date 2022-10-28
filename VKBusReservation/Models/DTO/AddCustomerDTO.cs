using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VKBusReservation.Models.DTO
{
    public class AddCustomerDTO
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;

        public int Pincode { get; set; }

        public string City { get; set; } = string.Empty;

        public string Password { get; set; }

        public int RoleId { get; set; }
        public List<SelectListItem> RoleIds { get; set; }
    }
}
