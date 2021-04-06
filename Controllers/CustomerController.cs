using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using uNeed.Data.Concrete.EfCore;
using uNeed.Models.Entity;

namespace uNeed.Controllers
{
    
    public class CustomerController:Controller
    {
        uNeedEntities1 db = new uNeedEntities1();
       public ActionResult profile()
        {
            ViewBag.Title = "AAAAAAAAAAAA";
            return View();
        }
        public ActionResult GetCustomer(int id)
        {
            return View("GetCustomer", db.Customer.Find(id));
        }
        public ActionResult Update(Customer c)
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
            Session["customerID"] = cus.CustomerID;
            Session["customerName"] = cus.FirstName.First().ToString().ToUpper() + cus.FirstName.Substring(1) + " "
                + cus.LastName.First().ToString().ToUpper() + cus.LastName.Substring(1);
            Session["email"] = cus.E_Mail;
            Session["phone"] = cus.Phone;
            Session["address1"] = cus.Address1;
            Session["address2"] = cus.Address2;
            // Session["current"] = Request.UrlReferrer.PathAndQuery.ToString();
            if (cus.Role == 1)
                Session["role"] = "ADMINISTRATOR";
            else if (cus.Role == 0)
                Session["role"] = "uNeed CUSTOMER";
    
            return RedirectToAction("profile");
        }
    }
}