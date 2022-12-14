
using LibrarySystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystemAPI.DataLayer
{
    public interface ISQLDataHelper
    {

        public bool InsertBook(book book);
        public book GetBook(string name);
        public bool InsertUser(user user);

        public user GetUser(int id);
        public bool UpdateUser(int userid, user user);
        public bool UpdateBook(int id, book book);
        public List<book> GetIssuedBooks(string name);
        public bool IssueBook(issuebookclass issue);
        public bool DeleteUser(int id);
        public bool DeleteBook(string name);
        public bool ReturnBook(string bookname);
        public List<issuebookclass> IssuedLists(int id);

    }
}
