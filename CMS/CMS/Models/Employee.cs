using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "Employee name can't be greater than 70 characters")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(150, ErrorMessage = "Address can't be greater than 150 characters")]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Remote(action: "IsEmailExist", controller: "Employees", ErrorMessage = "This Email is Already in Use. Try Another")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string Contact { get; set; }

        [Required]
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
