using Microsoft.AspNetCore.Mvc;

namespace ClothesRentalShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
