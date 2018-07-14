using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoporteEnLinea.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public string Name { get; set; }
    }
}