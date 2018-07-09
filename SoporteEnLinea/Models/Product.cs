using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoporteEnLinea.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [StringLength(30, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter {0}")]
        public string  Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "You must enter {0}")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "You must enter {0}")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public string Category { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
         public int Quantity { get; set; }
    }
}