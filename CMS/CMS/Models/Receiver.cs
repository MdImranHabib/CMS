﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Receiver
    {
        public int Id { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "Receiver name can't be greater than 70 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Address can't be greater than 150 characters")]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string Contact { get; set; }
    }
}
