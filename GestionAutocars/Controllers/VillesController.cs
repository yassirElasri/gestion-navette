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
    [RoutePrefix("villes")]
    public class VillesController : Controller
    {
        private GestionAutocarsEntities db = new GestionAutocarsEntities();

        // GET: Villes
        [Route()]
        public ActionResult Index()
        {
            return View(db.Villes.ToList());
        }

        // GET: Villes/Details/5
        [Route("{id}/details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ville ville = db.Villes.Find(id);
            if (ville == null)
            {
                return HttpNotFound();
            }
            return View(ville);
        }

        // GET: Villes/Create
        [Route("ajouter")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Villes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouter")]
        public ActionResult Create([Bind(Include = "nom")] Ville ville)
        {
            if (ModelState.IsValid)
            {
                db.Villes.Add(ville);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ville);
        }
        [Route("{id}/edit")]
        // GET: Villes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ville ville = db.Villes.Find(id);
            if (ville == null)
            {
                return HttpNotFound();
            }
            return View(ville);
        }

        // POST: Villes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id}/edit")]

        public ActionResult Edit([Bind(Include = "id,nom")] Ville ville)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ville).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ville);
        }

        // GET: Villes/Delete/5
        [Route("{id}/delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ville ville = db.Villes.Find(id);
            if (ville == null)
            {
                return HttpNotFound();
            }
            return View(ville);
        }

        // POST: Villes/Delete/5
        [HttpPost]
        [Route("{id}/deleteConf")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ville ville = db.Villes.Find(id);
            db.Villes.Remove(ville);
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
