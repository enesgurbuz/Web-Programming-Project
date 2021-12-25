using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EvrakYonetimSistemi.Data;
using EvrakYonetimSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EvrakYonetimSistemi.Controllers
{
    [Authorize]
    public class EvrakController : Controller
    {
        private readonly ApplicationDbContext c;
        private readonly UserManager<User> _userManager;
        private readonly IHostingEnvironment hostingEnvironment;
        public EvrakController(IHostingEnvironment environment, ApplicationDbContext c, UserManager<User> userManager)
        {
            hostingEnvironment = environment;
            this.c = c;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var degerler = c.Evraks.Include(x => x.EvrakTipi).ToList();
            return View(degerler);
        }
        [HttpGet]
        public IActionResult YeniEvrak()
        {

            List<SelectListItem> degerler = (from x in c.EvrakTipis.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.TipAdi,
                                                 Value = x.TipID.ToString()

                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public IActionResult YeniEvrak(Evrak p)
        {
            var file = p.Dosya;
            var fileName = Path.GetFileName(file.FileName);
            var contentType = file.ContentType;

            if (file != null)
            {
                var uniqueFileName = GetUniqueFileName(file.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, uniqueFileName);

                file.CopyTo(new FileStream(filePath, FileMode.Create));
                p.Url = uniqueFileName;
            }
            var per = c.EvrakTipis.Where(x => x.TipID == p.EvrakTipi.TipID).FirstOrDefault();
            p.EvrakTipi = per;
            p.EvrakID = new Guid();
            c.Evraks.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult EvrakSil(Guid id)
        {
            var per = c.Evraks.Find(id);
            c.Evraks.Remove(per);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult EvrakGetir(Guid id)
        {
            var prsn = c.Evraks.Where(x => x.EvrakID == id).Include("EvrakTipi").FirstOrDefault();
            List<SelectListItem> degerler = (from x in c.EvrakTipis.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.TipAdi,
                                                 Value = x.TipID.ToString()

                                             }).ToList();
            ViewData["EvrakTipleri"] = degerler;
            return View(prsn);

        }
        public IActionResult EvrakGuncelle(Evrak p)
        {
            var per = c.Evraks.Find(p.EvrakID);
            per.Adi = p.Adi;
            per.Tarih = p.Tarih;
            per.Konu = p.Konu;

            per.TipID = p.TipID;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
