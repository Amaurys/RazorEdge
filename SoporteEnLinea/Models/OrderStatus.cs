using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoporteEnLinea.Models
{
    public enum OrderStatus
    {
        Created, 
        InProgress,
        Shipped,
        Delivered
    }
}