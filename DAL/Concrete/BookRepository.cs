using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracticeWebAPP.DAL.Concrete
{
    public class BookRepository : IBookRepository
    {
        private MVCDBEntities _context;

        public BookRepository(MVCDBEntities bookContext)
        {
            this._context = bookContext;
        }

        public List<BookMaster> GetBooks()
        {
            return _context.BookMasters.ToList();
        }

        public BookMaster GetBookByID(int id)
        {
            return _context.BookMasters.Find(id);
        }

        public void InsertBook(BookMaster book)
        {
            _context.BookMasters.Add(book);
        }

        public void DeleteBook(int bookID)
        {
            BookMaster book = _context.BookMasters.Find(bookID);
            _context.BookMasters.Remove(book);
        }

        public void UpdateBook(BookMaster book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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