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

        public bool DeleteUser(int id)
        {
            var user = GetUser(id);
            if(user.issuelist.Count == 0)
            {
                DatabaseHandler.DeleteUser(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public user GetUser(int id)
        {
            var user = DatabaseHandler.GetUser(id);
            user.issuelist = DatabaseHandler.GetIssuedBooks(user.username).ToList();
            return user;
        }

        public bool InsertUser(user user)
        {
            return DatabaseHandler.InsertUser(user);
        }

        public bool UpdateUser(int id, user user)
        {
            return DatabaseHandler.UpdateUser(id, user);
        }
    }
}
