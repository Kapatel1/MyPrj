using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntityProject.Models;

namespace EntityProject.Controllers
{
    public class EFCrudoperationController : Controller
    {
        private EntityFWEntities db = new EntityFWEntities();

        // GET: EFCrudoperation
        public ActionResult Index()
        {
            return View(db.EntityMasters.ToList());
        }

        // GET: EFCrudoperation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityMaster entityMaster = db.EntityMasters.Find(id);
            if (entityMaster == null)
            {
                return HttpNotFound();
            }
            return View(entityMaster);
        }

        // GET: EFCrudoperation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EFCrudoperation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,City,Country,Amount")] EntityMaster entityMaster)
        {
            if (ModelState.IsValid)
            {
                db.EntityMasters.Add(entityMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(entityMaster);
        }

        // GET: EFCrudoperation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityMaster entityMaster = db.EntityMasters.Find(id);
            if (entityMaster == null)
            {
                return HttpNotFound();
            }
            return View(entityMaster);
        }

        // POST: EFCrudoperation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,City,Country,Amount")] EntityMaster entityMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entityMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entityMaster);
        }

        // GET: EFCrudoperation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityMaster entityMaster = db.EntityMasters.Find(id);
            if (entityMaster == null)
            {
                return HttpNotFound();
            }
            return View(entityMaster);
        }

        // POST: EFCrudoperation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntityMaster entityMaster = db.EntityMasters.Find(id);
            db.EntityMasters.Remove(entityMaster);
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
