using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uNeed.Models.Entity;

namespace uNeed.ViewModels
{
    public class OrderViewModel

    {
        public List<Shipper> ships { get; set; }
        public List<Payment> pays { get; set; }
        public List<OrderDetails> prods { get; set; }
        public double total { get; set; }
        public int cust { get; set; }

        public OrderViewModel()
        {
            this.ships = new List<Shipper>();
            this.pays = new List<Payment>();
            this.prods = new List<OrderDetails>();
            this.total = new double();
            this.cust = new int();
        }


    }
}