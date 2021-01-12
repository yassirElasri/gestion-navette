using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionArticles.Models;

namespace GestionArticles.Controllers
{
    [RoutePrefix("navettes")]
    public class NavettesController : Controller
    {
        private GestionAutocarsEntities db = new GestionAutocarsEntities();

        // GET: Navettes
        [Route()]
        public ActionResult Index()
        {
            var navettes = db.Navettes.Include(n => n.Autocar).Include(n => n.Ville).Include(n => n.Ville1);
            return View(navettes.ToList());
        }
        [Route("{id}/details")]

        // GET: Navettes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navette navette = db.Navettes.Find(id);
            if (navette == null)
            {
                return HttpNotFound();
            }
            return View(navette);
        }
        [Route("ajouter")]

        // GET: Navettes/Create
        public ActionResult Create()
        {
            ViewBag.id_car = new SelectList(db.Autocars, "id", "maricule");
            ViewBag.id_ville_depart = new SelectList(db.Villes, "id", "nom");
            ViewBag.id_ville_arriver = new SelectList(db.Villes, "id", "nom");
            return View();
        }

        // POST: Navettes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouter")]
        public ActionResult Create([Bind(Include = "id,id_ville_depart,id_ville_arriver,heur_depart,heur_arriver,disponible,demande,id_car")] Navette navette)
        {
            if (ModelState.IsValid)
            {
                db.Navettes.Add(navette);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_car = new SelectList(db.Autocars, "id", "maricule", navette.id_car);
            ViewBag.id_ville_depart = new SelectList(db.Villes, "id", "nom", navette.id_ville_depart);
            ViewBag.id_ville_arriver = new SelectList(db.Villes, "id", "nom", navette.id_ville_arriver);
            return View(navette);
        }

        // GET: Navettes/Edit/5
        [Route("{id}/edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navette navette = db.Navettes.Find(id);
            if (navette == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_car = new SelectList(db.Autocars, "id", "maricule", navette.id_car);
            ViewBag.id_ville_depart = new SelectList(db.Villes, "id", "nom", navette.id_ville_depart);
            ViewBag.id_ville_arriver = new SelectList(db.Villes, "id", "nom", navette.id_ville_arriver);
            return View(navette);
        }

        // POST: Navettes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id}/edit")]
        public ActionResult Edit([Bind(Include = "id,id_ville_depart,id_ville_arriver,heur_depart,heur_arriver,disponible,demande,id_car")] Navette navette)
        {
            if (ModelState.IsValid)
            {
                db.Entry(navette).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_car = new SelectList(db.Autocars, "id", "maricule", navette.id_car);
            ViewBag.id_ville_depart = new SelectList(db.Villes, "id", "nom", navette.id_ville_depart);
            ViewBag.id_ville_arriver = new SelectList(db.Villes, "id", "nom", navette.id_ville_arriver);
            return View(navette);
        }

        // GET: Navettes/Delete/5
        [Route("{id}/delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navette navette = db.Navettes.Find(id);
            if (navette == null)
            {
                return HttpNotFound();
            }
            return View(navette);
        }

        // POST: Navettes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id}/Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Navette navette = db.Navettes.Find(id);
            db.Navettes.Remove(navette);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
