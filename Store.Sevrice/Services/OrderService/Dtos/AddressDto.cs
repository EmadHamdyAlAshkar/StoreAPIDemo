﻿using System.ComponentModel.DataAnnotations;

namespace Store.Sevrice.Services.OrderService.Dtos
{
    public class AddressDto
    {
        //[Required]
        //public long Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
    }
}