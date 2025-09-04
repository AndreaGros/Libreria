using Libreria.Core.Models;
using Libreria.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libreria.Web.Controllers
{
    public class PrenotazioneController : Controller
    {
        private readonly PrenotazioneRepository _repoPrenotazioni;
        private readonly LibroRepository _repoLibri;

        public PrenotazioneController(IConfiguration configuration)
        {
            string connStr = configuration.GetConnectionString("DefaultConnection");
            _repoPrenotazioni = new PrenotazioneRepository(connStr);
            _repoLibri = new LibroRepository(connStr);
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
            ViewBag.SelectLibri = new SelectList(_repoLibri.GetNotAvaiables(), "Id", "Titolo");
            return View();
        }
    }
}
