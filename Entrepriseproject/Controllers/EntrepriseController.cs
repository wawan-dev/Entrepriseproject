using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using Applicationhackathon;
using Entrepriseproject.Models;
using Microsoft.AspNetCore.Mvc;

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
            var entreprises = _context.Entreprise.ToList();
            return View(entreprises);
        }

        //public IActionResult Recherche(string query)
        //{

        //    var entreprises = _context.Entreprise
        //        .Where(e => e.Nom.Contains(query) || e.Ville.Contains(query))
        //        .ToList();
        //    return View("Index", entreprises); // Réutilise la vue Index pour afficher les résultats.
        //}

        public async Task<IActionResult> Recherche(string query)
        {
            var allEntreprises = new List<Entreprise>();

            // Si 'query' est vide ou nul, vous pouvez décider de rechercher toutes les entreprises
            

            string apiUrl = $"https://recherche-entreprises.api.gouv.fr/search?q={query}"; // Utiliser la valeur de 'query'
            var response = await _httpClient.GetStringAsync(apiUrl);

            // Désérialisation de la réponse JSON
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(response);

            if (apiResponse?.Items != null)
            {
                allEntreprises = apiResponse.Items; // Récupérer les entreprises directement
            }

            // Retourner les entreprises sous forme de liste dans la vue
            return View(allEntreprises);
        }


        public class ApiResponse
        {
            public List<Entreprise> Items { get; set; } // Liste des entreprises
        }
    }
}
