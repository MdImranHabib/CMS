using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models.ViewModels
{
    public class PercelReceive
    {
        [Required]
        [StringLength(70, ErrorMessage = "Sender name can't be greater than 70 characters")]
        public string SenderName { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Address can't be greater than 150 characters")]
        public string SenderAddress { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string SenderEmail { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string SenderContact { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "Receiver name can't be greater than 70 characters")]
        public string ReceiverName { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Address can't be greater than 150 characters")]
        public string ReceiverAddress { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string ReceiverEmail { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string ReceiverContact { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "weight can't be negative")]
        public double Weight { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Cost can't be negative")]
        public double Cost { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}")]
        public DateTime ReceivingDate { get; set; }

        
    }
}
