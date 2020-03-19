using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class PercelLocation
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Branch")]
        public int BranchId { get; set; }

        [Required]
        [Display(Name ="Percel Id")]
        public int PercelId { get; set; }

        [Required]
        [StringLength(15)]
        public string Status { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}")]
        [Display(Name = "Receiving Date")]
        public DateTime ReceivingDate { get; set; }

        public Branch Branch { get; set; }
    }
}
