using Libreria.Core.Models;
using Libreria.Data;
using Microsoft.AspNetCore.Mvc;

namespace Libreria.Web.Controllers
{
    public class PrenotazioneController : Controller
    {
        private readonly PrenotazioneRepository _repoPrenotazioni;

        public PrenotazioneController(IConfiguration configuration)
        {
            string connStr = configuration.GetConnectionString("DefaultConnection");
            _repoPrenotazioni = new PrenotazioneRepository(connStr);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            List<Prenotazione> prenotazioni =_repoPrenotazioni.GetAll();
            return View(prenotazioni);
        }

        public IActionResult Reservation()
        {
            return View();
        }
    }
}
