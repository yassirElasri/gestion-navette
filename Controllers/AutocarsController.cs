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
    [RoutePrefix("autocars")]
    public class AutocarsController : Controller
    {
        private GestionAutocarsEntities db = new GestionAutocarsEntities();

        // GET: Autocars
        [Route()]
        public ActionResult Index()
        {
            var autocars = db.Autocars.Include(a => a.SocietéTransport);
            return View(autocars.ToList());
        }
        [Route("{id}/details")]

        // GET: Autocars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autocar autocar = db.Autocars.Find(id);
            if (autocar == null)
            {
                return HttpNotFound();
            }
            return View(autocar);
        }
        [Route("ajouter")]
        // GET: Autocars/Create
        public ActionResult Create()
        {
            ViewBag.id_societe = new SelectList(db.SocietéTransport, "id", "nom");
            return View();
        }

        // POST: Autocars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouter")]
        public ActionResult Create([Bind(Include = "id,id_societe,maricule,nombre_places")] Autocar autocar)
        {
            if (ModelState.IsValid)
            {
                db.Autocars.Add(autocar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_societe = new SelectList(db.SocietéTransport, "id", "nom", autocar.id_societe);
            return View(autocar);
        }

        // GET: Autocars/Edit/5
        [Route("{id}/edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autocar autocar = db.Autocars.Find(id);
            if (autocar == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_societe = new SelectList(db.SocietéTransport, "id", "nom", autocar.id_societe);
            return View(autocar);
        }

        // POST: Autocars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id}/edit")]
        public ActionResult Edit([Bind(Include = "id,id_societe,maricule,nombre_places")] Autocar autocar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autocar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_societe = new SelectList(db.SocietéTransport, "id", "nom", autocar.id_societe);
            return View(autocar);
        }

        // GET: Autocars/Delete/5
        [Route("{id}/delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autocar autocar = db.Autocars.Find(id);
            if (autocar == null)
            {
                return HttpNotFound();
            }
            return View(autocar);
        }

        // POST: Autocars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("{id}/delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Autocar autocar = db.Autocars.Find(id);
            db.Autocars.Remove(autocar);
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
