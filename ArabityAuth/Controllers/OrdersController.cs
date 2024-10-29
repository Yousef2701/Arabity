using ArabityAuth.Data;
using ArabityAuth.Models;
using ArabityAuth.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ArabityAuth.Controllers
{
    public class OrdersController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context,
                                  IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public IActionResult ConfirmOrder(OrderVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _context.Customers.Find(userId);

            if(model.OrderDate==null)
                model.OrderDate=DateTime.Now.ToString("dd/MM/yyyy");

            var order = new Order
            {
                clientName = user.FristName+" "+user.LastName,
                clientGmail= user.GmailAddress,
                clientGovernorate= user.Governorate,
                clientPhone= user.PhoneNumbre,
                OrderDate = model.OrderDate,
                clientCity = user.City,
                TotalPrice = model.TotalPrice,
                OrderStatus = "-"
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            List<Cart> cartItemsParcode = _context.Carts.Where(m => m.CustomerId == userId).ToList();
            foreach(var cartItem in cartItemsParcode)
            {
                var curProduct = _context.StoreProducts.Where(m => m.Parcode == cartItem.ProductParcode).FirstOrDefault();
                var orderproduct = new OrderProduct
                {
                    OrderNumbre = order.OrderNumbre,
                    ProductParcode = cartItem.ProductParcode,
                    ProductPrice = curProduct.Price,
                    ProductQuantity = cartItem.ProductQuantity,
                    TotalQuantityPrice = curProduct.Price * cartItem.ProductQuantity
                };
                _context.OrderProducts.Add(orderproduct);

                curProduct.NumberOfPeices -= cartItem.ProductQuantity;

                string CustomerFN = _context.Customers.Where(i => i.Id == userId).Select(f => f.FristName).FirstOrDefault();
                string CustomerLN = _context.Customers.Where(i => i.Id == userId).Select(l => l.LastName).FirstOrDefault();
                string name = CustomerFN + " " + CustomerLN;

                var check = _context.OrderStores.Find(order.OrderNumbre, curProduct.StoreId);
                if(check == null)
                {
                    var orderstore = new OrderStore
                    {
                        OrderNumbre = order.OrderNumbre,
                        StoreId = curProduct.StoreId,
                        TotalPrice = curProduct.Price * cartItem.ProductQuantity,
                        OrderStatus = "-",
                        ClintName = name,
                        OrderDate = model.OrderDate
                    };
                    _context.OrderStores.Add(orderstore);
                }
                else
                {
                    check.TotalPrice += curProduct.Price * cartItem.ProductQuantity;
                }

            }

            List<Cart> cartproduct = _context.Carts.Where(m => m.CustomerId == userId).ToList();
            foreach (Cart item in cartproduct)
            {
                _context.Carts.Remove(item);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult TotalOrders()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<OrderStore> TLOrders = _context.OrderStores.Where(m => m.StoreId == userId).ToList();
            List<OrderStore> Orders = TLOrders.Where(m => m.OrderStatus == "-").ToList();
            ViewBag.TlOrders = Orders;
            ViewBag.userid = userId;
            return View();
        }

        public IActionResult Accept(AcceptedOrdersVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<OrderStore> TLOrders = _context.OrderStores.Where(m => m.StoreId == userId).ToList();
            var order = TLOrders.Where(m => m.OrderNumbre == model.OrderNo).FirstOrDefault();
            order.OrderStatus = "Accepted";
            _context.OrderStores.Update(order);
            _context.SaveChanges();
            return RedirectToAction("AcceptedOrders");
        }

        public IActionResult AcceptedOrders()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<OrderStore> TLOrders = _context.OrderStores.Where(m => m.StoreId == userId).ToList();
            List<OrderStore> AcOrders = TLOrders.Where(m => m.OrderStatus == "Accepted").ToList();
            ViewBag.ACOrders = AcOrders;
            ViewBag.userid = userId;
            return View();
        }

        public IActionResult Pend(AcceptedOrdersVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<OrderStore> TLOrders = _context.OrderStores.Where(m => m.StoreId == userId).ToList();
            var order = TLOrders.Where(m => m.OrderNumbre == model.OrderNo).FirstOrDefault();
            order.OrderStatus = "Pended";
            _context.OrderStores.Update(order);
            _context.SaveChanges();
            return RedirectToAction("PendingOrders");
        }

        public IActionResult PendingOrders()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<OrderStore> TLOrders = _context.OrderStores.Where(m => m.StoreId == userId).ToList();
            List<OrderStore> PnOrders = TLOrders.Where(m => m.OrderStatus == "Pended").ToList();
            ViewBag.PNOrders = PnOrders;
            ViewBag.userid = userId;
            return View();
        }

        public IActionResult Close(AcceptedOrdersVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<OrderStore> TLOrders = _context.OrderStores.Where(m => m.StoreId == userId).ToList();
            var order = TLOrders.Where(m => m.OrderNumbre == model.OrderNo).FirstOrDefault();
            order.OrderStatus = "Closed";
            _context.OrderStores.Update(order);
            _context.SaveChanges();
            return RedirectToAction("ClosedOrders");
        }

        public IActionResult ClosedOrders()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<OrderStore> TLOrders = _context.OrderStores.Where(m => m.StoreId == userId).ToList();
            List<OrderStore> ClOrders = TLOrders.Where(m => m.OrderStatus == "Closed").ToList();
            ViewBag.CLOrders = ClOrders;
            ViewBag.userid = userId;
            return View();
        }

        public IActionResult Product_Chart(DashbordVM model)
        {
            ViewBag.ProductName = model.productname;
            return View();
        }
    }
}

