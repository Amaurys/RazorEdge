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
            orderView = Session["orderView"] as OrderView;

            var customerID = int.Parse(Request["CustomerID"]);

            var customer = db.Customers.Find(customerID);
            if (customer == null)
            {
                var listC = db.Customers.ToList();
                listC = listC.OrderBy(p => p.FirstName).ToList();
                ViewBag.CustomerID = new SelectList(listC, "CustomerID", "FirstName");
                ViewBag.Error = "Cliente no existe";

                return View(orderView);
            }

            if(orderView.Products.Count == 0)
            {
                var listC = db.Customers.ToList();
                listC = listC.OrderBy(p => p.FirstName).ToList();
                ViewBag.CustomerID = new SelectList(listC, "CustomerID", "FirstName");
                ViewBag.Error = "Debe de ingresar detalles";
            }

            int orderID = 0;

            using (var transation = db.Database.BeginTransaction())
            {
                try
                {
                    var order = new Order
                    {
                        CustomerID = customerID,
                        OrderDate = DateTime.Now,
                        OrderStatus = OrderStatus.Created
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();

                    orderID = db.Orders.ToList().Select(o => o.OrderID).Max();

                    foreach (var item in orderView.Products)
                    {
                        var orderDetail = new OrderDetail
                        {
                            ProductID = item.ProductID,
                            Description = item.Description,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            OrderID = orderID
                        };
                        db.OrderDetails.Add(orderDetail);
                        db.SaveChanges();
                    }
                    transation.Commit();
                }
                catch (Exception ex)
                {
                    transation.Rollback();
                    ViewBag.Error = "ERROR: " + ex.Message;
                    return View(orderView);
                }
                
            }
            
            ViewBag.Message = string.Format("La orden: {0}, grabada ok", orderID);

            //RedirectToAction("NewOrder");
            /*var list = db.Customers.ToList();
            list = list.OrderBy(c => c.FirstName).ToList();
            ViewBag.CustomerID = new SelectList(list, "CustomerID", "FirstName");*/

            var listClientes = db.Customers.ToList();
            listClientes = listClientes.OrderBy(p => p.FirstName).ToList();
            ViewBag.CustomerID = new SelectList(listClientes, "CustomerID", "FirstName");
            //ViewBag.Error = "Debe de ingresar detalles";

            orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();
            Session["orderView"] = orderView;
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

            productOrder = orderView.Products.Find(p => p.ProductID == productID);
            if (productOrder == null)
            {
                productOrder = new ProductOrder
                {
                    Description = product.Description,
                    Name = product.Name,
                    Price = product.Price,
                    ProductID = product.ProductID,
                    Quantity = float.Parse(Request["Quantity"])

                };
                orderView.Products.Add(productOrder);
            }
            else
            {
                productOrder.Quantity += float.Parse(Request["Quantity"]);
            }


            var listC = db.Customers.ToList();
            listC = listC.OrderBy(c => c.FirstName).ToList();
            ViewBag.CustomerID = new SelectList(listC, "CustomerID", "FirstName");

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