using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class CartController : Controller
    {
        DataClassesDataContext data = new DataClassesDataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        // Viet method lay gio hang tu Session["Cart"] neu co, neu ko thi khoi tao gio hang rong
        public List<CartModel> GetCart()
        {
            List<CartModel> listCart = Session["Cart"] as List<CartModel>;
            if (listCart == null)
            {
                listCart = new List<CartModel>(); // khoi tao gio hang rong
                Session["Cart"] = listCart;
            }
            return listCart;
        }

        // method add item to Cart
        public ActionResult AddItem(int bookId, string url)
        {
            List<CartModel> listCart = GetCart(); // lay listCart de kiem tra co san hay ko
            CartModel item = listCart.Find(n => n.BookId == bookId);
            if (item == null)
            {
                item = new CartModel(bookId);
                listCart.Add(item);
                return Redirect(url);
            }
            else
            {
                item.Amount++;
                return Redirect(url);
            }
        }

        // method tinh tong so luong
        private int Total()
        {
            int iTotal = 0;
            List<CartModel> listCart = Session["Cart"] as List<CartModel>;
            if (listCart != null )
            {
                iTotal = listCart.Sum(n => n.Amount);
            }
            return iTotal;
        }

        // method tinh tong tien
        private double TotalMoney()
        {
            double iTotalMoney = 0;
            List<CartModel> listCart = Session["Cart"] as List<CartModel>;
            if (listCart != null)
            {
                iTotalMoney = listCart.Sum(n => n.TotalMoney);
            }
            return iTotalMoney;
        }

        // Method hien thi gio hang ra View
        public ActionResult Cart()
        {
            List<CartModel> listCart = GetCart();
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            ViewBag.Total = Total();
            ViewBag.TotalMoney = TotalMoney();
            return View(listCart);
        }

        // method partial view de thi thi so luong san pham tren icon gio hang
        public ActionResult CartPartial()
        {
            ViewBag.Total = Total();
            return PartialView();
        }

        public ActionResult DeleteCart(int id)
        {
            // lay gio hang tu session
            List<CartModel> listCart = GetCart();
            // kiem tra sach co trong session chua
            CartModel item = listCart.SingleOrDefault(n => n.BookId == id);
            if (item != null)
            {
                listCart.RemoveAll(n => n.BookId == id);
                return RedirectToAction("Cart","Cart");
            }
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            return RedirectToAction("Cart", "Cart");
        }

        public ActionResult UpdateCart(int id, FormCollection f)
        {
            List<CartModel> listCart = GetCart();
            CartModel item = listCart.SingleOrDefault(n => n.BookId == id);
            if (item != null)
            {
                item.Amount = int.Parse(f["txtAmount"].ToString());
            }
            return RedirectToAction("Cart", "Cart");
        }

    }
}