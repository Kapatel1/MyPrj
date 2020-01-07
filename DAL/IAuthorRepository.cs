using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticeWebAPP.DAL
{
    public interface IAuthorRepository
    {
        List<AuthorMaster> GetAuthors();
        AuthorMaster GetAuthorById(int? AuthorID);
        void InsertAuthor(AuthorMaster Auth);
        void DeleteAuthor(int? AuthorID);
        void UpdateAuthor(AuthorMaster Auth);
        void Save();
    }
}