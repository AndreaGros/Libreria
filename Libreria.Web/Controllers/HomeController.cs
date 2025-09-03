using Microsoft.AspNetCore.Mvc;

namespace Libreria.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            ViewBag.Title = "Homepage";
            return View();

        }
    }
}
