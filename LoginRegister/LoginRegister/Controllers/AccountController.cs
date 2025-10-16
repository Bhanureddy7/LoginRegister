using LoginRegister.Models;
using LoginRegister.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginRegister.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _repository;

        public AccountController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if user already exists
                var existingUser = await _repository.GetByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    ViewBag.Error = "User already registered. Please login.";
                    return View();
                }

                await _repository.AddAsync(user);
                TempData["SuccessMessage"] = "Registration successful! Please login.";
                return RedirectToAction("Login");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var valid = await _repository.ValidateUserAsync(email, password);
            if (!valid)
            {
                var existingUser = await _repository.GetByEmailAsync(email);
                ViewBag.Error = existingUser == null
                    ? "User not registered. Please register first."
                    : "Invalid credentials.";
                return View();
            }

            // Save user email in session
            HttpContext.Session.SetString("UserEmail", email);
            return RedirectToAction("Index", "Home");
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
