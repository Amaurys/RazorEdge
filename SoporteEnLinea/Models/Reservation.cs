using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SoporteEnLinea.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public int IdEmployee { get; set; }
        public string IdUser { get; set; }
        public DateTime TurnDate { get; set; }
        public string Status { get; set; }

        [ForeignKey("IdEmployee")]
        public virtual Employee Employee { get; set; }
    }
}