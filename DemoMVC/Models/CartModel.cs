using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMVC.Models
{
    public class CartModel
    {
        DataClassesDataContext data = new DataClassesDataContext();

        public int BookId { get; set; }
        public string BookName { get; set; }
        public string CoverImage { get; set; }
        public Double Price { get; set; }
        public int Amount { get; set; }

        public Double TotalMoney
        {
            get { return Amount * Price; }
        }

        // initialize the cart according to the book code passed in, default is 1
        public CartModel(int bookId) // constructor khoi tao gio hang, tham so truyen vao la 'bookId'
        {
            BookId = bookId;
            Book book = data.Books.Single(n => n.Id == BookId);
            BookName = book.Name;
            CoverImage = book.CoverImage;
            Price = Convert.ToDouble(book.Price.ToString());
            Amount = 1;
        }
    }
}