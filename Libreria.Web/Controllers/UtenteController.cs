using Libreria.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Create()
        {
            ViewBag.Title = $"Aggiungi Utente";
            return View();
        }

        [HttpPost]
        public IActionResult Create(Utente utente)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(utente);
        }
    }
}
