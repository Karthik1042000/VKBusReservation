using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VKBusReservation.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberLogin { get; set;}
        public string ReturnUrl { get; set; } 
    }

}
