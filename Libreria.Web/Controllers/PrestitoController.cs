using Libreria.Core.Models;
using Libreria.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Libreria.Web.Controllers
{
    public class PrestitoController : Controller
    {
        private readonly PrenotazioneRepository _repoPrenotazioni;
        private readonly LibroRepository _repoLibri;
        private readonly UtenteRepository _repoUtenti;
        private readonly PrestitoRepository _repoPrestiti;

        public PrestitoController(IConfiguration configuration)
        {
            string connStr = configuration.GetConnectionString("DefaultConnection");
            _repoPrenotazioni = new PrenotazioneRepository(connStr);
            _repoLibri = new LibroRepository(connStr);
            _repoUtenti = new UtenteRepository(connStr);
            _repoPrestiti = new PrestitoRepository(connStr);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.SelectUtenti = new SelectList(_repoUtenti.GetAll(), "Id", "Nome");
            ViewBag.SelectLibri = new SelectList(_repoLibri.GetAll(), "Id", "Titolo");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Prestito prestito)
        {
            if (ModelState.IsValid)
            {
                if (_repoPrestiti.CountUser(prestito.IdUtente) < 3)
                {
                    _repoPrestiti.Add(prestito);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.SelectUtenti = new SelectList(_repoUtenti.GetAll(), "Id", "Nome");
            ViewBag.SelectLibri = new SelectList(_repoLibri.GetNotAvaiables(), "Id", "Titolo");

            return View(prestito);
        }

    }
}
