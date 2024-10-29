using ArabityAuth.Data;
using ArabityAuth.Models;
using ArabityAuth.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArabityAuth.Controllers
{
    public class WorkshopsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WorkshopsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult AllWorkshops()
        {
            List<Workshop> workshops = _context.Workshops.ToList();
            ViewBag.Workshops = workshops;

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
            AllWorkshopDataVM vM = new AllWorkshopDataVM();
            return View(vM);

        }

        [HttpGet]
        public IActionResult WorkshopData(AllWorkshopDataVM model)
        {
            var workshop = _context.Workshops.Find(model.Id);
            ViewBag.Workshop = workshop;
            var WorkshopComments = _context.workshopComments.Where(m => m.WorkshopId == model.Id).ToList();
            ViewBag.Comments = WorkshopComments;

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
            return View();
        }

        public IActionResult Add_W_Comment(WorkshopDataVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var comment = new WorkshopComment
            {
                Comment= model.Comment,
                WorkshopId = model.Id,
            };
            _context.workshopComments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("AllWorkshops");
        }
    }
}
