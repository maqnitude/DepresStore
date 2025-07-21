using Microsoft.AspNetCore.Mvc;

namespace DepresStore.Web.ViewComponents
{
    public class CatalogExplorerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}