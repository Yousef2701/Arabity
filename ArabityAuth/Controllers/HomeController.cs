using ArabityAuth.Data;
using ArabityAuth.Models;
using ArabityAuth.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace ArabityAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger,
                              ApplicationDbContext context,
                              IHttpContextAccessor httpContextAccessor,
                              UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Header-Cart Logic
            var type = _context.UserClaims.Where(m => m.UserId == userId).Select(o => o.ClaimType).FirstOrDefault();
            string img;

            if (type == "Customer")
            {
                img = _context.Customers.Where(c => c.Id == userId).Select(i => i.ImageUrl).FirstOrDefault();
                ViewBag.img = img;
            }
            else if (type == "Store")
            {
                img = _context.Stores.Where(c => c.Id == userId).Select(i => i.ImageUrl).FirstOrDefault();
                ViewBag.img = img;
            }
            else if (type == "Workshop")
            {
                img = _context.Workshops.Where(c => c.Id == userId).Select(i => i.ImageUrl).FirstOrDefault();
                ViewBag.img = img;
            }
            
            List<Cart> cartItemsParcode = _context.Carts.Where(m => m.CustomerId == userId).ToList();
            List<CartVM> cartVMs = new List<CartVM>();
            double TL = 0;

            foreach (Cart item in cartItemsParcode)
            {
                string parcode = item.ProductParcode;
                var product = _context.StoreProducts.Find(parcode);
                var pr = new CartVM { 
                    ProductParcode= parcode,
                    Name = product.Name,
                    Price= product.Price,
                    ProductQuantity = item.ProductQuantity,
                    TotalPrice = product.Price* item.ProductQuantity
                };
                cartVMs.Add(pr);
                TL += product.Price * item.ProductQuantity;
            }

            ViewBag.CartProduct = cartVMs.ToArray();
            ViewBag.Total = TL;
            IndexVM VM = new IndexVM();
            return View(VM);
        }

        public IActionResult Contact()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Header-Cart Logic
            var type = _context.UserClaims.Where(m => m.UserId == userId).Select(o => o.ClaimType).FirstOrDefault();
            string img;

            if (type == "Customer")
            {
                img = _context.Customers.Where(c => c.Id == userId).Select(i => i.ImageUrl).FirstOrDefault();
                ViewBag.img = img;
            }
            else if (type == "Store")
            {
                img = _context.Stores.Where(c => c.Id == userId).Select(i => i.ImageUrl).FirstOrDefault();
                ViewBag.img = img;
            }
            else if (type == "Workshop")
            {
                img = _context.Workshops.Where(c => c.Id == userId).Select(i => i.ImageUrl).FirstOrDefault();
                ViewBag.img = img;
            }
            List<Cart> cartItemsParcode = _context.Carts.Where(m => m.CustomerId == userId).ToList();
            List<CartVM> cartVMs = new List<CartVM>();
            double TL = 0;

            foreach (Cart item in cartItemsParcode)
            {
                string parcode = item.ProductParcode;
                var product = _context.StoreProducts.Find(parcode);
                var pr = new CartVM
                {
                    ProductParcode = parcode,
                    Name = product.Name,
                    Price = product.Price,
                    ProductQuantity = item.ProductQuantity,
                    TotalPrice = product.Price * item.ProductQuantity
                };
                cartVMs.Add(pr);
                TL += product.Price * item.ProductQuantity;
            }

            ViewBag.CartProduct = cartVMs.ToArray();
            ViewBag.Total = TL;
            IdVM idVM = new IdVM();
            return View(idVM);
        }

        public IActionResult About()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Header-Cart Logic
            var type = _context.UserClaims.Where(m => m.UserId == userId).Select(o => o.ClaimType).FirstOrDefault();
            string img;

            if (type == "Customer")
            {
                img = _context.Customers.Where(c => c.Id == userId).Select(i => i.ImageUrl).FirstOrDefault();
                ViewBag.img = img;
            }
            else if (type == "Store")
            {
                img = _context.Stores.Where(c => c.Id == userId).Select(i => i.ImageUrl).FirstOrDefault();
                ViewBag.img = img;
            }
            else if (type == "Workshop")
            {
                img = _context.Workshops.Where(c => c.Id == userId).Select(i => i.ImageUrl).FirstOrDefault();
                ViewBag.img = img;
            }
            List<Cart> cartItemsParcode = _context.Carts.Where(m => m.CustomerId == userId).ToList();
            List<CartVM> cartVMs = new List<CartVM>();
            double TL = 0;

            foreach (Cart item in cartItemsParcode)
            {
                string parcode = item.ProductParcode;
                var product = _context.StoreProducts.Find(parcode);
                var pr = new CartVM
                {
                    ProductParcode = parcode,
                    Name = product.Name,
                    Price = product.Price,
                    ProductQuantity = item.ProductQuantity,
                    TotalPrice = product.Price * item.ProductQuantity
                };
                cartVMs.Add(pr);
                TL += product.Price * item.ProductQuantity;
            }

            ViewBag.CartProduct = cartVMs.ToArray();
            ViewBag.Total = TL;
            IdVM idVM = new IdVM();
            return View(idVM);
        }

       
        public IActionResult DeleteFromCart(IdVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<Cart> cartproduct = _context.Carts.Where(m => m.CustomerId == userId).ToList();
            foreach (Cart item in cartproduct)
            {
                if (item.ProductParcode == model.ProductParcode)
                {
                    _context.Carts.Remove(item);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAllFromCart()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<Cart> cartproduct = _context.Carts.Where(m => m.CustomerId==userId).ToList();
            foreach(Cart item in cartproduct)
            {
                _context.Carts.Remove(item);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}