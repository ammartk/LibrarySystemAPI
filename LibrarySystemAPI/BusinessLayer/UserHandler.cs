using SampleProject.DataLayer;
using System.Collections.Generic;
using System.Linq;
using WebApplication16.Models;

namespace LibrarySystemAPI.BusinessLayer
{
    public class UserHandler : IUserHandler
    {
        ISQLDataHelper DatabaseHandler;
        public UserHandler(ISQLDataHelper sqldatahelper)
        {
            DatabaseHandler = sqldatahelper;
        }
        public user GetUser(int id)
        {
            var user = DatabaseHandler.GetUser(id);
            user.issuelist = DatabaseHandler.GetIssuedBooks(id).ToList();
            return user;
        }

        public bool InsertUser(user user)
        {
            return DatabaseHandler.InsertUser(user);
        }

        public bool UpdateUser(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
