using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Applicationhackathon;
using Entrepriseproject.Models;
using Entrepriseproject.Data;

namespace YourApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly EntrepriseContext _context;

        public AuthController(EntrepriseContext context)
        {
            _context = context;
        }

        // --- Afficher la page de connexion ---
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Entreprise"); // Ou une autre page principale
            }

            return View();
        }


        // --- Traiter le formulaire de connexion ---
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Psedo == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Motdepasse))
            {
                ModelState.AddModelError(string.Empty, "Nom d'utilisateur ou mot de passe invalide.");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Psedo)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("MesEntreprise", "Entreprise");
        }

        // --- Déconnexion ---
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        // --- Afficher la page d'inscription ---
        public IActionResult Register()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Entreprise");
            }

            return View();
        }


        // --- Traiter le formulaire d'inscription ---
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "Tous les champs sont obligatoires.");
                return View();
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Psedo == username);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Ce nom d'utilisateur est déjà pris.");
                return View();
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Psedo = username,
                Motdepasse = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Psedo) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("MesEntreprise", "Entreprise");
        }
    }
}
