
using Libreria.Data;
using Microsoft.AspNetCore.Mvc;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace AttivaMente.Web.Controllers
{
    public class DocXController : Controller
    {
        private readonly UtenteRepository _repoUtenti;
        private readonly PrestitoRepository _repoPrestiti;
        private readonly LibroRepository _repoLibri;
        public DocXController(IConfiguration configuration)
        {
            string connStr = configuration.GetConnectionString("DefaultConnection");
            _repoUtenti = new UtenteRepository(connStr);
            _repoPrestiti = new PrestitoRepository(connStr);
            _repoLibri = new LibroRepository(connStr);
        }

        public IActionResult GeneraLettereRichiamo()
        {
            var prestitiScaduti = _repoPrestiti.GetPrestitiScaduti();

            foreach (var prestito in prestitiScaduti)
            {
                var utente = _repoUtenti.GetById(prestito.IdUtente);
                var libro = _repoLibri.GetById(prestito.IdLibro);

                if (utente == null || libro == null) continue;

                string safeTitle = string.Join("_", libro.Titolo.Split(Path.GetInvalidFileNameChars()));
                string fileName = $"Richiamo_{utente.Cognome}_{utente.Nome}_{safeTitle}.docx";
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Richiami", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using (var document = DocX.Create(filePath))
                {
                    document.InsertParagraph("Biblioteca Comunale")
                        .FontSize(16)
                        .Bold()
                        .Alignment = Alignment.center;

                    document.InsertParagraph(Environment.NewLine);

                    document.InsertParagraph($"Data: {DateTime.Now:dd/MM/yyyy}")
                        .Alignment = Alignment.right;

                    document.InsertParagraph(Environment.NewLine);

                    document.InsertParagraph($"Gentile {utente.Nome} {utente.Cognome},").FontSize(12);

                    document.InsertParagraph(Environment.NewLine);

                    document.InsertParagraph(
                        $"Il libro \"{libro.Titolo}\" da Lei preso in prestito il {prestito.Data:dd/MM/yyyy} " +
                        $"risulta ad oggi non restituito e ha superato i 90 giorni concessi."
                    ).FontSize(12);

                    document.InsertParagraph(Environment.NewLine);

                    document.InsertParagraph(
                        "La invitiamo a restituire il volume entro 7 giorni dalla presente, " +
                        "pena l’applicazione delle sanzioni previste dal regolamento della biblioteca."
                    ).FontSize(12);

                    document.InsertParagraph(Environment.NewLine);

                    document.InsertParagraph("Cordiali saluti,").FontSize(12);
                    document.InsertParagraph("La Direzione della Biblioteca").FontSize(12);

                    document.Save();
                }
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
