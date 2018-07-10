using SoporteEnLinea.Models;
using SoporteEnLinea.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoporteEnLinea.Controllers
{
    public class OrdersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Orders
        public ActionResult NewOrder()
        {
            var orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();
            Session["orderView"] = orderView;

            var list = db.Customers.ToList();
            list = list.OrderBy(c => c.FirstName).ToList();
            ViewBag.CustomerID = new SelectList(list, "CustomerID", "FirstName");

            return View(orderView);
        }

        [HttpPost]
        public ActionResult NewOrder(OrderView orderView)
        {
            var list = db.Customers.ToList();
            list = list.OrderBy(c => c.FirstName).ToList();
            ViewBag.CustomerID = new SelectList(list, "CustomerID", "FirstName");

            return View(orderView);
        }

        public ActionResult AddProduct()
        {
            var list = db.Products.ToList();
            list = list.OrderBy(p => p.Name).ToList();
            ViewBag.ProductID = new SelectList(list, "ProductID", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder)
        {
            var orderView = Session["orderView"] as OrderView;

            int productID = int.Parse(Request["ProductID"]);

            var product = db.Products.Find(productID);
            if(product == null)
            {
                var list = db.Products.ToList();
                list = list.OrderBy(p => p.Name).ToList();
                ViewBag.ProductID = new SelectList(list, "ProductID", "Name");
                ViewBag.Error = "Producto no existe";

                return View(productOrder);
            }

            productOrder = new ProductOrder
            {
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                ProductID = product.ProductID,
                Quantity = float.Parse(Request["Quantity"])

            };

            orderView.Products.Add(productOrder);

            return View("NewOrder", orderView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}