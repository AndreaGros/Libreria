using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Libreria.Web.Controllers
{
    public class UtenteController : Controller
    {
        private readonly IConfiguration _configuration;
        public UtenteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            ViewBag.Title = "Utenti";
            return View();
        }
    }
}
