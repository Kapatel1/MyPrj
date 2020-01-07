using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticeWebAPP.DAL.Concrete
{
    public class AuthorRepository : IAuthorRepository
    {
        private MVCDBEntities _Context;

        public AuthorRepository(MVCDBEntities AuthorContext)
        {
            _Context = AuthorContext;
        }
        public List<AuthorMaster> GetAuthors()
        {
            return _Context.AuthorMasters.ToList();
        }
        public AuthorMaster GetAuthorById(int? id)
        {
            return _Context.AuthorMasters.Find(id);
        }
        public void InsertAuthor(AuthorMaster Auth)
        {
            _Context.AuthorMasters.Add(Auth);
        }
        public void DeleteAuthor(int? id)
        {
            AuthorMaster Auth = _Context.AuthorMasters.Find(id);
            _Context.AuthorMasters.Remove(Auth);
        }
        public void UpdateAuthor(AuthorMaster Auth)
        {
            _Context.Entry(Auth).State = System.Data.Entity.EntityState.Modified;  
        }
        public void Save()
        {
            _Context.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}