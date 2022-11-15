using System.Data.SqlClient;
using System.Data;
using WebApplication16.Models;
using SampleProject.DataLayer;

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
            throw new System.NotImplementedException();
        }

        public book GetBook(string name)
        {
            return DatabaseHandler.GetBook(name);
        }

        public bool InsertBook(book book)
        {
            return DatabaseHandler.InsertBook(book);
        }


        public bool IssueBook(string username, string bookname)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateBook(int id, book book)
        {
            throw new System.NotImplementedException();
        }
    }
}
