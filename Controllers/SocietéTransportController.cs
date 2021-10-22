using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionArticles.Models;

namespace GestionArticles.Controllers
{
    [RoutePrefix("societes")]
    public class SocietéTransportController : Controller
    {
        private GestionAutocarsEntities db = new GestionAutocarsEntities();

        // GET: SocietéTransport
        [Route()]
        public ActionResult Index()
        {

            return View(db.SocietéTransport.ToList());
        }
        [Route("{id}/details")]

        // GET: SocietéTransport/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocietéTransport societéTransport = db.SocietéTransport.Find(id);
            if (societéTransport == null)
            {
                return HttpNotFound();
            }
            return View(societéTransport);
        }
        [Route("ajouter")]

        // GET: SocietéTransport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SocietéTransport/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouter")]                                                  
        public ActionResult Create([Bind(Include = "nom,email,telephone,login,mdp,logo,cofirm_mdp")] SocietéTransport societéTransport,HttpPostedFileBase _image)
        {
           
                if (_image != null)
                {
                    string FileName = Path.GetFileNameWithoutExtension(_image.FileName);
                    string FileExtension = Path.GetExtension(_image.FileName);
                    FileName = FileName.Trim() + "_" + DateTime.Now.ToString("dd-mm-ss")+FileExtension;
                    string UploadPath = "~/Pictures/";
                    societéTransport.logo = FileName;
                    _image.SaveAs(Server.MapPath(UploadPath + FileName));
                }
                db.SocietéTransport.Add(societéTransport);
                db.SaveChanges();
                return RedirectToAction("Index");
            

        
        }

        // GET: SocietéTransport/Edit/5
        [Route("{id}/edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocietéTransport societéTransport = db.SocietéTransport.Find(id);
            if (societéTransport == null)
            {
                return HttpNotFound();
            }
            return View(societéTransport);
        }

        // POST: SocietéTransport/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id}/edit")]
        public ActionResult Edit([Bind(Include = "id,nom,email,telephone,login,mdp,logo,cofirm_mdp")]
        SocietéTransport societéTransport,HttpPostedFileBase _image)
        {
            if (ModelState.IsValid)
            {
                if (_image!=null)
                {
                    string FileName = Path.GetFileNameWithoutExtension(_image.FileName);
                    string FileExtension = Path.GetExtension(_image.FileName);
                    FileName = FileName.Trim() + "_" + DateTime.Now.ToString("dd-mm-ss") + FileExtension;
                    string UploadPath = "~/Pictures/";
                    societéTransport.logo = FileName;
                    _image.SaveAs(Server.MapPath(UploadPath + FileName));

                }
                db.Entry(societéTransport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(societéTransport);
        }

        // GET: SocietéTransport/Delete/5
        [Route("{id}/delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocietéTransport societéTransport = db.SocietéTransport.Find(id);
            if (societéTransport == null)
            {
                return HttpNotFound();
            }
            return View(societéTransport);
        }

        // POST: SocietéTransport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("{id}/delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SocietéTransport societéTransport = db.SocietéTransport.Find(id);
            db.SocietéTransport.Remove(societéTransport);
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
        public ActionResult login()
        {
            return View();
        }

        [Route("seconnecter")]
        [HttpPost]
        public ActionResult login(SocietéTransport u)
        {
            var trouve = db.SocietéTransport.Where(s => s.login == u.login && s.mdp == u.mdp).FirstOrDefault();
            if (trouve == null)
            {
                return View(u);
            }
            Session["id_societe"] = trouve.id;
            ViewData["nom_societe"] = trouve.nom;
 
            return RedirectToAction("Index1", trouve);
        }
        [Route("index1/{u?}")]
        public ActionResult Index1(SocietéTransport u=null)
        {
            if (u == null)
            {
                return RedirectToAction("login");
            }

            return View();
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouterCar")]
        public ActionResult CreateCar([Bind(Include = "id_societe,maricule,nombre_places")] Autocar autocar)
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

        [Route("ajouterNavette")]
        public ActionResult ajouterNavette(string idd)
        {
            int id = Int16.Parse(idd);
            ViewBag.id_car = new SelectList(db.Autocars.Where(s => s.id_societe == @id), "id", "maricule");
            ViewBag.id_ville_depart = new SelectList(db.Villes, "id", "nom");
            ViewBag.id_ville_arriver = new SelectList(db.Villes, "id", "nom");
            return View();
        }

        // POST: Navettes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouterNavette")]
        public ActionResult ajouterNavette([Bind(Include = "id,id_ville_depart,id_ville_arriver,heur_depart,heur_arriver,disponible,demande,id_car")] Navette navette)
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

    }
}
