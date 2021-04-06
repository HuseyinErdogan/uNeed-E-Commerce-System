using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using uNeed.Models.Entity;
using uNeed.ViewModels;

namespace uNeed.Controllers
{
    public class ProductController : Controller
    {

        uNeedEntities1 db = new uNeedEntities1();
        static int lastOrder = 0;

        static List<OrderDetails> cart = new List<OrderDetails>();

        public ActionResult list(int? Id, string q)
        {
            ProductViewModel viewmodel = new ProductViewModel();
            viewmodel.cats = db.Category.ToList();
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

        public ActionResult details(int Id)
        {
            ViewBag.proc = db.Product.ToList();
            var a = db.Product.Find(Id);
            return View(a);
        }
        public ActionResult basket()
        {
            return View(cart);
        }
        public ActionResult addToCart(int id)
        {
            var product = db.Product.Find(id);
            foreach (var item in cart.ToList())
            {
                if (item.ProductID == id)
                {
                    if (item.Product.Stock == item.Quantity)
                    {
                        return Redirect(Request.UrlReferrer.PathAndQuery);
                    }
                    item.Quantity += 1;
                    item.Price = item.Quantity * product.Price;
                    return Redirect(Request.UrlReferrer.PathAndQuery);
                }
            }

            var temp = new OrderDetails();

            temp.ProductID = product.ProductID;
            temp.Product = product;
            temp.Quantity = 1;
            temp.Price = product.Price;
            cart.Add(temp);

            return Redirect(Request.UrlReferrer.PathAndQuery);

        }

        public ActionResult reduceProductInCart(int id)
        {
            var product = db.Product.Find(id);
            foreach (var item in cart.ToList())
            {
                if (item.ProductID == id)
                {
                    if (item.Quantity == 1)
                    {
                        cart.Remove(item);
                    }
                    else
                    {
                        item.Quantity--;
                        item.Price -= item.Product.Price;
                    }
                    break;
                }
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult deleteFromCart(int id)
        {
            var product = db.Product.Find(id);
            foreach (var item in cart.ToList())
            {
                if (item.ProductID == id)
                {
                    cart.Remove(item);
                    break;
                }
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult confirmOrder()
        {


            double temp = 0;
            foreach (var item in cart)
            {
                temp += (double)item.Price;

            }
            OrderViewModel viewmodel = new OrderViewModel();

            viewmodel.prods = cart;
            viewmodel.ships = db.Shipper.ToList();
            viewmodel.pays = db.Payment.ToList();
            viewmodel.total = temp;
            viewmodel.cust = (int)Session["customerID"]; 

            return View(viewmodel);
        }

        public ActionResult addOrder(Order ord)
        {

            db.Order.Add(ord);
            db.SaveChanges();
            double sum = 0;
            foreach (var item in cart)
            {
                sum += (double)item.Price; //STORED PROCEDURE
                db.InsertOrderDetail(item.Price, item.Quantity, ord.OrderID, item.ProductID);
                db.SaveChanges();

                db.Product.Find(item.ProductID).Stock -= (int)item.Quantity;
                if (db.Product.Find(item.ProductID).Stock == 0)
                {
                    db.Product.Find(item.ProductID).ProductAvailable = false;
                }
                db.SaveChanges();
            }
            db.Order.Find(ord.OrderID).Paid=sum;
            db.Order.Find(ord.OrderID).OrderDate = DateTime.Now;
            db.Order.Find(ord.OrderID).ShipDate= DateTime.Now.AddDays(1);
            db.SaveChanges();
            cart.Clear();
            lastOrder = ord.OrderID;
            return RedirectToAction("orderScreen");
        }

        public ActionResult orderScreen()
        {
            List<OrderDetailView> orderDetailView = new List<OrderDetailView>();
            foreach (var item in db.OrderDetailView)
            {
                if (item.OrderID==lastOrder)
                {
                    orderDetailView.Add(item);

                }
                
            }
            
            return View(orderDetailView);
        }
    }
}