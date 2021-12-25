using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvrakYonetimSistemi.Data;
using EvrakYonetimSistemi.Models;
using Microsoft.AspNetCore.Authorization;

namespace EvrakYonetimSistemi.Controllers
{
    [Authorize]
    public class EvrakTipiController : Controller
    {
        private readonly ApplicationDbContext c;

        public EvrakTipiController(ApplicationDbContext c)
        {
            this.c = c;
        }

        public IActionResult Index()
        {
            var degerler = c.EvrakTipis.ToList();
            return View(degerler);
        }
        [HttpGet]
        public IActionResult YeniTip()
        {
            return View();
        }
        [HttpPost]
        public IActionResult YeniTip(EvrakTipi d)
        {
            c.EvrakTipis.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult TipSil(int id)
        {
            var dep = c.EvrakTipis.Find(id);
            c.EvrakTipis.Remove(dep);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult TipGetir(int id)
        {
            var depart = c.EvrakTipis.Find(id);
            return View(depart);

        }
        public IActionResult TipGuncelle(EvrakTipi d)
        {
            var dep = c.EvrakTipis.Find(d.TipID);
            dep.TipAdi = d.TipAdi;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult TipDetay(int id)
        {
            var degerler = c.Evraks.Where(x => x.TipID == id).ToList();
            var birimad = c.EvrakTipis.Where(x => x.TipID == id).Select(y => y.TipAdi).FirstOrDefault();
            ViewBag.brm = birimad;
            return View(degerler);
        }
    }
}
