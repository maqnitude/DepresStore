using Microsoft.AspNetCore.Mvc;

namespace DepresStore.Storefront.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}