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
    public class AuthMastersController : Controller
    {

        private readonly IAuthorRepository _IAuthorRepository;

        public AuthMastersController()
        {
             
        }
        public AuthMastersController(IAuthorRepository AuthorRepository)
        {
            _IAuthorRepository = AuthorRepository;
        }



        // GET: AuthMasters
        public ActionResult Index()
        {
            return View(_IAuthorRepository.GetAuthors().ToList());
            //return View(db.AuthMasters.ToList());


        }

        // GET: AuthMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorMaster authMaster = _IAuthorRepository.GetAuthorById(id);
            if (authMaster == null)
            {
                return HttpNotFound();
            }
            return View(authMaster);
        }

        // GET: AuthMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuthorID,AuthorName,Dob,EmailAddress")] AuthorMaster authMaster)
        {
            if (ModelState.IsValid)
            {
                _IAuthorRepository.InsertAuthor(authMaster);
                _IAuthorRepository.Save();
                return RedirectToAction("Index");
            }

            return View(authMaster);
        }

        // GET: AuthMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorMaster authMaster = _IAuthorRepository.GetAuthorById(id);
            if (authMaster == null)
            {
                return HttpNotFound();
            }
            return View(authMaster);
        }

        // POST: AuthMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuthorID,AuthorName,Dob,EmailAddress")] AuthorMaster authMaster)
        {
            if (ModelState.IsValid)
            {
                _IAuthorRepository.UpdateAuthor(authMaster);
                _IAuthorRepository.Save();
                return RedirectToAction("Index");
            }
            return View(authMaster);
        }

        // GET: AuthMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _IAuthorRepository.DeleteAuthor(id);
            _IAuthorRepository.Save();

            return View(_IAuthorRepository.GetAuthors().ToList());
        }

        // POST: AuthMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _IAuthorRepository.DeleteAuthor(id);
            _IAuthorRepository.Save();
            return RedirectToAction("Index");
        }

    }
}
