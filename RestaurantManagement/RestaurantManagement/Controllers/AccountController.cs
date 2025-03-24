using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DAL;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace RestaurantManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly RestaurantContext _context;

        public AccountController(RestaurantContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Username and password are required.";
                return View();
            }

            var account = _context.Accounts
                .Include(a => a.User)
                .FirstOrDefault(a => a.Username == username);

            if (account == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            try
            {
                if (!BCrypt.Net.BCrypt.Verify(password, account.PasswordHash))
                {
                    ViewBag.Error = "Invalid username or password.";
                    return View();
                }
            }
            catch (BCrypt.Net.SaltParseException)
            {
                ViewBag.Error = "Error verifying password. Please contact the administrator.";
                return View();
            }

            // Lưu thông tin vào session
            HttpContext.Session.SetString("UserID", account.UserID.ToString());
            HttpContext.Session.SetString("Username", account.Username);
            HttpContext.Session.SetString("Role", account.Role);

            // Ghi log để kiểm tra
            Console.WriteLine($"Session UserID: {HttpContext.Session.GetString("UserID")}");
            Console.WriteLine($"Session Username: {HttpContext.Session.GetString("Username")}");
            Console.WriteLine($"Session Role: {HttpContext.Session.GetString("Role")}");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_context.Accounts.Any(a => a.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Username already exists.");
                return View(model);
            }

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(model);
            }

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            var account = new Account
            {
                Username = model.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "User",
                UserID = user.UserID
            };
            _context.Accounts.Add(account);
            _context.SaveChanges();

            TempData["Success"] = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}