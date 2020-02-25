using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{
    [Table(name:"tblUsers")]
    public class User
    {
        [Key]
        [Required(ErrorMessage ="* Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="* Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Display(Name ="Remember Me")]
        //public bool RememberMe { get; set; }
    }
}