﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Percel
    {
        public int Id { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage ="weight can't be negative")]
        public double Weight { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Cost can't be negative")]
        public double Cost { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}")]
        public DateTime ReceivingDate { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        [StringLength(15)]
        public string Status { get; set; }

        public virtual Sender Sender { get; set; }

        public virtual Receiver Receiver { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
