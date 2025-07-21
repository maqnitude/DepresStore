using Microsoft.AspNetCore.Mvc;

namespace DepresStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}