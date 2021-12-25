using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvrakYonetimSistemi.Data;
using EvrakYonetimSistemi.Models;
using Microsoft.AspNetCore.Identity;
using  EvrakYonetimSistemi.Models;

namespace EvrakYonetimSistemi.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext db;
        public AccountController(SignInManager<User> signInManager, ApplicationDbContext db)
        {
            _signInManager = signInManager;
            this.db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var searchedUser = db.Users.FirstOrDefault(x => x.Email == userName || x.UserName == userName);
            if (searchedUser == null)
            {
                return RedirectToAction("Login");
            }
            var result = await _signInManager.PasswordSignInAsync(searchedUser, password, true, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "EvrakTipi");
            }
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
