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
    [RoutePrefix("demandes")]
    public class DemandesController : Controller
    {
        private GestionAutocarsEntities db = new GestionAutocarsEntities();

        // GET: Demandes
        [Route()]
        public ActionResult Index()
        {
            var demandes = db.Demandes.Include(d => d.Navette).Include(d => d.Utilissateur);
            return View(demandes.ToList());
        }
        [Route("{id}/details")]

        // GET: Demandes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demande demande = db.Demandes.Find(id);
            if (demande == null)
            {
                return HttpNotFound();
            }
            return View(demande);
        }
        [Route("ajouter")]

        // GET: Demandes/Create
        public ActionResult Create()
        {
            ViewBag.id_navette = new SelectList(db.Navettes, "id", "id");
            ViewBag.id_user = new SelectList(db.Utilissateurs, "id", "nom_complet");
            return View();
        }

        // POST: Demandes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouter")]
        public ActionResult Create([Bind(Include = "id_navette,id_user,id")] Demande demande)
        {
            if (ModelState.IsValid)
            {
                db.Demandes.Add(demande);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_navette = new SelectList(db.Navettes, "id", "id", demande.id_navette);
            ViewBag.id_user = new SelectList(db.Utilissateurs, "id", "nom_complet", demande.id_user);
            return View(demande);
        }

        // GET: Demandes/Edit/5
        [Route("{id}/edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demande demande = db.Demandes.Find(id);
            if (demande == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_navette = new SelectList(db.Navettes, "id", "id", demande.id_navette);
            ViewBag.id_user = new SelectList(db.Utilissateurs, "id", "nom_complet", demande.id_user);
            return View(demande);
        }

        // POST: Demandes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id}/edit")]
        public ActionResult Edit([Bind(Include = "id_navette,id_user,id")] Demande demande)
        {
            if (ModelState.IsValid)
            {
                db.Entry(demande).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_navette = new SelectList(db.Navettes, "id", "id", demande.id_navette);
            ViewBag.id_user = new SelectList(db.Utilissateurs, "id", "nom_complet", demande.id_user);
            return View(demande);
        }

        // GET: Demandes/Delete/5
        [Route("{id}/delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demande demande = db.Demandes.Find(id);
            if (demande == null)
            {
                return HttpNotFound();
            }
            return View(demande);
        }

        // POST: Demandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("{id}/delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Demande demande = db.Demandes.Find(id);
            db.Demandes.Remove(demande);
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
