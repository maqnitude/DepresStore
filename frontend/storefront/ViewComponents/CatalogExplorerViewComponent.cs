using Microsoft.AspNetCore.Mvc;

namespace DepresStore.Storefront.ViewComponents
{
    public class CatalogExplorerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}