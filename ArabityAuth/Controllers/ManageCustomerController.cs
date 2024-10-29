using ArabityAuth.Areas.Identity.Pages.Account;
using ArabityAuth.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ArabityAuth.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Policy;
using ArabityAuth.ViewModel;
using ArabityAuth.Code;


namespace ArabityAuth.Controllers
{

    public class ManageCustomerController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private object returnUrl;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _Environment;

        public ManageCustomerController(ApplicationDbContext context,
                                        IHttpContextAccessor httpContextAccessor,
                                        UserManager<IdentityUser> userManager,
                                        ILogger<HomeController> logger,
                                        SignInManager<IdentityUser> signInManager,
                                        Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment)
        {
            _context = context; 
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _Environment = Environment;
        }


        [HttpGet]
        public IActionResult FinishCustomerData()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost]
        public IActionResult FinishCustomerData([FromForm] FinishCustomerDataVM model)
        {
            
            var user = _context.Users.Where(o => o.UserName == model.UserName).FirstOrDefault();
            var userId = user.Id;

            var image = new Tools(_Environment);
            string imageUrl= image.AddImages(model.ImageFile,model.UserName);

            var customer = new Customer
            {
                Id = userId,
                FristName = model.FristName,
                LastName = model.LastName,
                PhoneNumbre = model.PhoneNumbre,
                GmailAddress = model.GmailAddress,
               ImageUrl = imageUrl,
               Governorate= model.Governorate,
               City= model.City
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ViewCustomerData()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var CurrentCustomer = _context.Customers.Find(userId);
            ViewBag.Customer = CurrentCustomer;

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

        [HttpGet]
        public IActionResult UpdateCustomerData()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var CurrentCustomer = _context.Customers.Find(userId);
            ViewBag.Customer = CurrentCustomer;

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
            UpdateCustomerDataVM vM = new UpdateCustomerDataVM();

            return View(vM);
        }

        [HttpPost]
        public IActionResult UpdateCustomerData([FromForm] UpdateCustomerDataVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var CurrentCustomer = _context.Customers.Find(userId);

            var username = _context.Users.Where(m => m.Id == userId).Select(o => o.UserName).FirstOrDefault();
            var image = new Tools(_Environment);
            string imageUrl = image.AddImages(model.ImageFile, username);

            CurrentCustomer.Id = userId;
            CurrentCustomer.FristName=model.FristName;
            CurrentCustomer.LastName=model.LastName;
            CurrentCustomer.GmailAddress=model.GmailAddress;
            CurrentCustomer.PhoneNumbre=model.PhoneNumbre;
            CurrentCustomer.ImageUrl = imageUrl;
            CurrentCustomer.Governorate = model.Governorate;
            CurrentCustomer.City = model.City;

            _context.Customers.Update(CurrentCustomer);
            _context.SaveChanges();

            return RedirectToAction("ViewCustomerData");
        }

        [HttpGet]
        public IActionResult Favourite()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<Favourite> favouriteItemsParcode = _context.Favorites.Where(m => m.CustomerId == userId).ToList();

            List<StoreProduct> _storeProducts = new List<StoreProduct>();

            foreach (Favourite favourite in favouriteItemsParcode)
            {
                string parcode = favourite.ProductParcode;
                var product = _context.StoreProducts.Find(parcode);
                _storeProducts.Add(product);
            }

            ViewBag.FavouriteProduct = _storeProducts;

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
            StoreDataVM VM = new StoreDataVM();

            return View(VM);
        }

        public IActionResult RemoveFromFavourite(StoreDataVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<Favourite> favouriteproduct = _context.Favorites.Where(m => m.CustomerId == userId).ToList();
            foreach (Favourite item in favouriteproduct)
            {
                if (item.ProductParcode == model.Parcode)
                {
                    _context.Favorites.Remove(item);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Favourite");
        }

        public async Task<IActionResult>  DeleteCustomerAccount()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (userId != null)
            {
                var customer = _context.Customers.Find(userId);
                _context.Customers.Remove(customer);

                var claim = _context.UserClaims.Where(m => m.UserId == userId).FirstOrDefault();
                _context.UserClaims.Remove(claim);

                var user = _context.Users.Find(userId);
                _context.Users.Remove(user);

                _context.SaveChanges();

                return RedirectToAction("Index","Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult My_Orders()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<Order> MyOrders = _context.Orders.Where(m => m.clientName== userId).ToList();
            ViewBag.Orders = MyOrders;
            return View();
        }

    }

}
