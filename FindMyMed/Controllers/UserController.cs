using Microsoft.AspNetCore.Mvc;

namespace FindMyMed.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
