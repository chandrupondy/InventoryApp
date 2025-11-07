using Microsoft.AspNetCore.Mvc;
using InventoryApp.Data;
using InventoryApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace InventoryApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                //HttpContext.Session.SetString("Username", user.Username);
                //return RedirectToAction("Index", "Inventory");
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                return RedirectToAction("Index", "Inventory");
            }

            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }

        // GET: /Account/Logout
        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Clear();
        //    Response.Cookies.Delete(".AspNetCore.Session");
        //    return RedirectToAction("Login", "Account");
        //}
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }


    }
}
