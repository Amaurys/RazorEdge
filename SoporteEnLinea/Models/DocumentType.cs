using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoporteEnLinea.Models
{
    public class DocumentType
    {
        [Key]
        public int DocumentTypeID { get; set; }

        [StringLength (30, ErrorMessage ="The field {0} must contain between {2} and {1} characters", MinimumLength =3)]
        [Required(ErrorMessage ="You must enter the field {0}")]
        [Display(Name ="Descripcion documento")]
        public string Descripcion { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}