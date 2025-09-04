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
        private readonly UtenteRepository _repoUtenti;

        public PrenotazioneController(IConfiguration configuration)
        {
            string connStr = configuration.GetConnectionString("DefaultConnection");
            _repoPrenotazioni = new PrenotazioneRepository(connStr);
            _repoLibri = new LibroRepository(connStr);
            _repoUtenti = new UtenteRepository(connStr);
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

        public IActionResult Create()
        {
            ViewBag.SelectUtenti = new SelectList(_repoUtenti.GetAll(), "Id", "Nome");
            ViewBag.SelectLibri = new SelectList(_repoLibri.GetNotAvaiables(), "Id", "Titolo");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Prenotazione prenotazione)
        {
            if (ModelState.IsValid)
            {
                _repoPrenotazioni.Add(prenotazione);
                return RedirectToAction("Index");
            }

            ViewBag.SelectUtenti = new SelectList(_repoUtenti.GetAll(), "Id", "Nome");
            ViewBag.SelectLibri = new SelectList(_repoLibri.GetNotAvaiables(), "Id", "Titolo");

            return View(prenotazione);
        }
    }
}
