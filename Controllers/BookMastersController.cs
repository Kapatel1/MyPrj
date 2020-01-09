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
        


        private readonly IBookRepository _IBookRepository;
        private readonly IAuthorRepository _IAuthorRepository;

        public BookMastersController()
        {
            
        }
        public BookMastersController(IBookRepository BookRepository, IAuthorRepository AuthorRepository)
        {
            _IBookRepository = BookRepository;
            _IAuthorRepository = AuthorRepository;
        }
        // GET: BookMasters
        public ActionResult Index()
        {
            var bookMasters = _IBookRepository.GetBooks();
            return View(bookMasters.ToList());
        }

        // GET: BookMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookMaster bookMaster = _IBookRepository.GetBookByID(id);
            if (bookMaster == null)
            {
                return HttpNotFound();
            }
            return View(bookMaster);
        }

        // GET: BookMasters/Create
        public ActionResult Create()
        {
            ViewBag.AuthID = new SelectList(_IAuthorRepository.GetAuthors(), "AuthorID", "AuthorName");
            return View();
        }

        // POST: BookMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,BookName,BookPrice,AuthorID")] BookMaster bookMaster)
        {
            if (ModelState.IsValid)
            {
                _IBookRepository.InsertBook(bookMaster);
                _IBookRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.AuthID = new SelectList(_IAuthorRepository.GetAuthors(), "AuthorID", "AuthorName", bookMaster.AuthorID);
            return View(bookMaster);
        }

        // GET: BookMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookMaster bookMaster = _IBookRepository.GetBookByID(id);
            if (bookMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthID = new SelectList(_IAuthorRepository.GetAuthors(), "AuthorID", "AuthorName", bookMaster.AuthorID);
            return View(bookMaster);
        }

        public ActionResult EditPopup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookMaster bookMaster = _IBookRepository.GetBookByID(id);
            if (bookMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthID = new SelectList(_IAuthorRepository.GetAuthors(), "AuthorID", "AuthorName", bookMaster.AuthorID);
            return PartialView("_BookEdit",bookMaster);
        }

        // POST: BookMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,BookName,BookPrice,AuthorID")] BookMaster bookMaster)
        {
            if (ModelState.IsValid)
            {
                _IBookRepository.UpdateBook(bookMaster);
                
                _IBookRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.AuthID = new SelectList(_IAuthorRepository.GetAuthors(), "AuthorID", "AuthorName", bookMaster.AuthorID);
            return View(bookMaster);
        }

        // GET: BookMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookMaster bookMaster = _IBookRepository.GetBookByID(id);
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
            
            _IBookRepository.DeleteBook(id);
            _IBookRepository.Save();
            return RedirectToAction("Index");
        }

     
    }
}
