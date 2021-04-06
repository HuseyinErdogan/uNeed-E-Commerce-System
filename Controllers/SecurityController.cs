using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using uNeed.Models.Entity;

namespace uNeed.Controllers
{
    public class SecurityController:Controller
    {
        uNeedEntities1 db = new uNeedEntities1();
        public ActionResult login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult login(Customer c)
        {
            var data = db.Customer.FirstOrDefault(x => x.E_Mail == c.E_Mail && x.Password == c.Password);
            if (data != null)
            {
                FormsAuthentication.SetAuthCookie(data.E_Mail, true);
                Session["customerID"] = data.CustomerID;
                Session["customerName"] = data.FirstName.First().ToString().ToUpper() + data.FirstName.Substring(1) + " " 
                    + data.LastName.First().ToString().ToUpper() + data.LastName.Substring(1);
                Session["email"] = data.E_Mail;
                Session["phone"] = data.Phone;
                Session["address"] = data.Address1;
                // Session["current"] = Request.UrlReferrer.PathAndQuery.ToString();
                if (data.Role == 1)
                    Session["role"] = "ADMINISTRATOR";
                else if (data.Role == 0)
                    Session["role"] = "uNeed CUSTOMER";
                return RedirectToAction("index", "home");
            }
            return View();
        }
        [HttpGet]
        public ActionResult signup()
        {


            return View();
        }
        [HttpPost]
        public ActionResult signup(Customer c)
        {
            c.DateEntered = DateTime.Now;
            c.Role = 0;
            db.Customer.Add(c);
            db.SaveChanges();
            return RedirectToAction("index", "home");
        }
        public ActionResult logout(Customer c)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login", "Security");
        }
    }
}