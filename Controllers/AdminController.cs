using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using uNeed.Models.Entity;

namespace uNeed.webui.Controllers
{
    [Authorize(Roles ="1")]
    public class AdminController : Controller
    {
        uNeedEntities1 db = new uNeedEntities1();

        

        public ActionResult index()
        {
            return RedirectToAction("productlist");
        }
        //PRODUCT 
        public ActionResult ProductList()
        {
            return View(db.Product.ToList());
        }
        [HttpGet]
        public ActionResult NewProduct()
        {
            var ret = db.Category.ToList();
            return View(ret);
        }

        [HttpPost]
        public ActionResult NewProduct(Product p, int[] categories)
        {
            db.Product.Add(p);
            db.SaveChanges();

            if (categories!=null)
            {
                for (int i = 0; i < categories.Length; i++)
                {
                    db.InsertProduct_Category(p.ProductID, categories[i]);
                }
            }



            db.SaveChanges();
            return RedirectToAction("ProductList", "Admin");
        }


        public ActionResult DeleteProduct(int id)
        {

            db.Product.Remove(db.Product.Find(id));
            db.SaveChanges();

            return RedirectToAction("ProductList");
        }

        public ActionResult GetProduct(int id)
        {
            ViewBag.cats = db.Category.ToList();
            return View("GetProduct", db.Product.Find(id));
        }
        public ActionResult UpdateProduct(Product p, int[] categories)
        {
            var prod = db.Product.Find(p.ProductID);
            prod.ProductName = p.ProductName;
            prod.ProductDescription = p.ProductDescription;
            prod.Price = p.Price;
            prod.Size = p.Size;
            prod.Stock = p.Stock;
            prod.Color = p.Color;
            prod.ProductAvailable = p.ProductAvailable;
            prod.Picture = p.Picture;

            if (categories!=null)
            {
                for (int i = 0; i < categories.Length; i++)
                {
                    if (!prod.Category.Contains(db.Category.Find(categories[i])))
                    {
                        prod.Category.Add(db.Category.Find(categories[i]));
                    }
                }
            }


            db.SaveChanges();
            return RedirectToAction("ProductList");
        }

        //PRODUCT END

        //CATEGORY STARTS

        public ActionResult CategoryList()
        {

            return View(db.Category.ToList());
        }

        [HttpGet]
        public ActionResult NewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCategory(Category c)
        {
            if (!ModelState.IsValid)
                return View();
            db.Category.Add(c);
            db.SaveChanges();
            return RedirectToAction("CategoryList");
        }

        public ActionResult DeleteCategory(int id)
        {

            db.Category.Remove(db.Category.Find(id));
            db.SaveChanges();

            return RedirectToAction("CategoryList");
        }

        public ActionResult GetCategory(int id)
        {
            return View("GetCategory", db.Category.Find(id));
        }
        public ActionResult UpdateCategory(Category c)
        {
            var cat = db.Category.Find(c.CategoryID);
            cat.CategoryName = c.CategoryName;
            cat.Description = c.Description;
            db.SaveChanges();
            return RedirectToAction("CategoryList");
        }

        //CATEGORY END

        //CUSTOMER START
        public ActionResult CustomerList()
        {
            return View(db.Customer.ToList());
        }

        public ActionResult DeleteCustomer(int id)
        {

            db.Customer.Remove(db.Customer.Find(id));
            db.SaveChanges();
            return RedirectToAction("CustomerList");
        }

        public ActionResult GetCustomer(int id)
        {
            return View("GetCustomer", db.Customer.Find(id));
        }
        
        public ActionResult UpdateCustomer(Customer c)
        {
            var cus = db.Customer.Find(c.CustomerID);
            cus.FirstName = c.FirstName;
            cus.LastName = c.LastName;
            cus.Password = c.Password;
            cus.E_Mail = c.E_Mail;
            cus.Phone = c.Phone;
            cus.PostalCode = c.PostalCode;
            cus.Address1 = c.Address1;
            cus.Address2 = c.Address2;
            db.SaveChanges();
            return RedirectToAction("CustomerList");
        }

        //CUSTOMER END

        //ORDER START

        public ActionResult OrderList()
        {
            return View(db.Order.ToList());
        }

        public ActionResult DeleteOrder(int id)
        {

            db.Order.Remove(db.Order.Find(id));
            db.SaveChanges();

            return RedirectToAction("OrderList");
        }

        public ActionResult GetOrder(int id)
        {
            return View("GetOrder", db.Order.Find(id));
        }
        public ActionResult UpdateOrder(Order c)
        {
            var cat = db.Order.Find(c.OrderID);
            cat.CustomerID = c.CustomerID;
            cat.OrderDate = c.OrderDate;
            cat.Paid = c.Paid;
            cat.PaymentID = c.PaymentID;
            cat.ShipperID = c.ShipperID;
            db.SaveChanges();
            return RedirectToAction("OrderList");
        }

        //ORDER END

        //SHIPPER START
        public ActionResult ShipperList()
        {
            return View(db.Shipper.ToList());
        }

        [HttpGet]
        public ActionResult NewShipper()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewShipper(Shipper c)
        {
            if (!ModelState.IsValid)
                return View();
            db.Shipper.Add(c);
            db.SaveChanges();
            return RedirectToAction("ShipperList");
        }

        public ActionResult DeleteShipper(int id)
        {

            db.Shipper.Remove(db.Shipper.Find(id));
            db.SaveChanges();

            return RedirectToAction("ShipperList");
        }

        public ActionResult GetShipper(int id)
        {
            return View("GetShipper", db.Shipper.Find(id));
        }
        public ActionResult UpdateShipper(Shipper c)
        {
            var cat = db.Shipper.Find(c.ShipperID);
            cat.CompanyName = c.CompanyName;
            cat.Phone = c.Phone;
            db.SaveChanges();
            return RedirectToAction("ShipperList");
        }
        //SHIPPER END

        //PAYMENT START
        public ActionResult PaymentList()
        {
            return View(db.Payment.ToList());
        }

        [HttpGet]
        public ActionResult NewPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewPayment(Payment c)
        {
            if (!ModelState.IsValid)
                return View();
            db.Payment.Add(c);
            db.SaveChanges();
            return RedirectToAction("PaymentList");
        }

        public ActionResult DeletePayment(int id)
        {

            db.Payment.Remove(db.Payment.Find(id));
            db.SaveChanges();

            return RedirectToAction("PaymentList");
        }

        public ActionResult GetPayment(int id)
        {
            return View("GetPayment", db.Payment.Find(id));
        }
        public ActionResult UpdatePayment(Payment c)
        {
            var cat = db.Payment.Find(c.PaymentID);
            cat.PaymentType = c.PaymentType;
            cat.Allowed = c.Allowed;
            db.SaveChanges();
            return RedirectToAction("PaymentList");
        }

        //PAYMENT END




    }
}