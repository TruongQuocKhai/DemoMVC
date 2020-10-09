using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class UserController : Controller
    {
        DataClassesDataContext data = new DataClassesDataContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        
        public ActionResult SignIn(FormCollection collection, Customer customer)
        {
            // gan cac gia tri nguoi dung nhap cho cac bien
            var account = collection["Account"];
            var password = collection["Password"];
            if (string.IsNullOrEmpty(account))
            {
                ViewData["Error1"] = "Phai nhap ten dang nhap";
            }
            else if (string.IsNullOrEmpty(password))
            {
                ViewData["Error2"] = "Phai nhap mat khau";
            }
            else
            {
                // gan cac gia tri nguoi dung nhap dung cho doi tuong 'customer'
                customer = data.Customers.SingleOrDefault(n => n.Account == account && n.Password == password);
                if (customer != null)
                {
                    ViewBag.Inform = "Chuc mung da dang nhap thanh cong!";
                    Session["Account"] = customer.Name;
                }
                else
                {
                    ViewBag.Inform = "Dang nhap khong thanh cong";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection collection, Customer customer)
        {
            // gan gia tri nguoi dung nhap cho cac bien
            var name = collection["Name"];
            var account = collection["Account"];
            var password = collection["Password"];
            var confirmPassword = collection["ConfirmPassword"];
            var address = collection["Address"];
            var email = collection["Email"];
            var cellPhone = collection["CellPhone"];
            var birhday = String.Format("{0:MM/dd/yy}", collection["Birthday"]);

            if (String.IsNullOrEmpty(name))
            {
                ViewData["Error2"] = "Ho ten khong duoc de trong!";
            }
            else if (String.IsNullOrEmpty(account))
            {
                ViewData["Error2"] = "Ten dang nhap khong duoc de trong!";
            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewData["Error3"] = "Mat khau khong duoc de trong!";
            }
            else if (String.IsNullOrEmpty(confirmPassword))
            {
                ViewData["Error4"] = "Phai nhap lai mat khau!";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Error5"] = "Email khong duoc de trong!";
            }
            else if (String.IsNullOrEmpty(cellPhone))
            {
                ViewData["Error6"] = "So dien thoai khong duoc de trong!";
            }
            else
            {
                // nhap dung het du lieu se dc gan vao bien 'customer'
                customer.Name = name;
                customer.Account = account;
                customer.Password = password;
                customer.Email = email;
                customer.Address = address;
                customer.CellPhone = cellPhone;
                customer.Birthday = Convert.ToDateTime(birhday);

                data.Customers.InsertOnSubmit(customer);
                data.SubmitChanges();
                return RedirectToAction("SignIn");
            }
            return this.SignUp();
        }
    }
}