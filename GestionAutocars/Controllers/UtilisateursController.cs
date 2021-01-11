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
{ [RoutePrefix("utilisateurs")]
    public class UtilisateursController : Controller
    {
       
        private GestionAutocarsEntities db = new GestionAutocarsEntities();
        [Route()]
        // GET: Utilisateurs
        public ActionResult Index()
        {
            var utilissateurs = db.Utilissateurs.Include(u => u.Navette);
            return View(utilissateurs.ToList());
        }
        [Route("{id}/details")]

        // GET: Utilisateurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilissateur utilissateur = db.Utilissateurs.Find(id);
            if (utilissateur == null)
            {
                return HttpNotFound();
            }
            return View(utilissateur);
        }
        [Route("ajouter")]
        // GET: Utilisateurs/Create
        public ActionResult Create()
        {
            ViewBag.id_Navette = new SelectList(db.Navettes, "id", "id");
            return View();
        }

        // POST: Utilisateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouter")]
        public ActionResult Create([Bind(Include = "isAdmin,id_Navette,nom_complet,email,telephone,login,mdp,cofirm_mdp")] Utilissateur utilissateur)
        {
            if (ModelState.IsValid)
            {
                db.Utilissateurs.Add(utilissateur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Navette = new SelectList(db.Navettes, "id", "id", utilissateur.id_Navette);
            return View(utilissateur);
        }

        // GET: Utilisateurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilissateur utilissateur = db.Utilissateurs.Find(id);
            if (utilissateur == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Navette = new SelectList(db.Navettes, "id", "id", utilissateur.id_Navette);
            return View(utilissateur);
        }

        // POST: Utilisateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_Navette,nom_complet,email,telephone,login,mdp,cofirm_mdp")] Utilissateur utilissateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilissateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Navette = new SelectList(db.Navettes, "id", "id", utilissateur.id_Navette);
            return View(utilissateur);
        }

        // GET: Utilisateurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilissateur utilissateur = db.Utilissateurs.Find(id);
            if (utilissateur == null)
            {
                return HttpNotFound();
            }
            return View(utilissateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilissateur utilissateur = db.Utilissateurs.Find(id);
            db.Utilissateurs.Remove(utilissateur);
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
        [Route("seconnecter")]
        public  ActionResult login()
        {
            return View();  
        }

        [Route("seconnecter")]
        [HttpPost]
        public ActionResult login(Utilissateur u)
        {
            var trouve = db.Utilissateurs.Where(s => s.login == u.login && s.mdp == u.mdp).FirstOrDefault();
            if (trouve==null)
            {
                return View(u);
            }
            Session["id_user"] = trouve.id;
            return RedirectToAction("Index1", trouve);
        }
        [Route("index1/{u?}")]
        public ActionResult Index1(SocietéTransport u = null)
        {
            if (u == null)
            {
                return RedirectToAction("login");
            }

            return View();
        }
    }
}
