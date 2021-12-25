using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvrakYonetimSistemi.Data;
using EvrakYonetimSistemi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EvrakYonetimSistemi.Models;

namespace EvrakYonetimSistemi.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> _userManager;

        public KullaniciController(ApplicationDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await db.Users.ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> Ekle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ekle(User user, string password)
        {
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            user.Id = Guid.NewGuid().ToString();
            await _userManager.CreateAsync(user, password);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Sil(string Id)
        {

            await _userManager.DeleteAsync(db.Users.First(x => x.Id == Id));
            return RedirectToAction("Index");
        }
    }
}
