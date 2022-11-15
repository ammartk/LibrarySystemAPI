using System.Data.SqlClient;
using System.Data;
using WebApplication16.Models;
using SampleProject.DataLayer;
using System;

namespace SampleProject.BusinessLayer
{
    public class BookHandler : IBookHandler
    {
        ISQLDataHelper DatabaseHandler;
        public BookHandler(ISQLDataHelper databaseHandler)
        {
            DatabaseHandler = databaseHandler;
        }

        public bool DeleteBook(string name)
        {
            return DatabaseHandler.DeleteBook(name);
        }

        public book GetBook(string name)
        {
            return DatabaseHandler.GetBook(name);
        }

        public bool InsertBook(book book)
        {
            return DatabaseHandler.InsertBook(book);
        }


        public issuebookclass IssueBook(int userid, string bookname)
        {
            book book = GetBook(bookname);
            user user = DatabaseHandler.GetUser(userid);
            if(book.availibilty == -1)
            {
                var value = new issuebookclass() { Username = user.username, Bookname = book.bookname, IssueDate = DateTime.Today, ReturnDate = DateTime.Today.AddDays(15)}
                 if (DatabaseHandler.IssueBook(value))
                 {
                    return value;
                 }
                 else
                 {
                    return null;
                 }
                
            }
            return null;
        }

        public bool ReturnBook(string name)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateBook(int id, book book)
        {
            throw new System.NotImplementedException();
        }
    }
}
