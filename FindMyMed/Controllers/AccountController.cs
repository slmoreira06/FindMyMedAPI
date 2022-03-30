using Microsoft.AspNetCore.Mvc;

namespace FindMyMed.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
