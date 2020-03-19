using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Percel
    {
        [Display(Name ="Percel Id")]
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
        [Display(Name = "Receiving Date")]
        public DateTime ReceivingDate { get; set; }

        [Required]
        [Display(Name ="Sender")]
        public int SenderId { get; set; }

        [Required]
        [Display(Name ="Receiver")]
        public int ReceiverId { get; set; }

        [Required]
        [Display(Name ="Branch")]
        public int BranchId { get; set; }

        [Required]
        [StringLength(15)]
        public string Status { get; set; }

        public virtual Sender Sender { get; set; }

        public virtual Receiver Receiver { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
