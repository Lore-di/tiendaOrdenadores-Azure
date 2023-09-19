using Microsoft.AspNetCore.Mvc;

namespace TiendaA01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
