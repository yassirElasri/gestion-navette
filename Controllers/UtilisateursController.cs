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
     

        // GET: Utilisateurs/Details/5
        [Route("{id}/details")]
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
        [Route("{id}/edit")]
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
        [Route("{id}/edit")]


        public ActionResult Edit([Bind(Include = "id_Navette,nom_complet,email,telephone,login")] Utilissateur utilissateur)
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
        [Route("{id}/delete")]
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
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id}/Delete")]
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
        [Route("trouver_navette")]
        public ActionResult trouver_navette()
        {
            var navettes = db.Navettes.Where(s => s.demande == null ).Include(n => n.Autocar).Include(n => n.Ville).Include(n => n.Ville1);
            return View(navettes.ToList());
        }
        
          [Route("ajouterNavette")]
        public ActionResult ajouterNavette(int idd) {
          //int id = Int16.Parse(idd);

            ViewBag.id_car = new SelectList(db.Autocars, "id", "maricule");
        ViewBag.id_ville_depart = new SelectList(db.Villes, "id", "nom");
        ViewBag.id_ville_arriver = new SelectList(db.Villes, "id", "nom");
            return View("ajouterNavette", idd);       
    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouterNavette")]
        public ActionResult ajouterNavette([Bind(Include = "id_ville_depart,id_ville_arriver,heur_depart,heur_arriver,disponible,demande,id_car")] Navette navette)
        {
            if (ModelState.IsValid )
            {
                navette.demande = true;
                db.Navettes.Add(navette);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_car = new SelectList(db.Autocars, "id", "maricule", navette.id_car);
            ViewBag.id_ville_depart = new SelectList(db.Villes, "id", "nom", navette.id_ville_depart);
            ViewBag.id_ville_arriver = new SelectList(db.Villes, "id", "nom", navette.id_ville_arriver);
            return View();
        }
    }
}

