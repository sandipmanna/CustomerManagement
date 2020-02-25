using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerManagement.Models
{
    public class Customer
    {
        [Required(ErrorMessage ="Customer Code can not be empty")]
        [RegularExpression("^[A-Z]{3,3}[0-9]{4,4}$", ErrorMessage ="3 Capital Character followed by 4 Number")]
        [Display(Name ="Customer Code")]
        [Key]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Customer Name can not be empty")]
        [Display(Name = "Customer Name")]
        [StringLength(10, ErrorMessage ="Name must be less than 10 Character")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Customer amount can not be empty")]
        public decimal CustomerAmount { get; set; }
    }
}