using Microsoft.AspNetCore.Mvc;
using ArabityAuth.Models;
using ArabityAuth.ViewModel;
using ArabityAuth.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static System.Formats.Asn1.AsnWriter;

namespace ArabityAuth.Controllers
{
    public class ManageStoreController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _Environment;

        public ManageStoreController(ApplicationDbContext context,
                                        IHttpContextAccessor httpContextAccessor,
                                        ILogger<HomeController> logger,
                                        SignInManager<IdentityUser> signInManager,
                                        UserManager<IdentityUser> userManager,
                                        Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _Environment = Environment;
        }

        [HttpGet]
        public IActionResult FinishStoreData()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost]
        public IActionResult FinishStoreData([FromForm] FinishStoreDataVM model)
        {

            var user = _context.Users.Where(o => o.UserName == model.UserName).FirstOrDefault();
            var userId = user.Id;

            var image = new Tools(_Environment);
            string imageUrl = image.AddImages(model.ImageFile, model.UserName);

            var store = new Store
            {
                Id = userId,
                Name = model.Name,
                Description = model.Description,
                PhoneNumbre = model.PhoneNumbre,
                WorkTime = model.WorkTime,
                GmailAddress = model.GmailAddress,
                ImageUrl = imageUrl,
                Governorate = model.Governorate,
                City = model.City
            };

            _context.Stores.Add(store);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ViewStoreData()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var CurrentStore = _context.Stores.Find(userId);
            ViewBag.Store = CurrentStore;
            List<StoreProduct> storeproducts = _context.StoreProducts.Where(m => m.StoreId==CurrentStore.Id).ToList();
            ViewBag.StoreProducts = storeproducts;
            return View();
        }

        [HttpGet]
        public IActionResult UpdateStoreData()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var CurrentStore = _context.Stores.Find(userId);
            ViewBag.Store = CurrentStore;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateStoreData([FromForm] FinishStoreDataVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var CurrentStore = _context.Stores.Find(userId);

            var username = _context.Users.Where(m => m.Id == userId).Select(o => o.UserName).FirstOrDefault();
            var image = new Tools(_Environment);
            string imageUrl = image.AddImages(model.ImageFile, username);

            CurrentStore.Id = userId;
            CurrentStore.Name = model.Name;
            CurrentStore.Description = model.Description;
            CurrentStore.WorkTime = model.WorkTime;
            CurrentStore.PhoneNumbre = model.PhoneNumbre;
            CurrentStore.GmailAddress = model.GmailAddress;
            CurrentStore.ImageUrl = imageUrl;
            CurrentStore.Governorate = model.Governorate;
            CurrentStore.City = model.City;

            _context.Stores.Update(CurrentStore);
            _context.SaveChanges();
            
            return RedirectToAction("ViewStoreData");
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ProductVM productVM = new ProductVM();
            return View(productVM);
        }

        [HttpPost]
        public IActionResult AddProduct([FromForm] ProductVM model1)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var CurrentStore = _context.Stores.Find(userId);
            bool status;
            double PriceAfterDiscount = (1 - model1.Discount) * model1.Price;

            if (model1.NumberOfPeices > 0)
                status = true;
            else
                status = false;

            var image = new Tools(_Environment);
            string imageUrl = image.AddImages(model1.ImageFile, model1.ImageName);

            var storeProduct = new StoreProduct
            {
                Parcode=model1.Parcode,
                Name = model1.Name,
                Discreption = model1.Discreption,
                Price = model1.Price,
                Discount = model1.Discount,
                NumberOfPeices = model1.NumberOfPeices,
                StoreId = userId,
                Status = status,
                PriceAfterDiscount = PriceAfterDiscount,
                DateOfPublish = model1.DateOfPublish,
                MadeIn = model1.MadeIn,
                Model = model1.Model,
                ImageUrl = imageUrl
            };

            _context.StoreProducts.Add(storeProduct);
            _context.SaveChanges();
            return RedirectToAction("ViewStoreData");
        }

        [HttpGet]
        public IActionResult UpdateProduct(StoreProduct model1) 
        {
            var product = _context.StoreProducts.Find(model1.Parcode);
            ViewBag.Product = product;
            return View();
        }

        [HttpPost]
        public IActionResult Update_Product([FromForm] ProductVM model1)
        {
            var product = _context.StoreProducts.Find(model1.Parcode);

            bool status;
            double PriceAfterDiscount = (1 - model1.Discount) * model1.Price;

            if (model1.NumberOfPeices > 0)
                status = true;
            else
                status = false;

            var image = new Tools(_Environment);
            string imageUrl = image.AddImages(model1.ImageFile, model1.ImageName);

            product.Name = model1.Name;
            product.Discreption = model1.Discreption;
            product.Price = model1.Price;
            product.Discount = model1.Discount;
            product.NumberOfPeices = model1.NumberOfPeices;
            product.Model = model1.Model;
            product.MadeIn = model1.MadeIn;
            product.Status = status;
            product.PriceAfterDiscount = PriceAfterDiscount;
            product.ImageUrl = imageUrl;

            _context.StoreProducts.Update(product);
            _context.SaveChanges();

            return RedirectToAction("ViewStoreData");
        }

        [HttpGet]
        public IActionResult DeleteProduct(StoreProduct model)
        {
            var product = _context.StoreProducts.Find(model.Parcode);
            _context.StoreProducts.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("ViewStoreData");
        }

        [HttpGet]
        public IActionResult Dashboart()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<OrderStore> Orders = _context.OrderStores.Where(m => m.StoreId == userId).ToList();
            List<OrderStore> TLOrders = Orders.Where(m => m.OrderStatus == "-").ToList();
            List<OrderStore> AcOrders = Orders.Where(m => m.OrderStatus == "Accepted").ToList();
            List<OrderStore> PnOrders = Orders.Where(m => m.OrderStatus == "Pended").ToList();
            List<OrderStore> ClOrders = Orders.Where(m => m.OrderStatus == "Closed").ToList();
            ViewBag.TlOrders = TLOrders.Count;
            ViewBag.AcOrders = AcOrders.Count;
            ViewBag.PnOrders = PnOrders.Count;
            ViewBag.ClOrders = ClOrders.Count;

            List<StoreProduct> products = _context.StoreProducts.Where(m => m.StoreId == userId).ToList();
            ViewBag.Products = products;

            return View();
        }

        public async Task<IActionResult> DeleteStoreAccount()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (userId != null)
            {
                var store = _context.Stores.Find(userId);
                _context.Stores.Remove(store);

                var claim = _context.UserClaims.Where(m => m.UserId == userId).FirstOrDefault();
                _context.UserClaims.Remove(claim);

                var user = _context.Users.Find(userId);
                _context.Users.Remove(user);

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
