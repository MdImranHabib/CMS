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
        [StringLength(50, ErrorMessage = "Sender name can't be greater than 50 characters")]
        [Display(Name ="Sender Name")]
        public string SenderName { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Address can't be greater than 150 characters")]
        [Display(Name = "Sender Address")]
        public string SenderAddress { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Sender Email")]
        public string SenderEmail { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Sender Contact")]
        [StringLength(17, ErrorMessage = "Contact number can't be greater than 17 characters")]
        public string SenderContact { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Receiver name can't be greater than 50 characters")]
        [Display(Name = "Receiver Name")]
        public string ReceiverName { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Address can't be greater than 150 characters")]
        [Display(Name = "Receiver Address")]
        public string ReceiverAddress { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Receiver Email")]
        public string ReceiverEmail { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Receiver Contact")]
        [StringLength(17, ErrorMessage = "Contact number can't be greater than 17 characters")]
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
        [Display(Name = "Receiving Date")]
        public DateTime ReceivingDate { get; set; }

        
    }
}
