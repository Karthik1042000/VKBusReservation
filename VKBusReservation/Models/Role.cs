using System;
using System.ComponentModel;
//using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;


namespace VKBusReservation.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
