using Microsoft.AspNetCore.Mvc;
using ArabityAuth.Models;
using ArabityAuth.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ArabityAuth.ViewModel;

namespace ArabityAuth.Controllers
{
    public class ManageWorkshopController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _Environment;

        public ManageWorkshopController(ApplicationDbContext context,
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
        public IActionResult FinishWorkshopData()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost]
        public IActionResult FinishWorkshopData(WorkshopVM model)
        {
            var user = _context.Users.Where(o => o.UserName == model.UserName).FirstOrDefault();
            var userId = user.Id;

            var image = new Tools(_Environment);
            string imageUrl = image.AddImages(model.ImageFile, model.UserName);

            var workshop = new Workshop
            {
                Id = userId,
                Name = model.Name,
                Description = model.Description,
                WorkTime = model.WorkTime,
                PhoneNumbre = model.PhoneNumbre,
                GmailAddress = model.GmailAddress,
                ImageUrl = imageUrl,
                Type = model.Type,
                Governorate = model.Governorate,
                City = model.City
            };

            _context.Workshops.Add(workshop);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ViewWorkshopData()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var CurrentWorkshop = _context.Workshops.Find(userId);
            ViewBag.Workshop = CurrentWorkshop; 
            return View();
        }

        [HttpGet]
        public IActionResult UpdateWorkshopData()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var CurrentWorkshop = _context.Workshops.Find(userId);
            ViewBag.Workshop = CurrentWorkshop;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateWorkshopData(WorkshopVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var CurrentWorkshop = _context.Workshops.Find(userId);

            var username = _context.Users.Where(m => m.Id == userId).Select(o => o.UserName).FirstOrDefault();
            var image = new Tools(_Environment);
            string imageUrl = image.AddImages(model.ImageFile, username);

            CurrentWorkshop.Id = userId;
            CurrentWorkshop.Name = model.Name;
            CurrentWorkshop.Description = model.Description;
            CurrentWorkshop.WorkTime = model.WorkTime;
            CurrentWorkshop.PhoneNumbre = model.PhoneNumbre;
            CurrentWorkshop.GmailAddress = model.GmailAddress;
            CurrentWorkshop.Type= model.Type;
            CurrentWorkshop.ImageUrl = imageUrl;
            CurrentWorkshop.Governorate = model.Governorate;
            CurrentWorkshop.City = model.City;

            _context.Workshops.Update(CurrentWorkshop);
            _context.SaveChanges();

            return RedirectToAction("ViewWorkshopData");
        }

        public async Task<IActionResult> DeleteWorkshopAccount()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (userId != null)
            {
                var workshop = _context.Workshops.Find(userId);
                _context.Workshops.Remove(workshop);

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
