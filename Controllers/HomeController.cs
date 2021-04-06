using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using uNeed.Data.Concrete.EfCore;
using uNeed.Models.Entity;
using uNeed.ViewModels;

namespace uNeed.Controllers
{
    public class HomeController : Controller
    {
        EFCoreCategoryRepository categories = new EFCoreCategoryRepository();
        EFCoreProductRepository products = new EFCoreProductRepository();
        uNeedEntities1 db = new uNeedEntities1();
        [Authorize]
        public ActionResult Index(int? Id,string q)
        {
            ProductViewModel viewmodel = new ProductViewModel();
            viewmodel.cats = categories.GetAll();
            var products = db.Product.ToList();
            if (Id != null)
            {
                List<Product> temp = new List<Product>();
                foreach (var p in products)
                {
                    foreach (var item in p.Category)
                    {
                        if (item.CategoryID == Id)
                        {
                            temp.Add(p);
                        }
                    }
                }
                products = temp;
            }
            if (!string.IsNullOrEmpty(q))
            {
                //|| i.ProductDescription.ToLower().Contains(q.ToLower()) --------- description içinden aratma şimdilik koymadık.
                products = products.Where(i => i.ProductName.ToLower().Contains(q.ToLower())).ToList();
            }

            viewmodel.pros = products;

            return View(viewmodel);
         
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}