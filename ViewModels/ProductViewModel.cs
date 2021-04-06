using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uNeed.Models.Entity;

namespace uNeed.ViewModels
{
    public class ProductViewModel

    {
        public List<Product> pros{get;set;}
        public List<Category> cats{ get; set; }

        public ProductViewModel()
        {
            this.pros = new List<Product>();
            this.cats = new List<Category>();
        }



    }
}