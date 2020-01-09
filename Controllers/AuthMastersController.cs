using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
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
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //return View(_IAuthorRepository.GetAuthors().ToList());
            ////return View(db.AuthMasters.ToList()); ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "AuthorName_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Dob" ? "Dob_desc" : "Date";
            ViewBag.EmailSortParm = sortOrder == "EmailAddress" ? "EmailAddress_desc" : "EmailAddress";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var Authors = _IAuthorRepository.GetAuthors();

            if (!String.IsNullOrEmpty(searchString))
            {
                //Authors = Authors.Where(s => s.AuthorName.Contains(searchString)
                //                       || s.EmailAddress.Contains(searchString));
                Authors = Authors.Where(x => x.AuthorName.ToLower().Contains(searchString.ToLower()) || x.EmailAddress.ToLower().Contains(searchString.ToLower())).ToList();

            }
            switch (sortOrder)
            {
                case "AuthorName_desc":
                    Authors = Authors.OrderByDescending(s => s.AuthorName).ToList();
                    break;
                case "Dob_desc":
                    Authors = Authors.OrderBy(s => s.Dob).ToList();
                    break;
                case "EmailAddress":
                    Authors = Authors.OrderByDescending(s => s.EmailAddress).ToList();
                    break;
                default:  // Name ascending 
                    Authors = Authors.OrderBy(s => s.AuthorName).ToList();
                    break;
            }

            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(Authors.ToPagedList(pageNumber, pageSize));



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
