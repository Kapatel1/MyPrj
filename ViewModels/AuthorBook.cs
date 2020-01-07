using PracticeWebAPP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticeWebAPP.ViewModels
{
    public class AuthorBook
    {
        public AuthorMaster Authmaster { get; set; }
        public List<BookMaster> Bookmaster { get; set; }
    }
}