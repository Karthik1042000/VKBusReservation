﻿using System;
using System.ComponentModel;
//using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKBusReservation.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }


        [Required(ErrorMessage = "Please Enter the Customer Name !!!")]
        [StringLength(20, ErrorMessage = "Customer Name length must be below 20 characters")]
        public string CustomerName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please Enter the Phone Number !!!")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "PhoneNumber must be 10 Digits")]
        public string PhoneNumber { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please Enter the EmailId !!!")]
        [EmailAddress(ErrorMessage = "Enter valid Email address")]
        public string EmailId { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please Enter the Pincode !!!")]
        public int Pincode { get; set; }


        [Required(ErrorMessage = "Please Enter the City !!!")]
        [StringLength(30, ErrorMessage = "City length must be below 30 characters")]
        public string City { get; set; } = string.Empty;


       
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "Must be between 5 and 20 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        

        [ForeignKey("Role")]
        [Required(ErrorMessage = "Please Enter the RoleId !!!")]
        public int RoleId { get; set; }


    }
}
