using Libreria.Core.Models;
using Libreria.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace Libreria.Web.Controllers
{
    public class UtenteController : Controller
    {
        private readonly UtenteRepository _repoUtenti;

        public UtenteController(IConfiguration configuration)
        {
            string connStr = configuration.GetConnectionString("DefaultConnection");
            _repoUtenti = new UtenteRepository(connStr);
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Utenti";
            List < Utente > utenti = _repoUtenti.GetAll();
            return View(utenti);
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
                _repoUtenti.Add(utente);
                return RedirectToAction("Index");
            }

            return View(utente);
        }

        public IActionResult Edit(int id)
        {
            var utente = _repoUtenti.GetById(id);
            if (utente == null) return NotFound();
            ViewBag.Title = $"Modifica Utente";
            return View(utente);
        }

        [HttpPost]
        public IActionResult Edit(Utente utente)
        {
            if (ModelState.IsValid)
            {
                _repoUtenti.Update(utente);
                return RedirectToAction("Index");
            }
            ViewBag.Title = $"Modifica Utente";
            return View(utente);
        }

        public IActionResult Delete(int id)
        {
            var utente = _repoUtenti.GetById(id);
            if (utente == null) return NotFound();
            return View(utente);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repoUtenti.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
