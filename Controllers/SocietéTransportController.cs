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
    [RoutePrefix("Societes")]
    public class SocietéTransportController : Controller
    {
        private GestionAutocarsEntities db = new GestionAutocarsEntities();

        // GET: SocietéTransport
        [Route()]
        public ActionResult Index()
        {
            return View(db.SocietéTransport.ToList());
        }

        // GET: SocietéTransport/Details/5
        [Route("{id}/details")]
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

        // GET: SocietéTransport/Create
        [Route("ajouter")]
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
        public ActionResult Create([Bind(Include = "nom,email,telephone,login,mdp")] SocietéTransport societéTransport,HttpPostedFileBase _image)
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
                    _image.SaveAs(Server.MapPath(UploadPath+ FileName));


                }
                db.SocietéTransport.Add(societéTransport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(societéTransport);
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
        public ActionResult Edit([Bind(Include = "id,nom,email,telephone,login,mdp,logo")] SocietéTransport societéTransport)
        {
            if (ModelState.IsValid)
            {
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
        [HttpPost]
        [Route("{id}/deleteConf")]
        [ValidateAntiForgeryToken]
       
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
    }
}
