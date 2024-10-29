using Microsoft.AspNetCore.Mvc;

namespace ArabityAuth.Controllers
{
    public class Admin : Controller
    {
        public IActionResult Ad_Dashbord()
        {
            return View();
        }
    }
}
