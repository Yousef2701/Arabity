using ArabityAuth.Data;
using ArabityAuth.Models;
using ArabityAuth.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Security.Claims;

namespace ArabityAuth.Controllers
{
    public class StoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StoresController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult AllStores()
        {
            List<Store> stores = _context.Stores.ToList();
            ViewBag.Store = stores;

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
            AllStoresVM vM = new AllStoresVM();
            return View(vM);
        }

        [HttpGet]
        public IActionResult StoreData(AllStoresVM model)
        {
            var store = _context.Stores.Find(model.Id);
            ViewBag.Store = store;
            var StoreComments = _context.StoreComments.Where(m => m.StoreId == model.Id).ToList();
            ViewBag.Comments = StoreComments;
            ProductVM productVM = new ProductVM();

            List<StoreProduct> Products = _context.StoreProducts.ToList();
            ViewBag.Product = Products;

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
            StoreDataVM vM = new StoreDataVM();
            return View(vM);
        }

        [HttpGet]
        public IActionResult Product(StoreDataVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var _product = _context.StoreProducts.Find(model.Parcode);
            ViewBag.product = _product;
            var ProductComments = _context.productComments.Where(m => m.StoreProductParcode == model.Parcode).ToList();
            ViewBag.Comments = ProductComments;
            ProductVM productVM = new ProductVM();

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
            return View(productVM);
        }

        [HttpGet]
        public IActionResult AddToCart(ProductVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userId == null)
            {
                
            }
            else
            {
                var checkproduct = _context.Carts.Where(m => m.ProductParcode == model.Parcode).ToList();
                var check = checkproduct.Where(o => o.CustomerId == userId).FirstOrDefault();

                if (check == null)
                {
                    var cart = new Cart
                    {
                        CustomerId = userId,
                        ProductParcode = model.Parcode,
                        ProductQuantity = model.ProductQuantity
                    };

                    _context.Carts.Add(cart);
                    _context.SaveChanges();
                }
                else
                {
                    check.ProductQuantity += model.ProductQuantity;
                    _context.Update(check);
                    _context.SaveChanges();
                }
            }     

            return RedirectToAction("AllStores");
        }

        [HttpGet]
        public IActionResult AddToFavourite(ProductVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {

            }
            else
            {
                var checkproduct = _context.Favorites.Where(m => m.ProductParcode == model.Parcode).ToList();
                var check = checkproduct.Where(o => o.CustomerId == userId).FirstOrDefault();

                if (check == null)
                {
                    var favourite = new Favourite
                    {
                        CustomerId = userId,
                        ProductParcode = model.Parcode,
                    };

                    _context.Favorites.Add(favourite);
                    _context.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Favourite", "ManageCustomer");
                }

            }

            return RedirectToAction("Favourite", "ManageCustomer");
        }

        public IActionResult Add_S_Comment (StoreDataVM model)
        {
            var comment = new StoreComment
            {
                Comment = model.Comment,
                StoreId = model.Id,
            };
            _context.StoreComments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("AllStores");
        }

        public IActionResult Add_P_Comment(ProductVM model)
        {
            var comment = new ProductComment
            {
                Comment = model.Comment,
                StoreProductParcode= model.Parcode,
            };
            _context.productComments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("AllStores");
        }
    }
}
