using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracticeWebAPP.DAL;

namespace PracticeWebAPP.Controllers
{
    public class BookMastersController : Controller
    {
        private MVCDBEntities db = new MVCDBEntities();


        private readonly IBookRepository _IBookRepository;

        public BookMastersController()
        {
            
        }
        public BookMastersController(IBookRepository BookRepository)
        {
            _IBookRepository = BookRepository;
        }
        // GET: BookMasters
        public ActionResult Index()
        {
            var bookMasters = db.BookMasters.Include(b => b.AuthorMaster);
            return View(bookMasters.ToList());
        }

        // GET: BookMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookMaster bookMaster = db.BookMasters.Find(id);
            if (bookMaster == null)
            {
                return HttpNotFound();
            }
            return View(bookMaster);
        }

        // GET: BookMasters/Create
        public ActionResult Create()
        {
            ViewBag.AuthID = new SelectList(db.AuthorMasters, "AuthID", "AuthName");
            return View();
        }

        // POST: BookMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,BookName,BookPrice,AuthID")] BookMaster bookMaster)
        {
            if (ModelState.IsValid)
            {
                db.BookMasters.Add(bookMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthID = new SelectList(db.AuthorMasters, "AuthID", "AuthName", bookMaster.AuthorID);
            return View(bookMaster);
        }

        // GET: BookMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookMaster bookMaster = db.BookMasters.Find(id);
            if (bookMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthID = new SelectList(db.AuthorMasters, "AuthID", "AuthName", bookMaster.AuthorID);
            return View(bookMaster);
        }

        // POST: BookMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,BookName,BookPrice,AuthID")] BookMaster bookMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthID = new SelectList(db.AuthorMasters, "AuthID", "AuthName", bookMaster.AuthorID);
            return View(bookMaster);
        }

        // GET: BookMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookMaster bookMaster = db.BookMasters.Find(id);
            if (bookMaster == null)
            {
                return HttpNotFound();
            }
            return View(bookMaster);
        }

        // POST: BookMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookMaster bookMaster = db.BookMasters.Find(id);
            db.BookMasters.Remove(bookMaster);
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
