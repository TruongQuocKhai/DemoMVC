using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class BookStoreController : Controller
    {
        // create a new 'data' ojbject  containing database 
        DataClassesDataContext data = new DataClassesDataContext();

        private List<Book> GetNewBook(int count)
        {
            // sap xep dam dan theo ngay cap nhat, va lay ra 5 quyen sach moi
            return data.Books.OrderByDescending(a => a.UpdateDay).Take(count).ToList();
        }

        // GET: BookStore
        public ActionResult Index()
        {
            // goi ham GetNewBook() lay ra 5 quyen sach moi
            var newBook = GetNewBook(4);
            return PartialView(newBook);
        }

        public ActionResult Theme()
        {
            //lay ra 5 album ban chay nhat
            var them = from t in data.Themes select t;
            return PartialView(them);
        }

        public ActionResult Publisher()
        {
            var phusher = from p in data.Publishers select p;
            return PartialView(phusher);
        }

        public ActionResult Detail(int id)
        {
            var detail = from b in data.Books where b.Id == id select b;
            return View(detail.Single()); // single() tra ve 1 phan tu duy nhat tu 1 tap hop
        }

        public ActionResult ProductsByTheme(int id)
        {
            var productsByTheme = from t in data.Books where t.ThemeId == id select t;
            return View(productsByTheme);
        }
        public ActionResult ProductsByPublisher(int id)
        {
            var productsByPublisher = from p in data.Books where p.PublisherId == id select p;
            return View(productsByPublisher);
        }


    }
}