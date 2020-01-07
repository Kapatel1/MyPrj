using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticeWebAPP.DAL
{
    public interface IBookRepository
    {
        List<BookMaster> GetBooks();
        BookMaster GetBookByID(int bookId);
        void InsertBook(BookMaster book);
        void DeleteBook(int bookID);
        void UpdateBook(BookMaster book);
        void Save();
    }
}