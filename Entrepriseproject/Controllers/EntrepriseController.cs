using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Web;
using Applicationhackathon;
using Entrepriseproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Prepare;
using Newtonsoft.Json;

namespace Entrepriseproject.Controllers
{
    public class EntrepriseController : Controller
    {
        private readonly ILogger<EntrepriseController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public EntrepriseController(ILogger<EntrepriseController> logger, ApplicationDbContext context, HttpClient httpClient)
        {
            _logger = logger;
            _context = context;
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {

            var entreprises = new Entreprisepage();
            return View(entreprises);
        }

        public async Task<IActionResult> Profil(int id)
        {
            var entreprise = await _context.Entreprise
                .Include(e => e.Commentaires) // Inclut les commentaires liés à l'entreprise
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entreprise == null)
            {
                return NotFound();
            }

            return View(entreprise); // Passe l'objet entreprise à la vue
        }


        public IActionResult MesEntreprise(int page = 1, string filtre = null)
        {
            int pageSize = 10;
            var query = _context.Entreprise.AsQueryable();

            // Si un filtre est fourni, on cherche dans le nom et le SIRET
            if (!string.IsNullOrEmpty(filtre))
            {
                query = query.Where(e => e.Nom.Contains(filtre) || e.Siret.Contains(filtre));
            }

            var totalEntreprises = query.Count();
            var entreprises = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var model = new Paginer<Entreprise>
            {
                Entreprises = entreprises,
                AllEntreprises = query.ToList(), // Toutes les entreprises pour la carte
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalEntreprises / pageSize),
                Filtre = filtre // Filtre à renvoyer pour le champ de recherche
            };

            return View(model);
        }






        public async Task<IActionResult> Recherche(string query, int page = 1, int perPage = 10)
        {
            var allEntreprises = new List<Entreprise>();
            string apiUrl = $"https://recherche-entreprises.api.gouv.fr/search?q={query}&page={page}&perPage={perPage}"; ; // Utiliser la valeur de 'query'
            var response = await _httpClient.GetStringAsync(apiUrl);

            // Désérialisation de la réponse JSON
            EntrepriseResult deserializedData = JsonConvert.DeserializeObject<EntrepriseResult>(response);

            foreach(var a in  deserializedData.Results) {
                var entreprise = new Entreprise
                {
                    Nom = a.nom_complet,
                    Siren = a.Siren,
                    Siret = a.Siege?.Siret,
                    Adresse = a.Siege?.Adresse,
                    Code_postal = a.Siege?.Code_Postal,
                    Ville = a.Siege?.Libelle_Commune,
                    Departement = a.Siege?.departement,
                    Pays = "France", 
                    Date_Creation = DateTime.TryParse(a.DateCreation, out DateTime parsedDate) ? parsedDate : (DateTime?)null,
                    Activite = a.activite_principale,
                    Dirigeants = string.Join(", ", a.Dirigeants.Select(d => $"{d.Prenoms} {d.Nom}")),
                    Coordonnees = a.Siege.Coordonnees,
                    CategorieEntreprise = a.Categorie_Entreprise
                };

                // Ajouter l'entreprise à la liste
                allEntreprises.Add(entreprise);
            }
            var model = new Entreprisepage
            {
                Results = allEntreprises,
                CurrentPage = deserializedData.Page,
                TotalPages = deserializedData.total_pages,
                Query = query,
                PerPage = perPage
            };

            // Retourner les entreprises sous forme de liste dans la vue
            return View("Index", model);
        }


        [HttpPost]
        public async Task<IActionResult> AjouterEntreprise(Entreprise entreprise)
        {
            
            if (ModelState.IsValid)
            {
                _context.Entreprise.Add(entreprise); // Ajout de l'entreprise en base de données
                await _context.SaveChangesAsync(); // Sauvegarde des modifications

                return RedirectToAction("MesEntreprise"); // Rediriger vers la page de la liste des entreprises
            }

            return View(entreprise);
        }

        [HttpPost]
        public async Task<IActionResult> AjouterCommentaire(int entrepriseId, string commentaire, int note)
        {
            if (note < 0 || note > 5 || string.IsNullOrWhiteSpace(commentaire))
            {
                return BadRequest("Données invalides.");
            }

            var entreprise = await _context.Entreprise.FindAsync(entrepriseId);
            if (entreprise == null)
            {
                return NotFound();
            }

            var nouveauCommentaire = new Commentaire
            {
                EntrepriseId = entrepriseId,
                Texte = System.Net.WebUtility.HtmlEncode(commentaire),  // Éviter l'exécution de code
                Note = note,
                Date_Creation = DateTime.Now
            };

            _context.Commentaire.Add(nouveauCommentaire);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profil", new { id = entrepriseId });
        }



    }
}
