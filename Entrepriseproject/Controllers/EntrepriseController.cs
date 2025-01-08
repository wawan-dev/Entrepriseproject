using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using Applicationhackathon;
using Entrepriseproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

            //var entreprises = _context.Entreprise.ToList();
            var entreprises = new Entreprisepage();
            return View(entreprises);
        }

        //public IActionResult Recherche(string query)
        //{

        //    var entreprises = _context.Entreprise
        //        .Where(e => e.Nom.Contains(query) || e.Ville.Contains(query))
        //        .ToList();
        //    return View("Index", entreprises); // Réutilise la vue Index pour afficher les résultats.
        //}

        public async Task<IActionResult> Recherche(string query, int page = 1, int perPage = 10)
        {
            var allEntreprises = new List<Entreprise>();

            // Si 'query' est vide ou nul, vous pouvez décider de rechercher toutes les entreprises


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
                    Code_postal = a.Siege?.CodePostal,
                    Ville = a.Siege?.LibelleCommune,
                    Departement = a.Siege?.Departement,
                    Region = a.Siege?.Region,
                    Pays = "France", // Vous pouvez définir la valeur par défaut ou utiliser l'information si disponible
                    Date_Creation = DateTime.TryParse(a.DateCreation, out DateTime parsedDate) ? parsedDate : (DateTime?)null,
                    Forme_Juridique = a.NatureJuridique,
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


        public async Task<IActionResult> AjouterEntreprise(string Nom, string Siren, string Siret, string Adresse, string CategorieEntreprise, string Coordonnees, string Dirigeants, string Activite, string Pays)
        {
            
            var entreprise = new Entreprise
            {
                Nom = Nom,
                Siren = Siren,
                Siret = Siret,
                Adresse = Adresse,
                CategorieEntreprise = CategorieEntreprise,
                Coordonnees = Coordonnees,
                Dirigeants = Dirigeants,
                Activite = Activite,
                Pays = Pays
            };

            // Ajouter l'entreprise à la base de données
            _context.Entreprise.Add(entreprise);
            await _context.SaveChangesAsync();

            // Rediriger vers la vue d'index ou vers une autre page si nécessaire
            return RedirectToAction("Index");
        }
    }
}
