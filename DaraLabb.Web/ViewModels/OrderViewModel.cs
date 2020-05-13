using DaraLabb.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaraLabb.Web.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public User User { get; set; }
    }
}
