﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using Applicationhackathon;
using Entrepriseproject.Models;
using Microsoft.AspNetCore.Mvc;
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
            EntrepriseResult deserializedData = JsonConvert.DeserializeObject<EntrepriseResult>(response);

            foreach(var a in  deserializedData.Results) {
                var entreprise = new Entreprise
                {
                    Nom = a.NomComplet,
                    Siren = a.Siren,
                    Siret = a.Siege?.Siret,
                    Adresse = a.Siege?.Adresse,
                    Code_postal = a.Siege?.CodePostal,
                    Ville = a.Siege?.LibelleCommune,
                    Departement = a.Siege?.Departement,
                    Region = a.Siege?.Region,
                    Pays = "France", // Vous pouvez définir la valeur par défaut ou utiliser l'information si disponible
                    Date_Creation = DateTime.Today,
                    Forme_Juridique = a.NatureJuridique,
                    Activite = a.ActivitePrincipale,
                    code_ape = a.ActivitePrincipale, // ou un autre champ qui pourrait être le code APE
                    Dirigeants = string.Join(", ", a.Dirigeants.Select(d => $"{d.Prenoms} {d.Nom}"))
                };

                // Ajouter l'entreprise à la liste
                allEntreprises.Add(entreprise);
            }

            // Retourner les entreprises sous forme de liste dans la vue
            return View(allEntreprises);
        }


       
    }
}
